using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.UI
{
    public abstract class UIModalWindowBase : UIWindowBase
    {
        protected Animator animator = null;
        protected readonly int openAnimKey = Animator.StringToHash("IsOpen");

        protected GameObject viewObj = null;
        protected CanvasGroup viewCanvasGroup = null;

        public override UIWindowState WindowState { get; protected set; } = UIWindowState.Closed;
        protected virtual bool viewCacheMode => true;
        protected virtual bool toggleByCanvasGroup => false;

        public UIModalWindowBase()
        {
            this.WindowState = UIWindowState.Closed;
        }

        public UIModalWindowBase(GameObject view) : this()
        {
            this.viewObj = view;
            this.viewCanvasGroup = this.viewObj.GetComponent<CanvasGroup>();
            this.animator = this.viewObj.GetComponent<Animator>();
            OnViewCreated();
        }

        public override void Open()
        {
            if (this.IsClosed)
            {
                GlobalCoroutine.StartCoroutine(OpenCoroutine());
                // GlobalCoroutine.StartCoroutine(OpenCoroutine(), this.GetType().Name + nameof(Open));
            }
        }

        private IEnumerator OpenCoroutine()
        {
            this.WindowState = UIWindowState.Opening;

            LoadingManager.Instance.StartLoading(LoadingType.Default);

            if (!this.viewObj)
            {
                var prefab = GetWindowPrefab();
                this.viewObj = GameObject.Instantiate(prefab, UIWindowSettings.Instance.ModalWindowCanvas.transform);
                this.viewCanvasGroup = this.viewObj.GetComponent<CanvasGroup>();
                this.animator = this.viewObj.GetComponent<Animator>();
                OnViewCreated();
            }

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

            // Awake待ち
            yield return null;

            yield return OnBeforeOpen();

            LoadingManager.Instance.EndLoading();

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

            if (this.animator)
            {
                this.animator.SetBool(this.openAnimKey, true);
            }

            this.WindowState = UIWindowState.Opened;

            yield return OnAfterOpen();
        }

        public override void Close()
        {
            if (this.IsOpened)
            {
                GlobalCoroutine.StartCoroutine(CloseCoroutine());
                // GlobalCoroutine.StartCoroutine(CloseCoroutine(), this.GetType().Name + nameof(Close) + );
            }
        }

        private IEnumerator CloseCoroutine()
        {
            this.WindowState = UIWindowState.Closing;

            if (this.animator)
            {
                this.animator.SetBool(this.openAnimKey, false);
                var currentAnimInfo = this.animator.GetCurrentAnimatorClipInfo(0);
                if (currentAnimInfo.Length > 0)
                {
                    var closeAnimTime = currentAnimInfo[0].clip.length;
                    yield return new WaitForSeconds(closeAnimTime);
                }
            }

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

            this.WindowState = UIWindowState.Closed;

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
