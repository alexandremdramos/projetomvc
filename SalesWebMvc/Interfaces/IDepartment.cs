using SalesWebMvc.Models;
using System;
using System.Collections.Generic;

namespace SalesWebMvc.Interfaces
{
    public interface IDepartment
    {
        int Id { get; set; }
        string Name { get; set; }
        ICollection<Seller> Sellers { get; set; }
        void AddSeller(Seller seller);
        double TotalSales(DateTime initial, DateTime final);
    }
}
