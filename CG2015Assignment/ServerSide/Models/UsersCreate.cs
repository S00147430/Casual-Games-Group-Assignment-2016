using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ServerSide.Models
{
    public class UsersCreate
    {
        [Key]
        public virtual int id { get; set; } //Primary Key.
        [Required]
        public string PlayerName { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Password { get; set; }
        [Display(Name = "Password")]
        [Required]

        public virtual ICollection<achievements> AchievementsList { get; set; }
    }

    public class achievements
    {
        public string Title { get; set; }//Primary Key.
        [Display(Name = "Title")]
        [Key]
        public virtual int id { get; set; }
    }

    public class ScoreBoard
    {
        public int Score { get; set; }
        [Key]
        public virtual int id { get; set; }
        public virtual List<UsersCreate> Scores { get; set; }
    }

    public class UsersDb : DbContext
    {
        public DbSet<UsersCreate> UsersDatabase { get; set; }
        public DbSet<ScoreBoard> UserScores { get; set; }
        public UsersDb():base("UsersDb")
        { }
    }
}