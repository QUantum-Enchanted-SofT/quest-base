using QuestBase.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace QuestTemplate.UI
{
    public class UISettingCheckBox : MonoBehaviour
    {
        [SerializeField]
        private UIText labelText;

        [SerializeField]
        private Toggle toggle;

        public void Init(string label, bool defaultValue, UnityAction<bool> onValueChanged)
        {
            this.labelText.SetTextDirectly(label);
            this.toggle.isOn = defaultValue;
            this.toggle.onValueChanged.AddListener(onValueChanged);
        }
    }
}
