using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eStore_web.EF_Models
{
    public class WalletHistory
    {
        [Key]
        public int WalletHistoryID { get; set; }
        [Required]
        public double CurrentBalance { get; set; }
        [Required,Range(-999,999)]
        public double TransactionAmount { get; set; }
        [Required]
        public bool IsIgra { get; set; }


        [ForeignKey("IgraID")]
        public int IgraID { get; set; }
        public Igra Igra { get; set; }
        [ForeignKey("WalletID")]
        public int? WalletID { get; set; }
        public Wallet Wallet { get; set; }
    }
}
