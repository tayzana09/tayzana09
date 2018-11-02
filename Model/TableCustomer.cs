using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test1.Model
{
    [Table("Table_Customer")]
    public partial class TableCustomer
    {
        [Key]
        [StringLength(4)]
        public string CustId { get; set; }
        [StringLength(4)]
        public string InitialCode { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string LastName { get; set; }
        [StringLength(4)]
        public string CustType { get; set; }

        [ForeignKey("InitialCode")]
        [InverseProperty("TableCustomers")]
        public TableTitleName InitialCodeNavigation { get; set; }
    }
}
