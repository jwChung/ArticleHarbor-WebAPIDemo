namespace WebApiPresentationModel
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading.Tasks;

    public class JsonCustomMediaTypeFormatter : JsonMediaTypeFormatter
    {
        private readonly Func<Type, string, object> deserializer;

        public JsonCustomMediaTypeFormatter(Func<Type, string, object> deserializer)
        {
            if (deserializer == null)
                throw new ArgumentNullException("deserializer");

            this.deserializer = deserializer;
        }

        public Func<Type, string, object> Deserializer
        {
            get { return this.deserializer; }
        }

        public override bool CanReadType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return !TypeHelper.CanConvertFromString(type);
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override Task<object> ReadFromStreamAsync(
            Type type,
            Stream readStream,
            HttpContent content,
            IFormatterLogger formatterLogger)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (readStream == null)
                throw new ArgumentNullException("content");

            if (content == null)
                throw new ArgumentNullException("content");

            if (formatterLogger == null)
                throw new ArgumentNullException("formatterLogger");

            return this.ReadFromStreamAsyncImpl(type, readStream, content);
        }

        private async Task<object> ReadFromStreamAsyncImpl(
            Type type, Stream readStream, HttpContent content)
        {
            using (var reader = new StreamReader(
                readStream, this.SelectCharacterEncoding(content.Headers)))
            {
                string jsonString = await reader.ReadToEndAsync();
                return this.deserializer(type, jsonString);
            }
        }

        private static class TypeHelper
        {
            public static bool CanConvertFromString(Type type)
            {
                return IsSimpleUnderlyingType(type) ||
                    HasStringConverter(type);
            }

            private static bool IsSimpleUnderlyingType(Type type)
            {
                Type underlyingType = Nullable.GetUnderlyingType(type);
                if (underlyingType != null)
                {
                    type = underlyingType;
                }

                return IsSimpleType(type);
            }

            private static bool IsSimpleType(Type type)
            {
                return type.IsPrimitive ||
                    type.Equals(typeof(string)) ||
                    type.Equals(typeof(DateTime)) ||
                    type.Equals(typeof(decimal)) ||
                    type.Equals(typeof(Guid)) ||
                    type.Equals(typeof(DateTimeOffset)) ||
                    type.Equals(typeof(TimeSpan));
            }

            private static bool HasStringConverter(Type type)
            {
                return TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string));
            }
        }
    }
}