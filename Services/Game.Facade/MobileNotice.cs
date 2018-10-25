using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Facade
{
    /// <summary>
    /// 移动公告类
    /// </summary>
    public class MobileNotice
    {
        /// <summary>
        /// 新闻标题
        /// </summary>
        private int _id;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// 新闻标题
        /// </summary>
        private string _title;
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        /// <summary>
        /// 发布日期
        /// </summary>
        private string _date;
        public string date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        /// <summary>
        /// 公告内容
        /// </summary>
        private string _content;
        public string content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="startTitle"></param>
        /// <param name="startDate"></param>
        /// <param name="startContent"></param>
        public MobileNotice(int newsid, string startTitle, DateTime startDate, string startContent)
        {
            _id = newsid;
            _title = startTitle;
            _date = startDate.ToString();
            _content = startContent;
        }
    }
}
