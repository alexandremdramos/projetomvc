using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;
using System;

namespace SalesWebMvc.Interfaces
{
    public interface ISalesRecord
    {
        int Id { get; set; }
        DateTime Date { get; set; }
        double Amount { get; set; }
        SaleStatus Status { get; set; }
        Seller Seller { get; set; }
    }
}
