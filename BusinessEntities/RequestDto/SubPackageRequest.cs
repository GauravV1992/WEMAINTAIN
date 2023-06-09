﻿using BusinessEntities.Common;

namespace BusinessEntities.RequestDto
{
    public class SubPackageRequest : BaseRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PackageId { get; set; }
    }
}
