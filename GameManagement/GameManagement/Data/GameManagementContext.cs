using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameManagement.Models;

namespace GameManagement.Data
{
    public class GameManagementContext : DbContext
    {
        public GameManagementContext (DbContextOptions<GameManagementContext> options)
            : base(options)
        {
        }

        public DbSet<GameManagement.Models.Game> Game { get; set; }
    }
}
