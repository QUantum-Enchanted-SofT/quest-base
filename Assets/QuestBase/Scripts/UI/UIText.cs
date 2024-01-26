using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace QuestBase.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UIText : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI tmpro;
        public TextMeshProUGUI TMPText => tmpro;

        private void Awake()
        {
            if (!tmpro)
            {
                tmpro = GetComponent<TextMeshProUGUI>();
            }
        }

        /// <summary>
        /// ラベルを指定して文字を表示
        /// </summary>
        /// <param name="textLabel"></param>
        public void SetText(LangTextLabel textLabel, params string[] textArguments)
        {
            var text = LanguageUtil.GetText(textLabel, SystemLanguage.Japanese, textArguments);
            tmpro.text = text;
        }

        /// <summary>
        /// ラベルを使わずに直接文字列を指定（非推奨）
        /// 言語対応が不要な場合に利用
        /// </summary>
        /// <param name="text"></param>
        public void SetTextDirectly(string text)
        {
            tmpro.text = text;
        }
    }
}
