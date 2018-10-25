using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Entity.Accounts
{
    public class AccountPay
    {
        public int PayId { get; set; }

        public int UserId { get; set; }

        public string TradeNo { get; set; }

        public decimal Amount { get; set; }

        public DateTime PayTime { get; set; }

        public int PayStatus { get; set; }

        public string Buyer_Id { get; set; }

        public string Buyer_Email { get; set; }

        public string PayType { get; set; }
    }
}
