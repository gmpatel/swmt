using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Swmt.Objects;

namespace Swmt.Extras.Converters
{
    public class GenderTypeEnumConverter : StringEnumConverter
    {
        public Gender DefaultValue { get; set; }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (JsonSerializationException)
            {
                return DefaultValue;
            }
        }
    }
}