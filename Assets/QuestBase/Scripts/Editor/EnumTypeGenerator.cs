using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System;
using QuestBase.UI;
using QuestBase.QuestObjectView;

namespace QuestBase.Editor
{
    public class EnumTypeGenerator
    {
        private static void CreateEnumTypeFile<T>(string outputFilePath, string className, string namespaceName)
        {
            var resultStr = "";
            resultStr += $"namespace {namespaceName}\n{{\n";
            resultStr += $"    public enum {className}\n{{\n";

            float progress = 0;
            EditorUtility.DisplayProgressBar("変換中...", "チョットマッテネ", progress);

            var windowTypes =
                System.Reflection.Assembly.GetAssembly(typeof(T))
                .GetTypes()
                .Where(t => typeof(T).IsAssignableFrom(t) && !t.IsAbstract)
                .ToList();

            foreach (var windowType in windowTypes)
            {
                var windowName = windowType.Name;
                var windowNameBytes = Encoding.UTF8.GetBytes(windowName);
                var md5Bytes = MD5.Create().ComputeHash(windowNameBytes);
                var md5Int = BitConverter.ToInt32(md5Bytes, 0);

                resultStr += $"        {windowType.Name} = {md5Int},\n";
                progress += windowTypes.Count;
                EditorUtility.DisplayProgressBar("変換中...", "チョットマッテネ", progress);
            }

            resultStr += "    }\n";
            resultStr += "}\n";
            Debug.Log(resultStr);

            var stream = new StreamWriter(outputFilePath, false, Encoding.UTF8);
            stream.Write(resultStr);
            stream.Flush();
            stream.Close();

            AssetDatabase.Refresh();

            EditorUtility.ClearProgressBar();
        }

        [MenuItem("QuestBase/Create UIWindowType")]
        public static void CreateUIWindowTypeEnumFile()
        {
            CreateEnumTypeFile<UIWindowBase>(UIWindowDefinitions.WinodwTypeFilePath, "UIWindowType", "QuestBase.UI");
        }

        [MenuItem("QuestBase/Create QuestObjectType")]
        public static void CreateQuestObjectTypeEnumFile()
        {
            CreateEnumTypeFile<QuestObjectViewBase>(QuestObjectDefinitions.QuestObjectViewTypeFilePath, "QuestObjectViewType", "QuestBase.QuestObjectView");
        }
    }
}
