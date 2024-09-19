
using LinkDev.IKEACompany.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEACompany.DAL.Persistance.Data.Configurations.Departments
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.Property(D => D.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(D => D.Code).HasColumnType("varchar(20)").IsRequired();
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETDATE()");
            //builder.Property(D => D.CreatedBy).HasDefaultValue("");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");
            //builder.Property(D => D.LastModifiedBy).HasComputedColumnSql();

            //builder.Property(D => D.CreationDate).HasComputedColumnSql("Convert(date, GETDATE())"); 
        }
    }
}
