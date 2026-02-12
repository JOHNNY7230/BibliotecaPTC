using Microsoft.EntityFrameworkCore;
using BibliotecaPTC.Models;

namespace BibliotecaPTC.Data
{
    public class BibliotecaContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;database=biblioteca;user=root;password=;",
                new MySqlServerVersion(new Version(8, 0, 21))
            );
        }
    }
}
