using System;
using System.Collections.Generic;
using System.Text;
using DigiKala.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigiKala.DataAccessLayer.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Role>  Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Store{ get; set; }
    }
}
