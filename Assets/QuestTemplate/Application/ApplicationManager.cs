using QuestTemplate.Data.Master;
using QuestTemplate.Data.Player;
using QuestTemplate.SceneManagement;
using QuestTemplate.SceneManagement.Main;
using QuestBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestTemplate
{
    public class ApplicationManager : MonoBehaviour
    {
        public static ApplicationManager Instance { get; private set; } = null;

        [SerializeField]
        private InputActionsManager inputActionsManagerPrefab = null;

        [SerializeField]
        private Texture2D cursorTexture = null;

        [SerializeField]
        private Vector2 cursorHotSpot = new Vector2(16, 30);

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            var playerDataManager = new PlayerDataManager();
            playerDataManager.Init();

            var masterDataManager = new MasterDataManager();
            masterDataManager.Init();

            SceneManagerMain.Create(SceneManager.SceneType.Menu);
            // Cursor.lockState = CursorLockMode.Confined;
            Instantiate(inputActionsManagerPrefab);

            ApplySettings();

            var initData = new MenuInitData
            {
            };
            SceneManager.Instance.LoadMenu(initData);

            // Cursor.SetCursor(this.cursorTexture, this.cursorHotSpot, CursorMode.ForceSoftware);
        }

        public void ApplySettings()
        {
            var playerData = PlayerDataManager.Instance.GetData<PlayerSettingData>();
        }
    }
}
