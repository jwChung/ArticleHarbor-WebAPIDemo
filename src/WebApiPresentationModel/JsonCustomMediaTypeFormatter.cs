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

        public override async Task<object> ReadFromStreamAsync(
            Type type,
            Stream readStream,
            HttpContent content,
            IFormatterLogger formatterLogger)
        {
            if (readStream == null)
                throw new ArgumentNullException("content");

            if (content == null)
                throw new ArgumentNullException("content");

            byte[] buffer = new byte[Math.Min(content.Headers.ContentLength.Value, 256)];
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