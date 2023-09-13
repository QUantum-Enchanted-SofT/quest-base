using QuestTemplate.Data.InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestTemplate.SceneManagement
{
    public abstract class SceneManager
    {
        public enum SceneType
        {
            Menu,
            InGame,
        }

        public static SceneManager Instance { get; protected set; }

        protected const string gameSceneName = "Game";
        protected const string menuSceneName = "Menu";

        private SceneType currentScene = SceneType.Menu;

        protected SceneManager(SceneType firstScene)
        {
            this.currentScene = firstScene;
        }

        public abstract void LoadInGame(InGameInitData initData);

        public abstract void LoadMenu(MenuInitData initData);
    }
}
