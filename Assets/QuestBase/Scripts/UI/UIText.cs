using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace QuestBase.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UIText : MonoBehaviour
    {
        private TextMeshProUGUI tmpro;

        private void Awake()
        {
            tmpro = GetComponent<TextMeshProUGUI>();
        }

        /// <summary>
        /// ラベルを指定して文字を表示
        /// </summary>
        /// <param name="textLabel"></param>
        public void SetText(LangTextLabel textLabel)
        {
            var text = LanguageUtil.GetText(textLabel, SystemLanguage.Japanese);
            tmpro.text = text;
        }

        /// <summary>
        /// ラベルを使わずに直接文字列を指定（非推奨）
        /// 言語対応が不要な場合に利用
        /// </summary>
        /// <param name="text"></param>
        public void SetText(string text)
        {
            tmpro.text = text;
        }
    }
}
