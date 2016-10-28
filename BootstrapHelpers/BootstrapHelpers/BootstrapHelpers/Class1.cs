using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BSMVC
{
    public static class BSHelpers
    {
        public static MvcHtmlString BSTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var value = metadata.Model;
            
            TagBuilder TextBoxTag = new TagBuilder("input");
            TextBoxTag.Attributes.Add("name", fieldName);
            TextBoxTag.Attributes.Add("id", fieldId);
            TextBoxTag.Attributes.Add("class", "form-control");
            TextBoxTag.Attributes.Add("value", value == null ? "" : value.ToString());

            var validationAttributes = helper.GetUnobtrusiveValidationAttributes(fullBindingName, metadata);
            
            foreach (var key in validationAttributes.Keys)
            {
                TextBoxTag.Attributes.Add(key, validationAttributes[key].ToString());
            }

            return new MvcHtmlString(TextBoxTag.ToString(TagRenderMode.SelfClosing));
        }

    }
}
