using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models.Database
{
    public class dboSinavSkor
    {
        [Key]
        public int Id { get; set; }
        public int UyeId { get; set; }
        public string Soru_Seviyesi { get; set; }
        public DateTime Sinav_Tarihi { get; set; }
        public int Yanlis_Cevap { get; set; }
        public int Dogru_Cevap { get; set; }
        public decimal Skor { get; set; }
        public decimal Kazanc_Bedeli { get; set; }
    }
}
