using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationTest.Models;

namespace WebApiTest.DTO
{
    public class SQLDatabaseContext : DbContext
    {
        public SQLDatabaseContext(DbContextOptions<SQLDatabaseContext> options) : base(options)
        {

        }

        //DbSet mean Product table create in database using entity framework
        public DbSet<Product> Products { get; set; }

        //You can use appsettings.json Or below this
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=MyStudentDb; Integrated Security=True");
        }*/
    }
}
