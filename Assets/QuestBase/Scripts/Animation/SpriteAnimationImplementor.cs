using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.Animation
{
    public abstract class SpriteAnimationImplementorBase
    {
        public abstract bool Loop { get; set; }
        public abstract float ChangeSpriteInterval { get; set; }
        public abstract Action<Sprite> OnSpriteChanged { get; set; }

        public abstract void Update(double deltaTime);
        public abstract void StartAnimation();
        public abstract void StopAnimation();
    }

    public class SpriteAnimationImplementor : SpriteAnimationImplementorBase
    {
        private IReadOnlyList<Sprite> spriteList = null;

        private int currentIndex = 0;

        public override bool Loop { get; set; } = true;
        public override float ChangeSpriteInterval { get; set; } = 0.2f;
        public override Action<Sprite> OnSpriteChanged { get; set; } = null;

        private IReadOnlyList<Sprite> SpriteList => spriteList;
        public Sprite CurrentSprite => spriteList[currentIndex];
        public int CurrentSpriteIndex => currentIndex;

        private double elapsedTime = 0;
        private double elapsedTimeSinceSpriteChanged = 0;

        private bool canAnimate = false;

        public SpriteAnimationImplementor(IReadOnlyList<Sprite> sprites)
        {
            this.spriteList = sprites;
        }

        private void Init()
        {
            this.elapsedTime = 0;
            this.elapsedTimeSinceSpriteChanged = 0;
            this.currentIndex = 0;
            this.canAnimate = true;
            OnSpriteChanged(spriteList[0]);
        }

        public override void StartAnimation()
        {
            Init();
        }

        public override void StopAnimation()
        {
            this.canAnimate = false;
        }

        public override void Update(double deltaTime)
        {
            if (!canAnimate)
                return;

            this.elapsedTime += deltaTime;
            this.elapsedTimeSinceSpriteChanged += deltaTime;

            if (this.ChangeSpriteInterval < this.elapsedTimeSinceSpriteChanged)
            {
                SetNextSprite();
                this.elapsedTimeSinceSpriteChanged = 0;
            }
        }

        /// <summary>
        /// 次のフレームを再生する
        /// </summary>
        /// <returns></returns>
        public bool SetNextSprite()
        {
            int nextIndex;
            if (this.currentIndex + 1 < this.spriteList.Count)
            {
                // 次のフレームがリストの範囲内なら取得
                nextIndex = currentIndex + 1;
            }
            else if (this.Loop)
            {
                // 範囲外でループなら0に戻す
                nextIndex = 0;
            }
            else
            {
                // その他は何もしない（失敗終了）
                return false;
            }

            this.OnSpriteChanged?.Invoke(this.spriteList[nextIndex]);

            this.currentIndex = nextIndex;
            return true;
        }
    }
}
