using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Kendo.Mvc.UI.Fluent;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel;

namespace ODMS.Core
{
    public static class ODMSHelpers
    {

        public static ComboBoxBuilder SelectFirstOne(this ComboBoxBuilder src, List<SelectListItem> items)
        {
            if (items == null) return src;
            if (items.Count == 1)
                src.SelectedIndex(0);
            return src;
        }

        public static bool UserHasPermission(string permissionCode)
        {
            return (new Security.UserPermissionManager()).UserPermissions.Any(v => v.PermissionCode == permissionCode);
        }

        public static MvcHtmlString ButtonWithMultipleAction(string id, string value, string actionName)
        {

            return
                MvcHtmlString.Create(
                    string.Format("<input type='submit' name='action:{2}' id='{0}' value='{1}' class='k-button'/>",
                        id, value, actionName));
        }

        public static MvcHtmlString Button(string id, string value, string actionName)
        {

            return
                MvcHtmlString.Create(
                    string.Format("<input type='submit' name='{2}' id='{0}' value='{1}' class='k-button'/>",
                        id, value, actionName));
        }


        public static MvcHtmlString Button(string id, string value, string permissionCode, string actionName)
        {

            return (string.IsNullOrEmpty(permissionCode) || UserHasPermission(permissionCode))
                       ? MvcHtmlString.Create(
                           string.Format(
                               "<input type='submit' name='action:{2}'' id='{0}' value='{1}' class='k-button'/>", id,
                               value, actionName))
                       : MvcHtmlString.Create(string.Empty);
            // Buton yetki yoksa görünmesin diye eski kod kapatıldı. Oya 05.11.2013
            //MvcHtmlString.Create(
            //    string.Format(
            //        "<input type='submit' name='action:{2}' id='{0}' value='{1}' disabled='disabled' class='k-button k-state-disabled'/>",
            //        id, value, actionName));
        }

        public static MvcHtmlString Button(string id, string value, string permissionCode, string actionName,
            string onClick)
        {
            return (string.IsNullOrEmpty(permissionCode) || UserHasPermission(permissionCode))
                       ? MvcHtmlString.Create(
                           string.Format(
                               @"<input type='submit' name='action:{3}' id='{0}' value='{1}' class='k-button' onClick=""{2}""/>",
                               id, value, onClick, actionName))
                       : MvcHtmlString.Create(string.Empty);
            // Buton yetki yoksa görünmesin diye eski kod kapatıldı. Oya 05.11.2013
            //: MvcHtmlString.Create(
            //    string.Format(
            //        "<input type='submit' name='action:{3}' id='{0}' value='{1}' disabled='disabled' class='k-button k-state-disabled' onClick='{2}'/>",
            //        id, value, onClick, actionName));
        }
        public static MvcHtmlString Button(string id, string value, string permissionCode, string actionName,
            string onClick, bool submit)
        {
            if (submit)
                return (string.IsNullOrEmpty(permissionCode) || UserHasPermission(permissionCode))
                           ? MvcHtmlString.Create(
                               string.Format(
                                   @"<input type='submit' name='action:{3}' id='{0}' value='{1}' class='k-button' onClick=""{2}""/>",
                                   id, value, onClick, actionName))
                           : MvcHtmlString.Create(string.Empty);
            // Buton yetki yoksa görünmesin diye eski kod kapatıldı. Oya 05.11.2013
            //: MvcHtmlString.Create(
            //    string.Format(
            //        "<input type='submit' name='action:{3}' id='{0}' value='{1}' disabled='disabled' class='k-button k-state-disabled' onClick='{2}'/>",
            //        id, value, onClick, actionName));
            else
            {
                return (string.IsNullOrEmpty(permissionCode) || UserHasPermission(permissionCode))
                           ? MvcHtmlString.Create(
                               string.Format(
                                   @"<input type='button' name='action:{3}' id='{0}' value='{1}' class='k-button' onClick=""{2}""/>",
                                   id, value, onClick, actionName))
                           : MvcHtmlString.Create(string.Empty);
                // Buton yetki yoksa görünmesin diye eski kod kapatıldı. Oya 05.11.2013
                //: MvcHtmlString.Create(
                //    string.Format(
                //        "<input type='button' name='action:{3}' id='{0}' value='{1}' disabled='disabled' class='k-button k-state-disabled' onClick='{2}'/>",
                //        id, value, onClick, actionName));
            }
        }

        public static MvcHtmlString Button(string id, string value, string permissionCode, string actionName,
            string onClick, string cssClass)
        {
            return (string.IsNullOrEmpty(permissionCode) || UserHasPermission(permissionCode))
                       ? MvcHtmlString.Create(
                           string.Format(
                               @"<input type='submit' name='action:{3}' id='{0}' value='{1}' class='k-button {4}' onClick=""{2}""/>",
                               id, value, onClick, actionName, cssClass))
                       : MvcHtmlString.Create(string.Empty);
            // Buton yetki yoksa görünmesin diye eski kod kapatıldı. Oya 05.11.2013
            //: MvcHtmlString.Create(
            //    string.Format(
            //        "<input type='submit' name='action:{3}' id='{0}' value='{1}' disabled='disabled' class='k-button k-state-disabled {4}' onClick='{2}'/>",
            //        id, value, onClick, actionName, cssClass));
        }

        public static MvcHtmlString LinkButton(string id, string value, string actionName, string cssClass,
            string modelTitle, string permissionCode)
        {

            return
                (string.IsNullOrEmpty(permissionCode) || UserHasPermission(permissionCode))
                    ? MvcHtmlString.Create(
                        string.Format(
                            "<input type='submit' name='{2}' id='{0}' style='font-weight:bold !important;' value='{1}' class='k-button {3}' frameTitle='{4}'/>",
                            id, value, actionName, cssClass, modelTitle))
                    : MvcHtmlString.Create(string.Empty);
            // Buton yetki yoksa görünmesin diye eski kod kapatıldı. Oya 05.11.2013
            //: MvcHtmlString.Create(
            //    string.Format(
            //        "<input type='submit' name='{2}' id='{0}' value='{1}' class='k-button k-state-disabled {3}' frameTitle='{4}'/>",
            //        id, value, actionName, cssClass, modelTitle));
        }

        public static MvcHtmlString LinkButton(string id, string value, string actionName, string cssClass,
            string modelTitle, string permissionCode, string style)
        {

            return (string.IsNullOrEmpty(permissionCode) || UserHasPermission(permissionCode))

                       ? MvcHtmlString.Create(
                           string.Format(
                               "<input type='submit' name='{2}' id='{0}' value='{1}' class='k-button {3}' frameTitle='{4}' style='{5}'/>",
                               id, value, actionName, cssClass, modelTitle, style))
                       : MvcHtmlString.Create(string.Empty);
            // Buton yetki yoksa görünmesin diye eski kod kapatıldı. Oya 05.11.2013
            //: MvcHtmlString.Create(
            //    string.Format(
            //        "<input type='submit' name='{2}' id='{0}' value='{1}' class='k-button k-state-disabled {3}' frameTitle='{4}' style='{5}'/>",
            //        id, value, actionName, cssClass, modelTitle, style));
        }

        public static IHtmlString DateTimePickerFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            // metadata gives you access to a variety of useful things, such as the
            // display name and required status
            var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            const string markup = ""; // markup for your input            

            var name = helper.NameFor(expression); // getting the name

            return MvcHtmlString.Create(markup);
        }

        public static MvcHtmlString ImageWithDynamicId(string src, string id, string style)
        {
            return MvcHtmlString.Create(string.Format("<img src=\"{0}\" id=\"{1}\" style=\"{2}\"/>", src, id, style));
        }

        public static MvcHtmlString CustomHiddenFieldWithValidation(string id, int? value, bool required)
        {
            var inputBuilder = new TagBuilder("input");
            var retVal = string.Empty;
            inputBuilder.GenerateId(id);
            inputBuilder.MergeAttribute("name", id);
            inputBuilder.MergeAttribute("type", "hidden");
            if (value.HasValue && value.Value > 0)
                inputBuilder.MergeAttribute("value", value.ToString());
            if (required)
            {
                inputBuilder.MergeAttribute("data-val", "true");
                inputBuilder.MergeAttribute("data-val-required", "*");
                retVal = inputBuilder.ToString(TagRenderMode.SelfClosing);
                inputBuilder = new TagBuilder("span");
                inputBuilder.MergeAttribute("data-valmsg-for", id);
                inputBuilder.MergeAttribute("data-valmsg-replace", "true");
                retVal += inputBuilder.ToString(TagRenderMode.Normal);
            }
            else
            {
                retVal = inputBuilder.ToString(TagRenderMode.SelfClosing);
            }

            return MvcHtmlString.Create(retVal);

        }
        public static MvcHtmlString CustomHiddenFieldWithValidation(string id, long? value, bool required, string name)
        {
            var inputBuilder = new TagBuilder("input");
            var retVal = string.Empty;
            inputBuilder.GenerateId(id);
            inputBuilder.MergeAttribute("name", string.IsNullOrEmpty(name) ? id : name);
            inputBuilder.MergeAttribute("type", "hidden");
            if (value.HasValue && value.Value > 0)
                inputBuilder.MergeAttribute("value", value.ToString());
            if (required)
            {
                inputBuilder.MergeAttribute("data-val", "true");
                inputBuilder.MergeAttribute("data-val-required", "*");
                retVal = inputBuilder.ToString(TagRenderMode.SelfClosing);
                inputBuilder = new TagBuilder("span");
                inputBuilder.MergeAttribute("data-valmsg-for", id);
                inputBuilder.MergeAttribute("data-valmsg-replace", "true");
                retVal += inputBuilder.ToString(TagRenderMode.Normal);
            }
            else
            {
                retVal = inputBuilder.ToString(TagRenderMode.SelfClosing);
            }

            return MvcHtmlString.Create(retVal);

        }
        public static MvcHtmlString ObjectSearchTextbox(string id, ODMSCommon.CommonValues.ObjectSearchType searchType, long? objectId, string preDefinedObjectText, bool hidden = false)
        {
            var inputBuilder = new TagBuilder("input");
            var retVal = string.Empty;
            inputBuilder.GenerateId(id);
            inputBuilder.MergeAttribute("name", id);
            inputBuilder.MergeAttribute("type", hidden ? "hidden" : "text");
            inputBuilder.MergeAttribute("readonly", "readonly");
            if (!string.IsNullOrWhiteSpace(preDefinedObjectText))
            {
                inputBuilder.MergeAttribute("value", preDefinedObjectText);
                retVal = inputBuilder.ToString(TagRenderMode.SelfClosing);
            }
            else if (objectId.HasValue && objectId > 0 && string.IsNullOrWhiteSpace(preDefinedObjectText))
            {
                var objectSearchBo = new ODMSBusiness.ObjectSearchBL();
                var inputValue = objectSearchBo.GetObjectTextWithId(UserManager.UserInfo,searchType, objectId.Value).Model;
                inputBuilder.MergeAttribute("value", inputValue);
                retVal = inputBuilder.ToString(TagRenderMode.SelfClosing);
            }
            else
            {
                retVal = inputBuilder.ToString(TagRenderMode.SelfClosing);
            }

            return MvcHtmlString.Create(retVal);
        }
        public static MvcHtmlString AntiForgeryTokenForAjaxPost(this HtmlHelper helper)
        {
            var antiForgeryInputTag = helper.AntiForgeryToken().ToString();

            var removedStart = antiForgeryInputTag.Replace(@"<input name=""__RequestVerificationToken"" type=""hidden"" value=""", "");
            var tokenValue = removedStart.Replace(@""" />", "");
            if (antiForgeryInputTag == removedStart || removedStart == tokenValue)
                throw new InvalidOperationException("Method seems to return something I did not expect.");

            return new MvcHtmlString(string.Format(@"{0}:""{1}""", "__RequestVerificationToken", tokenValue));
        }

        public static string GetDynamicSeleclist(string id)
        {
            return "<select><option value='13'>** - Depo 2</option><option value='8'>Test 20 - Depo 7</option></select>";
        }

        public static MvcHtmlString ExportExcelButton(this HtmlHelper htmlHelper, ExcelExportDto dto)
        {

            Func<ExcelExportDto, string> getControllerInput = c =>
                string.Format("<input type='hidden' name='Controller' value='{0}' />", c.Controller);

            Func<ExcelExportDto, string> getActionInput = c =>
                string.Format("<input type='hidden' name='Action' value='{0}' />", c.Action);

            Func<List<string>, string> toCsv = src =>
            {

                var str = string.Empty;
                src.ForEach(c =>
                {
                    str += c + ",";
                });

                return src.Any() ? str : "";


            };

            Func<ExcelExportDto, string> getListPropertiesInput = c =>
                string.Format("<input type='hidden' name='Properties' id='Properties' value='{0}' />", toCsv(c.Properties));

            Func<ExcelExportDto, string> getListTypeInput = c =>
                string.Format("<input type='hidden' name='ListType' value='{0}' />", dto.ListType);
            Func<ExcelExportDto, string> getReportName = c =>
                string.Format("<input type='hidden' name='ReportName' value='{0}' />", dto.ReportName);
            // we will send request from this form
            var tagBuilder = new TagBuilder("form");
            tagBuilder.MergeAttribute("style", "display:inline-block", true);
            tagBuilder.MergeAttribute("action", new UrlHelper(htmlHelper.ViewContext.RequestContext).Content("~/ExcelExporter/Export"));
            tagBuilder.MergeAttribute("method", "post");
            tagBuilder.GenerateId(dto.Id);
            //I will just add an anchor inside this form and handle the other stuff
            var anchorTag = new TagBuilder("a");
            anchorTag.AddCssClass("k-button");
            anchorTag.GenerateId("btn" + dto.Id);
            anchorTag.MergeAttribute("onclick", string.Format("{0}(this,{1});", dto.BeforeSubmitFunction, !string.IsNullOrEmpty(dto.GridSearchFunction) ? dto.GridSearchFunction : "''"));
            anchorTag.InnerHtml =
                ImageWithDynamicId(new UrlHelper(htmlHelper.ViewContext.RequestContext).Content("~/Images/excel.gif"),
                    "excel-exp", "height:14px; vertical-align:text-top;").ToHtmlString() + "&nbsp;" + MessageResource.Global_Display_ExportToExcel;

            //now setting the before submit function
            var scriptTag = new TagBuilder("script");
            var sb = new StringBuilder();
            scriptTag.MergeAttribute("type", "text/javascript");
            sb.Append("$('#btn" + dto.Id + "').click(function(){ ")
                .Append(dto.BeforeSubmitFunction)
                .Append("(this,")
                .Append(!string.IsNullOrEmpty(dto.GridSearchFunction) &&
                        !string.IsNullOrWhiteSpace(dto.GridSearchFunction)
                    ? dto.GridSearchFunction
                    : "5")
                .Append(");}); ");
            sb.Append("function ")
                .Append(dto.BeforeSubmitFunction)
                .Append("(obj,")
                .Append(!string.IsNullOrEmpty(dto.GridSearchFunction)
                && !string.IsNullOrWhiteSpace(dto.GridSearchFunction) ? dto.GridSearchFunction : "a")
                .Append("){")
                .Append(" var html=''; ");
            if (!string.IsNullOrEmpty(dto.GridSearchFunction))
            {

                sb.Append(" var params=").Append(dto.GridSearchFunction).Append("();")
                    .Append("for ( property in params) {")
                    .Append(
                        "html+=\"<input type='hidden' class='late-bind' name='\"+property+\"' value='\"+ params[property] + \"' >\";")
                    .Append("};");
                      //filters
                if (!string.IsNullOrEmpty(dto.FilterId))
                {
                    sb.Append(" var filterValues=")
                        .Append("GetFilterNameAndValues('")
                        .Append(dto.FilterId)
                        .Append("');");
                    sb.Append(
                        "html+=\"<input type='hidden' class='late-bind' name='Filters' value='\"+ JSON.stringify(filterValues, function(key, value) { if(typeof value === 'string')return value.replace(/'/g, \"&#39;\");else return value;}) + \"' >\";");
                }
                    
            }
            sb.Append(
                "$('#" + dto.Id + " .late-bind').remove(); $('#" + dto.Id + "').append(html); $('#" + dto.Id + "').submit();")
                .Append("}"); //end of function
            scriptTag.InnerHtml = sb.ToString();
            tagBuilder.InnerHtml = string.Format("{0} {1} {2} {3} {4} {5} {6} ", scriptTag.ToString(TagRenderMode.Normal),
                anchorTag.ToString(TagRenderMode.Normal), getControllerInput(dto), getActionInput(dto), getListPropertiesInput(dto), getListTypeInput(dto),getReportName(dto));
            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString ExportExcelButton(this HtmlHelper htmlHelper, ExcelExportDto dto,string ResourceName)
        {

            Func<ExcelExportDto, string> getControllerInput = c =>
                string.Format("<input type='hidden' name='Controller' value='{0}' />", c.Controller);

            Func<ExcelExportDto, string> getActionInput = c =>
                string.Format("<input type='hidden' name='Action' value='{0}' />", c.Action);

            Func<List<string>, string> toCsv = src =>
            {

                var str = string.Empty;
                src.ForEach(c =>
                {
                    str += c + ",";
                });

                return src.Any() ? str : "";


            };

            Func<ExcelExportDto, string> getListPropertiesInput = c =>
                string.Format("<input type='hidden' name='Properties' id='Properties' value='{0}' />", toCsv(c.Properties));

            Func<ExcelExportDto, string> getListTypeInput = c =>
                string.Format("<input type='hidden' name='ListType' value='{0}' />", dto.ListType);
            Func<ExcelExportDto, string> getReportName = c =>
                string.Format("<input type='hidden' name='ReportName' value='{0}' />", dto.ReportName);
            // we will send request from this form
            var tagBuilder = new TagBuilder("form");
            tagBuilder.MergeAttribute("style", "display:inline-block", true);
            tagBuilder.MergeAttribute("action", new UrlHelper(htmlHelper.ViewContext.RequestContext).Content("~/ExcelExporter/Export"));
            tagBuilder.MergeAttribute("method", "post");
            tagBuilder.GenerateId(dto.Id);
            //I will just add an anchor inside this form and handle the other stuff
            var anchorTag = new TagBuilder("a");
            anchorTag.AddCssClass("k-button");
            anchorTag.GenerateId("btn" + dto.Id);
            anchorTag.MergeAttribute("onclick", string.Format("{0}(this,{1});", dto.BeforeSubmitFunction, !string.IsNullOrEmpty(dto.GridSearchFunction) ? dto.GridSearchFunction : "''"));
            anchorTag.InnerHtml =
                ImageWithDynamicId(new UrlHelper(htmlHelper.ViewContext.RequestContext).Content("~/Images/excel.gif"),
                    "excel-exp", "height:14px; vertical-align:text-top;").ToHtmlString() + "&nbsp;" + CommonUtility.GetResourceValue(ResourceName);

            //now setting the before submit function
            var scriptTag = new TagBuilder("script");
            var sb = new StringBuilder();
            scriptTag.MergeAttribute("type", "text/javascript");
            sb.Append("$('#btn" + dto.Id + "').click(function(){ ")
                .Append(dto.BeforeSubmitFunction)
                .Append("(this,")
                .Append(!string.IsNullOrEmpty(dto.GridSearchFunction) &&
                        !string.IsNullOrWhiteSpace(dto.GridSearchFunction)
                    ? dto.GridSearchFunction
                    : "5")
                .Append(");}); ");
            sb.Append("function ")
                .Append(dto.BeforeSubmitFunction)
                .Append("(obj,")
                .Append(!string.IsNullOrEmpty(dto.GridSearchFunction)
                && !string.IsNullOrWhiteSpace(dto.GridSearchFunction) ? dto.GridSearchFunction : "a")
                .Append("){")
                .Append(" var html=''; ");
            if (!string.IsNullOrEmpty(dto.GridSearchFunction))
            {

                sb.Append(" var params=").Append(dto.GridSearchFunction).Append("();")
                    .Append("for ( property in params) {")
                    .Append(
                        "html+=\"<input type='hidden' class='late-bind' name='\"+property+\"' value='\"+ params[property] + \"' >\";")
                    .Append("};");
                //filters
                if (!string.IsNullOrEmpty(dto.FilterId))
                {
                    sb.Append(" var filterValues=")
                        .Append("GetFilterNameAndValues('")
                        .Append(dto.FilterId)
                        .Append("');");
                    sb.Append(
                        "html+=\"<input type='hidden' class='late-bind' name='Filters' value='\"+ JSON.stringify(filterValues) + \"' >\";");
                }

            }
            sb.Append(
                "$('#" + dto.Id + " .late-bind').remove(); $('#" + dto.Id + "').append(html); $('#" + dto.Id + "').submit();")
                .Append("}"); //end of function
            scriptTag.InnerHtml = sb.ToString();
            tagBuilder.InnerHtml = string.Format("{0} {1} {2} {3} {4} {5} {6} ", scriptTag.ToString(TagRenderMode.Normal),
                anchorTag.ToString(TagRenderMode.Normal), getControllerInput(dto), getActionInput(dto), getListPropertiesInput(dto), getListTypeInput(dto), getReportName(dto));
            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }
    }
    
    public class ExcelExportDto
    {
        public string Id { get; set; }
        public string ReportName { get; set; }
        public string FilterId { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string GridSearchFunction { get; set; }
        public string ListType { get; set; }
        public string BeforeSubmitFunction { get; set; }
        public List<string> Properties { get; set; }
        public List<FilterDto> Filters { get; set; }

        public ExcelExportDto()
        {
           Filters=new List<FilterDto>();
        }

        public ExcelExportDto(string controller, string action, string searchFunction, string beforeSubmitFunction, string id = null,string reportName=null, string filterId=null)
        {
            Id = id ?? "ExcelExport";
            Controller = controller;
            Action = action;
            GridSearchFunction = searchFunction;
            BeforeSubmitFunction = beforeSubmitFunction;
            ReportName = reportName;
            FilterId = filterId;
            Properties = new List<string>();
            Filters=new List<FilterDto>();
        }

        public ExcelExportDto Build<T, P>(params Expression<Func<T, P>>[] expressions)
        {
            ListType = typeof(T).FullName;
            foreach (var expression in expressions)
            {
                Properties.Add(GetPropertyName(expression));
            }
            return this;
        }




        private string GetPropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);
            var expString = propertyLambda.ToString();
            var lastDotIndex = 0;
            var expLength = expString.Length;
            if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                expString = propertyLambda.ToString();
                lastDotIndex = expString.LastIndexOf(".", StringComparison.InvariantCulture);
                expLength = expString.Length;
                return type.GetProperty(expString.Substring(lastDotIndex + 1, expLength - lastDotIndex - 1)).Name;
            }
            lastDotIndex = expString.LastIndexOf(".", StringComparison.InvariantCulture);
            var parantesisIndex = expString.IndexOf(")", lastDotIndex, StringComparison.InvariantCulture);
            return type.GetProperty(expString.Substring(lastDotIndex + 1, parantesisIndex - lastDotIndex - 1)).Name;

            //MemberExpression member = propertyLambda.Body as MemberExpression;
            //if (member == null)
            //    throw new ArgumentException(string.Format(
            //        "Expression '{0}' refers to a method, not a property.",
            //        propertyLambda.ToString()));

            //PropertyInfo propInfo = member.Member as PropertyInfo;
            //if (propInfo == null)
            //    throw new ArgumentException(string.Format(
            //        "Expression '{0}' refers to a field, not a property.",
            //        propertyLambda.ToString()));

            //if (type != propInfo.ReflectedType &&
            //    !type.IsSubclassOf(propInfo.ReflectedType))
            //    throw new ArgumentException(string.Format(
            //        "Expresion '{0}' refers to a property that is not from type {1}.",
            //        propertyLambda.ToString(),
            //        type));

            //return propInfo.Name;
        }

    }

}
