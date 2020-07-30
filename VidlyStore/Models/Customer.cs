using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace VidlyStore.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Customer's Name!")]
        [StringLength(255)]
        [Display(Name = "Customer Name")]
        public string Name { get; set; }

        [Display(Name = "Date Of Birth")]
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MemberShipType MemberShipType { get; set; }
        [Required(ErrorMessage = "Please Enter Membership Type! ")]
        [Display(Name = "Membership Type")]
        public byte MemberShipTypeId { get; set; }

        public String Title
        {
            get
            {
                if (this.Id !=0)
                {
                    return "Edit Customer";
                }

                return "New Customer";
            }
        }

    }
}