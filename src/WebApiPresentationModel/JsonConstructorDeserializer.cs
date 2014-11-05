namespace WebApiPresentationModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static class JsonConstructorDeserializer
    {
        private static readonly DeserializerContext Context = new DeserializerContext(
            new JsonObjectDeserializer(),
            new JsonValueDeserializer(),
            new JsonArrayDeserializer(),
            new JsonEnumerableDeserializer(),
            new JsonListDeserializer(),
            new JsonIListDeserializer(),
            new JsonCollectionDeserializer());

        private interface IDeserializer
        {
            object Deserialize(Type type, JToken token, DeserializerContext context);
        }

        public static object Deserialize(Type type, string json)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return Context.GetValue(
                type,
                JsonConvert.DeserializeObject<JToken>(json));
        }

        private class DeserializerContext
        {
            public static readonly object None = new object();
            private readonly IDeserializer[] deserializers;

            public DeserializerContext(params IDeserializer[] deserializers)
            {
                this.deserializers = deserializers;
            }

            public object GetValue(Type type, JToken token)
            {
                foreach (var deserializer in this.deserializers)
                {
                    var value = deserializer.Deserialize(type, token, this);
                    if (value != None)
                        return value;
                }

                throw new NotSupportedException(string.Format(
                    CultureInfo.CurrentCulture,
                    "There is no deserializer for the type '{0}'.",
                    type));
            }
        }

        private class JsonValueDeserializer : IDeserializer
        {
            public object Deserialize(Type type, JToken token, DeserializerContext context)
            {
                var val = token as JValue;
                if (val == null)
                    return DeserializerContext.None;

                if (!TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string)))
                    return null;

                return this.GetType().GetMethod("Value").MakeGenericMethod(type)
                    .Invoke(this, new object[] { val });
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "To support reflection")]
            public object Value<T>(JValue val)
            {
                return val.Value<T>();
            }
        }

        private class JsonArrayDeserializer : IDeserializer
        {
            public object Deserialize(Type type, JToken token, DeserializerContext context)
            {
                var arr = token as JArray;
                if (arr == null || !type.IsArray)
                    return DeserializerContext.None;

                var jarr = arr.ToArray();
                var elementType = type.GetElementType();
                var array = Array.CreateInstance(elementType, jarr.Length);

                for (int i = 0; i < jarr.Length; i++)
                    array.SetValue(context.GetValue(elementType, jarr[i]), i);

                return array;
            }
        }

        private class JsonEnumerableDeserializer : IDeserializer
        {
            public object Deserialize(Type type, JToken token, DeserializerContext context)
            {
                if (type.GetGenericTypeDefinition() != typeof(IEnumerable<>))
                    return DeserializerContext.None;

                var arrayType = type.GetGenericArguments()[0].MakeArrayType();
                return new JsonArrayDeserializer().Deserialize(arrayType, token, context);
            }
        }

        private class JsonListDeserializer : IDeserializer
        {
            public object Deserialize(Type type, JToken token, DeserializerContext context)
            {
                if (type.GetGenericTypeDefinition() != typeof(List<>))
                    return DeserializerContext.None;

                var elementType = type.GetGenericArguments()[0];
                var enumerableType = typeof(IEnumerable<>).MakeGenericType(elementType);
                var enumerable = new JsonEnumerableDeserializer()
                    .Deserialize(enumerableType, token, context);
                return type.GetConstructor(new Type[] { enumerableType })
                    .Invoke(new object[] { enumerable });
            }
        }

        private class JsonIListDeserializer : IDeserializer
        {
            public object Deserialize(Type type, JToken token, DeserializerContext context)
            {
                if (type.GetGenericTypeDefinition() != typeof(IList<>))
                    return DeserializerContext.None;

                var listType = typeof(List<>).MakeGenericType(type.GetGenericArguments()[0]);
                return new JsonListDeserializer().Deserialize(listType, token, context);
            }
        }

        private class JsonCollectionDeserializer : IDeserializer
        {
            public object Deserialize(Type type, JToken token, DeserializerContext context)
            {
                if (type.GetGenericTypeDefinition() != typeof(ICollection<>))
                    return DeserializerContext.None;

                var listType = typeof(List<>).MakeGenericType(type.GetGenericArguments()[0]);
                return new JsonListDeserializer().Deserialize(listType, token, context);
            }
        }

        private class JsonObjectDeserializer : IDeserializer
        {
            public object Deserialize(Type type, JToken token, DeserializerContext context)
            {
                var obj = token as JObject;
                if (obj == null)
                    return DeserializerContext.None;

                return new InnerJsonObjectDeserializer(
                    type, GetJsonObject(obj), context).Deserialize();
            }

            private static IDictionary<string, JToken> GetJsonObject(JObject obj)
            {
                var jsonObject = new Dictionary<string, JToken>();
                foreach (KeyValuePair<string, JToken> pair in obj)
                    jsonObject[pair.Key.ToUpper(CultureInfo.CurrentCulture)] = pair.Value;

                return jsonObject;
            }

            private class InnerJsonObjectDeserializer
            {
                private readonly Type type;
                private readonly IDictionary<string, JToken> jsonObject;
                private readonly DeserializerContext context;

                public InnerJsonObjectDeserializer(
                    Type type,
                    IDictionary<string, JToken> jsonObject,
                    DeserializerContext context)
                {
                    this.type = type;
                    this.jsonObject = jsonObject;
                    this.context = context;
                }

                public object Deserialize()
                {
                    var constructor = this.GetConstructor();
                    var arguments = this.GetArguments(constructor.GetParameters());
                    var result = constructor.Invoke(arguments.ToArray());
                    this.SetProperties(result);
                    return result;
                }

                private ConstructorInfo GetConstructor()
                {
                    var constructor = this.type.GetConstructors()
                        .OrderByDescending(c => c.GetParameters().Length)
                        .Where(c => c.GetParameters()
                            .Select(p => p.Name.ToUpper(CultureInfo.CurrentCulture))
                            .All(n => this.jsonObject.ContainsKey(n)))
                        .FirstOrDefault();

                    if (constructor == null)
                        throw new ArgumentException("There is no constructor matched with the given jason.");

                    return constructor;
                }

                private IEnumerable<object> GetArguments(ParameterInfo[] parameters)
                {
                    foreach (var parameter in parameters)
                    {
                        var key = parameter.Name.ToUpper();
                        var token = this.jsonObject[key];
                        this.jsonObject.Remove(key);

                        yield return this.context.GetValue(parameter.ParameterType, token);
                    }
                }

                private void SetProperties(object target)
                {
                    var properties = this.type.GetProperties()
                        .Where(x => x.GetGetMethod() != null && x.GetSetMethod() != null);

                    foreach (var property in properties)
                    {
                        JToken token;
                        if (!this.jsonObject.TryGetValue(
                            property.Name.ToUpper(CultureInfo.CurrentCulture), out token))
                            continue;

                        var value = this.context.GetValue(property.PropertyType, token);
                        property.SetValue(target, value);
                    }
                }
            }
        }
    }
}