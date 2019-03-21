using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swmt.Objects;

namespace Swmt.Concretes.Helpers
{
    public class Settings
    {
        public static string GetJsonFileDataSourceFromEnvironmentVariable<T>(out string environmentVariableName) {
            environmentVariableName = string.Format(
                "{0}_{1}", 
                "SWMT_JSON_FILE_DATA_SOURCE", 
                typeof(T).Name.ToUpper()
            );
            return Environment.GetEnvironmentVariable(environmentVariableName);
        }
    }
}