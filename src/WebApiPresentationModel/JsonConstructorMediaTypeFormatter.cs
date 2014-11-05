namespace WebApiPresentationModel
{
    using System.Net.Http.Formatting;

    public class JsonConstructorMediaTypeFormatter : MediaTypeFormatter
    {
        public override bool CanReadType(System.Type type)
        {
            throw new System.NotImplementedException();
        }

        public override bool CanWriteType(System.Type type)
        {
            throw new System.NotImplementedException();
        }
    }
}