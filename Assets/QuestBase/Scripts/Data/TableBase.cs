using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestBase.Data
{
    public abstract class TableBase<TKey, TValue> : ScriptableObject, ISerializationCallbackReceiver, IEnumerable<KeyValuePair<TKey, TValue>>
    {
        [Serializable]
        public class TableData
        {
            public TKey Key;
            public TValue Value;
        }

        public TableData[] TableDataList = new TableData[0];

        protected Dictionary<TKey, TValue> keyValuePairs;
        public Dictionary<TKey, TValue>.ValueCollection Values => this.keyValuePairs.Values;
        public Dictionary<TKey, TValue>.KeyCollection Keys => this.keyValuePairs.Keys;

        public virtual void OnBeforeSerialize()
        {
        }

        public virtual void OnAfterDeserialize()
        {
            keyValuePairs = TableDataList.ToDictionary(t => t.Key, t => t.Value);
        }

        public virtual TValue GetValue(TKey key)
        {
            if (keyValuePairs.TryGetValue(key, out var val))
            {
                return val;
            }
            else
            {
                Debug.LogError($"key {key} is not found.");
                throw new KeyNotFoundException();
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.keyValuePairs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.keyValuePairs.GetEnumerator();
        }
    }
}
