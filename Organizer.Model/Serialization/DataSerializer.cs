using KPZ_Lab2.Models;
using System.Text.Json;

namespace KPZ_Lab2.Serialization
{
    public static class DataSerializer
    {

        public static void SerializeData(string fileName, DataModel data)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(fileName, json);
        }

        public static DataModel DeserializeData(string fileName)
        {
            var json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<DataModel>(json);
        }

    }
}
