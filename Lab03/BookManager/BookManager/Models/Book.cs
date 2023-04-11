namespace BookManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required(ErrorMessage =" Title trống")]
        [StringLength(100, ErrorMessage = "Title max 100")]
        public string Title { get; set; }

        [Required(ErrorMessage = " Description trống")]
        [StringLength(255)]
        public string Description { get; set; }

        [Required(ErrorMessage = " Author trống")]
        [StringLength(30, ErrorMessage = "Author max 30")]
        public string Author { get; set; }

        [Required(ErrorMessage = " Images trống")]
        [StringLength(255)]
        public string Images { get; set; }
        [Required(ErrorMessage = " Price trống")]
        [Range(1000, 1000000, ErrorMessage = "Price trong khoang 1000 den 1000000")]
        public int Price { get; set; }
    }
}
