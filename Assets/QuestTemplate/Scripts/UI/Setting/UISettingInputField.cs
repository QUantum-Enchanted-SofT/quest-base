using QuestBase.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace QuestTemplate.UI
{
    public class UISettingInputField : MonoBehaviour
    {
        [SerializeField]
        private UIText labelText;

        [SerializeField]
        private TMP_InputField inputField;

        public void Init(string label, string defaultText, UnityAction<string> onValueChanged, TMP_InputField.ContentType contentType = TMP_InputField.ContentType.Autocorrected, TMP_InputField.LineType lineType = TMP_InputField.LineType.SingleLine)
        {
            this.labelText.SetTextDirectly(label);
            this.inputField.text = defaultText;
            this.inputField.contentType = contentType;
            this.inputField.lineType = lineType;

            this.inputField.onValueChanged.RemoveAllListeners();
            this.inputField.onValueChanged.AddListener(onValueChanged);
        }
    }
}
