﻿using API.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.ContextDB
{
    public class InstituteContext : DbContext
    {
        public InstituteContext()
        {
            connectionString = ConfigReader.GetConnectionString();
        }
        private string connectionString { get; set; }
        public DbSet<Institute> Institute { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
