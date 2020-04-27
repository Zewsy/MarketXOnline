using MarketX.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarketX.DAL
{
    public class MarketXContext : IdentityDbContext<User, Role, int>
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
        public DbSet<User> ShopUsers { get; set; } = null!;
        public DbSet<WrongAdvertisementMark> WrongAdvertisementMarks { get; set; } = null!;
        public DbSet<PropertyValue> PropertyValues { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Properties to Advertisements
            modelBuilder.Entity<AdvertisementProperty>()
                .HasKey(ap => new { ap.AdvertisementId, ap.PropertyId });
            modelBuilder.Entity<AdvertisementProperty>()
                .HasOne(ap => ap.Advertisement)
                .WithMany(a => a.AdvertisementProperties)
                .HasForeignKey(ap => ap.AdvertisementId);
            modelBuilder.Entity<AdvertisementProperty>()
                .HasOne(ap => ap.Property)
                .WithMany(p => p.AdvertisementProperties)
                .HasForeignKey(ap => ap.PropertyId);

            //Categories to Properties
            modelBuilder.Entity<CategoryProperty>()
                .HasKey(cp => new { cp.CategoryId, cp.PropertyId });
            modelBuilder.Entity<CategoryProperty>()
                .HasOne(cp => cp.Category)
                .WithMany(c => c.CategoryProperties)
                .HasForeignKey(cp => cp.CategoryId);
            modelBuilder.Entity<CategoryProperty>()
                .HasOne(cp => cp.Property)
                .WithMany(p => p.CategoryProperties)
                .HasForeignKey(cp => cp.PropertyId);

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
                .HasForeignKey(a => a.SellerId);
            modelBuilder.Entity<Advertisement>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.BuyingAdvertisements)
                .HasForeignKey(a => a.CustomerId);

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
