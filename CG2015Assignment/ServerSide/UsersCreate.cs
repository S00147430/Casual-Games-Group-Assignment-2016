namespace ServerSide
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UsersCreate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UsersCreate()
        {
            achievements = new HashSet<achievement>();
        }

        public int id { get; set; }

        [Required]
        public string PlayerName { get; set; }

        [Required]
        public string Password { get; set; }

        public int? ScoreBoard_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<achievement> achievements { get; set; }

        public virtual ScoreBoard ScoreBoard { get; set; }
    }
}
