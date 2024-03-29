﻿using Microsoft.AspNetCore.Identity;
using PensionManagementUserLoginService.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PensionManagementPensionerService.Models
{
    public class PensionerDetails
    {
        [Key]
        public Guid PensionerId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string AadharNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
        public Guid PensionPlanId { get; set; }

        [ForeignKey("Id")]
        public virtual IdentityUser identityUser { get; set; }

        [ForeignKey("PensionPlanId")]
        public virtual PensionPlanDetails PensionPlanDetails { get; set; }
    }
}
