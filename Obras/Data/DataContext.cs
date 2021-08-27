using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Project.Models;
using Microsoft.EntityFrameworkCore;
namespace Base.Project.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            :base(options)
        {             
        }
        
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
         
       

    }
}
