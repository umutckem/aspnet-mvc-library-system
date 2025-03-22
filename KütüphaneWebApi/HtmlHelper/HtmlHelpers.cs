using Microsoft.AspNetCore.Mvc.Rendering;

namespace YourNamespace.Helpers
{
    public static class HtmlHelpers
    {
        public static string CustomAboutSection(this IHtmlHelper htmlHelper, string title, string content)
        {
            return $@"
                <div class='about-section'>
                    <h2>{title}</h2>
                    <p>{content}</p>
                </div>";
        }
    }
}
