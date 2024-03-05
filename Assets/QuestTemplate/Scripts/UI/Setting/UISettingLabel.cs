using QuestBase.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace QuestTemplate.UI
{
    public class UISettingLabel : MonoBehaviour
    {
        [SerializeField]
        private UIText labelText;

        public void Init(string label)
        {
            this.labelText.SetTextDirectly(label);
        }
    }
}
