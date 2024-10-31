using QuestBase.QuestObjectLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dropin
{
    public abstract class UIWindowLogicBase : QuestObjectLogicBase
    {
        public interface IWindowBaseViewEvent
        {
            public virtual void OnOpen() { }
            public virtual void OnClose() { }
        }

        public enum WindowState
        {
            Open,
            Close,
        }

        protected WindowState windowState = WindowState.Close;
        public bool IsOpen => this.windowState == WindowState.Open;

        public void Open()
        {
            if (this.windowState == WindowState.Open)
            {
                return;
            }

            OnOpen();
            this.windowState = WindowState.Open;
            SendViewEvent<IWindowBaseViewEvent>(view => view.OnOpen());
        }

        public void Close()
        {
            if (this.windowState == WindowState.Close)
            {
                return;
            }

            OnClose();
            this.windowState = WindowState.Close;
            SendViewEvent<IWindowBaseViewEvent>(view => view.OnClose());
        }

        protected virtual void OnOpen()
        {
        }

        protected virtual void OnClose()
        {
        }
    }
}
