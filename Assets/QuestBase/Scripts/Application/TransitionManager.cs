using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using QuestBase.Data.SO;

namespace QuestBase
{
    public class TransitionManager
    {
        private static TransitionManager instance = null;
        public static TransitionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    var prefabTable = Resources.Load<TransitionPrefabTable>(TransitionDefinitions.transitionPrefabTablePath);
                    instance = new TransitionManager(prefabTable);
                }
                return instance;
            }
        }

        private TransitionPrefabTable transitionPrefabTable { get; }
        private GameObject canvasObj = null;

        private GameObject currentTransitionObj = null;

        protected TransitionManager(TransitionPrefabTable prefabTable)
        {
            transitionPrefabTable = prefabTable;
            canvasObj = Object.Instantiate(prefabTable.TransitionCanvasPrefab);
            Object.DontDestroyOnLoad(canvasObj);
        }

        public IEnumerator TransitionIn(TransitionInType transitionInType = TransitionInType.FadeIn)
        {
            InputActionsManager.Instance.LockAll();

            Object.Destroy(currentTransitionObj);

            var prefab = transitionPrefabTable.TransitionInPrefabList.Where(p => p.TransitionType == transitionInType).FirstOrDefault().Prefab;
            currentTransitionObj = Object.Instantiate(prefab, canvasObj.transform);

            var animator = currentTransitionObj.GetComponent<Animator>();
            yield return new WaitUntil(() => 1 <= animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

            Object.Destroy(currentTransitionObj);

            InputActionsManager.Instance.UnlockAll();
        }

        public IEnumerator TransitionOut(TransitionOutType transitionInType = TransitionOutType.FadeOut)
        {
            InputActionsManager.Instance.LockAll();

            var prefab = transitionPrefabTable.TransitionOutPrefabList.Where(p => p.TransitionType == transitionInType).FirstOrDefault().Prefab;
            currentTransitionObj = Object.Instantiate(prefab, canvasObj.transform);

            var animator = currentTransitionObj.GetComponent<Animator>();
            yield return new WaitUntil(() => 1 <= animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

            InputActionsManager.Instance.UnlockAll();
        }
    }
}
