﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Wordle.Api.Models
{
  
    public class WordleDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<WordOfTheDay> WordsOfTheDays { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<ConnectionsOfTheDay> ConnectionsOfTheDays { get; set; }
        public DbSet<ConnectionGroup> ConnectionGroups { get; set; }
        
        public WordleDbContext(DbContextOptions<WordleDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
