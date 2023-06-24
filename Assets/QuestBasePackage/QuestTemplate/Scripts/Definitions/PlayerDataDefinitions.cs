using SGG.Data.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.Data
{
    public class PlayerDataDefinitions
    {
        private static string baseDir => Application.persistentDataPath + "/PlayerData";

        public static Dictionary<Type, string> PlayerDataPaths = new Dictionary<Type, string>
        {
            {typeof(PlayerBasicData), baseDir + "/PlayerBasicData.json"},
            {typeof(PlayerSettingData), baseDir + "/PlayerSettingData.json"},
        };
    }
}
