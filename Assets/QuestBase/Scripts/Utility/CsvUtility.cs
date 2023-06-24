namespace QuestBase.Utility
{
    public static class CsvUtility
    {
        public static T[] FromCsv<T>(string csvText)
        {
            return CSVSerializer.Deserialize<T>(csvText);
        }
    }
}
