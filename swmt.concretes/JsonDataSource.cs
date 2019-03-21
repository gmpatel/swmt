using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swmt.Concretes.Helpers;
using Swmt.Extras.Extensions;
using Swmt.Interfaces;

namespace Swmt.Concretes
{
    public class JsonDataSource<T> : IDataSource<T>
    {
        private static string dataSourcePath;

        private readonly object readLock = new object();
        
        public T FirstOrDefault(string key, dynamic val) 
        {
            lock (readLock) {
                using (var s = File.Open(GetSource(), FileMode.Open, FileAccess.Read))
                    using (var sr = new StreamReader(s))
                        using (var reader = new JsonTextReader(sr))
                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonToken.StartObject)
                                {
                                    var result = GetObject(key, val, reader);
                                    if (result != null)
                                        return result;
                                }
                            }   
                return default(T);
            }
        }

        public IList<T> Find(string key, dynamic val) 
        {
            lock (readLock) {
                var data = new List<T>();
                using (var s = File.Open(GetSource(), FileMode.Open, FileAccess.Read))
                    using (var sr = new StreamReader(s))
                        using (var reader = new JsonTextReader(sr))
                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonToken.StartObject)
                                {
                                    var result = GetObject(key, val, reader);
                                    if (result != null)
                                        data.Add(result);
                                }
                            }    
                return data.Count <= 0 ? default(IList<T>) : data;
            }
        }

        public IList<T> All()
        {
            lock (readLock) {
                var data = new List<T>();
                using (var s = File.Open(GetSource(), FileMode.Open, FileAccess.Read)) 
                    using (var sr = new StreamReader(s))
                        using (var reader = new JsonTextReader(sr))
                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonToken.StartObject)
                                {
                                    var result = GetObject(reader);
                                    if (result != null)
                                        data.Add(result);
                                }
                            }    
                return data.Count <= 0 ? default(IList<T>) : data;
            }
        }

        public IDictionary<string, IList<T>> All(string key)
        {
            lock (readLock){
                var data = new SortedDictionary<string, IList<T>>();
                using (var s = File.Open(GetSource(), FileMode.Open, FileAccess.Read))
                    using (var sr = new StreamReader(s))
                        using (var reader = new JsonTextReader(sr))
                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonToken.StartObject)
                                {
                                    var result = GetObject(reader);
                                    if (result != null)
                                    {
                                        var propValue = result?.GetType()?
                                            .GetProperty(key)?
                                            .GetValue(result, null)?.ToString();

                                        if (!data.ContainsKey(propValue)) 
                                            data[propValue] = new List<T>();
                                        
                                        data[propValue].Add(result);
                                    }
                                }
                            }    
                return data.Count <= 0 ? default(IDictionary<string, IList<T>>) : data;
            }
        }

        private T GetObject(string key, dynamic val, JsonTextReader reader) 
        {
            var result = GetObject(reader);                
            try {
                var propValue = result?.GetType()?
                    .GetProperty(key)?
                    .GetValue(result, null);

                if (propValue != null && propValue.Equals(val))
                    return result;
            }
            catch (Exception) {
                // Do ... # Just skipping the record if error in reflection
            }
            return default(T);
        }

        private T GetObject(JsonTextReader reader) {
            try {
                var result = JObject.Load(reader).DeserializeObject<T>();
                return result;    
            }
            catch {
                // Do ... # Just skipping the record if error in serialization 
            }
            return default(T);
        }
        private static string GetSource() {
            if (dataSourcePath != null) 
                return dataSourcePath;

            var environmentVariableName = default(string);
            var fileSource = Settings.GetJsonFileDataSourceFromEnvironmentVariable<T>(out environmentVariableName);

            if (fileSource == null) 
            {
                var typeName = typeof(T).Name;
                throw new NullReferenceException(
                    string.Format("JSON file data source name/path for the 'JsonDataSource<{0}>' component has not been set and is null.", typeName),
                    new ArgumentNullException(
                        string.Format("Set environment variable '{0}' with the correct JSON file data source name/path for the 'JsonDataSource<{1}>' component. Current application path is '{2}'.", environmentVariableName, typeName, Directory.GetCurrentDirectory())                        
                    )
                );
            }

            if (!File.Exists(fileSource)) {
                var typeName = typeof(T).Name;
                throw new FileNotFoundException(
                    string.Format("The JSON file data source name/path set for the 'JsonDataSource<{0}>' component is invalid. The JSON file '{1}' not found!", typeName, fileSource),
                    new ArgumentOutOfRangeException(
                        string.Format("The environment variable '{0}' is set with the incorrect value '{1}' for the 'JsonDataSource<{2}>' component. Current application path is '{3}'.", environmentVariableName, fileSource, typeName, Directory.GetCurrentDirectory())                        
                    )
                );
            }

            dataSourcePath = fileSource;
            return fileSource;
        }
    }
}
