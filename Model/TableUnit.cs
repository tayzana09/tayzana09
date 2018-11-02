using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test1.Model
{
    [Table("Table_Unit")]
    public partial class TableUnit
    {
        [Key]
        [StringLength(4)]
        public string UnitCode { get; set; }
        [StringLength(30)]
        public string UnitName { get; set; }
    }
}
