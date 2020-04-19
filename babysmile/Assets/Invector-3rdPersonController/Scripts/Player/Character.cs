using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

namespace Invector
{ 
    [System.Serializable]
    public abstract class Character : MonoBehaviour, ICharacter
    {
        #region Character Variables
        // acess camera info
        [HideInInspector]   public TPCamera tpCamera;
        // acess hud controller 
        [HideInInspector]   public HUDController hud;
        // get the animator component of character
        [HideInInspector]   public Animator animator;   
        // physics material
        [HideInInspector]   public PhysicMaterial frictionPhysics, defaultPhysics;
        // get capsule collider information
        [HideInInspector]   public CapsuleCollider capsuleCollider;
        // storage capsule collider extra information
        [HideInInspector]   public float colliderRadius, colliderHeight;
        // storage the center of the capsule collider info
        [HideInInspector]   public Vector3 colliderCenter;
        // access the rigidbody component
        [HideInInspector]   public Rigidbody _rigidbody;
        // generate input for the controller
        [HideInInspector]   public Vector2 input;
        // lock all the character locomotion 
        [HideInInspector]   public bool lockPlayer;
        // general variables to the locomotion
        [HideInInspector]   public float speed, direction, verticalVelocity;
        // create a offSet base on the character hips 
        [HideInInspector]   public float offSetPivot;

        [HideInInspector]   public bool ragdolled;

        public Transform cameraTransform
        {
            get
            {
                Transform cT = transform;
                if (Camera.main != null)
                    cT = Camera.main.transform;
                if (tpCamera)
                    cT = tpCamera.transform;
                if (cT == transform)
                {
                    Debug.LogWarning("Invector : Missing TPCamera or MainCamera");
                    this.enabled = false;
                }                   

                return cT;
            }
        }

        [Header("--- Health & Stamina ---")]
        public bool isDead;
        public float startingHealth = 100;
        public float currentHealth { get; set; }
        public float startingStamina = 100;
        [HideInInspector] public float currentStamina;

        #endregion

        //**********************************************************************************//
        // INITIAL SETUP 																	//
        // prepare the initial information for capsule collider, physics materials, etc...  //
        //**********************************************************************************//
        public void InitialSetup()
        {
            animator = GetComponent<Animator>();

            tpCamera = TPCamera.instance;
            hud = HUDController.instance;
            // create a offset pivot to the character, to align camera position when transition to ragdoll
            var hips = animator.GetBoneTransform(HumanBodyBones.Hips);
            offSetPivot = Vector3.Distance(transform.position, hips.position);

            if (tpCamera != null)
            {
                tpCamera.offSetPlayerPivot = offSetPivot;
                tpCamera.target = transform;
            }

            if (hud == null) Debug.LogWarning("Invector : Missing HUDController, please assign on ThirdPersonController");

            // prevents the collider from slipping on ramps
            frictionPhysics = new PhysicMaterial();
            frictionPhysics.name = "frictionPhysics";
            frictionPhysics.staticFriction = 0.6f;
            frictionPhysics.dynamicFriction = 0.6f;

            // default physics 
            defaultPhysics = new PhysicMaterial();
            defaultPhysics.name = "defaultPhysics";
            defaultPhysics.staticFriction = 0f;
            defaultPhysics.dynamicFriction = 0f;

            // rigidbody info
            _rigidbody = GetComponent<Rigidbody>();

            // capsule collider 
            capsuleCollider = GetComponent<CapsuleCollider>();

            // save your collider preferences 
            colliderCenter = GetComponent<CapsuleCollider>().center;
            colliderRadius = GetComponent<CapsuleCollider>().radius;
            colliderHeight = GetComponent<CapsuleCollider>().height;       

            currentHealth = startingHealth;
            currentStamina = startingStamina;            

            if (hud == null)
                return;

            hud.damageImage.color = new Color(0f, 0f, 0f, 0f);

            cameraTransform.SendMessage("Init", SendMessageOptions.DontRequireReceiver);
            UpdateHUD();
        }   

        //**********************************************************************************//
        // UPDATE HUD	 																	//
        // sycronize the stamina value with the stamina slider	 		 					//
        // show/hide the damageHUD image effect												//
        //**********************************************************************************//
        public void UpdateHUD()
        {
            if (hud == null)
                return;

            hud.healthSlider.value = currentHealth;
            hud.staminaSlider.value = currentStamina;

            if (hud.damaged)
                hud.damageImage.color = hud.flashColour;
            else
                hud.damageImage.color = Color.Lerp(hud.damageImage.color, Color.clear, hud.flashSpeed * Time.deltaTime);

            hud.damaged = false;
        }

        //**********************************************************************************//
        // APPLY DAMAGE 																	//
        // call this method by SendMessage with the damage value 		 					//
        //**********************************************************************************//
        public void TakeDamage(int amount)
        {
            // Reduce the current health by the damage amount.
            currentHealth -= amount;

            if (hud != null)
            {
                // Set the damaged flag so the screen will flash.
                hud.damageImage.enabled = true;
                hud.damaged = true;
                // Set the health bar's value to the current health.
                hud.healthSlider.value = currentHealth;
            }

            // apply vibration on the gamepad 
            #if !UNITY_WEBPLAYER
            transform.SendMessage("GamepadVibration", 0.25f, SendMessageOptions.DontRequireReceiver);
            #endif

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (currentHealth <= 0)
            {
                // create and call here a method to die 
            }
        }      

        //**********************************************************************************//
        // GAMEPAD VIBRATION																//
        // call this method to use vibration on the gamepad									//
        //**********************************************************************************//	
    #if !UNITY_WEBPLAYER && !UNITY_ANDROID && !UNITY_IOS
        public IEnumerator GamepadVibration(float vibTime)
        {
            if (inputType == InputType.Controler)
            {
                XInputDotNetPure.GamePad.SetVibration(0, 1, 1);
                yield return new WaitForSeconds(vibTime);
                XInputDotNetPure.GamePad.SetVibration(0, 0, 0);
            }
        }
    #endif

        //**********************************************************************************//
        // INPUT TYPE																		//
        // check in real time if you are using the controller ou mouse/keyboard				//
        //**********************************************************************************//	
        [HideInInspector]
        public enum InputType
        {
            MouseKeyboard,
            Controler,
            Mobile
        };
        [HideInInspector]
        public InputType inputType = InputType.MouseKeyboard;

        void OnGUI()
        {
            switch (inputType)
            {
                case InputType.MouseKeyboard:
                    if (isControlerInput())
                    {
                        inputType = InputType.Controler;
                        hud.controllerInput = true;
                        if (hud != null)
                            hud.FadeText("Control scheme changed to Controller", 2f, 0.5f);
                    }
                    else if (isMobileInput())
                    {
                        inputType = InputType.Mobile;
                        hud.controllerInput = true;
                        if (hud != null)
                            hud.FadeText("Control scheme changed to Mobile", 2f, 0.5f);
                    }
                    break;
                case InputType.Controler:
                    if (isMouseKeyboard())
                    {
                        inputType = InputType.MouseKeyboard;
                        hud.controllerInput = false;
                        if (hud != null)
                            hud.FadeText("Control scheme changed to Keyboard/Mouse", 2f, 0.5f);
                    }
                    else if (isMobileInput())
                    {
                        inputType = InputType.Mobile;
                        hud.controllerInput = true;
                        if (hud != null)
                            hud.FadeText("Control scheme changed to Mobile", 2f, 0.5f);
                    }
                    break;
                case InputType.Mobile:
                    if (isMouseKeyboard())
                    {
                        inputType = InputType.MouseKeyboard;
                        hud.controllerInput = false;
                        if (hud != null)
                            hud.FadeText("Control scheme changed to Keyboard/Mouse", 2f, 0.5f);
                    }
                    else if (isControlerInput())
                    {
                        inputType = InputType.Controler;                    
                        hud.controllerInput = true;
                        if (hud != null)
                            hud.FadeText("Control scheme changed to Controller", 2f, 0.5f);
                    }
                    break;
            }            
        }

        public InputType GetInputState() { return inputType; }

        private bool isMobileInput()
        {
            #if UNITY_EDITOR && UNITY_MOBILE
            if (EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
            {
                return true;
            }

            #elif MOBILE_INPUT
            if (EventSystem.current.IsPointerOverGameObject() || (Input.touches.Length > 0))
                return true;
            #endif
            return false;
        }

        private bool isMouseKeyboard()
        {
            #if MOBILE_INPUT
                return false;
            #else
            // mouse & keyboard buttons
            if (Event.current.isKey || Event.current.isMouse)
                return true;
            // mouse movement
            if (Input.GetAxis("Mouse X") != 0.0f || Input.GetAxis("Mouse Y") != 0.0f)
                return true;
        
                return false;
            #endif
        }

        private bool isControlerInput()
        {
            // joystick buttons
            if (Input.GetKey(KeyCode.Joystick1Button0) ||
               Input.GetKey(KeyCode.Joystick1Button1) ||
               Input.GetKey(KeyCode.Joystick1Button2) ||
               Input.GetKey(KeyCode.Joystick1Button3) ||
               Input.GetKey(KeyCode.Joystick1Button4) ||
               Input.GetKey(KeyCode.Joystick1Button5) ||
               Input.GetKey(KeyCode.Joystick1Button6) ||
               Input.GetKey(KeyCode.Joystick1Button7) ||
               Input.GetKey(KeyCode.Joystick1Button8) ||
               Input.GetKey(KeyCode.Joystick1Button9) ||
               Input.GetKey(KeyCode.Joystick1Button10) ||
               Input.GetKey(KeyCode.Joystick1Button11) ||
               Input.GetKey(KeyCode.Joystick1Button12) ||
               Input.GetKey(KeyCode.Joystick1Button13) ||
               Input.GetKey(KeyCode.Joystick1Button14) ||
               Input.GetKey(KeyCode.Joystick1Button15) ||
               Input.GetKey(KeyCode.Joystick1Button16) ||
               Input.GetKey(KeyCode.Joystick1Button17) ||
               Input.GetKey(KeyCode.Joystick1Button18) ||
               Input.GetKey(KeyCode.Joystick1Button19))
            {
                return true;
            }

            // joystick axis
            if (Input.GetAxis("LeftAnalogHorizontal") != 0.0f ||
               Input.GetAxis("LeftAnalogVertical") != 0.0f ||
               Input.GetAxis("Triggers") != 0.0f ||
               Input.GetAxis("RightAnalogHorizontal") != 0.0f ||
               Input.GetAxis("RightAnalogVertical") != 0.0f)
            {
                return true;
            }
            return false;
        }

        public void ResetRagdoll()
        {
            tpCamera.offSetPlayerPivot = offSetPivot;
            tpCamera.SetTarget(this.transform);            
            lockPlayer = false;
            verticalVelocity = 0f;
            ragdolled = false;
        }

        public void RagdollGettingUp()
        {
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            capsuleCollider.enabled = true;
        }

        public void EnableRagdoll()
        {
            tpCamera.offSetPlayerPivot = 0f;
            tpCamera.SetTarget(animator.GetBoneTransform(HumanBodyBones.Hips));
            ragdolled = true;
            capsuleCollider.enabled = false;
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;
            lockPlayer = true;
        }
    }
}