using SGG.Data.Master;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.Data
{
    public class MasterDataDefinitions
    {
        private const string baseDir = "MasterData/";

        public static Dictionary<Type, string> PlayerDataPaths = new Dictionary<Type, string>
        {
            {typeof(SEVolumeData), baseDir + "SEVolume"},
        };
    }
}
