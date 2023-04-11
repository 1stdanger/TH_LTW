namespace BigSchool.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Attendancee")]
    public partial class Attendancee
    {
        [Key]
        [Column(Order = 0)]
        public int CourseId { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Attendee { get; set; }

        public virtual Cours Cours { get; set; }
    }
}
