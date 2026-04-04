using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
   public class NewUserDto
    {
        public string? ApplicationUserId { get; set; }

        // Extended fields
        public string? JobTitle { get; set; }
        public string? Department { get; set; }
        public int YearsOfExperience { get; set; }
        public string? Skills { get; set; }
        public string? Company { get; set; }
  
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;


        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? RoleRef { get; set; }

        public string? PhoneNumber { get; set; }
       


    }
}
