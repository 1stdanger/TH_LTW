using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BigSchool.Models
{
    public partial class BigSchoolContext : DbContext
    {
        public BigSchoolContext()
            : base("name=BigSchoolContext")
        {
        }

        public virtual DbSet<Attendancee> Attendancees { get; set; }
        public virtual DbSet<Categoryy> Categoryies { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<Following> Followings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoryy>()
                .HasMany(e => e.Courses)
                .WithOptional(e => e.Categoryy)
                .HasForeignKey(e => e.CategoryId);

            modelBuilder.Entity<Cours>()
                .HasMany(e => e.Attendancees)
                .WithRequired(e => e.Cours)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);
        }
    }
}
