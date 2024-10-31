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
        protected QuestObjectLogicBase logicBase;

        public void Init(QuestObjectViewType viewType, QuestObjectLogicBase presenter)
        {
            this.ViewType = viewType;
            this.logicBase = presenter;
            this.logicBase.RegisterView(this);
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
            this.logicBase.UnregisterView(this);
            allViews.Remove(this);
            QuestObjectFactory.Instance.ReturnCache(this);
        }

        private void OnDestroy()
        {
            OnDispose();
            this.logicBase.UnregisterView(this);
            allViews.Remove(this);
        }

        protected virtual void OnDispose()
        {
        }

        public void OnCollisionEnter(Collision collision)
        {
            this.logicBase.OnCollisionEnter(new CollisionParameter(collision));
        }

        public void OnCollisionStay(Collision collision)
        {
            this.logicBase.OnCollisionStay(new CollisionParameter(collision));
        }

        public void OnCollisionExit(Collision collision)
        {
            this.logicBase.OnCollisionExit(new CollisionParameter(collision));
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            this.logicBase.OnCollisionEnter(new CollisionParameter(collision));
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
            this.logicBase.OnCollisionStay(new CollisionParameter(collision));
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            this.logicBase.OnCollisionExit(new CollisionParameter(collision));
        }
    }
}
