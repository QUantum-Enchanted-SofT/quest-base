using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.UI
{
    [CreateAssetMenu(fileName = "WindowPrefabTable", menuName = "Quest Base/Scriptable Objects/Create WindowPrefabTable")]
    public class WindowPrefabTable : ScriptableObject
    {
        [System.Serializable]
        public class WindowObject
        {
            [SerializeField]
            private UIWindowType windowType;

            [SerializeField]
            private GameObject prefab;

            public UIWindowType WindowType => windowType;
            public GameObject Prefab => prefab;
        }

        [SerializeField]
        private List<WindowObject> windowPrefabList;
        public IReadOnlyList<WindowObject> WindowPrefabList => windowPrefabList;
    }
}
