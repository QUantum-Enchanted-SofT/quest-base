using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.UI
{
    public abstract class UIDialogBase : UIWindowBase
    {
        protected GameObject viewObj = null;
        public override UIWindowState WindowState { get; protected set; } = UIWindowState.Closed;

        public UIDialogBase()
        {
            this.WindowState = UIWindowState.Closed;
            var prefab = GetWindowPrefab();
            this.viewObj = GameObject.Instantiate(prefab, UIWindowSettings.Instance.DialogCanvas.transform);
        }

        public override void Open()
        {
            GlobalCoroutine.StartCoroutine(OpenCoroutine(), this.GetType().Name + nameof(Open));
        }

        private IEnumerator OpenCoroutine()
        {
            this.WindowState = UIWindowState.Opening;

            LoadingManager.Instance.StartLoading(LoadingType.Default);
            yield return OnBeforeOpen();
            LoadingManager.Instance.EndLoading();

            this.WindowState = UIWindowState.Opened;

            yield return OnAfterOpen();
        }

        public override void Close()
        {
            GlobalCoroutine.StartCoroutine(CloseCoroutine(), this.GetType().Name + nameof(Close));
        }

        private IEnumerator CloseCoroutine()
        {
            this.WindowState = UIWindowState.Closing;

            GameObject.Destroy(viewObj);
            while (viewObj)
            {
                yield return null;
            }

            this.WindowState = UIWindowState.Closed;
        }

        protected abstract IEnumerator OnBeforeOpen();
        protected virtual IEnumerator OnAfterOpen()
        {
            yield break;
        }
    }
}
