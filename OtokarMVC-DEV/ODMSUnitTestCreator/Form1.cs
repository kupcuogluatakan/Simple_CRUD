using ODMSCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ODMSUnitTestCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var basePath = @"C:\UnitTestCreator\";
            Assembly asm = Assembly.Load("ODMSBusiness");
            var list = asm.GetTypes();
            var errorList = new Dictionary<string, List<string>>();



            foreach (var obj in list)
            {
                var builder = new StringBuilder();
                var builderBody = new StringBuilder();
                var builderAddNameSpace = new StringBuilder();
                var objName = obj.Name;
                var baseType = obj.BaseType;
                var path = string.Format("{0}{1}Test.cs", basePath, objName);
                if (baseType != null && (baseType.Name == "BaseBusiness" || baseType.Name.Contains("BaseService")))
                {
                    var methods = obj.GetMethods();
                    var orderMethods = new Dictionary<int, MethodInfo>();

                    var guid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
                    var code = string.Format("UnitTest_{0}_{1}", guid, DateTime.Now.ToString("ddMMyyyy"));

                    int ordInsert = 1;
                    int ordGet = 50;
                    int ordList = 100;
                    var dmlContains = false;
                    methods.ToList().ForEach(x =>
                    {
                        if (x.Name.Contains("DML") || x.Name.Contains("Save") || x.Name.Contains("Insert") || x.Name.Contains("Update"))
                        {
                            if (orderMethods.ContainsKey(1))
                                ordInsert++;

                            orderMethods.Add(ordInsert, x);
                            dmlContains = true;
                        }
                        else if (x.Name.Contains("Get") && !x.Name.Contains("List"))
                        {
                            if (orderMethods.ContainsKey(50))
                                ordGet++;
                            orderMethods.Add(ordGet, x);
                        }
                        else if (x.Name.Contains("List") || (x.Name.Contains("Get") && x.Name.Contains("List")))
                        {
                            if (orderMethods.ContainsKey(100))
                                ordList++;
                            orderMethods.Add(ordList, x);
                        }

                    });

                    var methodList = new List<MethodInfo>();
                    orderMethods.OrderBy(x => x.Key).ToList().ForEach(x =>
                    {
                        methodList.Add(x.Value);
                    });

                    UnitTestHandler parent = null;
                    foreach (var method in methodList)
                    {
                        UnitTestHandler insert = new UnitTestInsertHandler();
                        UnitTestHandler getAll = new UnitTestGetAllHandler();
                        UnitTestHandler getModel = new UnitTestGetModelHandler();
                        insert.AfterHandler = getAll;
                        getAll.AfterHandler = getModel;

                        var name = method.Name;
                        var typeName = method.ReturnType.Name;
                        var paramList = method.GetParameters();

                        //ResponseModel dönmeyen business lar listeye alınır
                        if (typeName != "ResponseModel`1")
                        {
                            if (errorList.ContainsKey(objName))
                            {
                                errorList[objName].Add(typeName);
                            }
                            else
                            {
                                errorList.Add(objName, new List<string>() { typeName });
                            }
                        }
                        else
                        {
                            var paramString = string.Empty;
                            paramList.ToList().ForEach(x =>
                            {
                                //parametre namespaceleri toplanır
                                if (!builderAddNameSpace.ToString().Contains(x.ParameterType.Namespace))
                                {
                                    builderAddNameSpace.Append(string.Format("using {0};", x.ParameterType.Namespace));
                                    builderAddNameSpace.Append(Environment.NewLine);
                                }

                                paramString = string.Format("{0}###{1}={2}-{3}", paramString, x.Name, x.ParameterType.Namespace, x.ParameterType.Name);
                            });

                            if (paramString.Length > 0)
                                paramString = paramString.Substring(3, paramString.Length - 3);


                            //unit test adımları yazılır
                            if (paramString.Length > 0)
                            {
                                insert.Create(parent, objName, name, paramString, dmlContains);
                            }

                            if (insert.InsertBuilder.Length > 0)
                            {
                                parent = insert;
                            }

                            builderBody.Append(insert.Builder.ToString());
                            builderBody.Append(getAll.Builder.ToString());
                            builderBody.Append(getModel.Builder.ToString());
                        }

                    }

                    if (File.Exists(path))
                        File.Delete(path);

                    builder.Append("using System;");
                    builder.Append(Environment.NewLine);
                    builder.Append("using Microsoft.VisualStudio.TestTools.UnitTesting;");
                    builder.Append(Environment.NewLine);
                    builder.Append("using ODMSBusiness;");
                    builder.Append(Environment.NewLine);
                    builder.Append("using ODMSCommon.Security;");
                    builder.Append(Environment.NewLine);
                    builder.Append("using System.Linq;");
                    builder.Append(Environment.NewLine);
                    builder.Append(builderAddNameSpace.ToString());
                    builder.Append(Environment.NewLine);
                    builder.Append(Environment.NewLine);
                    builder.Append("namespace ODMSUnitTest");
                    builder.Append(Environment.NewLine);
                    builder.Append("{");
                    builder.Append(Environment.NewLine);
                    builder.Append(Environment.NewLine);
                    builder.Append("\t");
                    builder.Append("[TestClass]");
                    builder.Append(Environment.NewLine);
                    builder.Append("\t");
                    builder.Append(string.Format("public class {0}Test", objName));
                    builder.Append(Environment.NewLine);
                    builder.Append("\t");
                    builder.Append("{");
                    builder.Append(Environment.NewLine);
                    builder.Append(Environment.NewLine);
                    builder.Append("\t\t");
                    builder.Append(string.Format("{0} _{0} = new {0}();", objName));
                    builder.Append(Environment.NewLine);
                    //builder.Append("\t\t");
                    //builder.Append(string.Format("int Id = 0;", objName));
                    //builder.Append(Environment.NewLine);

                    ////////////////////////////////
                    //birleştirme yap
                    ////////////////////////////////

                    builder.Append(builderBody.ToString());

                    ////////////////////////////////
                    ////////////////////////////////

                    builder.Append(Environment.NewLine);
                    builder.Append(Environment.NewLine);
                    builder.Append("\t");
                    builder.Append("}");
                    builder.Append(Environment.NewLine);
                    builder.Append(Environment.NewLine);
                    builder.Append("}");
                    builder.Append(Environment.NewLine);
                    builder.Append(Environment.NewLine);
                    File.AppendAllText(path, builder.ToString());
                }
            }



        }
    }
}
