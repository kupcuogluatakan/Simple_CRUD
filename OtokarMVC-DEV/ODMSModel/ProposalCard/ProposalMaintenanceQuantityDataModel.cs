using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.ProposalCard
{
    [Validator(typeof(ProposalMaintenanceQuantityDataModelValidator))]
    public class ProposalMaintenanceQuantityDataModel : ProposalQuantityDataModel
    {
        public bool IsMust { get; set; }
        public bool DiffrentBrandAllowed { get; set; }
        public bool AlternateAllowed { get; set; }
        public int MaintenanceId { get; set; }
        public string NewPartId { get; set; }
    }
}
