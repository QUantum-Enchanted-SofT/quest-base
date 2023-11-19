using QuestBase.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.Data.SO
{
    [CreateAssetMenu(fileName = "BgmDataTable", menuName = "Quest Base/Scriptable Objects/Create BgmDataTable")]
    public class BgmDataTable : TableBase<BgmType, BgmData>
    {
    }

    [Serializable]
    public struct BgmData
    {
        [Range(0f, 1f)]
        public float Volume;
    }
}
