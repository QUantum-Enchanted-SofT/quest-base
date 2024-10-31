using QuestTemplate.Data.Master;
using QuestTemplate.Data.Player;
using QuestTemplate.SceneManagement;
using QuestTemplate.SceneManagement.Main;
using QuestBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestBase.Sound;

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
            masterDataManager.LoadAsset<QuestObjectPrefabTable>();

            var questObjectFactory = new QuestObjectFactory();

            SceneManagerMain.Create(SceneManager.SceneType.Menu);
            // Cursor.lockState = CursorLockMode.Confined;

            ApplySettings();

            // Cursor.SetCursor(this.cursorTexture, this.cursorHotSpot, CursorMode.ForceSoftware);
        }

        private void Start()
        {
            ApplySettings();
            LoadFirstScene();
        }

        public void ApplySettings()
        {
            var playerData = PlayerDataManager.Instance.GetData<PlayerSettingData>();
            SoundPlayer.Instance.SetMasterVolume(playerData.MasterVolume);
            SoundPlayer.Instance.SetBgmVolume(playerData.BgmVolume);
            SoundPlayer.Instance.SetSeVolume(playerData.SeVolume);
        }

        protected virtual void LoadFirstScene()
        {
            SceneManager.Instance.LoadMenu(new MenuInitData());
        }
    }
}
