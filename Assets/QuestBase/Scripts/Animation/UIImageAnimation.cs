using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QuestBase.Animation
{
    [RequireComponent(typeof(Image))]
    public class UIImageAnimation : MBSpriteAnimation
    {
        private Image image;

        protected override SpriteAnimationImplementorBase CreateSpriteAnimationImp()
        {
            return new SpriteAnimationImplementor(this.sprites);
        }

        protected override void Init()
        {
            this.image = GetComponent<Image>();
        }

        public override void SetSprite(Sprite sprite)
        {
            this.image.sprite = sprite;
        }
    }
}
