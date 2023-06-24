using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.Data.SO
{
    [CreateAssetMenu(fileName = "TransitionPrefabTable", menuName = "Quest Base/Scriptable Objects/Create TransitionPrefabTable")]
    public class TransitionPrefabTable : ScriptableObject
    {
        [System.Serializable]
        public class TransitionInObject
        {
            [SerializeField]
            private TransitionInType transitionInType;

            [SerializeField]
            private GameObject prefab;

            public TransitionInType TransitionType => transitionInType;
            public GameObject Prefab => prefab;
        }

        [System.Serializable]
        public class TransitionOutObject
        {
            [SerializeField]
            private TransitionOutType transitionOutType;

            [SerializeField]
            private GameObject prefab;

            public TransitionOutType TransitionType => transitionOutType;
            public GameObject Prefab => prefab;
        }

        [SerializeField]
        private GameObject transitionCanvasPrefab;

        [SerializeField]
        private List<TransitionInObject> transitionInPrefabList;

        [SerializeField]
        private List<TransitionOutObject> transitionOutPrefabList;

        public GameObject TransitionCanvasPrefab => transitionCanvasPrefab;
        public IReadOnlyList<TransitionInObject> TransitionInPrefabList => transitionInPrefabList.AsReadOnly();
        public IReadOnlyList<TransitionOutObject> TransitionOutPrefabList => transitionOutPrefabList.AsReadOnly();
    }
}
