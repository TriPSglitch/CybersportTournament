//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CybersportTournament
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CybersportTournamentEntities : DbContext
    {
        public CybersportTournamentEntities()
            : base("name=CybersportTournamentEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<MatchList> MatchList { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<Players> Players { get; set; }
        public virtual DbSet<PlayersList> PlayersList { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Rounds> Rounds { get; set; }
        public virtual DbSet<RoundsList> RoundsList { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Teams> Teams { get; set; }
        public virtual DbSet<TeamsList> TeamsList { get; set; }
        public virtual DbSet<Tournaments> Tournaments { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
