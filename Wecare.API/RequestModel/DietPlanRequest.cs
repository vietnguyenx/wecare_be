﻿using Wecare.Repositories.Data.Entities.Enum;

namespace Wecare.API.RequestModel
{
    public class DietPlanRequest : BaseRequest
    {
        public DateOnly DateAssigned { get; set; }
        public String Period { get; set; }
        public Status Status { get; set; }
    }
}
