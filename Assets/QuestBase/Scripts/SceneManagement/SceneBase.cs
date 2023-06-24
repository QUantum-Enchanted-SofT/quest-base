using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace QuestBase.SceneManagement
{
    public abstract class SceneBase : IDisposable
    {
        protected SceneBase()
        {

        }

        public void Dispose()
        {
            OnDispose();
        }

        /// <summary>
        /// 画面が開ける前のロード画面
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerator OnBeforeLoadScene(SceneInitData initData);

        /// <summary>
        /// 画面が完全に開けたタイミング
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerator OnAfterLoadScene();

        /// <summary>
        /// シーンがアンロードされる前のタイミング
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerator OnBeforeUnloadScene();

        /// <summary>
        /// シングルトンが破棄されるタイミング
        /// </summary>
        protected abstract void OnDispose();
    }
}
