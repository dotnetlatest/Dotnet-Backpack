using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Backpack.Web.Mvc.Extensions
{
    public static class HtmlExtensions
    {
        public static string IdFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            string exp = ExpressionHelper.GetExpressionText(expression);
            return TagBuilder.CreateSanitizedId(html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(exp));
        }

        public static string TextFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression<TModel, TValue>(expression, html.ViewData);
            string str = metadata.DisplayName ?? metadata.PropertyName;
            return str;
        }

        public static IHtmlString LabelFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, LabelTemplate template)
        {
            var desc = new FieldDescription
            {
                ID = html.IdFor(expression),
                Name = html.TextFor(expression)
            };

            string s = template(desc).ToHtmlString();
            return new HtmlString(s);
        }

        public static MvcHtmlString DropDownForValues(this HtmlHelper helper, string name, int minValue, int maxValue, int interval, object htmlAttributes)
        {
            var items = from i in Range(minValue, maxValue, interval)
                        select new SelectListItem
                        {
                            Text = i.ToString(),
                            Value = i.ToString()
                        };

            return helper.DropDownList(name, items, htmlAttributes);
        }

        private static IEnumerable<int> Range(int minValue, int maxValue, int interval)
        {
            for (int index = minValue; index <= maxValue; index += interval)
                yield return index;
        }

        public static string ArrayName(this HtmlHelper helper, string name, int id, string property)
        {
            return String.Format("{0}[{1}].{2}", name, id, property);
        }

        public static string MultipartName(this HtmlHelper helper, params string[] parts)
        {
            return String.Join("_", parts);
        }

        private static readonly IHtmlString _CheckedAttribute = new HtmlString("checked=\"checked\"");
        public static IHtmlString Checked(this HtmlHelper helper, bool selected)
        {
            return selected ? _CheckedAttribute : MvcHtmlString.Empty;
        }
    }
}