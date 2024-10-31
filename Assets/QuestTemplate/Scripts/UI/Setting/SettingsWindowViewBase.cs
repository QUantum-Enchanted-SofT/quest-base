using QuestBase.QuestObjectView;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace QuestTemplate.UI
{
    public abstract class SettingsWindowViewBase : QuestObjectViewBase
    {
        [SerializeField]
        protected UISettingsWindowView view = null;

        protected void SetDirty()
        {
            this.view.SetDirty();
            ApplicationManager.Instance.ApplySettings();
        }

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
    }
}
