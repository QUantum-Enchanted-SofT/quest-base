using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.UI
{
    public abstract class UIModalWindowBase : UIWindowBase
    {
        protected GameObject viewObj = null;
        public override bool IsClosed { get; protected set; } = false;

        public UIModalWindowBase()
        {
            this.IsClosed = false;
            var prefab = GetWindowPrefab();
            this.viewObj = GameObject.Instantiate(prefab, UIWindowSettings.Instance.ModalWindowCanvas.transform);
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
            OnAfterClose();
        }

        protected abstract IEnumerator OnBeforeOpen();

        protected virtual IEnumerator OnAfterOpen()
        {
            yield break;
        }

        protected virtual IEnumerator OnAfterClose()
        {
            yield break;
        }
    }
}
