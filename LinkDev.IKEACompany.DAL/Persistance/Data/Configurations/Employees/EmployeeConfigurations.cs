using LinkDev.IKEACompany.DAL.Common.Enums;
using LinkDev.IKEACompany.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEACompany.DAL.Persistance.Data.Configurations.Employees
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E => E.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(E => E.Address).HasColumnType("varchar(100)").IsRequired();
            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)").IsRequired();

            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETUTCDATE()");

            builder.Property(E => E.Gender)
                .HasConversion(
                (gender) => gender.ToString(),
                (gender) => Enum.Parse<Gender>(gender)

                );

            builder.Property(E => E.EmployeeType)
               .HasConversion(
               (EmpType) => EmpType.ToString(),
               (EmpType) => Enum.Parse<EmployeeType>( EmpType)

               );
        }
    }
}
