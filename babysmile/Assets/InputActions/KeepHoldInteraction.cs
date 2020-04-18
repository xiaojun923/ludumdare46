using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.InputSystem.Editor;
#endif

namespace LD46
{
    [InitializeOnLoad]
    public class CustomInteractionInitializeTempClass {
        static CustomInteractionInitializeTempClass()
        {
            InputSystem.RegisterInteraction<KeepHoldInteraction>();
        }
    }
    
    [Preserve]
    [DisplayName("KeepHold")]
    public class KeepHoldInteraction : IInputInteraction
    {
        public float duration;
        
        public float pressPoint;

        private float durationOrDefault => duration > 0.0 ? duration : InputSystem.settings.defaultHoldTime;
        private float pressPointOrDefault => pressPoint > 0.0 ? pressPoint : InputSystem.settings.defaultButtonPressPoint;
        
        private double m_TimePressed;

        public void Process(ref InputInteractionContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Waiting:
                    if (context.ControlIsActuated(pressPointOrDefault))
                    {
                        if (m_TimePressed <= 0)
                        {
                            m_TimePressed = context.time;
                            context.SetTimeout(durationOrDefault);
                        }
                        else if (context.time - m_TimePressed >= durationOrDefault)
                        {
                            context.Started();
                        }
                    }
                    else
                    {
                        Reset();
                    }
                    break;

                case InputActionPhase.Started:
                    if (!context.ControlIsActuated())
                    {
                        context.Performed();
                        Reset();
                    }
                    break;
                
                case InputActionPhase.Performed:
                    context.Canceled();
                    break;
            }
        }
        
        public void Reset()
        {
            m_TimePressed = 0;
        }
    }
    
#if UNITY_EDITOR
    struct CustomOrDefaultSetting
    {
        public void Initialize(string label, string tooltip, string defaultName, Func<float> getValue,
            Action<float> setValue, Func<float> getDefaultValue, bool defaultComesFromInputSettings = true,
            float defaultInitializedValue = default)
        {
            m_GetValue = getValue;
            m_SetValue = setValue;
            m_GetDefaultValue = getDefaultValue;
            m_ToggleLabel = EditorGUIUtility.TrTextContent("Default",
                defaultComesFromInputSettings
                ? $"If enabled, the default {label.ToLower()} configured globally in the input settings is used. See Edit >> Project Settings... >> Input (NEW)."
                : "If enabled, the default value is used.");
            m_ValueLabel = EditorGUIUtility.TrTextContent(label, tooltip);
            if (defaultComesFromInputSettings)
                m_OpenInputSettingsLabel = EditorGUIUtility.TrTextContent("Open Input Settings");
            m_DefaultInitializedValue = defaultInitializedValue;
            m_UseDefaultValue = Mathf.Approximately(getValue(), defaultInitializedValue);
            m_DefaultComesFromInputSettings = defaultComesFromInputSettings;
            m_HelpBoxText =
                EditorGUIUtility.TrTextContent(
                    $"Uses \"{defaultName}\" set in project-wide input settings.");
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(m_UseDefaultValue);
            var value = m_GetValue();
            if (m_UseDefaultValue)
                value = m_GetDefaultValue();
            ////TODO: use slider rather than float field
            var newValue = EditorGUILayout.FloatField(m_ValueLabel, value, GUILayout.ExpandWidth(false));
            if (!m_UseDefaultValue)
                m_SetValue(newValue);
            EditorGUI.EndDisabledGroup();
            var newUseDefault = GUILayout.Toggle(m_UseDefaultValue, m_ToggleLabel, GUILayout.ExpandWidth(false));
            if (newUseDefault != m_UseDefaultValue)
            {
                if (!newUseDefault)
                    m_SetValue(m_GetDefaultValue());
                else
                    m_SetValue(m_DefaultInitializedValue);
            }
            m_UseDefaultValue = newUseDefault;
            EditorGUILayout.EndHorizontal();

            // If we're using a default from global InputSettings, show info text for that and provide
            // button to open input settings.
            if (m_UseDefaultValue && m_DefaultComesFromInputSettings)
            {
                EditorGUILayout.HelpBox(m_HelpBoxText);
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(m_OpenInputSettingsLabel, EditorStyles.miniButton))
                {
                    SettingsService.OpenProjectSettings("Project/Input System Package");
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        private Func<float> m_GetValue;
        private Action<float> m_SetValue;
        private Func<float> m_GetDefaultValue;
        private bool m_UseDefaultValue;
        private bool m_DefaultComesFromInputSettings;
        private float m_DefaultInitializedValue;
        private GUIContent m_ToggleLabel;
        private GUIContent m_ValueLabel;
        private GUIContent m_OpenInputSettingsLabel;
        private GUIContent m_HelpBoxText;
    }

    
    internal class HoldInteractionEditor : InputParameterEditor<KeepHoldInteraction>
    {
        protected override void OnEnable()
        {
            m_PressPointSetting.Initialize("Press Point",
                "Float value that an axis control has to cross for it to be considered pressed.",
                "Default Button Press Point",
                () => target.pressPoint, v => target.pressPoint = v, () => InputSystem.settings.defaultButtonPressPoint);
            m_DurationSetting.Initialize("Hold Time",
                "Time (in seconds) that a control has to be held in order for it to register as a hold.",
                "Default Hold Time",
                () => target.duration, x => target.duration = x, () => InputSystem.settings.defaultHoldTime);
        }

        public override void OnGUI()
        {
            m_PressPointSetting.OnGUI();
            m_DurationSetting.OnGUI();
        }

        private CustomOrDefaultSetting m_PressPointSetting;
        private CustomOrDefaultSetting m_DurationSetting;
    }
#endif
}
