using QuestBase.Sound;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.Data.SO
{
    [CreateAssetMenu(fileName = "SeDataTable", menuName = "Quest Base/Scriptable Objects/Create SeDataTable")]
    public class SeDataTable : TableBase<SeType, SeData>
    {
    }

    [Serializable]
    public struct SeData
    {
        [Range(0f, 1f)]
        public float Volume;
    }
}
