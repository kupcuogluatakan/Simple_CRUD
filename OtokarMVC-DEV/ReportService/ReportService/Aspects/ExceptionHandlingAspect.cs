using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KingAOP.Aspects;
using System.Reflection;
using System.Text;

namespace ReportService.Aspects
{
    public class ExceptionHandlingAspect : OnMethodBoundaryAspect
    {
        public override void OnException(MethodExecutionArgs args)
        {
            int i;
            Logger logger = new Logger();

            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < args.Arguments.Count; k++)
            {
                sb.Append(k + 1);
                sb.AppendLine(". value : ");
                sb.AppendLine(args.Arguments[k].ToString());
            }

            logger.Log(args.Exception.ToString(), sb.ToString(), out i);
            if (args.Exception.InnerException != null)
            {
                logger.Log(args.Exception.ToString(), sb.ToString(), out i);
            }
        }
    }
}