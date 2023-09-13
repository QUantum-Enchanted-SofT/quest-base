using QuestBase.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestTemplate.Data.Player
{
    public class PlayerDataManager
    {
        public static PlayerDataManager Instance { get; private set; }
        private Dictionary<Type, object> dataDict = new Dictionary<Type, object>();

        public PlayerDataManager()
        {
            if (Instance != null)
            {
                Debug.LogError("PlayerDataManager already exists.");
                return;
            }

            Instance = this;
        }

        public void Init()
        {
            this.dataDict.Clear();
            LoadOrCreate<PlayerBasicData>();
            LoadOrCreate<PlayerSettingData>();
        }

        private void LoadOrCreate<T>() where T : new()
        {
            var data = PlayerDataLoader.Exists<T>() ? PlayerDataLoader.Load<T>() : new T();
            this.dataDict.Add(typeof(T), data);
        }

        public T GetData<T>()
        {
            if (this.dataDict.TryGetValue(typeof(T), out var data))
            {
                return (T)data;
            }
            else
            {
                Debug.LogError("Save data is not found.");
                throw new NotImplementedException();
            }
        }

        public void Save<T>()
        {
            if (this.dataDict.TryGetValue(typeof(T), out var data))
            {
                PlayerDataLoader.Save((T)data);
            }
            else
            {
                Debug.LogError("Save data is not found.");
            }
        }
    }
}
