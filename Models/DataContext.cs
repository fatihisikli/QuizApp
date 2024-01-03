using Microsoft.EntityFrameworkCore;
using QuizApp.Models.Database;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Models
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlServer(@"Server=FATIH;Database=QuizApp;Trusted_Connection=True;TrustServerCertificate=True;");//Connection String.

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 
        }
        
        public DbSet<dboKullanicilar> Kullanicilar { get; set; }
        public DbSet<dboSorular> Sorular { get; set; }
        public DbSet<dboSinavSkor> SinavSkor { get; set; }




    }
}
