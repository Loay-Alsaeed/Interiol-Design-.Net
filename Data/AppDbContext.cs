using Backend_.Net.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend_.Net.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Design> Designs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<DesignDescription> DesignDescriptions { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<KeyFeature> KeyFeatures { get; set; }
        public DbSet<LayoutImage> LayoutImages { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<DesignComment> DesignComments { get; set; }
        public DbSet<DesignMaterial> DesignMaterials { get; set; }
        //public DbSet<DesignColor> DesignColors { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Consideration> Considerations { get; set; }
        public DbSet<DesignConcept> DesignConcepts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // LayoutImage ↔ Design (One-to-One)
            //modelBuilder.Entity<Design>()
            //    .HasOne(d => d.LayoutImage)
            //    .WithOne(l => l.Design)
            //    .HasForeignKey<Design>(d => d.LayoutImageId);

            // Design → Images (One-to-Many)
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Design)
                .WithMany(d => d.Images)
                .HasForeignKey(i => i.DesignId);
            // Design → DesignConcept (One-to-Many)
            modelBuilder.Entity<DesignConcept>()
                .HasOne(i => i.Design)
                .WithMany(d => d.Concepts)
                .HasForeignKey(i => i.DesignId);


            // Design → Style (Many-to-One)
            //modelBuilder.Entity<Design>()
            //    .HasOne(d => d.DesignStyle)
            //    .WithMany(s => s.Designs)
            //    .HasForeignKey(d => d.StyleId);

            // Design → Category (Many-to-One)
            //modelBuilder.Entity<Design>()
            //    .HasOne(d => d.DesignCategory)
            //    .WithMany(c => c.Designs)
            //    .HasForeignKey(d => d.CategoryId);

            // Design → Designer (Many-to-One)
            modelBuilder.Entity<Design>()
                .HasOne(d => d.Designer)
                .WithMany()
                .HasForeignKey(d => d.DesignerId);

            // Designer → User (One-to-One)
            modelBuilder.Entity<Designer>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);

            // Design → Descriptions (One-to-Many)
            modelBuilder.Entity<DesignDescription>()
                .HasOne(dd => dd.Design)
                .WithMany(d => d.Descriptions)
                .HasForeignKey(dd => dd.DesignId);

            // Design → KeyFeatures (One-to-Many)
            modelBuilder.Entity<KeyFeature>()
                .HasOne(kf => kf.Design)
                .WithMany(d => d.KeyFeatures)
                .HasForeignKey(kf => kf.DesignId);

            // Design → Color (One-to-Many)
            //modelBuilder.Entity<Color>()
            //    .HasOne(kf => kf.Design)
            //    .WithMany(d => d.Colors)
            //    .HasForeignKey(kf => kf.DesignId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // Design → Considerations (One-to-Many)
            modelBuilder.Entity<Consideration>()
                .HasOne(c => c.Design)
                .WithMany(d => d.DesignConsiderations)
                .HasForeignKey(c => c.DesignId);

            // Design ↔ Comment (Many-to-Many via DesignComment)
            modelBuilder.Entity<DesignComment>()
                .HasOne(dc => dc.Design)
                .WithMany(d => d.DesignComments)
                .HasForeignKey(dc => dc.DesignId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DesignComment>()
                .HasOne(dc => dc.Comment)
                .WithMany()
                .HasForeignKey(dc => dc.CommentId)
                 .OnDelete(DeleteBehavior.Cascade);

            // Design ↔ Material (Many-to-Many via DesignMaterial)
            //modelBuilder.Entity<DesignMaterial>()
            //    .HasOne(dm => dm.Design)
            //    .WithMany(d => d.DesignMaterials)
            //    .HasForeignKey(dm => dm.DesignId);

            modelBuilder.Entity<DesignMaterial>()
                .HasOne(dm => dm.Material)
                .WithMany()
                .HasForeignKey(dm => dm.MaterialId);

            // Design ↔ Color (Many-to-Many via DesignColor)
            //modelBuilder.Entity<DesignColor>()
            //    .HasOne(dc => dc.Design)
            //    .WithMany(d => d.DesignColors)
            //    .HasForeignKey(dc => dc.DesignId);

            //modelBuilder.Entity<DesignColor>()
            //    .HasOne(dc => dc.Color)
            //    .WithMany()
            //    .HasForeignKey(dc => dc.ColorId);
        }
    }
}
