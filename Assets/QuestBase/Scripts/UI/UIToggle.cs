using QuestBase.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestBase.UI
{
    public class UIToggle : MonoBehaviour
    {
        [SerializeField]
        private Toggle toggle;

        [Header("On Settings")]
        [SerializeField]
        private GameObject[] onObjects;

        [SerializeField]
        private bool onSeEnabled = true;

        [SerializeField]
        private SeType onSeType;

        [Header("Off Settings")]
        [SerializeField]
        private GameObject[] offObjects;

        [SerializeField]
        private bool offSeEnabled = true;

        [SerializeField]
        private SeType offSeType;

        [SerializeField]
        private GameObject[] disableObjects;

        public bool OnSeEnabled => this.onSeEnabled;
        public SeType OnSeType => this.onSeType;

        public bool OffSeEnabled => this.offSeEnabled;
        public SeType OffSeType => this.offSeType;

        public bool IsOn => this.toggle.isOn;

        private Action<bool> onToggleChanged = null;
        private bool dontPlaySeNextOnChanged = false;
        private bool dontInvokeCallbackNextOnChanged = false;

        // ToggleGroupが設定されているとAwakeで各トグルが初期化されるので、その検知用フラグ
        private bool isAwake = false;

        private void Awake()
        {
            this.isAwake = true;
            this.toggle.onValueChanged.AddListener(OnToggleChanged);
            UpdateToggleObjects();
        }

        private void Start()
        {
            this.isAwake = false;
        }

        public void SetToggleValue(bool isOn, bool playSE = false, bool invokeCallback = false)
        {
            if (this.toggle.isOn != isOn)
            {
                this.dontPlaySeNextOnChanged = !playSE;
                this.dontInvokeCallbackNextOnChanged = !invokeCallback;
                this.toggle.isOn = isOn;
            }
        }

        public void SetToggleChangedEvent(Action<bool> onToggleChangedAction)
        {
            this.onToggleChanged = onToggleChangedAction;
        }

        public void RemoveAllToggleEvents()
        {
            this.onToggleChanged = null;
        }

        public void SetOnSeEnabled(bool isEnabled)
        {
            this.onSeEnabled = isEnabled;
        }

        public void SetOnSEType(SeType seType)
        {
            this.onSeType = seType;
        }

        public void SetOffSeEnabled(bool isEnabled)
        {
            this.offSeEnabled = isEnabled;
        }

        public void SetOffSEType(SeType seType)
        {
            this.onSeType = seType;
        }

        public void SetInteractable(bool interactable)
        {
            this.toggle.interactable = interactable;
            UpdateToggleObjects();
        }

        private void OnToggleChanged(bool isOn)
        {
            if (!this.dontInvokeCallbackNextOnChanged)
            {
                this.onToggleChanged?.Invoke(isOn);
            }
            this.dontInvokeCallbackNextOnChanged = false;

            UpdateToggleObjects();

            if (!this.dontPlaySeNextOnChanged && !isAwake)
            {
                PlayToggleSE(isOn);
            }
            this.dontPlaySeNextOnChanged = false;
        }

        private void UpdateToggleObjects()
        {
            var isOn = this.toggle.isOn;
            foreach (var obj in this.onObjects)
            {
                obj.SetActive(isOn);
            }

            foreach (var obj in this.offObjects)
            {
                obj.SetActive(!isOn);
            }

            foreach (var obj in this.disableObjects)
            {
                if (obj)
                {
                    obj.SetActive(!this.toggle.interactable);
                }
            }
        }

        private void PlayToggleSE(bool isOn)
        {
            if (isOn)
            {
                if (this.onSeEnabled)
                {
                    SoundPlayer.Instance.PlaySE(this.onSeType);
                }
            }
            else
            {
                if (this.offSeEnabled)
                {
                    SoundPlayer.Instance.PlaySE(this.offSeType);
                }
            }
        }
    }
}
