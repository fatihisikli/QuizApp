using QuizApp.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models.Inputs
{
    public class SignUp
    { 
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
        public int Yaptıgı_Test_Adedi { get; set; }
        public bool KVKK_Onayi { get; set; }
        public bool Sozlesme_Onayi { get; set; }
        public Nullable<DateTime> Kayit_Tarihi { get; set; }
        public DateTime SonSinavHakki { get; set; }
        public int Sinav_Hakki { get; set; }
    }
}
