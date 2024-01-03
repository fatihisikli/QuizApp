using QuizApp.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models.Inputs
{
    public class Sorular
    {
        [Key]
        public int Id { get; set; }
        public int Soru_Kodu { get; set; }
        public string Soru_Metni { get; set; }
        public string Dogru_Cevap { get; set; }
        public string Yanlis_Cevap1 { get; set; }
        public string Yanlis_Cevap2 { get; set; }
        public string Yanlis_Cevap3 { get; set; }
        public string Soru_Seviyesi { get; set; }
    }
}
