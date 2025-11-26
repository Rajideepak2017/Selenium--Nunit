using DotNetEnv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHSBloodTest.Utilities
{
    public class EnvLoader
    {
        public static void LoadSecureEnv()
        {
            string projectRoot = AppDomain.CurrentDomain.BaseDirectory;
            string envPath = Path.Combine(projectRoot, "..", "..", "..", "Secure.env");
            string fullEnvPath = Path.GetFullPath(envPath);

            if (File.Exists(fullEnvPath))
            {
                Env.Load(fullEnvPath); 
            }
            else
            {
                Console.WriteLine("Secure.env file not found! Check path.");
            }
        }

        public static string GetEnvironmentVariable(string variableName)
        {
            string value = Environment.GetEnvironmentVariable(variableName);
            if (value == null)
            {
                throw new InvalidOperationException($"Environment variable '{variableName}' is not set.");
            }
            return value;
        }
    }
}
