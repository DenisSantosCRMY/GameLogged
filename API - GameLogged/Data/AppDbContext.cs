using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Plataforma> Plataforma { get; set; }
        public DbSet<Jogo> Jogos { get; set; }
        public DbSet<Catalogo> Catalogos { get; set; }
        public DbSet<Conquista> Conquistas { get; set; }
        public DbSet<UsuarioConquista> UsuarioConquistas { get; set; }
        public DbSet<UsuarioConexao> UsuarioConexoes { get; set; }
        public DbSet<JogoPlataforma> JogosPlataformas { get; set; }
        public DbSet<Seguidor> Seguidores { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Chave composta para Seguidores
            modelBuilder.Entity<Seguidor>()
                .HasKey(s => new { s.id_seguidor, s.id_seguindo });

            // Relações de Seguidor (auto-referência em Usuario)
            modelBuilder.Entity<Seguidor>()
                .HasOne(s => s.UsuarioSeguidor)
                .WithMany()
                .HasForeignKey(s => s.id_seguidor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seguidor>()
                .HasOne(s => s.UsuarioSeguindo)
                .WithMany()
                .HasForeignKey(s => s.id_seguindo)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
