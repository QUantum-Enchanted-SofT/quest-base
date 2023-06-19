using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.Data.SO
{
    [CreateAssetMenu(fileName = "LoadingPrefabTable", menuName = "Quest Base/Scriptable Objects/Create LoadingPrefabTable")]
    public class LoadingPrefabTable : ScriptableObject
    {
        [System.Serializable]
        public class LoadingObject
        {
            [SerializeField]
            private LoadingType loadingType;

            [SerializeField]
            private GameObject prefab;

            public LoadingType LoadingType => loadingType;
            public GameObject Prefab => prefab;
        }

        [SerializeField]
        private GameObject loadingCanvasPrefab;

        [SerializeField]
        private List<LoadingObject> loadingPrefabList;

        public GameObject LoadingCanvasPrefab => loadingCanvasPrefab;
        public IReadOnlyList<LoadingObject> LoadingPrefabList => loadingPrefabList.AsReadOnly();
    }
}
