using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjeA.Models
{
    public class Information
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Photo { get; set; }
        public string Iban { get; set; }
        public string LinkedIn { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string Tiktok { get; set; }
        public string WebSite { get; set; }
        public string QrCode { get; set; }


        public string NameDurum { get; set; }
        public string SurnameDurum { get; set; }
        public string NumberDurum { get; set; }
        public string EmailDurum { get; set; }
        public string AdressDurum { get; set; }
        public string PhotoDurum { get; set; }
        public string IbanDurum { get; set; }
        public string LinkedInDurum { get; set; }
        public string CompanyDurum { get; set; }
        public string TitleDurum { get; set; }
        public string InstagramDurum { get; set; }
        public string FacebookDurum { get; set; }
        public string TiktokDurum { get; set; }
        public string WebSiteDurum { get; set; }

        public string User_Id { get; set; }

    }
}