namespace WebApiPresentationModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.IO;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static class JsonConstructorFormatter
    {
        public static object Read(Type type, string json)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (json == null)
                throw new ArgumentNullException("json");

            var dic = GetJsonDictionary(json);
            var constructor = GetConstructor(type.GetConstructors(), dic);
            var arguments = GetArguments(constructor.GetParameters(), dic);
            var result = constructor.Invoke(arguments.ToArray());
            SetProperties(result, type.GetProperties(), dic);
            return result;
        }

        private static IDictionary<string, string> GetJsonDictionary(string json)
        {
            var jsonObject = JsonConvert.DeserializeObject<JObject>(json);
            var dic = new Dictionary<string, string>();

            foreach (KeyValuePair<string, JToken> keyValuePair in jsonObject)
                dic[keyValuePair.Key.ToUpper()] = keyValuePair.Value.Value<string>();

            return dic;
        }

        private static ConstructorInfo GetConstructor(
            ConstructorInfo[] constructors, IDictionary<string, string> jsonObject)
        {
            return constructors
                .OrderByDescending(c => c.GetParameters().Length)
                .Where(c => c.GetParameters()
                    .Select(p => p.Name.ToUpper())
                    .All(n => jsonObject.ContainsKey(n)))
                .First();
        }

        private static IEnumerable<object> GetArguments(
            ParameterInfo[] parameters, IDictionary<string, string> dic)
        {
            return parameters.Select(
                p =>
                {
                    var key = p.Name.ToUpper();
                    var valueString = dic[key];
                    dic.Remove(key);

                    return TypeDescriptor.GetConverter(p.ParameterType)
                        .ConvertFromString(valueString);
                });
        }

        private static void SetProperties(
            object result, PropertyInfo[] properties, IDictionary<string, string> dic)
        {
            foreach (var property in properties
                .Where(x => x.GetGetMethod() != null && x.GetSetMethod() != null))
            {
                string valueString;
                if (!dic.TryGetValue(property.Name.ToUpper(), out valueString))
                    continue;

                var value = TypeDescriptor.GetConverter(property.PropertyType)
                    .ConvertFromString(valueString);

                property.SetValue(result, value);
            }
        }
    }
}