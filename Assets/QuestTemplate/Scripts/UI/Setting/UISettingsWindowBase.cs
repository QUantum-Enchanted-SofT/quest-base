﻿using QuestTemplate.Data.Player;
using QuestBase.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace QuestTemplate.UI
{
    public abstract class UISettingsWindowBase : UIModalWindowBase
    {
        protected UISettingsWindowView view = null;

        protected bool isDirty = false;

        private IReadOnlyList<GameObject> createdCells => this.view.CreatedCells;

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

        protected void DestroyAllSettingCells() => this.view.DestroyAllSettingCells();

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

        protected void SetDirty() => this.view.SetDirty();

        protected void ClearDirty() => this.view.ClearDirty();

        protected void CreateSettingCell(string label)
            => this.view.CreateSettingCell(label);

        protected void CreateSettingCell(string label, string defaultText, UnityAction<string> onValueChanged, TMP_InputField.ContentType contentType = TMP_InputField.ContentType.Autocorrected, TMP_InputField.LineType lineType = TMP_InputField.LineType.SingleLine)
            => this.view.CreateSettingCell(label, defaultText, onValueChanged, contentType, lineType);

        protected void CreateSettingCell(string label, float defaultFloat, UnityAction<float> onValueChanged)
            => this.view.CreateSettingCell(label, defaultFloat, onValueChanged);

        protected void CreateSettingCell(string label, int defaultInt, UnityAction<int> onValueChanged)
            => this.view.CreateSettingCell(label, defaultInt, onValueChanged);

        protected void CreateSettingCell(string label, short defaultShort, UnityAction<short> onValueChanged)
            => this.view.CreateSettingCell(label, defaultShort, onValueChanged);

        protected void CreateSettingCell(string label, byte defaultByte, UnityAction<byte> onValueChanged)
            => this.view.CreateSettingCell(label, defaultByte, onValueChanged);

        protected void CreateSettingCell(string label, bool defaultBool, UnityAction<bool> onValueChanged)
            => this.view.CreateSettingCell(label, defaultBool, onValueChanged);

        protected void CreateSettingCell(string label, List<TMP_Dropdown.OptionData> dropDownOptions, int defaultIndex, UnityAction<int> onValueChanged)
            => this.view.CreateSettingCell(label, dropDownOptions, defaultIndex, onValueChanged);

        protected void CreateSettingCellSlider(string label, float defaultFloat, UnityAction<float> onValueChanged)
            => this.view.CreateSettingCellSlider(label, defaultFloat, onValueChanged);

        // private void CreateInputFieldAndButtonSettingCell(string label, string defaultInputText, string defaultButtonText, UnityAction<string> onClick)
        // {
        //     var inputFieldAndButton = GameObject.Instantiate(this.settingInputFieldAndButtonPrefab, this.view.Content);
        //     inputFieldAndButton.Init(label, defaultInputText, defaultButtonText, onClick);
        //     this.createdCells.Add(inputFieldAndButton.gameObject);
        // }
    }
}
