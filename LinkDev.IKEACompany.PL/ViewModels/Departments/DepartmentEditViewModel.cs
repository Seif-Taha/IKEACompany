using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEACompany.PL.ViewModels.Departments
{
    public class DepartmentEditViewModel
    {
        //public int Id { get; set; }

        [Required(ErrorMessage = "Code Is Required Ya Hamda!!")]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        [Display(Name = "Date of Creation")]
        public DateOnly CreationDate { get; set; }
    }
}
