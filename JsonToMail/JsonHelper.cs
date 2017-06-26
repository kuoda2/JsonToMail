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

        private static JArray ConvertTo(Config config)
        {
            return ConvertTo(ReadJson(config.SourcePath), config);
        }

        private static JArray ConvertTo(JArray jarray, Config config)
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
                        if (item.TryGetValue(value, out JToken va))
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

        public static IEnumerable<OfficeTool.MailInfo> GetMailInfos(Config config)
        {
            return GetMailInfos(ConvertTo(config), config);
        }

        public static IEnumerable<OfficeTool.MailInfo> GetMailInfos(string sourcePath, Config config)
        {
            return GetMailInfos(ConvertTo(ReadJson(sourcePath), config), config);
        }

        private static IEnumerable<OfficeTool.MailInfo> GetMailInfos(JArray jarray, Config config)
        {
            var result = JsonConvert.DeserializeObject<IEnumerable<OfficeTool.MailInfo>>(jarray.ToString());

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
    }

    public class Config
    {
        public Dictionary<string, List<string>> ColumnMapping;
        public Dictionary<string, string> EmailDictionary;
        public string SourcePath;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            #region 匯入資料來源的位置

            sb.AppendFormat("SourcePath:{0}", SourcePath).AppendLine();
            sb.AppendLine();

            #endregion 匯入資料來源的位置

            #region Email 對照表

            foreach (var emailKvp in EmailDictionary)
            {
                sb.AppendFormat("{0}:{1}", emailKvp.Key, emailKvp.Value).AppendLine();
            }
            sb.AppendLine();

            #endregion Email 對照表

            #region 欄位對照表

            foreach (var kvp in ColumnMapping)
            {
                sb.AppendLine(kvp.Key);
                foreach (var value in kvp.Value)
                {
                    sb.Append("\t");
                    sb.AppendLine(value);
                }
            }
            sb.AppendLine();

            #endregion 欄位對照表

            return sb.ToString();
        }
    }
}