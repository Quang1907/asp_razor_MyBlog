﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP_RAZOR_5.Models
{
    public class MyBlogContext : IdentityDbContext<IdentityUser>
    {
        public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
        {
            // .. 
        } 

        public DbSet<Article> articles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder )
        {
            base.OnModelCreating(modelBuilder);

            foreach( var entityType in modelBuilder.Model.GetEntityTypes() )
            {
                var tableName = entityType.GetTableName() ?? "";
                if ( tableName.StartsWith("AspNet") )
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
        }
    }
}
