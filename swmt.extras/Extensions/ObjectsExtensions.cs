using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swmt.Extras.Converters;
using Swmt.Objects;

namespace Swmt.Extras.Extensions
{
    public static class ObjectsExtensions 
    {
        public static T DeserializeObject<T>(this JObject obj) 
        {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new GenderTypeEnumConverter { DefaultValue = Gender.Unknown });
            return JsonConvert.DeserializeObject<T>(obj.ToString(), settings);            
        }

        public static string GetFullName(this Person person) 
        {
            return string.Format("{0} {1}", person.First, person.Last);
        }
    }
}