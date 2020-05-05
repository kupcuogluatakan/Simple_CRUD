using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KingAOP.Aspects;
using System.Text;

namespace ReportService.Aspects
{
    public class LoginInterceptionAspect : MethodInterceptionAspect
    {
        public override void OnInvoke(MethodInterceptionArgs args)
        {
            int i;
            Logger logger = new Logger();

            try
            {
                var consumer = (UserCredentials)args.Arguments.FirstOrDefault();
                if (consumer != null)
                {
                    if (ReportUtility.checkConsumer(consumer))
                    {
                        consumer.returnValue = true;
                    }
                    else
                    {
                        logger.Log("Login Failed !", string.Empty, out i);
                    }
                }
                else
                {
                    logger.Log("Please enter your credentials !", string.Empty, out i);
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.ToString(), string.Empty, out i);
            }
            finally
            {               
                args.Proceed();
            }
        }
    }
}