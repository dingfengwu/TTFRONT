using Game.Entity.Treasure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Facade.DataStruct
{
    public class MobileRecordInsure : RecordInsure
    {
        /// <summary>
        /// 转账账号
        /// </summary>
        public string TransferAccounts
        { get; set; }

        /// <summary>
        /// 银行操作类型描述
        /// </summary>
        public string TradeTypeDescription
        { get; set; }
    }
}
