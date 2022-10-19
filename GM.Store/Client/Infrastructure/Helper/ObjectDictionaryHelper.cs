using System.Collections.Generic;
using System.Text.Json;

namespace GM.Store.Client.Infrastructure.Helper
{
    public class ObjectDictionaryHelper
    {

        public Dictionary<string, object> ReadObject(JsonElement element, JsonSerializerOptions options)
        {
            var result = new Dictionary<string, object>();
            foreach (var property in element.EnumerateObject())
            {
                var name = property.Name;
                var value = property.Value;
                var convertedValue = ReadValue(value, options);
                result[name] = convertedValue;
            }

            return result;
        }
        public List<Dictionary<string, object>> ReadArray(JsonElement element, JsonSerializerOptions options)
        {
            var result = new List<Dictionary<string, object>>();
            if (element.ValueKind == JsonValueKind.Array)
            {
                foreach (var arrayElement in element.EnumerateArray())
                { 
                    var v = arrayElement;
                    var arrayValue = ReadObject(v, options);
                    result.Add(arrayValue);
                }
            }

            return result;
        }

        public object ReadValue(JsonElement element, JsonSerializerOptions options)
        {
            if (element.ValueKind == JsonValueKind.Null)
                return null;

            if (element.ValueKind == JsonValueKind.Object) return ReadObject(element, options);

            if (element.ValueKind == JsonValueKind.Array)
            {
                var items = new List<object>();
                foreach (var arrayElement in element.EnumerateArray())
                {
                    var v = arrayElement;
                    var arrayValue = ReadValue(v, options);
                    items.Add(arrayValue);
                }

                return items;
            }

            object value;
            switch (element.ValueKind)
            {
                case JsonValueKind.True:
                    value = true;
                    break;
                case JsonValueKind.False:
                    value = false;
                    break;
                case JsonValueKind.Number:
                    {
                        if (element.TryGetInt32(out var intValue))
                            value = intValue;
                        else if (element.TryGetInt64(out var longValue))
                            value = longValue;
                        else
                            value = element.GetDouble();

                        break;
                    }
                case JsonValueKind.String:
                    value = element.GetString();
                    break;
                default:
                    value = element.GetRawText();
                    break;
            }

            return value;
        }
    }
}
