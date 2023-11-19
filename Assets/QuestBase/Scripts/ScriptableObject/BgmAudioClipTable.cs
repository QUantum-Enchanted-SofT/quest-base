using QuestBase.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.Data.SO
{
    [CreateAssetMenu(fileName = "BgmAudioClipTable", menuName = "Quest Base/Scriptable Objects/Create BgmAudioClipTable")]
    public class BgmAudioClipTable : ScriptableObject
    {
        [System.Serializable]
        public class BgmAudioClipEntity
        {
            [SerializeField]
            private BgmType bgmType;

            [SerializeField]
            private AudioClip clip;

            public BgmType BgmType => this.bgmType;
            public AudioClip Clip => this.clip;
        }

        [SerializeField]
        private List<BgmAudioClipEntity> bgmAudioClipList;
        public IReadOnlyList<BgmAudioClipEntity> BgmAudioClipList => this.bgmAudioClipList;
    }
}
