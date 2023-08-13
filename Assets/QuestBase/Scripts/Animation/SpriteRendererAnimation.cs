using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRendererAnimation : MBSpriteAnimation
    {
        private SpriteRenderer spriteRenderer;

        protected override void Init()
        {
            this.spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override SpriteAnimationImplementorBase CreateSpriteAnimationImp()
        {
            return new SpriteAnimationImplementor(this.sprites);
        }

        public override void SetSprite(Sprite sprite)
        {
            this.spriteRenderer.sprite = sprite;
        }
    }
}
