namespace WebApiPresentationModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static class JsonConstructorDeserializer
    {
        public static object Deserialize(Type type, string json)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return new JsonConstructorDeserializerImplementor(
                type, GetJsonDictionary(json)).Read();
        }

        private static IDictionary<string, JToken> GetJsonDictionary(string json)
        {
            var jsonObject = new Dictionary<string, JToken>();

            foreach (KeyValuePair<string, JToken> keyValuePair
                in JsonConvert.DeserializeObject<JObject>(json))
                jsonObject[keyValuePair.Key.ToUpper()] = keyValuePair.Value;

            return jsonObject;
        }

        private class JsonConstructorDeserializerImplementor
        {
            private readonly Type type;
            private readonly IDictionary<string, JToken> jsonObject;
            
            public JsonConstructorDeserializerImplementor(
                Type type, IDictionary<string, JToken> jsonObject)
            {
                this.type = type;
                this.jsonObject = jsonObject;
            }

            public object Read()
            {
                var constructor = this.GetConstructor();
                var arguments = this.GetArguments(constructor.GetParameters());
                var result = constructor.Invoke(arguments.ToArray());
                this.SetProperties(result);
                return result;
            }

            private static object GetValue(Type type, JToken token)
            {
                var converter = TypeDescriptor.GetConverter(type);
                return converter.CanConvertFrom(typeof(string))
                    ? converter.ConvertFromString(token.Value<string>())
                    : null;
            }

            private ConstructorInfo GetConstructor()
            {
                return this.type.GetConstructors()
                    .OrderByDescending(c => c.GetParameters().Length)
                    .Where(c => c.GetParameters()
                        .Select(p => p.Name.ToUpper())
                        .All(n => this.jsonObject.ContainsKey(n)))
                    .First();
            }

            private IEnumerable<object> GetArguments(ParameterInfo[] parameters)
            {
                foreach (var parameter in parameters)
                {
                    var key = parameter.Name.ToUpper();
                    var token = this.jsonObject[key];
                    this.jsonObject.Remove(key);

                    yield return GetValue(parameter.ParameterType, token);
                }
            }

            private void SetProperties(object result)
            {
                foreach (var property in this.type.GetProperties()
                    .Where(x => x.GetGetMethod() != null && x.GetSetMethod() != null))
                {
                    JToken token;
                    if (!this.jsonObject.TryGetValue(property.Name.ToUpper(), out token))
                        continue;

                    var value = GetValue(property.PropertyType, token);
                    property.SetValue(result, value);
                }
            }
        }
    }
}