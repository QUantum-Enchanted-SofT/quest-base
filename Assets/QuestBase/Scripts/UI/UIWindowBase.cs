using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuestBase.UI
{
    public enum UIWindowState
    {
        Opening,
        Opened,
        Closing,
        Closed,
    }

    public abstract class UIWindowBase
    {
        public abstract UIWindowState WindowState { get; protected set; }
        public bool IsClosed => WindowState == UIWindowState.Closed;
        public bool IsOpened => WindowState == UIWindowState.Opened;

        public abstract void Open();
        public abstract void Close();
        public abstract void Dispose();

        protected GameObject GetWindowPrefab()
        {
            var table = Resources.Load(UIWindowDefinitions.WindowPrefabTablePath) as WindowPrefabTable;
            Enum.TryParse(GetType().Name, out UIWindowType windowType);
            var prefab = table.WindowPrefabList.FirstOrDefault(w => w.WindowType == windowType).Prefab;
            return prefab;
        }
    }
}
