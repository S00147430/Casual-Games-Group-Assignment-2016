namespace ServerSide
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class ModelUsers1 : DbContext
    {
        public ModelUsers1()
            : base("name=ModelUsers1")
        {
            
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<achievement> achievements { get; set; }
        public virtual DbSet<ScoreBoard> ScoreBoards { get; set; }
        public virtual DbSet<UsersCreate> UsersCreates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScoreBoard>()
                .HasMany(e => e.UsersCreates)
                .WithOptional(e => e.ScoreBoard)
                .HasForeignKey(e => e.ScoreBoard_id);

            modelBuilder.Entity<UsersCreate>()
                .HasMany(e => e.achievements)
                .WithOptional(e => e.UsersCreate)
                .HasForeignKey(e => e.UsersCreate_id);
        }
    }
}
