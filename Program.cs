using System;
using Microsoft.EntityFrameworkCore;

namespace console
{
    public class PESSOA
    {
        public int ID_PESSOA { get; set; }
        public string NOME { get; set; }
    }

    public class MyContext : DbContext 
    { 
        public MyContext() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            string connectionString = 
            "User=SYSDBA;" +
            "Password=masterkey;" +
            "Database=c:\\dype\\db\\gestor.dev.trunk.fdb;" +
            "DataSource=localhost;" +
            "Port=3050;" +
            "Dialect=3;" +
            "Charset=NONE;" +
            "Role=;" +
            "Connection lifetime=15;" +
            "Pooling=true;" +
            "MinPoolSize=0;" +
            "MaxPoolSize=50;" +
            "Packet Size=8192;" +
            "ServerType=0";
            optionsBuilder.UseFirebird(connectionString);   
        }        
        public DbSet<PESSOA> PESSOA { get; set; } 


       protected override void OnModelCreating(ModelBuilder modelo)
       {
           //Fluent Api
           modelo.Entity<PESSOA>(entity =>
           {
               entity.HasKey(e => e.ID_PESSOA);
           });
       }   
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World?!");
            var tmpContext = new MyContext();
            tmpContext.Database.OpenConnection();
            tmpContext.Database.ExecuteSqlCommand("execute procedure dypedev_force_login2('dype')");
            tmpContext.PESSOA.Add(new PESSOA {NOME="Teste1234567890"});
            tmpContext.SaveChanges();
        }
    }
}
