using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.UI
{
    public abstract class UIDialogBase : UIWindowBase
    {
        protected GameObject viewObj = null;
        public override bool IsClosed { get; protected set; } = false;

        public UIDialogBase()
        {
            this.IsClosed = true;
            var prefab = GetWindowPrefab();
            this.viewObj = GameObject.Instantiate(prefab, UIWindowSettings.Instance.DialogCanvas.transform);
        }

        public override void Open()
        {
            GlobalCoroutine.StartCoroutine(OpenCoroutine(), this.GetType().Name + nameof(Open));
        }

        private IEnumerator OpenCoroutine()
        {
            LoadingManager.Instance.StartLoading(LoadingType.Default);
            yield return OnBeforeOpen();
            LoadingManager.Instance.EndLoading();

            yield return OnAfterOpen();
        }

        public override void Close()
        {
            GlobalCoroutine.StartCoroutine(CloseCoroutine(), this.GetType().Name + nameof(Close));
        }

        private IEnumerator CloseCoroutine()
        {
            GameObject.Destroy(viewObj);
            while (viewObj)
            {
                yield return null;
            }
            this.IsClosed = true;
        }

        protected abstract IEnumerator OnBeforeOpen();
        protected virtual IEnumerator OnAfterOpen()
        {
            yield break;
        }
    }
}
