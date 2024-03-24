using System.Data.Entity;

namespace ClinicaVeterinaria.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext")
        {
            this.Configuration.ProxyCreationEnabled = false;

        }

        public virtual DbSet<Animale> Animale { get; set; }
        public virtual DbSet<Armadietti> Armadietti { get; set; }
        public virtual DbSet<Cassetti> Cassetti { get; set; }
        public virtual DbSet<DettagliVendita> DettagliVendita { get; set; }
        public virtual DbSet<Dipendenti> Dipendenti { get; set; }
        public virtual DbSet<Fornitori> Fornitori { get; set; }
        public virtual DbSet<Prodotti> Prodotti { get; set; }
        public virtual DbSet<Ricovero> Ricovero { get; set; }
        public virtual DbSet<Vendite> Vendite { get; set; }
        public virtual DbSet<Visita> Visita { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animale>()
                .HasMany(e => e.Ricovero)
                .WithRequired(e => e.Animale)
                .HasForeignKey(e => e.id_Animale_FK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Animale>()
                .HasMany(e => e.Visita)
                .WithRequired(e => e.Animale)
                .HasForeignKey(e => e.IdAnimale_Fk)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Armadietti>()
                .HasMany(e => e.Cassetti)
                .WithRequired(e => e.Armadietti)
                .HasForeignKey(e => e.Id_Armadietto_FK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Armadietti>()
                .HasMany(e => e.Prodotti)
                .WithRequired(e => e.Armadietti)
                .HasForeignKey(e => e.Id_Armadietto_FK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cassetti>()
                .HasMany(e => e.Prodotti)
                .WithRequired(e => e.Cassetti)
                .HasForeignKey(e => e.Id_Cassetto_FK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Fornitori>()
                .HasMany(e => e.Prodotti)
                .WithRequired(e => e.Fornitori)
                .HasForeignKey(e => e.IdFornitore_FK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prodotti>()
                .HasMany(e => e.DettagliVendita)
                .WithRequired(e => e.Prodotti)
                .HasForeignKey(e => e.Id_Prodotto_FK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ricovero>()
                .Property(e => e.Costo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Vendite>()
                .HasMany(e => e.DettagliVendita)
                .WithRequired(e => e.Vendite)
                .HasForeignKey(e => e.Id_Vendita_FK)
                .WillCascadeOnDelete(false);
        }
    }
}
