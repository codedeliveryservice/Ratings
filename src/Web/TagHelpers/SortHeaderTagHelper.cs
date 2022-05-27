using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Web.Models.Enums;

namespace Web.TagHelpers;

public class SortHeaderTagHelper : TagHelper
{
    private readonly IUrlHelperFactory _urlHelperFactory;

    public SortHeaderTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    public SortState Property { get; set; }
    public SortState Current { get; set; }
    public bool Ascending { get; set; }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; } = null!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        const string action = "Index";

        bool ascending = Ascending;

        // NOTE: Set the default sort for numeric properties to descending; otherwise ascending
        if (Current != Property)
        {
            var propertyIsNumber = Property is SortState.Classical or SortState.Rapid or SortState.Blitz;
            if (propertyIsNumber)
            {
                ascending = false;
            }
            else
            {
                ascending = true;
            }
        }

        var factory = _urlHelperFactory.GetUrlHelper(ViewContext);
        var url = factory.Action(action, new { SortBy = Property, Ascending = ascending });

        output.TagName = "a";
        output.Attributes.SetAttribute("href", url);
        output.Attributes.SetAttribute("class", "position-relative");

        if (Current == Property)
        {
            var tag = new TagBuilder("span");
            tag.AddCssClass("material-symbols-outlined");

            // NOTE: Alternatively, extract to a separate class in stylesheets
            tag.Attributes.Add("style", "position: absolute; left: -23px; top: 0px;");

            if (ascending)
            {
                tag.InnerHtml.AppendHtml("expand_more");
            }
            else
            {
                tag.InnerHtml.AppendHtml("expand_less");
            }

            output.PreContent.AppendHtml(tag);
        }
    }
}
