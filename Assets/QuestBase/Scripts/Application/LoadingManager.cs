using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using QuestBase.Data.SO;

namespace QuestBase
{
    public class LoadingManager
    {
        private static LoadingManager instance = null;
        public static LoadingManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var prefabTable = Resources.Load<LoadingPrefabTable>(LoadingDefinitions.loadingPrefabTablePath);
                    instance = new LoadingManager(prefabTable);
                }
                return instance;
            }
        }

        private int retainCount = 0;

        private LoadingPrefabTable loadingPrefabTable { get; }
        private GameObject canvasObj = null;

        private GameObject currentLoadingObj = null;

        // ローディング開始時点でのInputActionsの状態
        public Dictionary<IInputActionCollection, bool> prevInputActionsState = null;

        protected LoadingManager(LoadingPrefabTable prefabTable)
        {
            this.loadingPrefabTable = prefabTable;
            this.canvasObj = GameObject.Instantiate(prefabTable.LoadingCanvasPrefab);
            GameObject.DontDestroyOnLoad(this.canvasObj);
            this.prevInputActionsState = new Dictionary<IInputActionCollection, bool>();
        }

        public void StartLoading(LoadingType loadingType = LoadingType.Default)
        {
            if (retainCount == 0)
            {
                var prefab = loadingPrefabTable.LoadingPrefabList.Where(p => p.LoadingType == loadingType).FirstOrDefault().Prefab;
                this.currentLoadingObj = GameObject.Instantiate(prefab, this.canvasObj.transform);
                InputActionsManager.Instance.LockAll();
            }

            retainCount++;
        }

        public void EndLoading()
        {
            retainCount--;

            if (0 == retainCount)
            {
                GameObject.Destroy(this.currentLoadingObj);
                this.currentLoadingObj = null;
                InputActionsManager.Instance.UnlockAll();
            }
        }
    }
}
