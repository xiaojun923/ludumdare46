using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace Invector.CharacterController
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]

    public class ThirdPersonController : ThirdPersonAnimator
    {
        private static ThirdPersonController _instance;
        public static ThirdPersonController instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<ThirdPersonController>();
                    //Tell unity not to destroy this object when loading a new scene
                    //DontDestroyOnLoad(_instance.gameObject);
                }
                return _instance;
            }
        }        
	   
        void Awake()
        {
            StartCoroutine("UpdateRaycast");	// limit raycasts calls for better performance            
        }

        void Start()
        {
            InitialSetup();						// setup the basic information, created on Character.cs	
        }

        void FixedUpdate()
        {
		    UpdateMotor();					// call ThirdPersonMotor methods
		    UpdateAnimator();				// update animations on the Animator and their methods
		    UpdateHUD();                    // update HUD elements like health bar, texts, etc
            //ControlCameraState();			// change CameraStates
        }

        void LateUpdate()
        {
            HandleInput();					// handle input from controller, keyboard&mouse or touch
            RotateWithCamera();				// rotate the character with the camera    
		    DebugMode();					// display information about the character on PlayMode
        }  


        //**********************************************************************************//
        // INPUT  		      																//
        // here you can config everything that recieve input								//   
        //**********************************************************************************//
        void HandleInput()
        {
            //CloseApp();
            //CameraInput();
            if (!lockPlayer)
            {
                ControllerInput();
                RunningInput();
                //RollingInput();
                //CrouchInput();
                //AimInput();                
                //JumpInput();
                //AttackInput();
            }
            else
            {
                input = Vector2.zero;
                speed = 0f;
			    canSprint = false;
            }
        }

	    //**********************************************************************************//
	    // UPDATE RAYCASTS																	//
	    // handles a separate update for better performance									//
	    //**********************************************************************************//
	    public IEnumerator UpdateRaycast()
	    {
		    while (true)
		    {
			    yield return new WaitForEndOfFrame();
			
			    //CheckAutoCrouch();
			    //CheckForwardAction();
			    StopMove();
                SlopeLimit();
            }
	    }

        //**********************************************************************************//
        // CAMERA STATE    		      														//
        // you can change de CameraState here, the bool means if you want lerp of not		//
        // make sure to use the same CameraState String that you named on TPCameraListData  //
        //**********************************************************************************//
        //void ControlCameraState()
        //{
        //    if (tpCamera == null)
        //        return;

        //    if (changeCameraState && !strafing)
        //        tpCamera.ChangeState(customCameraState, customlookAtPoint, smoothCameraState);
        //    else if (crouch)            
        //        tpCamera.ChangeState("Crouch", true);
        //    else if (strafing)
        //        tpCamera.ChangeState("Aim", true);
        //    else
        //        tpCamera.ChangeState("Default", true);
        //}

	    //**********************************************************************************//
	    // MOVE CONTROLLER 		      														//
	    // gets input from keyboard, gamepad or mobile touch								//
	    //**********************************************************************************//
        void ControllerInput()
        {
            if (inputType == InputType.Mobile)
                input = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
            else if (inputType == InputType.MouseKeyboard)
                input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            else if (inputType == InputType.Controler)
            {
                float deadzone = 0.25f;
                input = new Vector2(Input.GetAxis("LeftAnalogHorizontal"), Input.GetAxis("LeftAnalogVertical"));
                if (input.magnitude < deadzone)
                    input = Vector2.zero;
                else
                    input = input.normalized * ((input.magnitude - deadzone) / (1 - deadzone));
            }
        }

	    ////**********************************************************************************//
	    //// CAMERA INPUT	 		      														//
	    ////**********************************************************************************//
     //   void CameraInput()
     //   {
     //       if (tpCamera == null)
     //           return;

     //       if (inputType == InputType.Mobile)
     //       {
     //           tpCamera.RotateCamera(CrossPlatformInputManager.GetAxis("Mouse X"), CrossPlatformInputManager.GetAxis("Mouse Y"));
     //       }            
     //       else if (inputType == InputType.MouseKeyboard)
     //       {
     //           tpCamera.RotateCamera(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
     //           tpCamera.Zoom(Input.GetAxis("Mouse ScrollWheel"));
     //       }
     //       else if (inputType == InputType.Controler)
     //       {
     //           tpCamera.RotateCamera(Input.GetAxis("RightAnalogHorizontal"), Input.GetAxis("RightAnalogVertical"));
     //       }            
     //   }

        //**********************************************************************************//
        // RUNNING INPUT		      														//
        //**********************************************************************************//
        void RunningInput()
        {
            if (inputType == InputType.Mobile)
            {
                if (CrossPlatformInputManager.GetButtonDown("RB") && currentStamina > 0 && input.sqrMagnitude > 0.1f)
                    canSprint = true;
                if (CrossPlatformInputManager.GetButtonUp("RB") || currentStamina <= 0 || input.sqrMagnitude < 0.1f || strafing)
                    canSprint = false;
            }
            else
            {
                if (Input.GetButtonDown("RB") && currentStamina > 0 && input.sqrMagnitude > 0.1f)
                {
                    if (onGround && !strafing && !crouch)
                        canSprint = true;
                }
                else if (Input.GetButtonUp("RB") || currentStamina <= 0 || input.sqrMagnitude < 0.1f || strafing)
                    canSprint = false;
            }

		    if (canSprint)
		    {
			    currentStamina -= 0.5f;
			    if (currentStamina < 0)
				    currentStamina = 0;
		    }
		    else
		    {
			    currentStamina += 1f;
			    if (currentStamina >= startingStamina)
				    currentStamina = startingStamina;
		    }         
        }

	    ////**********************************************************************************//
	    //// CROUCH INPUT		      															//
	    ////**********************************************************************************//
     //   void CrouchInput()
     //   {
     //       if (autoCrouch)
     //           crouch = true;
     //       else if (pressToCrouch)
     //       {
     //           if (inputType == InputType.Mobile)
     //               crouch = (CrossPlatformInputManager.GetButton ("Y") && onGround);
			  //  else
     //               crouch = Input.GetButton("Y") && onGround && !actions;			
     //       }
     //       else
     //       {
     //           if (inputType == InputType.Mobile)
     //           {
     //               if (CrossPlatformInputManager.GetButtonDown("Y") && onGround)
     //                   crouch = !crouch;
     //           }                
			  //  else
     //           {
     //               if (Input.GetButtonDown("Y") && onGround && !actions)
     //                   crouch = !crouch;
     //           }			
     //       }
     //   }

     //   //**********************************************************************************//
     //   // JUMP INPUT   		      														//
     //   //**********************************************************************************//
     //   void JumpInput()
     //   {
     //       bool jumpConditions = !crouch && onGround && !actions;

     //       if (inputType == InputType.Mobile)
     //       {
     //           if (CrossPlatformInputManager.GetButtonDown("X") && jumpConditions)
     //               jump = true;
     //       }
     //       else
     //       {
     //           if (Input.GetButtonDown("X") && jumpConditions)
     //               jump = true;
     //       }
     //   }

     //   //**********************************************************************************//
     //   // AIMING INPUT		      															//
     //   //**********************************************************************************//
     //   void AimInput()
     //   {
     //       if (!locomotionType.Equals(LocomotionType.OnlyFree))
     //       {
     //           if (inputType == InputType.Mobile)
     //           {
     //               if (CrossPlatformInputManager.GetButtonDown("LB") && !actions)
     //                   strafing = !strafing;
     //           }              
			  //  else
     //           {
     //               if (Input.GetButtonDown("LB") && !actions)
     //                   strafing = !strafing;                                            
     //           }
     //       }           
     //   }

     //   // prototype 
     //   void AttackInput()
     //   {
     //       if (Input.GetMouseButtonDown(0) && !actions)
     //           animator.SetTrigger("MeleeAttack");
     //   }

	    ////**********************************************************************************//
	    //// ROLLING INPUT		      														//
	    ////**********************************************************************************//
     //   void RollingInput()
     //   {
     //       if (inputType == InputType.Mobile)
     //       {
     //           if (CrossPlatformInputManager.GetButtonDown("B"))
     //               Rolling();
     //       }            
		   // else
     //       {
     //           if (Input.GetButtonDown("B"))
     //               Rolling();
     //       }           
     //   }

        //**********************************************************************************//
        // ACTIONS 		      																//
        // WHITE raycast to check if there is anything interactable ahead					//
        //**********************************************************************************//
      //  void CheckForwardAction()
      //  {
      //      var hitObject = CheckActionObject();
      //      if (hitObject != null)
      //      {
      //          try
      //          {
      //              if (hitObject.CompareTag("ClimbUp"))
      //                  DoAction(hitObject, ref climbUp);
      //              else if (hitObject.CompareTag("StepUp"))
      //                  DoAction(hitObject, ref stepUp);
      //              else if (hitObject.CompareTag("JumpOver"))
      //                  DoAction(hitObject, ref jumpOver);
      //              else if (hitObject.CompareTag("AutoCrouch"))
      //                  autoCrouch = true;                    
      //              else if (hitObject.CompareTag("EnterLadderBottom") && !jump)
      //                  DoAction(hitObject, ref enterLadderBottom);
      //              else if (hitObject.CompareTag("EnterLadderTop") && !jump)
      //                  DoAction(hitObject, ref enterLadderTop);
      //          }
      //          catch (UnityException e)
      //          {
      //              Debug.LogWarning(e.Message);
      //          }
      //      }
      //      else if (hud != null)
      //      {
      //          if (hud.showInteractiveText) hud.DisableSprite();
      //      }
      //  }

      //  void DoAction(GameObject hitObject, ref bool action)
      //  {
      //      var triggerAction = hitObject.transform.GetComponent<TriggerAction>();
      //      if (!triggerAction)
      //      {
      //          Debug.LogWarning("Missing TriggerAction Component on " + hitObject.transform.name + "Object");
      //          return;
      //      }
      //      if (hud != null && !triggerAction.autoAction) hud.EnableSprite(triggerAction.message);

      //      if (inputType == InputType.Mobile)
      //      {
      //          if (CrossPlatformInputManager.GetButtonDown("A") && !actions || triggerAction.autoAction && !actions)
      //          {
      //              if (hud != null) hud.DisableSprite();
      //              matchTarget = triggerAction.target;
      //              var rot = hitObject.transform.rotation;
      //              transform.rotation = rot;
      //              action = true;
      //          }
      //      }        
		    //else
      //      {
      //          if (Input.GetButton("A") && !actions || triggerAction.autoAction && !actions)
      //          {
      //              // turn the action bool true and call the animation
      //              action = true;
      //              // disable the text and sprite 
      //              if (hud != null) hud.DisableSprite();
      //              // find the cursorObject height to match with the character animation
      //              matchTarget = triggerAction.target;
      //              // align the character rotation with the object rotation
      //              var rot = hitObject.transform.rotation;
      //              transform.rotation = rot;
      //          }
      //      }
      //  }        

        //**********************************************************************************//
        // ON TRIGGER STAY                                                                  //
        //**********************************************************************************//
        void OnTriggerStay(Collider other)
        {
            //try
            //{
            //    // if you are using the ladder and reach the exit from the bottom
            //    if (other.CompareTag("ExitLadderBottom") && usingLadder)
            //    {
            //        if (inputType == InputType.Mobile)
            //        {
            //            if (CrossPlatformInputManager.GetButtonDown("B") || speed <= -0.05f && !enterLadderBottom)
            //                exitLadderBottom = true;
            //        }
            //        else
            //        {
            //            if (Input.GetButtonDown("B") || speed <= -0.05f && !enterLadderBottom)
            //                exitLadderBottom = true;
            //        }
            //    }
            //    // if you are using the ladder and reach the exit from the top
            //    if (other.CompareTag("ExitLadderTop") && usingLadder && !enterLadderTop)
            //    {
            //        if (speed >= 0.05f)
            //            exitLadderTop = true;
            //    }
            //}
            //catch (UnityException e)
            //{
            //    Debug.LogWarning(e.Message);
            //}
        }

        // close the app/exe
        void CloseApp()
	    {
		    //if (Input.GetKeyDown(KeyCode.Escape))
			   // Application.Quit();
	    }    
    }    
}