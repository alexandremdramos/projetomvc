using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Interfaces
{
    public interface ISeller
    {
        int Id { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        DateTime BirthDate { get; set; }
        double BaseSalary { get; set; }
        Department Department { get; set; }
        ICollection<SalesRecord> Sales { get; set; }
        void AddSales(SalesRecord salesRecord);
        void RemoveSales(SalesRecord salesRecord);
        double TotalSales(DateTime initial,DateTime final );
    }
}
