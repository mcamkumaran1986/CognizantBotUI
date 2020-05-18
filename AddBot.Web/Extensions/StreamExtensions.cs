using Newtonsoft.Json;
using System.IO;

namespace AddBot.Web
{
    public static class StreamExtensions
    {
        public static T Deserialize<T>(this Stream stream) where T : class
        {
            if (stream == null)
                return default;

            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return new JsonSerializer().Deserialize<T>(jsonReader);
            }
        }
    }
}
