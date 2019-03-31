using System;
using Microsoft.EntityFrameworkCore;
using NET_CMONO.Model;

namespace NET_CMONO.EfRepository
{
    public class PRMDbContext : DbContext
    {
        public PRMDbContext(DbContextOptions<PRMDbContext> options)
            : base(options)
        { }

        public DbSet<ConfigurationModel> Configurations { get; set; }

        public DbSet<ProjectModel> Projects { get; set; }

        public DbSet<ArticleModel> Articles { get; set; }

        public DbSet<ArticleTagModel> ArticleTags { get; set; }

        public DbSet<ArticleCategoryModel> ArticleCategories { get; set; }

        public DbSet<ArticleCategoryMappingModel> ArticleCategoryMappings { get; set; }

        public DbSet<ArticleTagMappingModel> ArticleTagMappings { get; set; }

        public DbSet<MenuModel> Menus { get; set; }

        public DbSet<UserModel> Users { get; set; }

        public DbSet<UserProfileModel> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.EnableAutoHistory(null);
        }
    }
}
