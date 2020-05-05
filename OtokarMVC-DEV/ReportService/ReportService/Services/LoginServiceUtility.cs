using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;
using KingAOP;
using ReportService.Aspects;

namespace ReportService.Services
{
    public class LoginServiceUtility
    {
        public bool checkConsumer(UserCredentials consumer)
        {
            int i;
            Logger logger = new Logger();

            try
            {
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Log(ex.ToString(), string.Empty, out i);
                return false;
            }

            return consumer.returnValue;
        }
    }
}