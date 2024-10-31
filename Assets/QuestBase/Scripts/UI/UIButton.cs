using QuestBase.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestBase.UI
{
    public class UIButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private bool clickSeEnabled = true;

        [SerializeField]
        private SeType clickSeType;

        public bool ClickSeEnabled => this.clickSeEnabled;
        public SeType ClickSeType => this.clickSeType;
        private Action onClickEvent = null;

        private void Awake()
        {
            this.button.onClick.AddListener(OnClick);
        }

        public void SetClickEvent(Action onClick)
        {
            this.onClickEvent = onClick;
        }

        public void SetClickSeEnabled(bool isEnabled)
        {
            this.clickSeEnabled = isEnabled;
        }

        public void RemoveAllClickEvents()
        {
            this.onClickEvent = null;
        }

        public void SetClickSEType(SeType seType)
        {
            this.clickSeType = seType;
        }

        public void SetInteractable(bool interactable)
        {
            this.button.interactable = interactable;
        }

        private void OnClick()
        {
            this.onClickEvent?.Invoke();

            if (this.clickSeEnabled)
            {
                SoundPlayer.Instance.PlaySE(clickSeType);
            }
        }
    }
}
