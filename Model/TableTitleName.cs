using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test1.Model
{
    [Table("Table_TitleName")]
    public partial class TableTitleName
    {
        public TableTitleName()
        {
            TableCustomers = new HashSet<TableCustomer>();
        }

        [Key]
        [StringLength(4)]
        public string InitialCode { get; set; }
        [StringLength(10)]
        public string InitialName { get; set; }

        [InverseProperty("InitialCodeNavigation")]
        public ICollection<TableCustomer> TableCustomers { get; set; }
    }
}
