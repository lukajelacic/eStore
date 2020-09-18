using eStore_web.EF_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF
{
    public class eContext : DbContext
    {
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<Regija> Regija { get; set; }
        public DbSet<Grad> Grad { get; set; }
        public DbSet<EmailAddress> EmailAddress { get; set; }
        public DbSet<LoginInfo> LoginInfo { get; set; }
        public DbSet<KupacKupuje> KupacKupuje { get; set; }
        public DbSet<Igra> Igra { get; set; }
        public DbSet<GameGenre> GameGenre { get; set; }
        public DbSet<RatingCategorie> RatingCategorie { get; set; }
        public DbSet<Refund> Refund { get; set; }
        public DbSet<Recenzija> Recenzija { get; set; }
        public DbSet<PrijavaRecenzije> PrijavaRecenzije { get; set; }
        public DbSet<Novost> Novost { get; set; }
        public DbSet<PreuzimanjeIgre> PreuzimanjeIgre { get; set; }
        public DbSet<Osoba> Osoba { get; set; }
        public DbSet<Kupac> Kupac { get; set; }
        public DbSet<Developer> Developer { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Popust> Popust { get; set; }
        public DbSet<IgricaImage> IgricaImage { get; set; }
        public DbSet<OsobaImage> OsobaImage { get; set; }
        public DbSet<Prati> Prati { get; set; }
        public DbSet<WalletHistory> WalletHistory { get; set; }
        public DbSet<PrijavaPremium> PrijavaPremium { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Notifikacije> Notifikacija { get; set; }

       
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            optionsBuilder.UseSqlServer("Server=app.fit.ba,1431;Database=p1803;Trusted_Connection=False;MultipleActiveResultSets=true;User ID=mk;Password=Xqtb03_6");
            
            //optionsBuilder.UseSqlServer("Server=den1.mssql8.gear.host;Database=eStore1;Trusted_Connection=False;MultipleActiveResultSets=true;User ID=estore1;Password=Au3Cthp!_g8w");
            //optionsBuilder.UseSqlServer("Server=.;Database=eStore;Trusted_Connection=True;");
            optionsBuilder.UseSqlServer("Server=.;Database=finalStore;Trusted_Connection=True;");

            // optionsBuilder.UseSqlServer("Server=p1803.app.fit.ba;Database=RS1;Trusted_Connection=False;MultipleActiveResultSets=true;User ID=P1803_;Password=ENxR8B!");
            // optionsBuilder.UseSqlServer("Server=den1.mssql7.gear.host;Database=eStore1;Trusted_Connection=False;MultipleActiveResultSets=true;User ID=estore1;Password=Lx2Kl!8f60_K");
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Osoba>()
                .HasOne(a => a.EmailAddress)
                .WithOne(b => b.Osoba)
                .HasForeignKey<EmailAddress>(b => b.OsobaID);

            modelBuilder.Entity<Osoba>()
                .HasOne(a => a.LoginInfo)
                .WithOne(b => b.Osoba)
                .HasForeignKey<LoginInfo>(b => b.OsobaID);

            modelBuilder.Entity<Kupac>()
                .HasOne(a => a.Wallet)
                .WithOne(b => b.Kupac)
                .HasForeignKey<Wallet>(b => b.KupacID);
           
            modelBuilder.Entity<WishList>()
            .HasKey(c => new { c.KupacID, c.IgraID });

            modelBuilder.Entity<KupacKupuje>()
            .HasKey(c => new { c.KupacID, c.IgraID });

            modelBuilder.Entity<IgricaImage>(entity => {
                entity.HasIndex(e => e.IgraID).IsUnique();
            });


            modelBuilder.Entity<OsobaImage>(entity => {
                entity.HasIndex(oi => oi.OsobaID).IsUnique();
            });
        }

    }

}
