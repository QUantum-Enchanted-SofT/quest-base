using SGG.Data.InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QuestBase.SceneManagement;
using QuestBase;

namespace SGG.SceneManagement.Main
{
    public class SGGSceneManagerMain : SGGSceneManager
    {
        private SceneBase currentScene = null;

        protected SGGSceneManagerMain(SceneType firstScene) : base(firstScene)
        {
        }

        public static void Create(SceneType firstScene)
        {
            if (Instance == null)
            {
                Instance = new SGGSceneManagerMain(firstScene);
            }
        }

        private IEnumerator LoadSceneCoroutine<T>(SceneInitData initData, string sceneName) where T : SceneBase, new()
        {
            // transition and loading
            yield return TransitionManager.Instance.TransitionOut();
            LoadingManager.Instance.StartLoading();

            // dispose before scene
            yield return currentScene?.OnBeforeUnloadScene();
            this.currentScene?.Dispose();

            // init next scene
            this.currentScene = new T();

            // load scene
            SceneManager.LoadScene(sceneName);

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
