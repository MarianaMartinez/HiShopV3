using HiShop.Entity;
using Microsoft.EntityFrameworkCore;
using HiShop.Models;

namespace HiShop.Entity.Data
{
    /// <summary>
    /// En esta clase se tienen que agregar las clases entity para que haga la magia entity framawork
    /// AxelMolaro
    /// </summary>
    public class HiShopContext : DbContext
    {
        public HiShopContext()
        {
        }

        public HiShopContext(DbContextOptions<HiShopContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; internal set; }
        public DbSet<Negocio> Negocios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Banner> Elementos { get; set; }
        public DbSet<Banner> Banners { get ; set; }
        public DbSet<Cuerpo> Cuerpos { get; set; }
        //public DbSet<ContenedorDeTexto> ContenedoresDeTexto { get; set; }
        public DbSet<Provincia> Provincias { get; set; } 
        public DbSet<Localidad> Localidades { get; set; }
        public DbSet<OrdenPedido> OrdenPedidos { get; set; }
        public DbSet<Showroom> Showrooms { get; set; }

        public DbSet<ElementoMenuDAD> ElementosMenuDADs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Categoria>().ToTable("Categoria");
            modelBuilder.Entity<Negocio>().ToTable("Negocio");
            modelBuilder.Entity<Servicio>().ToTable("Servicio");
            modelBuilder.Entity<Articulo>().ToTable("Articulo");
            modelBuilder.Entity<Producto>().ToTable("Producto");

            //  modelBuilder.Entity<ContenedorDeTexto>().ToTable("ContenedorDeTexto");
            modelBuilder.Entity<Banner>().ToTable("Cuerpo");
            modelBuilder.Entity<Banner>().ToTable("Banner");
            modelBuilder.Entity<Elemento>().ToTable("Elemento");

            modelBuilder.Entity<Provincia>().ToTable("Provincia");
            modelBuilder.Entity<Localidad>().ToTable("Localidad");
            modelBuilder.Entity<OrdenPedido>().ToTable("OrdenPedido");
            modelBuilder.Entity<Showroom>().ToTable("Showroom");
            modelBuilder.Entity<ElementoMenuDAD>().ToTable("ElementoMenuDAD");

        }
}
}
