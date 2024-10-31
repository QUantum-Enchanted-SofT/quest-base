using QuestBase.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace QuestTemplate.UI
{
    public class UISettingButton : MonoBehaviour
    {
        [SerializeField]
        private UIButton button;

        [SerializeField]
        private UIText buttonText;

        public void Init(string buttonName, Action onClick)
        {
            this.buttonText.SetTextDirectly(buttonName);
            this.button.SetClickEvent(onClick);
        }
    }
}
