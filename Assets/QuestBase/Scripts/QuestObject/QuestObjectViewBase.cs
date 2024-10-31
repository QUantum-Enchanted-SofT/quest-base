using QuestBase.QuestObjectLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase.QuestObjectView
{
    public abstract class QuestObjectViewBase : MonoBehaviour, IQuestObjectView
    {
        private static List<QuestObjectViewBase> allViews = new List<QuestObjectViewBase>();
        protected static IReadOnlyList<QuestObjectViewBase> AllViews => allViews;

        public QuestObjectViewType ViewType { get; private set; }
        protected QuestObjectBase questObject;

        public void Init(QuestObjectViewType viewType, QuestObjectBase presenter)
        {
            this.ViewType = viewType;
            this.questObject = presenter;
            this.questObject.RegisterView(this);
            allViews.Add(this);

            OnInit();
        }

        /// <summary>
        /// 初期化関数（Awakeとの順番保証は怪しい）
        /// キャッシュ利用時にも呼ばれる
        /// </summary>
        protected virtual void OnInit()
        {
        }

        protected void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        protected void Start()
        {
            OnStart();
        }

        protected virtual void OnStart()
        {
        }

        protected void Update()
        {
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
        }

        public void OnLogicDisposed()
        {
            Destroy(gameObject);
        }

        protected void ReturnView()
        {
            this.questObject.UnregisterView(this);
            allViews.Remove(this);
            QuestObjectFactory.Instance.ReturnCache(this);
        }

        private void OnDestroy()
        {
            OnDispose();
            this.questObject.UnregisterView(this);
            allViews.Remove(this);
        }

        protected virtual void OnDispose()
        {
        }

        public void OnCollisionEnter(Collision collision)
        {
            this.questObject.OnCollisionEnter(new CollisionParameter(collision));
        }

        public void OnCollisionStay(Collision collision)
        {
            this.questObject.OnCollisionStay(new CollisionParameter(collision));
        }

        public void OnCollisionExit(Collision collision)
        {
            this.questObject.OnCollisionExit(new CollisionParameter(collision));
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            this.questObject.OnCollisionEnter(new CollisionParameter(collision));
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            this.questObject.OnCollisionStay(new CollisionParameter(collision));
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            this.questObject.OnCollisionExit(new CollisionParameter(collision));
        }
    }
}
