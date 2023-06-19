using QuestBase.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGG.Data.InGame
{
    public class InGameScene : SceneBase
    {
        public static InGameScene Instance { get; private set; }
        public InGameInitData InitData;

        public InGameScene()
        {
            Instance = this;
        }

        public override IEnumerator OnBeforeLoadScene(SceneInitData initData)
        {
            this.InitData = initData as InGameInitData;

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
