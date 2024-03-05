using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestTemplate.UI
{
    public class UISettingsWindowView : MonoBehaviour
    {
        public Button SaveButton;
        public Transform Content;
        public UISettingInputField InputFieldPrefab;
        public UISettingLabel LabelPrefab;
        public UISettingDropdown DropdownPrefab;
        public UISettingCheckBox TogglePrefab;
        public UISettingSlider SliderPrefab;
    }
}
