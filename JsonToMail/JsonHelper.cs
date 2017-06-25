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

        public static JArray ConvertTo(JArray jarray, string configPath)
        {
            using (StreamReader r = File.OpenText(configPath))
            {
                var jsonText = r.ReadToEnd();
                Dictionary<string, List<string>> kvps = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(jsonText);

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
        }

        public static List<OfficeTool.MailInfo> GetMailInfos(JArray jarray)
        {
            var result = JsonConvert.DeserializeObject<List<OfficeTool.MailInfo>>(jarray.ToString());

            foreach (var mailInfo in result)
            {
                foreach (var kvp in MailTable)
                {
                    if (mailInfo.CarbonCopy.Equals(kvp.Key, StringComparison.InvariantCultureIgnoreCase))
                        mailInfo.CarbonCopy = kvp.Value;
                }
            }
            return result;
        }

        private static Dictionary<string, string> MailTable
        {
            get
            {
                if (mailTable.Count == 0)
                {
                    mailTable.Add("Eddie", "eddie_kuo@systemweb.com");
                    mailTable.Add("Frank", "frank_wu@systemweb.com");
                }
                return mailTable;
            }
        }

        private static Regex regex = new Regex("^【.*】$");
        private static Dictionary<string, string> mailTable = new Dictionary<string, string>() { };
    }
}