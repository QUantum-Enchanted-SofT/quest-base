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

namespace QuestBase.Editor
{
    public class UIWindowTypeGenerator
    {
        [MenuItem("QuestBase/Create UIWindowType")]
        public static void CreateUIWindowTypeEnumFile()
        {
            var resultStr = "";
            resultStr += "public enum UIWindowType\n{\n";

            float progress = 0;
            EditorUtility.DisplayProgressBar("変換中...", "チョットマッテネ", progress);

            var windowTypes =
                System.Reflection.Assembly.GetAssembly(typeof(UIWindowBase))
                .GetTypes()
                .Where(t => typeof(UIWindowBase).IsAssignableFrom(t) && !t.IsAbstract)
                .ToList();

            foreach (var windowType in windowTypes)
            {
                var windowName = windowType.Name;
                var windowNameBytes = Encoding.UTF8.GetBytes(windowName);
                var md5Bytes = MD5.Create().ComputeHash(windowNameBytes);
                var md5Int = BitConverter.ToInt32(md5Bytes, 0);

                resultStr += $"     {windowType.Name} = {md5Int},\n";
                progress += windowTypes.Count;
                EditorUtility.DisplayProgressBar("変換中...", "チョットマッテネ", progress);
            }

            resultStr += "}";
            Debug.Log(resultStr);

            var stream = new StreamWriter(UIWindowDefinitions.WinodwTypeFilePath, false, Encoding.UTF8);
            stream.Write(resultStr);
            stream.Flush();
            stream.Close();

            AssetDatabase.Refresh();

            EditorUtility.ClearProgressBar();
        }
    }
}
