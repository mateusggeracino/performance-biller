using System.IO;
using Newtonsoft.Json;
using PerformanceBiller.Models;

namespace PerformanceBiller.Repository
{
    public class JsonReader : IJsonReader
    {

        public T GetData<T>(string path)
        {
            var jsonText = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(jsonText);
        }

        public Play GetPlayById(string playId)
        {
            throw new System.NotImplementedException();
        }
    }
}