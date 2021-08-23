using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Enitites
{
    public class Customer : BaseEntity
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Required]
        [Duplication]
        public string CustomerCode { get; set; }

        [Required]
        public string Name { get; set; }

        public string GroupCustomer { get; set; }

        [Required]
        [Duplication]
        public string PhoneNumber { get; set; }

        public string DateOfBirth { get; set; }

        public string CompanyName { get; set; }

        public string TaxCode { get; set; }

        [Required]
        [Duplication]
        public string Email { get; set; }

        public string Address { get; set; }

        public string Note { get; set; }

    }
}
