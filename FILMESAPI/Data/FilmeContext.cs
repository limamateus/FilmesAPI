using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FILMESAPI.Data
{
    public class FilmeContext : DbContext
    {
        public FilmeContext(DbContextOptions<FilmeContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder buider)
        {
            buider.Entity<Sessao>().HasKey(sessao => new { sessao.FilmeId, sessao.CinemaId });

            buider.Entity<Sessao>() // A entidadade Sessao
                .HasOne(sessao => sessao.Cinema) // Vai ter um cinema
                .WithMany(cinema => cinema.Sessoes) //  Com muitas sessoes ou seja, relacionamento de  1 : n
                .HasForeignKey(sessao => sessao.CinemaId); // Chave secuntaria é cinemaId

            buider.Entity<Sessao>()// A entidadade Sessao
                .HasOne(sessao => sessao.Filme)// Vai ter um filme
                .WithMany(filme => filme.Sessao)//  Com muitas sessoes ou seja, relacionamento de  1 : n
                .HasForeignKey(filme => filme.FilmeId);// Chave secuntaria é FilmeId

            // Tipo de Deleção

            buider.Entity<Endereco>()
                .HasOne(endereco => endereco.Cinema)
                .WithOne(cinama => cinama.Endereco)
                .OnDelete(DeleteBehavior.Restrict);

        }
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Sessao> Sessao { get; set; }

    }
}
