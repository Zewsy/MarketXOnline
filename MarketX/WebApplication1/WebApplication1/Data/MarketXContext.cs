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

        public DbSet<Advertisement> Advertisements { get; set; } = null!;
        public DbSet<AdvertisementPhoto> AdvertisementPhotos { get; set; } = null!;
        public DbSet<AdvertisementProperty> AdvertisementProperties { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<CategoryProperty> CategoryProperties { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<County> Counties { get; set; } = null!;
        public DbSet<MarkType> MarkTypes { get; set; } = null!;
        public DbSet<MessageContent> MessageContents { get; set; } = null!;
        public DbSet<MessageHeader> MessageHeaders { get; set; } = null!;
        public DbSet<Property> Properties { get; set; } = null!;
        public DbSet<Rating> Ratings { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<WrongAdvertisementMark> WrongAdvertisementMarks { get; set; } = null!;
        public DbSet<PropertyValue> PropertyValues { get; set; } = null!;

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
