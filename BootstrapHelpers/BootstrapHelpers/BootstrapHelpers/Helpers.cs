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
        /// <summary>
        /// Extension Method For Bootstrap 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="helper">The type we extend</param>
        /// <param name="expression">lamda expression eg model=>model.Username</param>
        /// <param name="cssClass">By default is null which means that applies the default boootstrap rendering, if provided it also takes account a custom css class</param>
        /// <returns></returns>
        /// 
        public static MvcHtmlString BSTextBoxFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string cssClass=null)
        {
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var value = metadata.Model;
            
            string bsCssClass ="form-control";

            TagBuilder TextBoxTag = new TagBuilder("input");
            TextBoxTag.Attributes.Add("name", fieldName);
            TextBoxTag.Attributes.Add("id", fieldId);
            TextBoxTag.Attributes.Add("class", cssClass == null ? bsCssClass : String.Format("{0} {1}",bsCssClass,cssClass));
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
