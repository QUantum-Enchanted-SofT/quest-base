using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestBase
{
    public class GlobalCoroutine
    {
        private static MonoBehaviour mb = null;
        private static Dictionary<string, Coroutine> labeledCoroutines = new Dictionary<string, Coroutine>();

        private static void CreateMB()
        {
            var obj = new GameObject("Global MonoBehaviour");
            GameObject.DontDestroyOnLoad(obj);
            mb = obj.AddComponent<GlobalMonoBehaviour>();
        }

        public static Coroutine StartCoroutine(IEnumerator routine)
        {
            if (!mb)
            {
                CreateMB();
            }

            return mb.StartCoroutine(routine);
        }

        public static void StopCoroutine(Coroutine routine)
        {
            mb.StopCoroutine(routine);
        }

        /// <summary>
        /// ラベル指定でコルーチンを実行（既に実行中の物があれば停止後に実行される）
        /// </summary>
        /// <param name="routine"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static Coroutine StartCoroutine(IEnumerator routine, string label)
        {
            if (labeledCoroutines.TryGetValue(label, out var prevCoroutine))
            {
                mb.StopCoroutine(prevCoroutine);
            }

            var coroutine = StartCoroutine(routine);
            labeledCoroutines[label] = coroutine;
            return coroutine;
        }
    }
}
