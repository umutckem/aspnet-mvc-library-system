using Microsoft.AspNetCore.Razor.TagHelpers;

namespace YourNamespace.TagHelpers
{
    public class BilgiKartTagHelper : TagHelper
    {
        public string Baslik { get; set; }
        public string Icerik { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div"; // Render edilen HTML elementi
            output.Attributes.SetAttribute("class", "bilgi-kart"); // CSS sınıfı ekleniyor

            output.Content.SetHtmlContent($@"
                <h2>{Baslik}</h2>
                <p>{Icerik}</p>
            ");
        }
    }
}
