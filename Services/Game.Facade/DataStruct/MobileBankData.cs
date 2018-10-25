using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Facade.DataStruct
{
    public class MobileBankData
    {
        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal PayAmount
        { get; set; }

        /// <summary>
        /// 到账货币
        /// </summary>
        public decimal Currency
        { get; set; }

        /// <summary>
        /// 充值时间
        /// </summary>
        public DateTime ApplyDate
        { get; set; }
    }
}
