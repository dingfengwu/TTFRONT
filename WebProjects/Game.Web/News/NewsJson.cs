using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.Web.News
{
    public class NewsJson
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool IsReaded { get; set; }
    }
}