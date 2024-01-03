using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using QuizApp.Helpers;
using QuizApp.Models;
using QuizApp.Models.Database;
using QuizApp.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Services
{
    public class OfferService
    {
        private DataContext _context;

        public OfferService(Models.DataContext context)
        {
            _context = context;
        }
        public dboKullanicilar Login(Users users)
        {
            string hashedPassword = Encryption.HashPassword(users.Password);
            try
            {
                var res = _context.Kullanicilar.Where(a => a.Email == users.Email && a.Sifre == hashedPassword).FirstOrDefault();
                if (res == null)
                {
                    return null;
                }
                return res;
            }
            catch (Exception)
            {
            }
            return null;
        }    

        //kolay sorular için çek
        public List<QuizApp.Models.Database.dboSorular> SorucekK(Models.Inputs.Sorular sorular)
        {            
            try
            {
                List<QuizApp.Models.Database.dboSorular> res = _context.Sorular.Where(x => x.Soru_Seviyesi == "K").ToList();
                return res;
            }
            catch (Exception)
            {
                return null;
            }
        }
        //orta sorular için çek
        public List<QuizApp.Models.Database.dboSorular> SorucekO(Models.Inputs.Sorular sorular)
        {
            try
            {
                List<QuizApp.Models.Database.dboSorular> res = _context.Sorular.Where(x => x.Soru_Seviyesi == "O").ToList();
                return res;
            }
            catch (Exception)
            {
                return null;
            }
        }
        //zor sorular için çek
        public List<QuizApp.Models.Database.dboSorular> SorucekZ(Models.Inputs.Sorular sorular)
        {
            try
            {
                List<QuizApp.Models.Database.dboSorular> res = _context.Sorular.Where(x => x.Soru_Seviyesi == "Z").ToList();
                return res;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> SignUp(SignUp signUp)
        {
            try
            {
                dboKullanicilar dbo = new dboKullanicilar();
                dbo.Ad = signUp.Ad;
                dbo.Soyad = signUp.Soyad;
                dbo.Dogum_Tarihi = signUp.Dogum_Tarihi;
                dbo.Egitimi = signUp.Egitimi;
                dbo.GSM_No = signUp.GSM_No;
                dbo.Il = signUp.Il;
                dbo.Ilce = signUp.Ilce;
                dbo.Adres = signUp.Adres;
                dbo.Email = signUp.Email;
                dbo.KVKK_Onayi = true;
                dbo.Sozlesme_Onayi = true;
                dbo.Sifre = Encryption.HashPassword(signUp.Sifre);
                // Veriyi veritabanına kaydet
                _context.Kullanicilar.Add(dbo);
                await _context.SaveChangesAsync();
                // Eklenen veriyi geri döndür
                return true;
            }
            catch (Exception ex)
            {
                // Hata durumlarını yönetmek için burada uygun bir işlem yapılabilir.
                // Hata mesajını veya detaylarını loglamak veya uygun şekilde işlemek gibi.
            }
            return false;
        }
        public async Task<bool> SinavSkor(Models.Inputs.SinavSkor sinavSkor)
        {
            try
            {
                Models.Database.dboSinavSkor dbos = new Models.Database.dboSinavSkor();
                dbos.Sinav_Tarihi = DateTime.Now;
                dbos.Soru_Seviyesi = "Z";
                dbos.Dogru_Cevap = sinavSkor.Dogru_Cevap;
                dbos.Yanlis_Cevap = sinavSkor.Yanlis_Cevap;
                dbos.UyeId = sinavSkor.UyeId;
                dbos.Skor = sinavSkor.Skor;
                dbos.Kazanc_Bedeli = 2;
                // Veriyi veritabanına kaydet
                _context.SinavSkor.Add(dbos);
                await _context.SaveChangesAsync();
                // Eklenen veriyi geri döndür
                return true;
            }
            catch (Exception ex)
            {
                // Hata durumlarını yönetmek için burada uygun bir işlem yapılabilir.
                // Hata mesajını veya detaylarını loglamak veya uygun şekilde işlemek gibi.
            }
            return false;
        }
        public async Task<bool> CevapKontrol(Models.Database.dboSorular sbh)
        {
            try
            {
                var res = _context.Sorular.Where(x => x.Soru_Kodu == sbh.Soru_Kodu).FirstOrDefault();
                if (res.Dogru_Cevap.Trim() == sbh.Dogru_Cevap.Trim())
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        public async Task<bool> Sorular(Models.Database.dboSorular sbh)
        {
            try
            {
                var res = _context.Sorular.Where(x => x.Id == sbh.Id).FirstOrDefault();
                if (res.Dogru_Cevap.Trim() == sbh.Dogru_Cevap.Trim())
                {
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }
}
