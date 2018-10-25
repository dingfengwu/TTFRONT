using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Entity.Treasure;

namespace Game.Facade.DataStruct
{
    public class UserGameInfoRank
    {
        private int _ranking = 0;

        private int _userID = 0;

        private string _nickname = string.Empty;

        private int _lineGrandTotal = 0;

        private int _lineWinMax = 1;

        private int _faceID = 0;

        private int _customID = 0;

        private int _gender = 0;

        private byte _trend = 0;

        /// <summary>
        /// 排名
        /// </summary>
        public int Ranking
        {
            get
            {
                return _ranking;
            }
            set
            {
                _ranking = value;
            }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName
        {
            get
            {
                return _nickname;
            }
            set
            {
                _nickname = value;
            }
        }

        /// <summary>
        /// 总赢局数
        /// </summary>
        public int LineGrandTotal
        {
            get
            {
                return _lineGrandTotal;
            }
            set
            {
                _lineGrandTotal = value;
            }
        }

        /// <summary>
        /// 做大赢值
        /// </summary>
        public int LineWinMax
        {
            get
            {
                return _lineWinMax;
            }
            set
            {
                _lineWinMax = value;
            }
        }

        /// <summary>
        /// 头像ID
        /// </summary>
        public int FaceID
        {
            get
            {
                return _faceID;
            }
            set
            {
                _faceID = value;
            }
        }

        /// <summary>
        /// 自定义头像ID
        /// </summary>
        public int CustomID
        {
            get
            {
                return _customID;
            }
            set
            {
                _customID = value;
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }

        /// <summary>
        /// 趋势 0：上升 1：下降 2：持平
        /// </summary>
        public byte Trend
        {
            get
            {
                return _trend;
            }
            set
            {
                _trend = value;
            }
        }
    }
}
