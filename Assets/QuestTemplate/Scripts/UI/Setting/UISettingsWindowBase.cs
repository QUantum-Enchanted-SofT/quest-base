using QuestTemplate.Data;
using QuestTemplate.Data.InGame;
using QuestTemplate.Data.Player;
using QuestTemplate.SceneManagement;
using QuestBase.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace QuestTemplate.UI
{
    public abstract class UISettingsWindowBase : UIModalWindowBase
    {
        protected UISettingsWindowView view = null;
        protected List<GameObject> createdCells = new List<GameObject>();

        protected bool isDirty = false;

        public override void Dispose()
        {
        }

        protected override void OnViewCreated()
        {
            this.view = this.viewObj.GetComponent<UISettingsWindowView>();

            this.view.SaveButton.onClick.AddListener(OnClickSaveButton);
            this.view.SaveButton.interactable = false;
        }

        protected override IEnumerator OnBeforeOpen()
        {
            yield break;
        }

        protected void DestroyAllSettingCells()
        {
            foreach (var settingCell in this.createdCells)
            {
                GameObject.Destroy(settingCell);
            }
            this.createdCells.Clear();
        }

        protected abstract void OnClickStartButton();

        protected virtual void OnClickSaveButton()
        {
            PlayerDataManager.Instance.Save<PlayerSettingData>();
            ClearDirty();
            ApplicationManager.Instance.ApplySettings();
        }

        protected virtual void OnClickQuitButton()
        {
            Application.Quit();
        }

        protected void SetDirty()
        {
            this.isDirty = true;
            this.view.SaveButton.interactable = true;
            ApplicationManager.Instance.ApplySettings();
        }

        protected void ClearDirty()
        {
            this.isDirty = false;
            this.view.SaveButton.interactable = false;
        }

        protected void CreateSettingCell(string label)
        {
            var labelCmp = GameObject.Instantiate(this.view.LabelPrefab, this.view.Content);
            labelCmp.Init(label);
            this.createdCells.Add(labelCmp.gameObject);
        }

        protected void CreateSettingCell(string label, string defaultText, UnityAction<string> onValueChanged, TMP_InputField.ContentType contentType = TMP_InputField.ContentType.Autocorrected, TMP_InputField.LineType lineType = TMP_InputField.LineType.SingleLine)
        {
            var inputField = GameObject.Instantiate(this.view.InputFieldPrefab, this.view.Content);
            inputField.Init(label, defaultText, txt =>
            {
                onValueChanged?.Invoke(txt);
                SetDirty();
            }
            , contentType, lineType);
            this.createdCells.Add(inputField.gameObject);
        }

        protected void CreateSettingCell(string label, float defaultFloat, UnityAction<float> onValueChanged)
        {
            var inputField = GameObject.Instantiate(this.view.InputFieldPrefab, this.view.Content);
            inputField.Init(label, defaultFloat.ToString(), txt =>
            {
                onValueChanged?.Invoke(float.Parse(txt));
                SetDirty();
            }, TMP_InputField.ContentType.DecimalNumber);
            this.createdCells.Add(inputField.gameObject);
        }

        protected void CreateSettingCell(string label, int defaultInt, UnityAction<int> onValueChanged)
        {
            var inputField = GameObject.Instantiate(this.view.InputFieldPrefab, this.view.Content);
            inputField.Init(label, defaultInt.ToString(), txt =>
            {
                onValueChanged?.Invoke(int.Parse(txt));
                SetDirty();
            }, TMP_InputField.ContentType.IntegerNumber);
            this.createdCells.Add(inputField.gameObject);
        }

        protected void CreateSettingCell(string label, short defaultShort, UnityAction<short> onValueChanged)
        {
            var inputField = GameObject.Instantiate(this.view.InputFieldPrefab, this.view.Content);
            inputField.Init(label, defaultShort.ToString(), txt =>
            {
                onValueChanged?.Invoke(short.Parse(txt));
                SetDirty();
            }, TMP_InputField.ContentType.IntegerNumber);
            this.createdCells.Add(inputField.gameObject);
        }

        protected void CreateSettingCell(string label, byte defaultByte, UnityAction<byte> onValueChanged)
        {
            var inputField = GameObject.Instantiate(this.view.InputFieldPrefab, this.view.Content);
            inputField.Init(label, defaultByte.ToString(), txt =>
            {
                onValueChanged?.Invoke(byte.Parse(txt));
                SetDirty();
            }, TMP_InputField.ContentType.IntegerNumber);
            this.createdCells.Add(inputField.gameObject);
        }

        protected void CreateSettingCell(string label, bool defaultBool, UnityAction<bool> onValueChanged)
        {
            var checkBox = GameObject.Instantiate(this.view.TogglePrefab, this.view.Content);
            checkBox.Init(label, defaultBool, val =>
            {
                onValueChanged?.Invoke(val);
                SetDirty();
            });
            this.createdCells.Add(checkBox.gameObject);
        }

        protected void CreateSettingCell(string label, List<TMP_Dropdown.OptionData> dropDownOptions, int defaultIndex, UnityAction<int> onValueChanged)
        {
            var dropdown = GameObject.Instantiate(this.view.DropdownPrefab, this.view.Content);
            dropdown.Init(label, dropDownOptions, defaultIndex, i =>
            {
                onValueChanged?.Invoke(i);
                SetDirty();
            });
            this.createdCells.Add(dropdown.gameObject);
        }

        protected void CreateSettingCellSlider(string label, float defaultFloat, UnityAction<float> onValueChanged)
        {
            var slider = GameObject.Instantiate(this.view.SliderPrefab, this.view.Content);
            slider.Init(label, defaultFloat, val =>
            {
                onValueChanged?.Invoke(val);
                SetDirty();
            });
            this.createdCells.Add(slider.gameObject);
        }

        // private void CreateInputFieldAndButtonSettingCell(string label, string defaultInputText, string defaultButtonText, UnityAction<string> onClick)
        // {
        //     var inputFieldAndButton = GameObject.Instantiate(this.settingInputFieldAndButtonPrefab, this.view.Content);
        //     inputFieldAndButton.Init(label, defaultInputText, defaultButtonText, onClick);
        //     this.createdCells.Add(inputFieldAndButton.gameObject);
        // }
    }
}
