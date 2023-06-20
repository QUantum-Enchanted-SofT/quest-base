using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QuestBase.Data
{
    public class PlayerDataLoader
    {
        private static T LoadJson<T>(string path)
        {
            var basicJson = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(basicJson);
        }

        private static void SaveJson(string json, string path)
        {
            var directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(path, json);
        }

        private static string GetFilePath<T>()
        {
            if (PlayerDataDefinitions.PlayerDataPaths.TryGetValue(typeof(T), out var path))
            {
                return path;
            }

            Debug.LogError($"{typeof(T).Name} path is not defined");
            throw new NotImplementedException();
        }

        public static bool Exists<T>()
        {
            return File.Exists(GetFilePath<T>());
        }

        public static T Load<T>()
        {
            if (PlayerDataDefinitions.PlayerDataPaths.TryGetValue(typeof(T), out var path))
            {
                return LoadJson<T>(path);
            }

            Debug.LogError($"{typeof(T).Name} path is not defined");
            throw new NotImplementedException();
        }

        public static void Save<T>(T data)
        {
            var json = JsonUtility.ToJson(data);
            SaveJson(json, GetFilePath<T>());
        }
    }
}
