using QuestBase.QuestObjectLogic;
using QuestBase.QuestObjectView;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using QuestTemplate.Data.Master;

namespace QuestBase
{
    public class QuestObjectFactory
    {
        public static QuestObjectFactory Instance { get; private set; }
        private Dictionary<QuestObjectViewType, List<QuestObjectViewBase>> viewCache = new Dictionary<QuestObjectViewType, List<QuestObjectViewBase>>();

        public QuestObjectFactory()
        {
            Instance = this;
        }

        /// <summary>
        /// Create QuestObjectView
        /// </summary>
        /// <param name="viewType">Type of quest object</param>
        /// <param name="logic">QuestObject (presenter)</param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public QuestObjectViewBase CreateQuestObjectView(
            QuestObjectViewType viewType,
            QuestObjectLogicBase logic,
            Vector3 position = default(Vector3),
            Quaternion rotation = default(Quaternion),
            Transform parent = null,
            bool useCache = true)
        {
            var prefabTable = MasterDataManager.Instance.GetData<QuestObjectPrefabTable>();
            var prefab = prefabTable.GetValue(viewType);

            QuestObjectViewBase view;
            if (useCache
                && this.viewCache.TryGetValue(viewType, out var cacheList)
                && cacheList.Any())
            {
                // キャッシュが使える場合
                view = cacheList[0];
                if (parent)
                {
                    view.transform.SetParent(parent);
                }
                view.transform.position = position;
                view.transform.rotation = rotation;
                view.gameObject.SetActive(true);
                cacheList.RemoveAt(0);
            }
            else
            {
                // キャッシュが使えない場合
                if (parent == null)
                {
                    view = GameObject.Instantiate(prefab, position, rotation);
                }
                else
                {
                    view = GameObject.Instantiate(prefab, position, rotation, parent);
                }
            }

            view.Init(viewType, logic);

            return view;
        }

        public void ReturnCache<T>(T view) where T : QuestObjectViewBase
        {
            if (this.viewCache.TryGetValue(view.ViewType, out var cacheList))
            {
                cacheList.Add(view);
            }
            else
            {
                this.viewCache.Add(view.ViewType, new List<QuestObjectViewBase> { view });
            }
            view.gameObject.SetActive(false);
        }

        public void ClearAllCache()
        {
            foreach (var viewList in this.viewCache.Values)
            {
                foreach (var view in viewList)
                {
                    if (view)
                    {
                        GameObject.Destroy(view);
                    }
                }
                viewList.Clear();
            }
            this.viewCache.Clear();
        }
    }
}
