using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test1.Model
{
    [Table("Table_Categorys")]
    public partial class TableCategory
    {
        public TableCategory()
        {
            TableProducts = new HashSet<TableProduct>();
        }

        [Key]
        [StringLength(4)]
        public string CatId { get; set; }
        [StringLength(50)]
        public string CatName { get; set; }

        [InverseProperty("Cat")]
        public ICollection<TableProduct> TableProducts { get; set; }
    }
}
