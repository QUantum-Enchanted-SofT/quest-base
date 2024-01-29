using QuestBase.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

namespace QuestBase
{
    public class InputActionsManager : SingletonMonoBehaviour<InputActionsManager>
    {
        protected override bool dontDestroyOnLoad => true;
        protected override bool destroyGameObject => true;

        private EventSystem eventSystem = null;
        private InputSystemUIInputModule inputSystemModule = null;

        [SerializeField]
        private GameObject defaultInputSystemPrefab = null;

        private GameObject defaultInputSystemInst = null;

        public MainInputActions InputActions { get; private set; }
        public InputSystemUIInputModule InputSystemModule => inputSystemModule;

        private Dictionary<ActionMapType, int> lockCounts = new Dictionary<ActionMapType, int>();

        private int lockAllCount = 0;

        public InputDeviceType InputDevice { get; private set; }
        public UnityAction OnInputDeviceChanged = null;

        public Vector2 MousePosition { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            CreateDefaultInputModule();

            this.InputActions = new MainInputActions();
            this.InputActions.Enable();
            foreach (var mapType in Enum.GetValues(typeof(ActionMapType)))
            {
                this.lockCounts[(ActionMapType)mapType] = 0;
            }

            this.InputActions.Common.AnyMouseInput.performed += OnAnyMouseInput;
            this.InputActions.Common.AnyKeyboardInput.performed += OnAnyKeyboardInput;
            this.InputActions.Common.AnyGamepadInput.performed += OnAnyGamepadInput;
            this.InputActions.Common.MousePosition.performed += OnInputMousePosition;
        }

        private void OnDestroy()
        {
            this.InputActions.Common.AnyMouseInput.performed -= OnAnyMouseInput;
            this.InputActions.Common.AnyKeyboardInput.performed -= OnAnyKeyboardInput;
            this.InputActions.Common.AnyGamepadInput.performed -= OnAnyGamepadInput;
            this.InputActions.Common.MousePosition.performed -= OnInputMousePosition;
        }

        public void LockAll()
        {
            this.lockAllCount++;
            if (1 < this.lockAllCount)
            {
                return;
            }

            foreach (var mapType in Enum.GetValues(typeof(ActionMapType)))
            {
                Lock((ActionMapType)mapType);
            }
        }

        public void LockAllExcept(ActionMapType actionMapType)
        {
            LockAll();
            Unlock(actionMapType);
        }

        public void Lock(ActionMapType actionMapType)
        {
            if (this.lockCounts[actionMapType] == 0)
            {
                switch (actionMapType)
                {
                    case ActionMapType.InputSystemModule:
                        // if (this.eventSystem)
                        // {
                        //     this.eventSystem.enabled = false;
                        // }
                        // if (this.inputSystemModule)
                        // {
                        //     // this.inputSystemModule.actionsAsset.Disable();
                        // }

                        DestroyDefaultInputModule();
                        break;
                    default:
                        InputActionDefinitions.Lock(this.InputActions, actionMapType);
                        break;
                }
            }
            this.lockCounts[actionMapType]++;
        }

        public void UnlockAll()
        {
            this.lockAllCount--;
            if (0 < this.lockAllCount)
            {
                return;
            }

            foreach (var mapType in Enum.GetValues(typeof(ActionMapType)))
            {
                Unlock((ActionMapType)mapType);
            }
        }

        public void UnlockAllExcept(ActionMapType actionMapType)
        {
            UnlockAll();
            Lock(actionMapType);
        }

        public void Unlock(ActionMapType actionMapType)
        {
            this.lockCounts[actionMapType]--;
            if (this.lockCounts[actionMapType] == 0)
            {
                switch (actionMapType)
                {
                    case ActionMapType.InputSystemModule:
                        // if (this.eventSystem)
                        // {
                        //     this.eventSystem.enabled = true;
                        // }
                        // if (this.inputSystemModule)
                        // {
                        //     // this.inputSystemModule.actionsAsset.Enable();
                        // }
                        CreateDefaultInputModule();
                        break;
                    default:
                        InputActionDefinitions.Unlock(this.InputActions, actionMapType);
                        break;
                }
            }
        }

        private void OnAnyMouseInput(InputAction.CallbackContext context)
        {
            if (this.InputDevice != InputDeviceType.Mouse)
            {
                this.InputDevice = InputDeviceType.Mouse;
                this.OnInputDeviceChanged?.Invoke();
            }
        }

        private void OnAnyKeyboardInput(InputAction.CallbackContext context)
        {
            if (this.InputDevice != InputDeviceType.Keyboard)
            {
                this.InputDevice = InputDeviceType.Keyboard;
                this.OnInputDeviceChanged?.Invoke();
            }
        }

        private void OnAnyGamepadInput(InputAction.CallbackContext context)
        {
            if (this.InputDevice != InputDeviceType.Gamepad)
            {
                this.InputDevice = InputDeviceType.Gamepad;
                this.OnInputDeviceChanged?.Invoke();
            }
        }

        private void OnInputMousePosition(InputAction.CallbackContext context)
        {
            this.MousePosition = context.ReadValue<Vector2>();
        }

        public void DestroyDefaultInputModule()
        {
            Destroy(this.defaultInputSystemInst);
            this.eventSystem = null;
            this.inputSystemModule = null;
        }

        public void CreateDefaultInputModule()
        {
            this.defaultInputSystemInst = Instantiate(defaultInputSystemPrefab, transform);
            this.eventSystem = this.defaultInputSystemInst.GetComponent<EventSystem>();
            this.inputSystemModule = this.defaultInputSystemInst.GetComponent<InputSystemUIInputModule>();
        }
    }
}
