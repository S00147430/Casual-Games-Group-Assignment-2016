namespace ServerSide
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class achievement
    {
        public int id { get; set; }

        public string Title { get; set; }

        public int? UsersCreate_id { get; set; }

        public virtual UsersCreate UsersCreate { get; set; }
    }
}
