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
            if (MasterDataDefinitions.MasterDataPaths.TryGetValue(typeof(T), out var path))
            {
                return path;
            }

            Debug.LogError($"{typeof(T).Name} path is not defined");
            throw new NotImplementedException();
        }

        public static T LoadAsset<T>() where T : ScriptableObject
        {
            var resource = Resources.Load(GetFilePath<T>());

            return resource as T;
        }

        public static T[] LoadCsv<T>()
        {
            var csv = Resources.Load<TextAsset>(GetFilePath<T>());
            var csvData = CsvUtility.FromCsv<T>(csv.text);
            Resources.UnloadAsset(csv);
            return csvData;
        }
    }
}
