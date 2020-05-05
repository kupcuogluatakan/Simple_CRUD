using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Reflection;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Xml.Serialization;
using ODMSCommon.TCIdentity;
using System.Configuration;
using System.Collections;
using ODMSCommon.Security;

namespace ODMSCommon
{
    public static class CommonUtility
    {
        public static string ToXml(this DataSet ds)
        {
            if (ds != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (TextWriter streamWriter = new StreamWriter(memoryStream))
                    {
                        var xmlSerializer = new XmlSerializer(typeof(DataSet));
                        xmlSerializer.Serialize(streamWriter, ds);
                        return Encoding.UTF8.GetString(memoryStream.ToArray());
                    }
                }
            }
            return string.Empty;

        }

        /// TODO : Test öncesi şifrelerde kullanılacak.
        ///  <summary>
        ///  SHA1 Hash for Password
        ///  </summary>
        /// <param name="text"></param>
        /// <param name="enc"></param>
        /// <returns>return string</returns>
        public static string CalculateSha1(string text, Encoding enc)
        {
            byte[] buffer = enc.GetBytes(text);

            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();

            return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace(CommonValues.Minus, "");
        }
        /// <summary>
        /// Roll that 14:33 => 15:00    , 15:14 => 15:30
        /// </summary>
        /// <returns></returns>
        public static DateTime GetSeedDateForPublish()
        {
            var currentDate = DateTime.Now;
            return (currentDate.Minute >= 30 || (currentDate.Minute == 29 && currentDate.Second >= 50)) ?
                     new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, 30, 0) :
                     new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, currentDate.Hour, 0, 0);

        }
        public static DateTime GetSeedDateForPublish(int interval)
        {
            var currentDate = DateTime.Now;
            int minute = DateTime.Now.Minute;
            int count = Math.Ceiling((double)minute / interval).GetValue<int>();
            int minValue = count == 0 ? count : (count * interval);
            int hourValue = minValue == 60 ? currentDate.Hour + 1 : currentDate.Hour;
            minValue = minValue == 60 ? 0 : minValue;
            int dayValue = hourValue == 24 ? currentDate.Day + 1 : currentDate.Day;

            return (currentDate.Minute >= interval || (currentDate.Minute == interval - 1 && currentDate.Second >= 50)) ?
                     new DateTime(currentDate.Year, currentDate.Month, dayValue, hourValue, minValue, 0) :
                     new DateTime(currentDate.Year, currentDate.Month, currentDate.Day, 8, 0, 0);
        }
       
        public static string GetResourceValue(string key)
        {
            string languageCode = (UserManager.UserInfo == null || UserManager.UserInfo.LanguageCode == null) ? "TR-tr" : UserManager.UserInfo.LanguageCode;
            var resourceManager = new ResourceManager("ODMSCommon.Resources.MessageResource", Assembly.GetExecutingAssembly());
            var culture = new CultureInfo(languageCode);
            var retVal = resourceManager.GetString(key, culture);
            return string.IsNullOrEmpty(retVal) ? CommonValues.DefaultUndefinedResourceValue : retVal;
        }

        public static string BuildObjectSearchUrl(System.Web.Routing.RequestContext requestContext, CommonValues.ObjectSearchType searchType, string referenceObjectId, string clearCallBackFunction, string selectCallBackFunction, string filterId)
        {
            System.Web.Mvc.UrlHelper urlHelper = new System.Web.Mvc.UrlHelper(requestContext);
            string retVal = string.Empty;
            object routeValues = new { ReferenceObjectId = referenceObjectId, SelectCallBackFunction = selectCallBackFunction, ClearCallBackFunction = clearCallBackFunction, FilterId = filterId };
            switch (searchType)
            {
                case CommonValues.ObjectSearchType.Customer:
                    {
                        retVal = urlHelper.Action("CustomerSearch", "ObjectSearch", routeValues);
                    }
                    break;
                case CommonValues.ObjectSearchType.AppointmentIndicatorSubCategory:
                    {
                        retVal = urlHelper.Action("AppointmentIndicatorSubCategorySearch", "ObjectSearch", routeValues);
                    }
                    break;
                case CommonValues.ObjectSearchType.Vehicle:
                    {
                        retVal = urlHelper.Action("VehicleSearch", "ObjectSearch", routeValues);
                    }
                    break;
                case CommonValues.ObjectSearchType.Appointment:
                    {
                        retVal = urlHelper.Action("AppointmentSearch", "ObjectSearch", routeValues);
                    }
                    break;
                case CommonValues.ObjectSearchType.Fleet:
                    {
                        retVal = urlHelper.Action("FleetSearch", "ObjectSearch", routeValues);
                    }
                    break;
                case CommonValues.ObjectSearchType.PurchaseOrder:
                    {
                        retVal = urlHelper.Action("PurchaseOrderSearch", "ObjectSearch", routeValues);
                    }
                    break;
            }

            return retVal;
        }

        public static string GeneratePassword()
        {
            string password = Membership.GeneratePassword(10, 0);
            password = Regex.Replace(password, @"[^a-zA-Z0-9]", m => "9");
            return password;
        }

        public static object DeepClone(object obj)
        {
            object objResult;
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, obj);

                ms.Position = 0;
                objResult = bf.Deserialize(ms);
            }
            return objResult;
        }

        public static bool CreateAuthenticationCookie(Security.UserInfo uInfo)
        {
            try
            {
                var userData = Newtonsoft.Json.JsonConvert.SerializeObject(uInfo);

                var ticket = new FormsAuthenticationTicket(1, "admin", DateTime.Now,
                                                                                 (uInfo.RememberMe ? DateTime.Now.AddDays(1000) : DateTime.Now.AddMinutes(15)), true, userData,
                                                                                 FormsAuthentication.FormsCookiePath);
                var encTicket = FormsAuthentication.Encrypt(ticket);
                var httpCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
                {
                    Expires = uInfo.RememberMe ? DateTime.Now.AddYears(50) : DateTime.MinValue
                };
                HttpContext.Current.Response.Cookies.Set(httpCookie);
            }
            catch
            {
                return false;
            }
            return true;
        }
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static T GetValue<T>(this object value)
        {
            Type t = typeof(T);
            if (value == null || value == DBNull.Value || ReferenceEquals(value, string.Empty))
            {
                if (t.Name == "String")
                {
                    return (T)Convert.ChangeType(string.Empty, typeof(T));
                }
                return default(T);
            }
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                Type valueType = t.GetGenericArguments()[0];
                object result = Convert.ChangeType(value, valueType);
                return (T)result;
            }
            return (T)Convert.ChangeType(value, typeof(T));
        }
        public static string GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : value.ToString();
        }
        public static void SendMail(string to, string subject, string body)
        {
            SmtpSection section = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            string from = section.From;
            bool enableSsl = section.Network.EnableSsl;

            SmtpClient client = new SmtpClient();
            MailAddress fromAddress = new MailAddress(from, "Otokar DMS");
            MailAddress toAddress = new MailAddress(to);
            MailMessage mail = new MailMessage(fromAddress, toAddress)
            {
                BodyEncoding = UTF8Encoding.UTF8,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            client.Host = ConfigurationManager.AppSettings.Get("EmailHost");
            client.Port = 25;
            client.EnableSsl = enableSsl;
            //client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            //client.Credentials = new System.Net.NetworkCredential(user, password);
            client.Send(mail);
        }

        /// <summary>
        /// 
        /// </summary>
        public static string GetMailBodyStringFromTemplate(string template, object data)
        {
            var templateData = new Dictionary<string, object> { { "item", data } };
            string bodyString = NVelocityTemplateEngine.Process(HttpContext.Current.Server.MapPath("~/Views/MailTemplates"), template, templateData);
            return bodyString;
        }

        /// <summary>
        /// its encrypt your orginal key     
        /// </summary>
        /// <param name="text"></param>     
        public static string EncryptSymmetric(string text)
        {
            try
            {
                string initVector = ConfigurationManager.AppSettings.Get("initVector");
                var initVectorBytes = Encoding.UTF8.GetBytes(initVector);
                var plainTextBytes = Encoding.UTF8.GetBytes(text);
                var password = new PasswordDeriveBytes(ConfigurationManager.AppSettings.Get("passwordKey"), null);
                var keyBytes = password.GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
                var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                var memoryStream = new MemoryStream();
                var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                var cipherTextBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                return Convert.ToBase64String(cipherTextBytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// its decrypt your encrypted key     
        /// </summary>
        /// <param name="text"></param>    
        public static string DecryptSymmetric(string text)
        {
            try
            {
                string initVector = ConfigurationManager.AppSettings.Get("initVector");
                var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                var cipherTextBytes = Convert.FromBase64String(text);
                var password = new PasswordDeriveBytes(ConfigurationManager.AppSettings.Get("passwordKey"), null);
                var keyBytes = password.GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
                var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                var memoryStream = new MemoryStream(cipherTextBytes);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                var plainTextBytes = new byte[cipherTextBytes.Length];
                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Dealer code encryption for catalog access
        /// </summary>
        /// <param name="text"></param>    
        public static string EncryptForCatalog(string text)
        {

            const string PasswordHash = "!P@@Sw0rd!";
            const string SaltKey = "!S@LT&KEYRMI!";
            const string VIKey = "!@1B2c3D4e5F6g7H8!";
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static bool In(this int src, params int[] parameters)
        {
            return parameters.Any(c => c.Equals(src));
        }




        /// <summary>
        /// this methods works within fluentvalidation framework
        /// </summary>
        #region Custom Validation Methods

        public static bool IsTurkishContentFilled(string content)
        {
            content = content.Replace("TR ||", "TR||");

            if (content.Length < 5)
                return false;
            string fifthChar = content.Substring(4, 1);
            if (fifthChar == "|")
                return false;
            return true;
        }
        /// <summary>
        /// Uses official webservice to validate TCIdentity No
        /// </summary>
        public static bool IsValidTCIdentityNo(string identityNo, string firstName, string lastName, int birthdayYear)
        {
            try
            {
                if (!string.IsNullOrEmpty(identityNo))
                {
                    var client = new KPSPublicSoapClient();
                    client.Open();
                    var result = client.TCKimlikNoDogrula(long.Parse(identityNo), firstName.ToUpper(),
                                                          lastName.ToUpper(), birthdayYear);
                    client.Close();
                    return result;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Validates TC Identity no based on the algorithim
        /// </summary>
        public static bool ValidateTcIdentityNumber(string tcIdentityNumber)
        {
            //must be 11 chars
            if (string.IsNullOrEmpty(tcIdentityNumber) || tcIdentityNumber.Length != 11 || Regex.IsMatch(tcIdentityNumber, @"^\d{11}$") == false || tcIdentityNumber.StartsWith("0"))
                return false;
            /* 1. 3. 5. 7. ve 9. hanelerin toplamının 7 katından, 
               2. 4. 6. ve 8. hanelerin toplamı çıkartıldığında, 
               elde edilen sonucun 10'a bölümünden kalan, yani Mod10'u bize 10. haneyi verir.
            */
            var numArr = tcIdentityNumber.ToCharArray();
            var evenDigitsTotal = 0;
            for (int i = 0; i < 4; i++)
                evenDigitsTotal += int.Parse(numArr[2 * i + 1].ToString());
            var oddDigitsTotal = 0;
            for (int i = 0; i <= 4; i++)
                oddDigitsTotal += int.Parse(numArr[2 * i].ToString());

            if ((oddDigitsTotal * 7 - evenDigitsTotal) % 10 != int.Parse(numArr[9].ToString()))
                return false;

            /* 1. 2. 3. 4. 5. 6. 7. 8. 9. ve 10. hanelerin toplamından
             * elde edilen sonucun 10'a bölümünden kalan, 
             * yani Mod10'u bize 11. haneyi verir.
             */

            var firstTenDigitsTotal = numArr.Sum(c => int.Parse(c.ToString())) - int.Parse(numArr[10].ToString());
            if (firstTenDigitsTotal % 10 != int.Parse(numArr[10].ToString()))
                return false;
            return true;
        }
        #endregion

        #region distinct By Extension
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
         (this IEnumerable<TSource> source,
          Func<TSource, TKey> keySelector)
        {
            return source.DistinctBy(keySelector,
                                     EqualityComparer<TKey>.Default);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source,
             Func<TSource, TKey> keySelector,
             IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException("keySelector");
            }
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
            return DistinctByImpl(source, keySelector, comparer);
        }

        private static IEnumerable<TSource> DistinctByImpl<TSource, TKey>
            (IEnumerable<TSource> source,
             Func<TSource, TKey> keySelector,
             IEqualityComparer<TKey> comparer)
        {
            HashSet<TKey> knownKeys = new HashSet<TKey>(comparer);
            return source.Where(element => knownKeys.Add(keySelector(element)));
        }

        #endregion

        #region DateTimeToTimeSpan Extension

        public static TimeSpan ToTimeSpan(this DateTime date)
        {
            if (date == null)
                throw new ArgumentNullException("date", "Date cannot be null");
            return new TimeSpan(0, date.Hour, date.Minute, date.Second);
        }

        public static string ToDotedString(this decimal decimalValue)
        {
            return decimalValue.ToString().Replace(CommonValues.Comma, CommonValues.Dot);
        }

        public static Decimal ToCommaString(this string decimalValue)
        {
            return Decimal.Parse(decimalValue.Replace(CommonValues.Dot, CommonValues.Comma));
        }

        public static string ToDisplayString(this TimeSpan timespan)
        {
            if (timespan == null)
                throw new ArgumentNullException("timespan", "TimeSpan cannot be null");
            var sb = new StringBuilder();
            if (timespan.Hours < 10)
                sb.Append(0);
            sb.Append(timespan.Hours).Append(":");
            if (timespan.Minutes < 10)
                sb.Append(0);
            sb.Append(timespan.Minutes);
            return sb.ToString();
        }

        public static decimal RoundUp(this decimal src, int digits)
        {
            return Math.Round(src, digits);
        }

        public static string CurrencyFormat(this string src)
        {
            /*
                why we use the invariant culture for decimal convert is the same as the value of the format from webservice !
            */
            decimal money = Convert.ToDecimal(src, new CultureInfo("en-US"));

            NumberFormatInfo nfi = CultureInfo.CurrentCulture.NumberFormat;
            nfi = (NumberFormatInfo)nfi.Clone();
            nfi.CurrencySymbol = "";

            return string.Format(nfi, "{0:c}", money);
        }

        #endregion

        public static string Normalize(this DateTime date)
        {
            return date == null || date == default(DateTime)
                ? string.Empty
                : date.ToString("dd/MM/yyyy").Replace(CommonValues.Dot, CommonValues.Slash);
        }
        public static string Normalize(this DateTime date, bool includeHour = false)
        {
            return date == null || date == default(DateTime)
                ? string.Empty
                : includeHour == false ? date.ToString("dd/MM/yyyy").Replace(CommonValues.Dot, CommonValues.Slash)
                : date.ToString("dd/MM/yyyy HH:mm").Replace(CommonValues.Dot, CommonValues.Slash);
        }
        public static string Normalize(this Nullable<DateTime> date, bool includeHour = false)
        {
            return date == null || date == default(DateTime)
                ? string.Empty
                : includeHour == false ? date.GetValueOrDefault().ToString("dd/MM/yyyy").Replace(CommonValues.Dot, CommonValues.Slash)
                : date.GetValueOrDefault().ToString("dd/MM/yyyy HH:mm").Replace(CommonValues.Dot, CommonValues.Slash);
        }
        public static string Normalize(this Nullable<DateTime> date)
        {
            return date.GetValueOrDefault().Normalize();
        }

        public static Func<decimal, bool> ValidateIntegerPart(int maxIntegerPartLength)
        {
            return c =>
            {
                var actualLength = Math.Truncate(c).ToString().Length;
                return actualLength <= maxIntegerPartLength;
            };
        }
        public static Func<decimal?, bool> ValidateIntegerPartNullable(int maxIntegerPartLength)
        {
            return c =>
            {
                if (c.HasValue == false) return true;
                var actualLength = Math.Truncate(c.GetValueOrDefault(0)).ToString().Length;
                return actualLength <= maxIntegerPartLength;
            };
        }


        public static List<string> ListOfLanguage()
        {
            return new List<string> { "TR", "EN", "DE", "FR" };
        }


        public static void MakeFirstItemSelectedIfLengthIsOne(List<System.Web.Mvc.SelectListItem> list)
        {
            if (!list.Any()) return;
            if (list.Count == 1)
            {
                list.First().Selected = true;
            }
        }

        public static string GetJsResources(string cultureString)
        {
            string s = "{";
            ResourceSet resourceSet = ODMSCommon.Resources.MessageResource.ResourceManager.GetResourceSet(new CultureInfo(cultureString), true, true);
            var items = resourceSet.OfType<DictionaryEntry>();
            s += string.Join(",", items.Select(w => w.Key.ToString() + ":\"" + w.Value.ToString().Replace("\n", " ").Replace("\r", "") + "\"").ToArray());
            s += "}";
            return s;
        }
        /// <summary>
        /// multi select combodan gelen csv valusuna '' ekler
        /// </summary>
        /// <param name="str"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string AddSingleQuote(this string str, string seperator = ",")
        {
            if (str == null)
                return null;

            var arr = str.Split(new[] { seperator }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var item in arr)
            {
                sb.Append("'").Append(item).Append("',");
            }
            var returnValue = sb.ToString();
            return returnValue.Length > 0 ? returnValue.Substring(0, returnValue.Length - 1) : string.Empty;
            //return sb.ToString();
        }

    }


    /// <summary>
    /// This attribute is used to represent a string value
    /// for a value in an enum.
    /// </summary>
    public class StringValueAttribute : Attribute
    {

        #region Properties

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="resourceKey"></param>
        public StringValueAttribute(string resourceKey)
        {
            StringValue = CommonUtility.GetResourceValue(resourceKey);
        }

        #endregion

    }





}
