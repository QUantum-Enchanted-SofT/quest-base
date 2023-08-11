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

        public static string GetText(LangTextLabel label, SystemLanguage language, params string[] textArguments)
        {
            if (!hasInit)
                Init();

            var text = langDict[language][label.ToString()];
            for (int i = 0; i < text.Length; i++)
            {
                if (textArguments.Length <= i)
                {
                }
                var replaceText = "{" + i + "}";
                if (text.Contains(replaceText))
                {
                    if (i < textArguments.Length)
                    {
                        text.Replace(replaceText, textArguments[i]);
                    }
                    else
                    {
                        Debug.LogError($"Text argument is not exist. {{{i}}}");
                    }
                }
                else
                {
                    if (i < textArguments.Length)
                    {
                        Debug.LogError($"Text argument is not specified. {{{i}}}");
                    }
                    break;
                }
            }

            return text;
        }

        public static LangTextLabel GetLangTextLabel(string label)
        {
            Enum.TryParse<LangTextLabel>(GetLangTextLabelStr(label), out var labelEnum);
            return labelEnum;
        }

        public static string GetLangTextLabelStr(string label)
        {
            var labelSp = label.Split('.');
            for (int i = 0; i < labelSp.Length; i++)
            {
                // 頭文字を大文字に
                var chars = labelSp[i].ToCharArray();
                var initial = char.ToUpper(chars[0]);
                chars[0] = initial;
                labelSp[i] = new string(chars);
            }
            var enumKey = string.Join("", labelSp);
            return enumKey;
        }
    }
}
