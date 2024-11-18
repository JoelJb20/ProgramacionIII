using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class CinemaContext : DbContext
{
    public CinemaContext()
    {
    }

    public CinemaContext(DbContextOptions<CinemaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActorsInMovie> ActorsInMovies { get; set; }

    public virtual DbSet<ActorsInMoviesView> ActorsInMoviesViews { get; set; }

    public virtual DbSet<Cinema> Cinemas { get; set; }

    public virtual DbSet<CinemaScreen> CinemaScreens { get; set; }

    public virtual DbSet<CinemaScreensView> CinemaScreensViews { get; set; }

    public virtual DbSet<CinemasView> CinemasViews { get; set; }

    public virtual DbSet<CountriesView> CountriesViews { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CountryState> CountryStates { get; set; }

    public virtual DbSet<CountryStatesView> CountryStatesViews { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieClassification> MovieClassifications { get; set; }

    public virtual DbSet<MovieClassificationsView> MovieClassificationsViews { get; set; }

    public virtual DbSet<MovieGender> MovieGenders { get; set; }

    public virtual DbSet<MovieGendersView> MovieGendersViews { get; set; }

    public virtual DbSet<MoviesByScreen> MoviesByScreens { get; set; }

    public virtual DbSet<MoviesByScreensView> MoviesByScreensViews { get; set; }

    public virtual DbSet<MoviesView> MoviesViews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserRolesView> UserRolesViews { get; set; }

    public virtual DbSet<UsersView> UsersViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActorsInMovie>(entity =>
        {
            entity.HasKey(e => e.AcInMoId).HasName("PK__ActorsIn__8ABD1CF9FE216D70");

            entity.HasOne(d => d.Movie).WithMany(p => p.ActorsInMovies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ActorsInM__Movie__60A75C0F");
        });

        modelBuilder.Entity<ActorsInMoviesView>(entity =>
        {
            entity.ToView("ActorsInMoviesView");
        });

        modelBuilder.Entity<Cinema>(entity =>
        {
            entity.HasKey(e => e.CinemaId).HasName("PK__Cinemas__59C9264641D7DFF4");

            entity.HasOne(d => d.CountryState).WithMany(p => p.Cinemas).HasConstraintName("FK__Cinemas__Country__49C3F6B7");
        });

        modelBuilder.Entity<CinemaScreen>(entity =>
        {
            entity.HasKey(e => e.ScreenId).HasName("PK__CinemaSc__0AB60FA5F80EE1E0");

            entity.HasOne(d => d.Cinema).WithMany(p => p.CinemaScreens).HasConstraintName("FK__CinemaScr__Cinem__4E88ABD4");
        });

        modelBuilder.Entity<CinemaScreensView>(entity =>
        {
            entity.ToView("CinemaScreensView");
        });

        modelBuilder.Entity<CinemasView>(entity =>
        {
            entity.ToView("CinemasView");
        });

        modelBuilder.Entity<CountriesView>(entity =>
        {
            entity.ToView("CountriesView");

            entity.Property(e => e.CountryId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Countrie__10D1609F5463D5D5");
        });

        modelBuilder.Entity<CountryState>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__CountryS__C3BA3B3A837874A8");

            entity.HasOne(d => d.Country).WithMany(p => p.CountryStates).HasConstraintName("FK__CountrySt__Count__44FF419A");
        });

        modelBuilder.Entity<CountryStatesView>(entity =>
        {
            entity.ToView("CountryStatesView");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__4BD2941A0C206B16");

            entity.HasOne(d => d.Classification).WithMany(p => p.Movies).HasConstraintName("FK__Movies__Classifi__5BE2A6F2");

            entity.HasOne(d => d.Gender).WithMany(p => p.Movies).HasConstraintName("FK__Movies__GenderId__5AEE82B9");
        });

        modelBuilder.Entity<MovieClassification>(entity =>
        {
            entity.HasKey(e => e.ClassificationId).HasName("PK__MovieCla__DA747D11645B6D00");
        });

        modelBuilder.Entity<MovieClassificationsView>(entity =>
        {
            entity.ToView("MovieClassificationsView");

            entity.Property(e => e.ClassificationId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<MovieGender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__MovieGen__4E24E9F7CAC20B8C");
        });

        modelBuilder.Entity<MovieGendersView>(entity =>
        {
            entity.ToView("MovieGendersView");

            entity.Property(e => e.GenderId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<MoviesByScreen>(entity =>
        {
            entity.HasKey(e => e.MovieByScreenId).HasName("PK__MoviesBy__85B9D6DAB8E7F2BC");

            entity.HasOne(d => d.Movie).WithMany(p => p.MoviesByScreens)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MoviesByS__Movie__656C112C");

            entity.HasOne(d => d.Screen).WithMany(p => p.MoviesByScreens)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MoviesByS__Scree__66603565");
        });

        modelBuilder.Entity<MoviesByScreensView>(entity =>
        {
            entity.ToView("MoviesByScreensView");
        });

        modelBuilder.Entity<MoviesView>(entity =>
        {
            entity.ToView("MoviesView");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CB854077D");

            entity.HasOne(d => d.Role).WithMany(p => p.Users).HasConstraintName("FK__Users__RoleId__3C69FB99");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__8AFACE1A9005ADD4");
        });

        modelBuilder.Entity<UserRolesView>(entity =>
        {
            entity.ToView("UserRolesView");

            entity.Property(e => e.RoleId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<UsersView>(entity =>
        {
            entity.ToView("UsersView");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
