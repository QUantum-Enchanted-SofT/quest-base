using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestTemplate.SceneManagement;
using QuestBase.SceneManagement;

namespace QuestTemplate.Data.InGame
{
    public class MenuScene : SceneBase
    {
        public static MenuScene Instance { get; private set; }
        public MenuInitData InitData { get; private set; }

        public MenuScene()
        {
            Instance = this;
        }

        public override IEnumerator OnBeforeLoadScene(SceneInitData initData)
        {
            this.InitData = initData as MenuInitData;

            yield break;
        }

        public override IEnumerator OnAfterLoadScene()
        {
            yield break;
        }

        public override IEnumerator OnBeforeUnloadScene()
        {
            yield break;
        }

        protected override void OnDispose()
        {
            Instance = null;
        }
    }
}
