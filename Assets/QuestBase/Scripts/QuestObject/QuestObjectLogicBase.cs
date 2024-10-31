using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.QuestObjectLogic
{
    public class CollisionParameter
    {
        public Collision Collision3D = null;
        public Collision2D Collision2D = null;

        public CollisionParameter(Collision collision)
        {
            this.Collision3D = collision;
        }

        public CollisionParameter(Collision2D collision)
        {
            this.Collision2D = collision;
        }
    }

    public class CollisionContactPoint
    {
        public CollisionContactPoint(ContactPoint contact)
        {
            this.Point = contact.point;
            this.Normal = contact.normal;
        }

        public CollisionContactPoint(ContactPoint2D contact)
        {
            this.Point = contact.point;
            this.Normal = contact.normal;
        }

        public Vector3 Point;
        public Vector3 Normal;
    }

    public abstract class QuestObjectLogicBase : IDisposable
    {
        protected List<IQuestObjectView> views = new List<IQuestObjectView>();

        public virtual void OnCollisionEnter(CollisionParameter collision)
        {

        }

        public virtual void OnCollisionStay(CollisionParameter collision)
        {

        }

        public virtual void OnCollisionExit(CollisionParameter collision)
        {

        }

        public void RegisterView(IQuestObjectView view)
        {
            this.views.Add(view);
        }

        public void UnregisterView(IQuestObjectView view)
        {
            this.views.Remove(view);
        }

        protected void SendViewEvent<T>(Action<T> viewEventFunc)
        {
            foreach (var view in this.views)
            {
                if (view is T viewEvent)
                {
                    viewEventFunc.Invoke(viewEvent);
                }
            }
        }

        public void Dispose()
        {
            foreach (var view in this.views)
            {
                view.OnLogicDisposed();
            }
        }
    }
}
