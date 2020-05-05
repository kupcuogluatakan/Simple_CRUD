using System;
using System.Collections.Generic;
using System.Web.Mvc.Html;
using System.Web.Routing;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.StockMovement.Handlers;

namespace ODMSTerminal.Infrastructure.Utility
{
    using System.Linq;
    using ODMSCommon.Security;
    using ODMSTerminal.Security;
    using System.Web.Mvc;
    public static class HtmlExtensions
    {
        public static MvcHtmlString HasPermission(this MvcHtmlString htmlString, string permissionCode)
        {
            if (UserManager.UserInfo == null || new UserPermissionManager().UserPermissions.All(c => c.PermissionCode != permissionCode))
            {
                return new MvcHtmlString(string.Empty);
            }
            return htmlString;
        }

        public static MvcHtmlString ForJavascript(this bool value)
        {
            return new MvcHtmlString(value.ToString().ToLower());
        }

        public static MvcHtmlString FormatForView(this DateTime value)
        {
            if (value == DateTime.MinValue) return new MvcHtmlString(string.Empty);
            return new MvcHtmlString(value.ToString("dd/MM/yyyy"));
        }

        public static MvcHtmlString GeneratePagingLinks(this HtmlHelper helper ,GridPagingInfo pagingInfo,string actionName,string controllerName,RouteValueDictionary routes=null)
        {
            if (pagingInfo==null || pagingInfo.TotalPages==1) return new MvcHtmlString(string.Empty);

            bool canGoPrev = pagingInfo.CurrentPage > 1 && pagingInfo.CurrentPage <= pagingInfo.TotalPages;

            bool canGoNext= pagingInfo.CurrentPage < pagingInfo.TotalPages;

            var tagBuilder = new TagBuilder("table");
    
            tagBuilder.Attributes.Add("border", "0");
            tagBuilder.Attributes.Add("cellspacing", "1");
            tagBuilder.Attributes.Add("width","100%");
            var trBuilder = new TagBuilder("tr");
            if (canGoPrev)
            {
                var tdPrevBuilder = TdBuilder(helper, actionName, controllerName, routes, pagingInfo.CurrentPage - 1, "‹");
                trBuilder.InnerHtml += tdPrevBuilder.ToString();
            }
            if (canGoNext)
            {
                var tdNextBuilder = TdBuilder(helper, actionName, controllerName, routes, pagingInfo.CurrentPage + 1, "›");
                trBuilder.InnerHtml += tdNextBuilder.ToString();
            }
            tagBuilder.InnerHtml = trBuilder.ToString();
            return new MvcHtmlString(tagBuilder.ToString());
        }

        private static TagBuilder TdBuilder(HtmlHelper helper, string actionName,
            string controllerName, RouteValueDictionary routes, int pageNo,string linkText)
        {
            var tdBuilder = new TagBuilder("td");
            tdBuilder.AddCssClass("text-center");
            var routesInner = new RouteValueDictionary();
            if (routes != null)
            {
                foreach (var route in routes)
                {
                    routesInner.Add(route.Key, route.Value);
                }
            }
            routesInner.Add("pageId", pageNo);
            var htmlAttributes = new Dictionary<string, object> {{"class", "pagingLink"}};
            tdBuilder.InnerHtml =
                helper.ActionLink(linkText, actionName, controllerName, routesInner,
                    htmlAttributes).ToHtmlString();
            return tdBuilder;
        }
    }
}