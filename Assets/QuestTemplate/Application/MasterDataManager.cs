using QuestBase.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestTemplate.Data.Master
{
    public class MasterDataManager
    {
        public static MasterDataManager Instance { get; private set; }
        private Dictionary<Type, object> dataDict = new Dictionary<Type, object>();

        public MasterDataManager()
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
        }

        public void LoadAsset<T>() where T : ScriptableObject
        {
            var data = MasterDataLoader.LoadAsset<T>();
            this.dataDict.Add(typeof(T), data);
        }

        public void LoadCsv<T>()
        {
            var data = MasterDataLoader.LoadCsv<T>();
            this.dataDict.Add(typeof(T[]), data);
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
    }
}
