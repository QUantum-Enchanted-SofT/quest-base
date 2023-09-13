using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.UI
{
    public abstract class UIModalWindowBase : UIWindowBase
    {
        protected GameObject viewObj = null;
        protected CanvasGroup viewCanvasGroup = null;

        public override bool IsClosed { get; protected set; } = false;
        protected virtual bool viewCacheMode => true;
        protected virtual bool toggleByCanvasGroup => false;

        public UIModalWindowBase()
        {
            this.IsClosed = true;
        }

        public UIModalWindowBase(GameObject view) : this()
        {
            this.viewObj = view;
            this.viewCanvasGroup = this.viewObj.GetComponent<CanvasGroup>();
            OnViewCreated();
        }

        public override void Open()
        {
            GlobalCoroutine.StartCoroutine(OpenCoroutine(), this.GetType().Name + nameof(Open));
        }

        private IEnumerator OpenCoroutine()
        {
            LoadingManager.Instance.StartLoading(LoadingType.Default);

            if (this.viewObj)
            {
                if (this.toggleByCanvasGroup)
                {
                    this.viewCanvasGroup.alpha = 1f;
                    this.viewCanvasGroup.blocksRaycasts = true;
                    this.viewCanvasGroup.interactable = true;
                }
                else
                {
                    this.viewObj.SetActive(true);
                }
            }
            else
            {
                var prefab = GetWindowPrefab();
                this.viewObj = GameObject.Instantiate(prefab, UIWindowSettings.Instance.ModalWindowCanvas.transform);
                this.viewCanvasGroup = this.viewObj.GetComponent<CanvasGroup>();
                OnViewCreated();
            }

            yield return OnBeforeOpen();

            this.IsClosed = false;

            LoadingManager.Instance.EndLoading();
            yield return OnAfterOpen();
        }

        public override void Close()
        {
            GlobalCoroutine.StartCoroutine(CloseCoroutine(), this.GetType().Name + nameof(Close));
        }

        private IEnumerator CloseCoroutine()
        {
            if (this.viewCacheMode)
            {
                if (this.toggleByCanvasGroup)
                {
                    this.viewCanvasGroup.alpha = 0f;
                    this.viewCanvasGroup.blocksRaycasts = false;
                    this.viewCanvasGroup.interactable = false;
                }
                else
                {
                    this.viewObj.SetActive(false);
                }
            }
            else
            {
                GameObject.Destroy(this.viewObj);
                while (this.viewObj)
                {
                    yield return null;
                }
                this.viewObj = null;
            }

            this.IsClosed = true;
            OnAfterClose();
        }

        protected abstract void OnViewCreated();

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
