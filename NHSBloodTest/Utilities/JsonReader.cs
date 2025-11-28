using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SeleniumProject.Utilities
{
    public class JsonReader
    {
        public List<string> dataextract(string tokenname)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string folderPath = Path.Combine(basePath, "TestData");

            if (!Directory.Exists(folderPath))
                throw new FileNotFoundException($"Test data folder not found at: {folderPath}");

            string[] ListFiles = Directory.GetFiles(folderPath, "*.json");

            if (ListFiles == null || ListFiles.Length == 0)
                throw new Exception($"No JSON files found in: {folderPath}");

            List<string> tokenvalues = new List<string>();

            foreach (string filePath in ListFiles)
            {
                string jsonContent = File.ReadAllText(filePath);
                var jsonObject = JToken.Parse(jsonContent);
                var token = jsonObject.SelectToken(tokenname);

                if (token != null)
                {
                    tokenvalues.Add(token.Value<string>());
                }
            }

            if (tokenvalues.Count == 0)
                throw new Exception($"Token '{tokenname}' not found in any JSON file in: {folderPath}");

            return tokenvalues;
        }

        public string Getvalue(string tokenname)
        {
            var Values = dataextract(tokenname);
            var Value = Values.FirstOrDefault();

            if (string.IsNullOrEmpty(Value))
                throw new Exception($"'{tokenname}' is not found inside the JSON files");

            return Value;
        }
    }
}
