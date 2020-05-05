using ODMSCommon.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSUnitTestCreator
{
    public abstract class UnitTestHandler
    {

        private StringBuilder _insertBuilder;
        public StringBuilder InsertBuilder
        {
            get
            {
                if (_insertBuilder == null)
                    _insertBuilder = new StringBuilder();
                return _insertBuilder;
            }
            set
            {
                _insertBuilder = value;
            }
        }

        private StringBuilder _updateBuilder;
        public StringBuilder UpdateBuilder
        {
            get
            {
                if (_updateBuilder == null)
                    _updateBuilder = new StringBuilder();
                return _updateBuilder;
            }
            set
            {
                _updateBuilder = value;
            }
        }

        public string UpdateMethodName { get; set; }

        private StringBuilder _deleteBuilder;
        public StringBuilder DeleteBuilder
        {
            get
            {
                if (_deleteBuilder == null)
                    _deleteBuilder = new StringBuilder();
                return _deleteBuilder;
            }
            set
            {
                _deleteBuilder = value;
            }
        }

        public string DeleteMethodName { get; set; }

        private StringBuilder _builder;
        public StringBuilder Builder
        {
            get
            {
                if (_builder == null)
                    _builder = new StringBuilder();
                return _builder;
            }
            set
            {
                _builder = value;
            }
        }

        protected UnitTestHandler _AfterHandler;
        public UnitTestHandler AfterHandler { set { _AfterHandler = value; } }

        public abstract void Create(UnitTestHandler parent, string businessName, string methodName, string filterNames, bool dmlContains);


        #region Add Tab & Line

        public void AddNewLine()
        {
            Builder.Append(Environment.NewLine);
        }

        public void AddTab()
        {
            Builder.Append(Environment.NewLine);
            Builder.Append("\t");
        }

        public void AddTwoTab()
        {
            Builder.Append(Environment.NewLine);
            Builder.Append("\t\t");
        }

        public void AddThreeTab()
        {
            Builder.Append(Environment.NewLine);
            Builder.Append("\t\t\t");
        }


        #endregion

        #region Builder Add Tab & Line

        public void AddNewLine(StringBuilder builder)
        {
            builder.Append(Environment.NewLine);
        }

        public void AddTab(StringBuilder builder)
        {
            builder.Append(Environment.NewLine);
            builder.Append("\t");
        }

        public void AddTwoTab(StringBuilder builder)
        {
            builder.Append(Environment.NewLine);
            builder.Append("\t\t");
        }

        public void AddThreeTab(StringBuilder builder)
        {
            builder.Append(Environment.NewLine);
            builder.Append("\t\t\t");
        }

        #endregion

    }
}
