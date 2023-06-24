using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using QuestBase.Utility;

namespace QuestBase.Data
{
    public class MasterDataLoader
    {
        private static string GetFilePath<T>()
        {
            if (MasterDataDefinitions.PlayerDataPaths.TryGetValue(typeof(T), out var path))
            {
                return path;
            }

            Debug.LogError($"{typeof(T).Name} path is not defined");
            throw new NotImplementedException();
        }

        public static T[] Load<T>()
        {
            var csv = Resources.Load<TextAsset>(GetFilePath<T>());
            var data = CsvUtility.FromCsv<T>(csv.text);
            Resources.UnloadAsset(csv);

            return data;
        }
    }
}
