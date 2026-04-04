using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class NewUserGroupDto
    {
        public string UserGroupName { get; set; } = string.Empty;
        public Guid UserGroupId { get; set; }
        public string AboutGroup { get; set; } = string.Empty;

        public string Contact  { get; set; }= string.Empty;
        public string Email { get; set; } = string.Empty;
        public int GroupTypeId { get; set; }
        public int AddressId { get; set; }
        public int GroupCategoryId { get; set; } = 2;

        //address
        public string City { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;




    }
}
