
namespace ShopBridge.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name ="Product Name")]
        public string ProdName { get; set; }
        [Required]
        public Nullable<int> price { get; set; }
        [Required]
        public Nullable<int> stockCount { get; set; }
    }
}
