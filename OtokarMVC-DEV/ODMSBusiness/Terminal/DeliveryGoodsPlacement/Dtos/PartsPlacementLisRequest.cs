﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSModel.DeliveryGoodsPlacement;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos
{
    public class PartsPlacementListRequest:PartsPlacementListModel, IRequest<PartsPlacementListRequest,ListResponse<PartsPlacementListItem>>
    {
        public PartsPlacementListRequest(long seqNo)
        {
            DeliverySeqNo = seqNo;
        }
    }
}