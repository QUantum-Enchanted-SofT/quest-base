using QuestBase.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.Data.SO
{
    [CreateAssetMenu(fileName = "SeAudioClipTable", menuName = "Quest Base/Scriptable Objects/Create SeAudioClipTable")]
    public class SeAudioClipTable : ScriptableObject
    {
        [System.Serializable]
        public class SeAudioClipEntity
        {
            [SerializeField]
            private SeType seType;

            [SerializeField]
            private AudioClip clip;

            public SeType SeType => seType;
            public AudioClip Clip => clip;
        }

        [SerializeField]
        private List<SeAudioClipEntity> seAudioClipList;
        public IReadOnlyList<SeAudioClipEntity> SeAudioClipList => this.seAudioClipList;
    }
}
