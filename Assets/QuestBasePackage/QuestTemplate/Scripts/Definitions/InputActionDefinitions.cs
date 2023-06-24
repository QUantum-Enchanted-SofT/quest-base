using QuestBase.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase
{
    public enum ActionMapType
    {
        InputSystemModule,
        InGame,
        Result,
        Menu,
        StageEditor,
        Tutorial,
    }

    public enum InputDeviceType
    {
        None,
        Mouse,
        Keyboard,
        Gamepad,
    }

    public class InputActionDefinitions
    {
        public static void Lock(MainInputActions inputActions, ActionMapType actionMapType)
        {
            switch (actionMapType)
            {
                case ActionMapType.InGame:
                    inputActions.InGame.Disable();
                    break;
                case ActionMapType.Result:
                    inputActions.Result.Disable();
                    break;
                case ActionMapType.Menu:
                    inputActions.Menu.Disable();
                    break;
                case ActionMapType.StageEditor:
                    inputActions.StageEditor.Disable();
                    break;
                case ActionMapType.Tutorial:
                    inputActions.Tutorial.Disable();
                    break;
            }
        }

        public static void Unlock(MainInputActions inputActions, ActionMapType actionMapType)
        {
            switch (actionMapType)
            {
                case ActionMapType.InGame:
                    inputActions.InGame.Enable();
                    break;
                case ActionMapType.Result:
                    inputActions.Result.Enable();
                    break;
                case ActionMapType.Menu:
                    inputActions.Menu.Enable();
                    break;
                case ActionMapType.StageEditor:
                    inputActions.StageEditor.Enable();
                    break;
                case ActionMapType.Tutorial:
                    inputActions.Tutorial.Enable();
                    break;
            }
        }
    }
}
