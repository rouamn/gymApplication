using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using GymApplication.Repository.Models;

namespace GymApplication.Repository
{
    public partial class GymDbContext : DbContext
    {
        public GymDbContext()
        {
        }

        public GymDbContext(DbContextOptions<GymDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Abonnement> Abonnements { get; set; } = null!;
        public virtual DbSet<Cour> Cours { get; set; } = null!;
        public virtual DbSet<Evenement> Evenements { get; set; } = null!;
        public virtual DbSet<Paiement> Paiements { get; set; } = null!;

        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-NJJQ8LD6\\SQLEXPRESS;Initial Catalog=gymapp;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


                    modelBuilder.Entity<Abonnement>(entity =>
                    {
                        entity.HasKey(e => e.IdAbonnement)
                            .HasName("PK__Abonneme__395058AB06AC8C7D");

                        entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                        entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

              
                    });


            modelBuilder.Entity<Cour>(entity =>
            {
                entity.HasKey(e => e.IdCours)
                    .HasName("PK__Cours__7927EBB9D64C6A59");
            });

            modelBuilder.Entity<Evenement>(entity =>
            {
                entity.HasKey(e => e.IdEvenement)
                    .HasName("PK__Evenemen__F6BFCE778F3DA503");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Paiement>(entity =>
            {
                entity.HasKey(e => e.IdPaiement)
                    .HasName("PK__Paiement__72D44CFF086B29B8");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdUtilisateurNavigation)
                    .WithMany(p => p.Paiements)
                    .HasForeignKey(d => d.IdUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Paiement__id_uti__46E78A0C");
            });


            modelBuilder.Entity<Paiement>(entity =>
            {
                entity.HasKey(e => e.IdPaiement)
                    .HasName("PK__Paiement__72D44CFF086B29B8");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdUtilisateurNavigation)
                    .WithMany(p => p.Paiements)
                    .HasForeignKey(d => d.IdUtilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Paiement__id_uti__46E78A0C");

                entity.HasOne(d => d.Abonnement)
                    .WithOne(p => p.Paiements)  // Optional: If Abonnement can have multiple Paiements

                    .HasForeignKey<Paiement>(d => d.FkAbonnement)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Paiement__id_abo__4E88ABD4");
            });

          

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.IdUtilisateur)
                    .HasName("PK__Utilisat__1A4FA5B8F3C735FF");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
