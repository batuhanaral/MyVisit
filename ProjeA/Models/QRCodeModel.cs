using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjeA.Models
{
    public class QRCodeModel
    {
        [Key]
        public int Id { get; set; }
        public string QRCodeText { get; set; }
        public string QRCodeAdress { get; set; }
    }
}