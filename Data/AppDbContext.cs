using Microsoft.EntityFrameworkCore;
using MinhaApi.Models;

namespace MinhaApi.Data

{
    //Representação do Db
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        //Representação da tabela no Db
        public DbSet<TodoModel> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite("DataSource=app.db;Cache=Shared");
    }
} 