using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace QuizApp.Models.Database
{
    public class dboKullanicilar
    {
        [Key]
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime Dogum_Tarihi { get; set; }
        public string Egitimi { get; set; }
        public string GSM_No { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string Adres { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public int? Yaptıgı_Test_Adedi { get; set; }
        public bool KVKK_Onayi { get; set; }
        public bool Sozlesme_Onayi { get; set; }
        public Nullable<DateTime> Kayit_Tarihi { get; set; }

    }
}
