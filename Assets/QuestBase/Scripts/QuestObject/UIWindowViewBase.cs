using Cysharp.Threading.Tasks;
using QuestBase.QuestObjectView;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace QuestTemplate
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIWindowViewBase : QuestObjectViewBase, UIWindowLogicBase.IWindowBaseViewEvent
    {
        private readonly int IsOpenAnimKey = Animator.StringToHash("IsOpen");

        [SerializeField]
        private bool fadeEnable = false;

        [SerializeField]
        private float fadeTime = 0.3f;

        [SerializeField]
        private bool animatorEnable = false;

        [SerializeField]
        private float openAnimTime = 0.5f;

        [SerializeField]
        private float closeAnimTime = 0.5f;

        private CanvasGroup canvasGroup = null;
        private Animator animator = null;

        private CancellationTokenSource openCloseCTS = null;

        protected override void OnInit()
        {
            base.OnInit();
            this.canvasGroup = gameObject.GetComponent<CanvasGroup>();
            this.canvasGroup.alpha = 0f;
            this.canvasGroup.blocksRaycasts = false;

            this.animator = gameObject.GetComponent<Animator>();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
        }

        protected async void Open()
        {
            this.openCloseCTS?.Cancel();
            this.openCloseCTS = new CancellationTokenSource();
            await OpenAsync(this.openCloseCTS.Token);
        }

        protected async void Close()
        {
            this.openCloseCTS?.Cancel();
            this.openCloseCTS = new CancellationTokenSource();
            await CloseAsync(this.openCloseCTS.Token);
        }

        protected virtual async UniTask OpenAsync(CancellationToken token)
        {
            this.canvasGroup.blocksRaycasts = false;

            if (!this.fadeEnable)
            {
                this.canvasGroup.alpha = 1f;
            }

            var anim = this.animatorEnable ? OpenAnimation(token) : UniTask.Delay(0);
            var fade = this.fadeEnable ? FadeIn(token) : UniTask.Delay(0);

            await UniTask.WhenAll(anim, fade);

            this.canvasGroup.blocksRaycasts = true;
        }

        protected virtual async UniTask CloseAsync(CancellationToken token)
        {
            this.canvasGroup.blocksRaycasts = false;

            var anim = this.animatorEnable ? CloseAnimation(token) : UniTask.Delay(0);
            var fade = this.fadeEnable ? FadeOut(token) : UniTask.Delay(0);

            await UniTask.WhenAll(anim, fade);

            if (!this.fadeEnable)
            {
                this.canvasGroup.alpha = 0f;
            }
        }

        protected virtual async UniTask OpenAnimation(CancellationToken token)
        {
            this.animator.SetBool(this.IsOpenAnimKey, true);
            await UniTask.Delay((int)(this.openAnimTime * 1000f), cancellationToken: token);
        }

        protected virtual async UniTask CloseAnimation(CancellationToken token)
        {
            this.animator.SetBool(this.IsOpenAnimKey, false);
            await UniTask.Delay((int)(this.closeAnimTime * 1000f), cancellationToken: token);
        }

        private async UniTask FadeIn(CancellationToken token)
        {
            while (this.canvasGroup.alpha < 1)
            {
                this.canvasGroup.alpha += Time.deltaTime / fadeTime;
                if (1 <= this.canvasGroup.alpha)
                {
                    this.canvasGroup.alpha = 1f;
                }

                await UniTask.NextFrame(cancellationToken: token);
            }
        }

        private async UniTask FadeOut(CancellationToken token)
        {
            while (0 < this.canvasGroup.alpha)
            {
                this.canvasGroup.alpha -= Time.deltaTime / fadeTime;
                if (this.canvasGroup.alpha <= 0)
                {
                    this.canvasGroup.alpha = 0f;
                }

                await UniTask.NextFrame(cancellationToken: token);
            }
        }

        void UIWindowLogicBase.IWindowBaseViewEvent.OnOpen()
        {
            Open();
        }

        void UIWindowLogicBase.IWindowBaseViewEvent.OnClose()
        {
            Close();
        }
    }
}
