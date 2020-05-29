using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double BaseSalary { get; set; }
        public DateTime BirthDate { get; set; }
        public Department Department { get; set; }
        public ICollection<SalesRecord> SalesRecords { get; set; } = new List<SalesRecord>();

        public Seller() { }

        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.BaseSalary = baseSalary;
            this.BirthDate = birthDate;
            this.Department = department;
        }

        public void AddSales(SalesRecord salesRecord)
        {
            this.SalesRecords.Add(salesRecord);
        }

        public void RemoveSales(SalesRecord salesRecord)
        {
            this.SalesRecords.Remove(salesRecord);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return this.SalesRecords.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}