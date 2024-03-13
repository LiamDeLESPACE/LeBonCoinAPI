using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LeBonCoinAPI.Models.EntityFramework
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
            
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("DBContext");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Modèle builder
            /*
            modelBuilder.Entity<Etudiant>(entity =>
            {
                entity.HasKey(e => e.EtudiantId)
                    .HasName("t_e_etudiant_etu_pkey");

                entity.Property(e => e.Ine).IsFixedLength();

                entity.Property(e => e.DateInscription).HasDefaultValueSql("CURRENT_DATE");

                entity.HasOne(d => d.FormationEtudiant)
                    .WithMany(p => p.EtudiantsFormation)
                    .HasForeignKey(d => d.FormationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_etu_for");
            });*/

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
