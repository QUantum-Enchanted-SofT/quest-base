using QuestBase.Data;
using QuestBase.QuestObjectView;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase
{
    [CreateAssetMenu(fileName = "QuestObjectPrefabTable", menuName = "Quest Base/Scriptable Objects/Create QuestObjectPrefabTable")]
    public class QuestObjectPrefabTable : TableBase<QuestObjectViewType, QuestObjectViewBase>
    {
    }
}
