using Microsoft.EntityFrameworkCore;
using RocketSeatAuction.API.Entities;

namespace RocketSeatAuction.API.Repositories;

public class RocketSeatAuctionDbContext : DbContext
{
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Offer> Offers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\Andersin\\Downloads\\leilaoDbNLW.db");
    }
}
