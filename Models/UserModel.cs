using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MACHINE_TEST_PRACTICE.Models
{
    public class UserModel
    {
        public int Id { get; set; } = 0;

        public string FullName { get; set; }

        public string Email { get; set; }

        public string MobNo { get; set; }

        public string Photo { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public DateTime DateOfBirth { get; set; }   

        public int CountryId { get; set; }

        public string CountryName { get; set; }

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