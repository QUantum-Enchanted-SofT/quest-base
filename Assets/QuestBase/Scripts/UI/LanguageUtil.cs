using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QuestBase.UI
{
    public static class LanguageUtil
    {
        private static bool hasInit = false;
        private static Dictionary<SystemLanguage, Dictionary<string, string>> langDict = null;

        public static void Init()
        {
            langDict = new Dictionary<SystemLanguage, Dictionary<string, string>>();
            foreach (SystemLanguage lang in UITextDefinitions.AvailableLanguages)
            {
                langDict.Add(lang, new Dictionary<string, string>());
            }

            var langTextAsset = Resources.Load<TextAsset>(UITextDefinitions.LangTextCsvPath);
            var reader = new StringReader(langTextAsset.text);
            var lineCount = 0;
            while (reader.Peek() != -1)
            {
                var line = reader.ReadLine().Split(',');
                if (lineCount == 0)
                {
                    // ヘッダーは読まない
                    lineCount++;
                    continue;
                }

                var label = line[0];
                var labelSp = label.Split('.');
                for (int i = 0; i < labelSp.Length; i++)
                {
                    // 頭文字を大文字に
                    var chars = labelSp[i].ToCharArray();
                    var initial = char.ToUpper(chars[0]);
                    chars[0] = initial;
                    labelSp[i] = new string(chars);
                }
                var langTextEnumKey = string.Join("", labelSp);

                for (int i = 0; i < UITextDefinitions.AvailableLanguages.Length; i++)
                {
                    var text = line[i + 1];
                    langDict[UITextDefinitions.AvailableLanguages[i]].Add(langTextEnumKey, text);
                }

                lineCount++;
            }

            hasInit = true;
        }

        public static string GetText(LangTextLabel label, SystemLanguage language)
        {
            if (!hasInit)
                Init();

            return langDict[language][label.ToString()];
        }
    }
}
