using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
//using NVelocity;
//using NVelocity.App;
//using NVelocity.Runtime;
using Commons.Collections;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;

namespace ODMSCommon
{
    public static class NVelocityTemplateEngine
    {
        public static string Process(string filePath, string templateName, IDictionary<string, object> item)
        {
            if (string.IsNullOrEmpty(templateName))
            {
                throw new ArgumentException("The \"templateName\" parameter must be specified", "templateName");
            }

            var engine = new VelocityEngine();
            var props = new ExtendedProperties();
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, filePath);
            engine.Init(props);
            var template = engine.GetTemplate(templateName);
            template.Encoding = Encoding.UTF8.BodyName;
            var context = new VelocityContext();

            var templateData = item ?? new Dictionary<string, object>();
            foreach (var key in templateData.Keys)
            {
                context.Put(key, templateData[key]);
            }

            using (var writer = new StringWriter())
            {
                engine.MergeTemplate(templateName,template.Encoding, context, writer);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
