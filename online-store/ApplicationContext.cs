using Microsoft.EntityFrameworkCore;
using online_store.Models;

namespace online_store
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasKey(_ => _.Id);
            modelBuilder.Entity<Order>().HasKey(_ => _.Id);
            modelBuilder.Entity<Order>().HasMany(_ => _.Itens).WithOne(_ => _.Pedido);
            modelBuilder.Entity<Order>().HasOne(_ => _.Cadastro).WithOne(_ => _.Pedido).IsRequired();//1-1 cada pedido tem um cadastro

            modelBuilder.Entity<ItemOrder>().HasKey(_ => _.Id);
            modelBuilder.Entity<ItemOrder>().HasOne(_ => _.Pedido);
            modelBuilder.Entity<ItemOrder>().HasOne(_ => _.Produto);

            modelBuilder.Entity<Cadastro>().HasKey(_ => _.Id);
            modelBuilder.Entity<Cadastro>().HasOne(_ => _.Pedido);//1-1 cada cadastro tem 1 pedido
        }
    }
}