using Game.Entity.NativeWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Facade.DataStruct
{
    public class MobileAwardOrder : AwardOrder
    {
        /// <summary>
        /// 状态描述
        /// </summary>
        public string OrderStatusDescription
        {
            get;
            set;
        }

        public string AwardName
        {
            get;
            set;
        }
    }
}
