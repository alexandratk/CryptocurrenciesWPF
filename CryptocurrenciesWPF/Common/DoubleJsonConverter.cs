using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptocurrenciesWPF.Common
{
    public class DoubleJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override bool CanRead => true;

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var strValue = reader.Value.ToString();
                
                if (strValue.Last() == '.') 
                {
                    strValue = strValue.Remove(strValue.Length - 1, 1);
                }

                strValue = strValue.Replace('.', ',');

                if (double.TryParse(strValue, out var parsedValue)) 
                {
                    return parsedValue;
                }

                return 0.0;
            }
            else
            {
                return 0.0;
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Not implemented yet");
        }
    }
}
