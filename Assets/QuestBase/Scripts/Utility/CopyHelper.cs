using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuestBase.Utility
{
    public static class CopyHelper
    {
        /// <summary>
        /// DeepCopy
        /// </summary>
        public static T DeepCopy<T>(this T src)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, src);
                stream.Position = 0;

                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
