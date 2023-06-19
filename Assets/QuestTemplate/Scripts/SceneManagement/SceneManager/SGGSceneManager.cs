using SGG.Data.InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGG.SceneManagement
{
    public abstract class SGGSceneManager
    {
        public enum SceneType
        {
            Menu,
            InGame,
        }

        public static SGGSceneManager Instance { get; protected set; }

        protected const string gameSceneName = "Game";
        protected const string menuSceneName = "Menu";

        private SceneType currentScene = SceneType.Menu;

        protected SGGSceneManager(SceneType firstScene)
        {
            this.currentScene = firstScene;
        }

        public abstract void LoadInGame(InGameInitData initData);

        public abstract void LoadMenu(MenuInitData initData);
    }
}
