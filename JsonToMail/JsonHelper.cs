using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text.RegularExpressions;

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

        public static JArray ConvertTo(JArray jarray, string configPath)
        {
            List<KeyValuePair<string, List<string>>> kvps = new List<KeyValuePair<string, List<string>>>();

            kvps.Add(new KeyValuePair<string, List<string>>("Subject", new List<string>() { "#", "【所以我說】", "問題類別" }));
            kvps.Add(new KeyValuePair<string, List<string>>("Recipient", new List<string>() { "" }));
            kvps.Add(new KeyValuePair<string, List<string>>("CarbonCopy", new List<string>() { "負責人" }));
            kvps.Add(new KeyValuePair<string, List<string>>("Content", new List<string>() { "問題描述", "解決歷程" }));
            JArray result = new JArray();
            foreach (JObject item in jarray)
            {
                var jo = new JObject();
                foreach (var kvp in kvps)
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

        public static List<OfficeTool.MailInfo> GetMailInfos(JArray jarray)
        {
            var result = JsonConvert.DeserializeObject<List<OfficeTool.MailInfo>>(jarray.ToString());
            return result;
        }

        private static Regex regex = new Regex("^【.*】$");
    }
}