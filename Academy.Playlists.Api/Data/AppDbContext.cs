using Academy.Playlists.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Academy.Playlists.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    public DbSet<Playlist> Playlists => Set<Playlist>();

    public DbSet<Song> Songs => Set<Song>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(user => user.Playlists)
            .WithOne(playlist => playlist.User)
            .HasForeignKey(playlist => playlist.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Playlist>()
            .HasMany(playlist => playlist.Songs)
            .WithOne(song => song.Playlist)
            .HasForeignKey(song => song.PlaylistId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}