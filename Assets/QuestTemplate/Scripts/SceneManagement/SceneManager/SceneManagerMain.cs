using QuestTemplate.Data.InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QuestBase.SceneManagement;
using QuestBase;
using System;

namespace QuestTemplate.SceneManagement.Main
{
    public class SceneManagerMain : SceneManager
    {
        private SceneBase currentScene = null;
        private Dictionary<Type, Dictionary<string, object>> sceneCache = new Dictionary<Type, Dictionary<string, object>>();

        protected SceneManagerMain(SceneType firstScene) : base(firstScene)
        {
        }

        public static void Create(SceneType firstScene)
        {
            if (Instance == null)
            {
                Instance = new SceneManagerMain(firstScene);
            }
        }

        private IEnumerator LoadSceneCoroutine<T>(SceneInitData initData, string sceneName) where T : SceneBase, new()
        {
            // transition and loading
            yield return TransitionManager.Instance.TransitionOut();
            LoadingManager.Instance.StartLoading();

            // dispose before scene
            if (this.currentScene != null)
            {
                yield return this.currentScene.OnBeforeUnloadScene();
                this.sceneCache[currentScene.GetType()] = this.currentScene.GetCache();
                this.currentScene.Dispose();
            }

            // init next scene
            this.currentScene = new T();
            if (this.sceneCache.TryGetValue(typeof(T), out var cache))
            {
                this.currentScene.SetCache(cache);
            }

            // init next scene
            this.currentScene = new T();

            // load scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

            yield return this.currentScene.OnBeforeLoadScene(initData);

            // transition and loading out
            LoadingManager.Instance.EndLoading();
            yield return TransitionManager.Instance.TransitionIn();

            // done
            yield return this.currentScene.OnAfterLoadScene();
        }

        public override void LoadInGame(InGameInitData initData)
        {
            GlobalCoroutine.StartCoroutine(LoadSceneCoroutine<InGameScene>(initData, gameSceneName));
        }
        public override void LoadMenu(MenuInitData initData)
        {
            GlobalCoroutine.StartCoroutine(LoadSceneCoroutine<MenuScene>(initData, menuSceneName));
        }
    }
}
