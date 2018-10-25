using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Facade.DataStruct
{
    public class UserScoreRank
    {
        private int _ranking = 0;

        private int _userID = 0;

        private Int64 _score = 0;

        private string _nickname = string.Empty;

        private int _faceID = 0;

        private int _customID = 0;


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
        /// 用户金币
        /// </summary>
        public Int64 Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
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
    }
}
