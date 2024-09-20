﻿using LinkDev.IKEACompany.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEACompany.DAL.Models.Employees
{
    public class Employee : ModelBase
    {
        //[Required]
        //[MaxLength(50, ErrorMessage = "Max Length is 50 Chars")]
        //[MinLength(5, ErrorMessage = "Min Length is 5 Chars")]
        public string Name { get; set; } = null!;

        //[Range(22, 30)]
        public int Age { get; set; }

        //[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
        //    ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }

        //[DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        //[Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        //[DataType(DataType.EmailAddress)]
        //[EmailAddress]
        public string? Email { get; set; }

        //[Display(Name = "Phone Number")]
        //[Phone]
        //[DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

    }
}