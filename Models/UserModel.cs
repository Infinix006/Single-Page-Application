using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MACHINE_TEST_PRACTICE.Models
{
    public class UserModel
    {
        public int Id { get; set; } = 0;

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string MobNo { get; set; }

        
        public string Photo { get; set; }

        [Required]
        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int CountryId { get; set; }

        
        public string CountryName { get; set; }

        [Required]
        public int StateId { get; set; }

        
        public string StateName { get; set; }


        public bool comm { get; set; } = false;
        public bool atwup { get; set; } = false;
        public bool dm { get; set; } = false;
        public bool tm { get; set; } = false;
        public bool sm { get; set; } = false;
        public bool cr { get; set; } = false;
        public bool lead { get; set; } = false;
        public bool adapt { get; set; } = false;
    }
}