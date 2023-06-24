using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace QuestBase.UI
{
    public class LangTextEditorExtention
    {
        [MenuItem("QuestBase/Create LangTextLabel")]
        public static void CreateLangEnumFile()
        {
            var resultStr = "";
            resultStr += "public enum LangTextLabel\n{\n";

            //プログレスバー表示
            float progress = 0;
            EditorUtility.DisplayProgressBar("Creating", "Wait a moment please...", progress);

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
                var enumKey = string.Join("", labelSp);
                resultStr += $"    {enumKey},\n";

                lineCount++;
            }

            resultStr += "}";

            progress = 0.5f;
            EditorUtility.DisplayProgressBar("Creating", "Wait a moment please...", progress);

            var stream = new StreamWriter(UITextDefinitions.LangEnumFilePath);
            stream.WriteLine(resultStr);
            stream.Flush();
            stream.Close();

            AssetDatabase.Refresh();

            LanguageUtil.Init();

            progress = 0.8f;
            EditorUtility.DisplayProgressBar("Creating", "Wait a moment please...", progress);

            // チェック
            foreach (SystemLanguage lang in UITextDefinitions.AvailableLanguages)
            {
                foreach (LangTextLabel label in Enum.GetValues(typeof(LangTextLabel)))
                {
                    Debug.Log($"{lang} {label} : {LanguageUtil.GetText(label, lang)}");
                }
            }

            progress = 1f;
            EditorUtility.DisplayProgressBar("Creating", "Wait a moment please...", progress);
            EditorUtility.ClearProgressBar();
        }
    }
}
