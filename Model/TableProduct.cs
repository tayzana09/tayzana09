using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test1.Model
{
    [Table("Table_Product")]
    public partial class TableProduct
    {
        [Key]
        [StringLength(4)]
        public string Code { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        [Column(TypeName = "text")]
        public string Description { get; set; }
        public int? Price { get; set; }
        public int? UnitPerPrice { get; set; }
        public int? Qty { get; set; }
        public short? Status { get; set; }
        [StringLength(4)]
        public string UnitCode { get; set; }
        [StringLength(4)]
        public string CatId { get; set; }

        [ForeignKey("CatId")]
        [InverseProperty("TableProducts")]
        public TableCategory Cat { get; set; }
    }
}
