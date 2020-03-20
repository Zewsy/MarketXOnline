using MarketX.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketX.Data
{
    public class MarketXContext : DbContext
    {
        public MarketXContext(DbContextOptions<MarketXContext> options)
            : base(options)
        {
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdvertisementPhoto> AdvertisementPhotos { get; set; }
        public DbSet<AdvertisementProperty> AdvertisementProperties { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProperty> CategoryProperties { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<MarkType> MarkTypes { get; set; }
        public DbSet<MessageContent> MessageContents { get; set; }
        public DbSet<MessageHeader> MessageHeaders { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WrongAdvertisementMark> WrongAdvertisementMarks { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Properties to Advertisements
            modelBuilder.Entity<AdvertisementProperty>()
                .HasKey(ap => new { ap.AdvertisementID, ap.PropertyID });
            modelBuilder.Entity<AdvertisementProperty>()
                .HasOne(ap => ap.Advertisement)
                .WithMany(a => a.AdvertisementProperties)
                .HasForeignKey(ap => ap.AdvertisementID);
            modelBuilder.Entity<AdvertisementProperty>()
                .HasOne(ap => ap.Property)
                .WithMany(p => p.AdvertisementProperties)
                .HasForeignKey(ap => ap.PropertyID);

            //Categories to Properties
            modelBuilder.Entity<CategoryProperty>()
                .HasKey(cp => new { cp.CategoryID, cp.PropertyID });
            modelBuilder.Entity<CategoryProperty>()
                .HasOne(cp => cp.Category)
                .WithMany(c => c.CategoryProperties)
                .HasForeignKey(cp => cp.CategoryID);
            modelBuilder.Entity<CategoryProperty>()
                .HasOne(cp => cp.Property)
                .WithMany(p => p.CategoryProperties)
                .HasForeignKey(cp => cp.PropertyID);

            //Messages to Users
            modelBuilder.Entity<MessageHeader>()
                .HasOne(mh => mh.Recipient)
                .WithMany(r => r.ReceivedMessages)
                .HasForeignKey(mh => mh.RecipientID);

            modelBuilder.Entity<MessageHeader>()
                .HasOne(mh => mh.Sender)
                .WithMany(s => s.SentMessages)
                .HasForeignKey(mh => mh.SenderID);

            modelBuilder.Entity<MessageHeader>()
                .HasOne(mh => mh.Owner)
                .WithMany(o => o.OwnMessages)
                .HasForeignKey(mh => mh.OwnerID);

            //Advertisements to Users
            modelBuilder.Entity<Advertisement>()
                .HasOne(a => a.Seller)
                .WithMany(s => s.SellingAdvertisements)
                .HasForeignKey(a => a.SellerID);
            modelBuilder.Entity<Advertisement>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.BuyingAdvertisements)
                .HasForeignKey(a => a.CustomerID);

            //Saved Ads
            modelBuilder.Entity<SavedAdvertisementsUsers>()
                .HasKey(sau => new { sau.AdvertisementID, sau.UserID });
            modelBuilder.Entity<SavedAdvertisementsUsers>()
                .HasOne(sau => sau.Advertisement)
                .WithMany(sa => sa.SavedAdvertisementsUsers)
                .HasForeignKey(sau => sau.AdvertisementID);
            modelBuilder.Entity<SavedAdvertisementsUsers>()
                .HasOne(sau => sau.User)
                .WithMany(u => u.SavedAdvertisementsUsers)
                .HasForeignKey(sau => sau.UserID);
        }
    }
}
