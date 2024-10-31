using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace QuestBase.SceneManagement
{
    public abstract class SceneBase : IDisposable
    {
        private Dictionary<string, object> sceneCache = new Dictionary<string, object>();

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

        public Dictionary<string, object> GetCache()
        {
            return this.sceneCache;
        }

        public void SetCache(string key, object value)
        {
            this.sceneCache[key] = value;
        }

        public void SetCache(Dictionary<string, object> cache)
        {
            this.sceneCache = cache;
        }

        public void ClearCache()
        {
            this.sceneCache.Clear();
        }
    }
}
