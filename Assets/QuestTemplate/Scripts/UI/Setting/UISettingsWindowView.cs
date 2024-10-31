using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
        public UISettingButton ButtonPrefab;

        protected bool isDirty = false;
        public bool IsDirty => this.isDirty;

        protected List<GameObject> createdCells = new List<GameObject>();
        public IReadOnlyList<GameObject> CreatedCells => this.createdCells;

        public void SetDirty()
        {
            this.isDirty = true;
            this.SaveButton.interactable = true;
        }

        public void ClearDirty()
        {
            this.isDirty = false;
            this.SaveButton.interactable = false;
        }

        public void DestroyAllSettingCells()
        {
            foreach (var settingCell in this.createdCells)
            {
                GameObject.Destroy(settingCell);
            }
            this.createdCells.Clear();
        }

        public void AddCell(GameObject cell)
        {
            this.createdCells.Add(cell);
        }

        public void CreateSettingCell(string label)
        {
            var labelCmp = GameObject.Instantiate(this.LabelPrefab, this.Content);
            labelCmp.Init(label);
            this.createdCells.Add(labelCmp.gameObject);
        }

        public void CreateSettingCell(string label, string defaultText, UnityAction<string> onValueChanged, TMP_InputField.ContentType contentType = TMP_InputField.ContentType.Autocorrected, TMP_InputField.LineType lineType = TMP_InputField.LineType.SingleLine)
        {
            var inputField = GameObject.Instantiate(this.InputFieldPrefab, this.Content);
            inputField.Init(label, defaultText, txt =>
            {
                onValueChanged?.Invoke(txt);
                SetDirty();
            }
            , contentType, lineType);
            this.createdCells.Add(inputField.gameObject);
        }

        public void CreateSettingCell(string label, float defaultFloat, UnityAction<float> onValueChanged)
        {
            var inputField = GameObject.Instantiate(this.InputFieldPrefab, this.Content);
            inputField.Init(label, defaultFloat.ToString(), txt =>
            {
                onValueChanged?.Invoke(float.Parse(txt));
                SetDirty();
            }, TMP_InputField.ContentType.DecimalNumber);
            this.createdCells.Add(inputField.gameObject);
        }

        public void CreateSettingCell(string label, int defaultInt, UnityAction<int> onValueChanged)
        {
            var inputField = GameObject.Instantiate(this.InputFieldPrefab, this.Content);
            inputField.Init(label, defaultInt.ToString(), txt =>
            {
                onValueChanged?.Invoke(int.Parse(txt));
                SetDirty();
            }, TMP_InputField.ContentType.IntegerNumber);
            this.createdCells.Add(inputField.gameObject);
        }

        public void CreateSettingCell(string label, short defaultShort, UnityAction<short> onValueChanged)
        {
            var inputField = GameObject.Instantiate(this.InputFieldPrefab, this.Content);
            inputField.Init(label, defaultShort.ToString(), txt =>
            {
                onValueChanged?.Invoke(short.Parse(txt));
                SetDirty();
            }, TMP_InputField.ContentType.IntegerNumber);
            this.createdCells.Add(inputField.gameObject);
        }

        public void CreateSettingCell(string label, byte defaultByte, UnityAction<byte> onValueChanged)
        {
            var inputField = GameObject.Instantiate(this.InputFieldPrefab, this.Content);
            inputField.Init(label, defaultByte.ToString(), txt =>
            {
                onValueChanged?.Invoke(byte.Parse(txt));
                SetDirty();
            }, TMP_InputField.ContentType.IntegerNumber);
            this.createdCells.Add(inputField.gameObject);
        }

        public void CreateSettingCell(string label, bool defaultBool, UnityAction<bool> onValueChanged)
        {
            var checkBox = GameObject.Instantiate(this.TogglePrefab, this.Content);
            checkBox.Init(label, defaultBool, val =>
            {
                onValueChanged?.Invoke(val);
                SetDirty();
            });
            this.createdCells.Add(checkBox.gameObject);
        }

        public void CreateSettingCell(string label, List<TMP_Dropdown.OptionData> dropDownOptions, int defaultIndex, UnityAction<int> onValueChanged)
        {
            var dropdown = GameObject.Instantiate(this.DropdownPrefab, this.Content);
            dropdown.Init(label, dropDownOptions, defaultIndex, i =>
            {
                onValueChanged?.Invoke(i);
                SetDirty();
            });
            this.createdCells.Add(dropdown.gameObject);
        }

        public void CreateSettingCellSlider(string label, float defaultFloat, UnityAction<float> onValueChanged)
        {
            var slider = GameObject.Instantiate(this.SliderPrefab, this.Content);
            slider.Init(label, defaultFloat, val =>
            {
                onValueChanged?.Invoke(val);
                SetDirty();
            });
            this.createdCells.Add(slider.gameObject);
        }

        public void CreateSettingCellButton(string buttonName, Action onClick)
        {
            var button = GameObject.Instantiate(this.ButtonPrefab, this.Content);
            button.Init(buttonName, () =>
            {
                onClick?.Invoke();
                SetDirty();
            });
            this.createdCells.Add(button.gameObject);
        }
    }
}
