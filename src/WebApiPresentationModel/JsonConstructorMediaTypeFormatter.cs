namespace WebApiPresentationModel
{
    using System;
    using System.ComponentModel;
    using System.Net.Http.Formatting;

    public class JsonConstructorMediaTypeFormatter : MediaTypeFormatter
    {
        public override bool CanReadType(Type type)
        {
            return !TypeHelper.CanConvertFromString(type);
        }

        public override bool CanWriteType(Type type)
        {
            return false;
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