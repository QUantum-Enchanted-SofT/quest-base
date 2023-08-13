using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.Animation
{
    /// <summary>
    /// (勉強中だから)Bridgeパターン, Factory Methodで書いてみる
    /// </summary>
    public abstract class MBSpriteAnimation : MonoBehaviour
    {
        [SerializeField]
        protected List<Sprite> sprites;

        [SerializeField]
        protected bool loop = true;

        [SerializeField]
        protected float spriteChangeInterval = 0.2f;

        [SerializeField]
        protected bool playOnAwake = true;

        public IReadOnlyList<Sprite> Sprites => sprites;

        protected SpriteAnimationImplementorBase spriteAnimationImp { get; private set; }

        protected virtual void Awake()
        {
            Init();
            this.spriteAnimationImp = CreateSpriteAnimationImp();

            if (playOnAwake)
            {
                StartAnimation();
            }
        }

        protected virtual void Update()
        {
            this.spriteAnimationImp.Update(Time.deltaTime);
        }

        protected virtual void InitAnimParams()
        {
            this.spriteAnimationImp.Loop = loop;
            this.spriteAnimationImp.ChangeSpriteInterval = spriteChangeInterval;
            this.spriteAnimationImp.OnSpriteChanged = SetSprite;
        }

        public virtual void StartAnimation()
        {
            InitAnimParams();
            this.spriteAnimationImp.StartAnimation();
        }

        public virtual void StopAnimation()
        {
            this.spriteAnimationImp.StopAnimation();
        }

        protected abstract void Init();
        protected abstract SpriteAnimationImplementorBase CreateSpriteAnimationImp();
        public abstract void SetSprite(Sprite sprite);
    }
}
