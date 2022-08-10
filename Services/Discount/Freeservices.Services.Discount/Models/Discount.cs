﻿using System;
using Dapper.Contrib.Extensions;

namespace Freeservices.Services.Discount.Models
{
    [Table("discount")]
    public class Discount
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}