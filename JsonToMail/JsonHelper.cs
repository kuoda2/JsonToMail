using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;

namespace JsonToMail
{
    public static class JsonHelper
    {
        private static string jsonSrc =
@"[{
    ""d"": ""2015-01-01T00:00:00Z"",
    ""n"": 32767,
    ""s"": ""darkthread"",
    ""a"": [ 1,2,3,4,5 ],
    ""t"": ""kerker""
},
{
    ""d"": ""2011-01-01T00:00:00Z"",
    ""n"": 111111,
    ""s"": ""eddie"",
    ""a"": [ 1,2,3,4,5 ],
    ""t"": ""kerker2""
}]";

        public static JArray ReadJson(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    var result = JsonConvert.DeserializeObject<JArray>(sr.ReadToEnd());
                    return result;
                }
            }
            else
                throw new FileNotFoundException(string.Format("{0} is not found.", path));
        }

        public static Config GetConfig(string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    var result = JsonConvert.DeserializeObject<Config>(sr.ReadToEnd());
                    return result;
                }
            }
            else
                throw new FileNotFoundException(string.Format("{0} is not found.", path));
        }

        public static JArray ConvertTo(JArray jarray, Config config)
        {
            JArray result = new JArray();
            foreach (JObject item in jarray)
            {
                var jo = new JObject();
                foreach (var kvp in config.ColumnMapping)
                {
                    string tmpValue = string.Empty;
                    foreach (var value in kvp.Value)
                    {
                        if (!regex.IsMatch(value) && item.TryGetValue(value, out JToken va))
                            tmpValue += item[value].ToString();
                        else
                            tmpValue += value;
                    }
                    jo.Add(kvp.Key, tmpValue);
                }
                result.Add(jo);
            }
            return result;
        }

        public static List<OfficeTool.MailInfo> GetMailInfos(JArray jarray, Config config)
        {
            var result = JsonConvert.DeserializeObject<List<OfficeTool.MailInfo>>(jarray.ToString());

            foreach (var mailInfo in result)
            {
                foreach (var kvp in config.EmailDictionary)
                {
                    if (mailInfo.CarbonCopy.Equals(kvp.Key, StringComparison.InvariantCultureIgnoreCase))
                        mailInfo.CarbonCopy = kvp.Value;
                }
            }
            return result;
        }

        private static Regex regex = new Regex("^【.*】$");
    }

    public class Config
    {
        public Dictionary<string, List<string>> ColumnMapping;
        public Dictionary<string, string> EmailDictionary;
    }
}