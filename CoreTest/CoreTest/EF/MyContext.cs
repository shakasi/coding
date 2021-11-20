﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTest.EF
{
    public class MyContext : DbContext
    {
        public DbSet<User> Users { set; get; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          => optionsBuilder
              .UseSqlite(@"data source=docare.db;");
              //.UseMySql(@"Server=localhost;database=migrationtest;uid=root;pwd=abcd;");
    }
}