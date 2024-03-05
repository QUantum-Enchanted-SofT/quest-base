using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using QuestBase.UI;

namespace QuestTemplate.UI
{
    public class UISettingDropdown : MonoBehaviour
    {
        [SerializeField]
        private UIText labelText;

        [SerializeField]
        private TMP_Dropdown dropdown;

        public void Init(string label, List<TMP_Dropdown.OptionData> options, int defaultIndex, UnityAction<int> onValueChanged)
        {
            this.labelText.SetTextDirectly(label);
            this.dropdown.AddOptions(options);
            this.dropdown.value = defaultIndex;
            this.dropdown.onValueChanged.AddListener(onValueChanged);
        }
    }
}
