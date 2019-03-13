using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Interfaces;

namespace SalesWebMvc.Models
{
    public class Department : IDepartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
