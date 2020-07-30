using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VidlyStore.api;
using VidlyStore.Models;

namespace VidlyStore.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

      //  [Min18YearsIfAMember]
       

        public bool IsSubscribedToNewsletter { get; set; }

        
        [Required]
        public byte MemberShipTypeId { get; set; }

        public MembershipTypeDto MemberShipType { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}