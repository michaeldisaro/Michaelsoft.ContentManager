namespace Michaelsoft.ContentManager.Common.HttpModels.Content
{
    public class CreateRequest
    {

        public string Type { get; set; }

        public string Locale { get; set; }

        public Content Content { get; set; }

    }
}