using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjeA.Models
{
    public class Contact
    {
        public class Member
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Message { get; set; }
            public bool? Durum { get; set; }
        }

        public class Foreing
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Message { get; set; }
            public bool? Durum { get; set; }

        }
    }
}