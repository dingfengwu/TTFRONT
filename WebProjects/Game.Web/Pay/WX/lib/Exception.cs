using System;
using System.Collections.Generic;
using System.Web;

namespace Game.Web.Pay.WX
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}