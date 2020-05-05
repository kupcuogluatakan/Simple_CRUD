using ODMSData.Utility;
using ODMSModel;
using ODMSModel.PartCostService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSData
{
    public class PartCostServiceData : DataAccessBase
    {
        private readonly DbHelper _dbHelper;
        public PartCostServiceData()
        {
            _dbHelper = new DbHelper();
        }
        public List<PartCostServiceModel> GetPart(Int64? id, int? seq)
        {
            return _dbHelper.ExecuteListReader<PartCostServiceModel>("P_GET_CONFIRM_GUARANTEE_STATUS_PART", MakeDbNull(id), seq);
        }
        public void SetPartCostVAlue(List<PartCostServiceModel> model)
        {
            var retVal = new ModelBase();
            var dt = CreateDataTableFromList(model);
            _dbHelper.ExecuteNonQuery("P_SET_PART_COST_VALUE", dt);
        }
        private DataTable CreateDataTableFromList(List<PartCostServiceModel> list)
        {
            DataTable table = new DataTable();

            DataColumn col1 = new DataColumn("PART_ID");
            DataColumn col2 = new DataColumn("AVG_COST");
            DataColumn col3 = new DataColumn("G_ID");
            DataColumn col4 = new DataColumn("G_SEQ");
            DataColumn col5 = new DataColumn("WOD_ID");

            col1.DataType = System.Type.GetType("System.Int64");
            col2.DataType = System.Type.GetType("System.Decimal");
            col3.DataType = System.Type.GetType("System.Int64");
            col4.DataType = System.Type.GetType("System.Int32");
            col5.DataType = System.Type.GetType("System.Int64");

            table.Columns.Add(col1);
            table.Columns.Add(col2);
            table.Columns.Add(col3);
            table.Columns.Add(col4);
            table.Columns.Add(col5);

            foreach (var item in list)
            {
                DataRow row = table.NewRow();
                row[0] = item.PartId;
                row[1] = item.Avg_Cost;
                row[2] = item.GuaranteeId;
                row[3] = item.GuaranteeSeq;
                row[4] = item.WorkOrderDetailId;
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
