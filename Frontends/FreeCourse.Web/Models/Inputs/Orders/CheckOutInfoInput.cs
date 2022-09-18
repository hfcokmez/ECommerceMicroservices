using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FreeCourse.Web.Models.Inputs.Orders
{
    public class CheckOutInfoInput
    {
        [Display(Name="Province")]
        public string Province { get; set; }
        [Display(Name="District")]
        public string District { get; set; }
        [Display(Name="Street")]
        public string Street { get; set; }
        [Display(Name="Zip Code")]
        public string ZipCode { get; set; }
        [Display(Name="Line")]
        public string Line { get; set; }
        [Display(Name="Card Name and Surname")]
        public string CardName { get; set; }
        [Display(Name="Card Number")]
        public string CardNumber { get; set; }        
        [Display(Name="Expiration Date MM/YY")]
        public string Expiration { get; set; }
        [Display(Name="Security Code (CVV)")]
        public string CVV { get; set; }
    }
}