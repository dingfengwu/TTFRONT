using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Entity.NativeWeb
{
    public class NewsReader
    {
        public int Id { get; set; }

        public int NewsId { get; set; }

        public int UserId { get; set; }

        public DateTime ReadTime { get; set; }
    }
}
