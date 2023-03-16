using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AidBackOfficeCRUD.Models;
using AidBackOfficeCRUD.Repositories;

namespace AidBackOfficeCRUD.Data
{
    public class AidBackOfficeCRUDContext : DbContext
    {
        public AidBackOfficeCRUDContext () {
        }

        public AidBackOfficeCRUDContext (DbContextOptions<AidBackOfficeCRUDContext> options)
            : base(options)
        {
        }
        public DbSet<MyUser> user { get; set; } = default!;
        public DbSet<UserStore> userStore { get; set; } = default!;
        
    }
}
