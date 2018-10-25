using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Kernel;
using Game.IData;
using Game.Entity.Treasure;
using Game.Entity.Accounts;
using System.Data.Common;
using System.Data;

namespace Game.Data
{
    /// <summary>
    /// 金币数据访问层
    /// </summary>
    public class TreasureDataProvider : BaseDataProvider, ITreasureDataProvider
    {
        #region 构造方法

        public TreasureDataProvider(string connString)
            : base(connString)
        {

        }
        #endregion

        #region 在线充值

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public Message RequestOrder(OnLineOrder orderInfo)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwOperUserID", orderInfo.OperUserID));
            prams.Add(Database.MakeInParam("dwShareID", orderInfo.ShareID));
            prams.Add(Database.MakeInParam("strAccounts", orderInfo.Accounts));
            prams.Add(Database.MakeInParam("strOrderID", orderInfo.OrderID));
            prams.Add(Database.MakeInParam("dcOrderAmount", orderInfo.OrderAmount));
            prams.Add(Database.MakeInParam("strIPAddress", orderInfo.IPAddress));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));
            return MessageHelper.GetMessageForObject<OnLineOrder>(Database, "NET_PW_ApplyOnLineOrder", prams);
        }

        /// <summary>
        /// 写天天付返回记录
        /// </summary>
        /// <param name="returnKQ"></param>
        public void WriteReturnDayDetail(ReturnDayDetailInfo returnDay)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("@OrderID", returnDay.OrderID));
            parms.Add(Database.MakeInParam("@MerID", returnDay.MerID));
            parms.Add(Database.MakeInParam("@PayMoney", returnDay.PayMoney));
            parms.Add(Database.MakeInParam("@UserName", returnDay.UserName));
            parms.Add(Database.MakeInParam("@Sign", returnDay.Sign));
            parms.Add(Database.MakeInParam("@PayType", returnDay.PayType));
            parms.Add(Database.MakeInParam("@Status", returnDay.Status));
            Database.RunProc("NET_PW_AddReturnDayInfo", parms);
        }

        /// <summary>
        /// 写快钱返回记录
        /// </summary>
        /// <param name="returnKQ"></param>
        public void WriteReturnKQDetail(ReturnKQDetailInfo returnKQ)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("strMerchantAcctID", returnKQ.MerchantAcctID));
            parms.Add(Database.MakeInParam("strVersion", returnKQ.Version));
            parms.Add(Database.MakeInParam("dwLanguage", returnKQ.Language));
            parms.Add(Database.MakeInParam("dwSignType", returnKQ.SignType));
            parms.Add(Database.MakeInParam("strPayType", returnKQ.PayType));
            parms.Add(Database.MakeInParam("strBankID", returnKQ.BankID));
            parms.Add(Database.MakeInParam("strOrderID", returnKQ.OrderID));
            parms.Add(Database.MakeInParam("dtOrderTime", returnKQ.OrderTime));
            parms.Add(Database.MakeInParam("fOrderAmount", returnKQ.OrderAmount));
            parms.Add(Database.MakeInParam("strDealID", returnKQ.DealID));
            parms.Add(Database.MakeInParam("strBankDealID", returnKQ.BankDealID));
            parms.Add(Database.MakeInParam("dtDealTime", returnKQ.DealTime));
            parms.Add(Database.MakeInParam("fPayAmount", returnKQ.PayAmount));
            parms.Add(Database.MakeInParam("fFee", returnKQ.Fee));
            parms.Add(Database.MakeInParam("strPayResult", returnKQ.PayResult));
            parms.Add(Database.MakeInParam("strErrCode", returnKQ.ErrCode));
            parms.Add(Database.MakeInParam("strSignMsg", returnKQ.SignMsg));
            parms.Add(Database.MakeInParam("strExt1", returnKQ.Ext1));
            parms.Add(Database.MakeInParam("strExt2", returnKQ.Ext2));

            parms.Add(Database.MakeInParam("CardNumber", returnKQ.CardNumber));
            parms.Add(Database.MakeInParam("CardPwd", returnKQ.CardPwd));
            parms.Add(Database.MakeInParam("BossType", returnKQ.BossType));
            parms.Add(Database.MakeInParam("ReceiveBossType", returnKQ.ReceiveBossType));
            parms.Add(Database.MakeInParam("ReceiverAcctId", returnKQ.ReceiverAcctId));

            Database.RunProc("NET_PW_AddReturnKQInfo", parms);
        }

        /// <summary>
        /// 写电话充值返回记录
        /// </summary>
        /// <param name="returnKQ"></param>
        public Message WriteReturnVBDetail(ReturnVBDetailInfo returnVB)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("Rtmd5", returnVB.Rtmd5));
            parms.Add(Database.MakeInParam("Rtka", returnVB.Rtka));
            parms.Add(Database.MakeInParam("Rtmi", returnVB.Rtmi));
            parms.Add(Database.MakeInParam("Rtmz", returnVB.Rtmz));
            parms.Add(Database.MakeInParam("Rtlx", returnVB.Rtlx));
            parms.Add(Database.MakeInParam("Rtoid", returnVB.Rtoid));
            parms.Add(Database.MakeInParam("OrderID", returnVB.OrderID));
            parms.Add(Database.MakeInParam("Rtuserid", returnVB.Rtuserid));
            parms.Add(Database.MakeInParam("Rtcustom", returnVB.Rtcustom));
            parms.Add(Database.MakeInParam("Rtflag", returnVB.Rtflag));
            parms.Add(Database.MakeInParam("EcryptStr", returnVB.EcryptStr));
            parms.Add(Database.MakeInParam("SignMsg", returnVB.SignMsg));

            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_AddReturnVBInfo", parms); 
        }

        /// <summary>
        /// 写易宝返回记录
        /// </summary>
        /// <param name="returnYB"></param>
        public Message WriteReturnYBDetail(ReturnYPDetailInfo returnYB)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("p1_MerId", returnYB.P1_MerId));
            parms.Add(Database.MakeInParam("r0_Cmd", returnYB.R0_Cmd));
            parms.Add(Database.MakeInParam("r1_Code", returnYB.R1_Code));
            parms.Add(Database.MakeInParam("r2_TrxId", returnYB.R2_TrxId));
            parms.Add(Database.MakeInParam("r3_Amt", returnYB.R3_Amt));
            parms.Add(Database.MakeInParam("r4_Cur", returnYB.R4_Cur));
            parms.Add(Database.MakeInParam("r5_Pid", returnYB.R5_Pid));
            parms.Add(Database.MakeInParam("r6_Order", returnYB.R6_Order));
            parms.Add(Database.MakeInParam("r7_Uid", returnYB.R7_Uid));
            parms.Add(Database.MakeInParam("r8_MP", returnYB.R8_MP));
            parms.Add(Database.MakeInParam("r9_BType", returnYB.R9_BType));
            parms.Add(Database.MakeInParam("rb_BankId", returnYB.Rb_BankId));
            parms.Add(Database.MakeInParam("ro_BankOrderId", returnYB.Ro_BankOrderId));
            parms.Add(Database.MakeInParam("rp_PayDate", returnYB.Rp_PayDate));
            parms.Add(Database.MakeInParam("rq_CardNo", returnYB.Rq_CardNo));
            parms.Add(Database.MakeInParam("ru_Trxtime", returnYB.Ru_Trxtime));
            parms.Add(Database.MakeInParam("hmac", returnYB.Hmac));

            return MessageHelper.GetMessage(Database, "NET_PW_AddReturnYBInfo", parms); 
        }

        /// <summary>
        /// 在线充值
        /// </summary>
        /// <param name="olDetial"></param>
        /// <returns></returns>
        public Message FilliedOnline(ShareDetialInfo olDetial, int isVB)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("strOrdersID", olDetial.OrderID));
            parms.Add(Database.MakeInParam("PayAmount", olDetial.PayAmount));
            parms.Add(Database.MakeInParam("isVB", isVB));
            parms.Add(Database.MakeInParam("strIPAddress", olDetial.IPAddress));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_FilledOnLine", parms);
        }

        /// <summary>
        /// 苹果充值
        /// </summary>
        /// <param name="olDetial"></param>
        /// <returns></returns>
        public Message FilliedApp(ShareDetialInfo olDetial, string productID)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", olDetial.UserID));
            parms.Add(Database.MakeInParam("strOrdersID", olDetial.OrderID));
            parms.Add(Database.MakeInParam("PayAmount", olDetial.PayAmount));
            parms.Add(Database.MakeInParam("strProductID", productID));
            parms.Add(Database.MakeInParam("dwShareID", olDetial.ShareID));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_FilledApp", parms);
        }

        /// <summary>
        /// 手游充值
        /// </summary>
        /// <param name="olDetial"></param>
        /// <returns></returns>
        public Message FilliedMobile(ShareDetialInfo olDetial)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("strOrdersID", olDetial.OrderID));
            parms.Add(Database.MakeInParam("PayAmount", olDetial.PayAmount));
            parms.Add(Database.MakeInParam("strIPAddress", olDetial.IPAddress));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_FilledMobile", parms);
        }

        /// <summary>
        /// 实卡充值
        /// </summary>
        /// <param name="associator"></param>
        /// <param name="operUserID"></param>
        /// <param name="accounts"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message FilledLivcard(ShareDetialInfo detialInfo, string password)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwOperUserID", detialInfo.OperUserID));

            prams.Add(Database.MakeInParam("strSerialID", detialInfo.SerialID));
            prams.Add(Database.MakeInParam("strPassword", password));
            prams.Add(Database.MakeInParam("strAccounts", detialInfo.Accounts));

            prams.Add(Database.MakeInParam("strClientIP", detialInfo.IPAddress));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_FilledLivcard", prams);
        }

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public OnLineOrder GetOnlineOrder(string orderID)
        {
            string sqlQuery = string.Format("SELECT * FROM OnLineOrder(NOLOCK) WHERE OrderID=@OrderID");

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("OrderID", orderID));

            OnLineOrder order = Database.ExecuteObject<OnLineOrder>(sqlQuery, prams);

            return order;
        }
        public int GetInsureSocore(int UserId)
        {
            int val = 0;
            string sqlQuery = string.Format("SELECT InsureScore FROM GameScoreInfo WHERE UserId=@UserId");
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserId", UserId));
            DbDataReader order = Database.ExecuteReader(sqlQuery, prams);
            if (order.Read())
                val = order.GetInt32(0);
            order.Close();
            return val;
        }
        /// <summary>
        /// 获取苹果产品列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetAppList()
        {
            string sqlQuery = string.Format("SELECT ProductID,ProductName,Description,Price,AttachCurrency FROM GlobalAppInfo(NOLOCK) ORDER BY AppID ASC");
            return Database.ExecuteDataset(CommandType.Text, sqlQuery);
        }

        /// <summary>
        /// 获取苹果产品列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetAppListByTagID(int tagID)
        {
            string sqlQuery = string.Format("SELECT ProductID,ProductName,Description,Price,AttachCurrency FROM GlobalAppInfo(NOLOCK) WHERE TagID={0} ORDER BY AppID ASC", tagID);
            return Database.ExecuteDataset(CommandType.Text, sqlQuery);
        }

        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public DataSet GetAppInfoByProductID(string productID)
        {
            string sqlQuery = string.Format("SELECT ProductID,ProductName,Description,Price FROM GlobalAppInfo(NOLOCK) WHERE ProductID=@ProductID");
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("ProductID", productID));
            return Database.ExecuteDataset(CommandType.Text, sqlQuery, prams.ToArray());
        }

        /// <summary>
        /// 写苹果返回记录
        /// </summary>
        /// <param name="detialInfo"></param>
        /// <param name="receipt"></param>
        public void WriteReturnAppDetail(ShareDetialInfo detialInfo, AppReceiptInfo receipt)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("INSERT INTO ReturnAppDetailInfo(UserID,OrderID,PayAmount,Status,quantity,product_id,transaction_id,purchase_date,original_transaction_id,");
            sqlQuery.Append("original_purchase_date,app_item_id,version_external_identifier,bid,bvrs)");
            sqlQuery.Append(" VALUES(");
            sqlQuery.Append("@UserID,@OrderID,@PayAmount,@Status,@quantity,@product_id,@transaction_id,@purchase_date,@original_transaction_id,");
            sqlQuery.Append("@original_purchase_date,@app_item_id,@version_external_identifier,@bid,@bvrs)");

            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserID", detialInfo.UserID));
            prams.Add(Database.MakeInParam("OrderID", detialInfo.OrderID));
            prams.Add(Database.MakeInParam("PayAmount", detialInfo.PayAmount));
            prams.Add(Database.MakeInParam("Status", receipt.Status));
            prams.Add(Database.MakeInParam("quantity", receipt.Receipt.quantity));
            prams.Add(Database.MakeInParam("product_id", receipt.Receipt.product_id));
            prams.Add(Database.MakeInParam("transaction_id", receipt.Receipt.transaction_id));
            prams.Add(Database.MakeInParam("purchase_date", receipt.Receipt.purchase_date));
            prams.Add(Database.MakeInParam("original_transaction_id", receipt.Receipt.original_transaction_id));
            prams.Add(Database.MakeInParam("original_purchase_date", receipt.Receipt.original_purchase_date));
            prams.Add(Database.MakeInParam("app_item_id", receipt.Receipt.app_item_id));
            prams.Add(Database.MakeInParam("version_external_identifier", receipt.Receipt.version_external_identifier));
            prams.Add(Database.MakeInParam("bid", receipt.Receipt.bid));
            prams.Add(Database.MakeInParam("bvrs", receipt.Receipt.bvrs));

            Database.ExecuteNonQuery(CommandType.Text, sqlQuery.ToString(), prams.ToArray());
        }

        #endregion

        #region 充值记录

        /// <summary>
        /// 充值记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetPayRecord(string whereQuery, int pageIndex, int pageSize)
        {
            string orderQuery = "ORDER By ApplyDate DESC";
            string[] returnField = new string[16] { "DetailID", "OperUserID", "ShareID", "UserID", "GameID", "Accounts", "SerialID", "OrderID", "CardTypeID", "Currency", "BeforeCurrency", "OrderAmount", "DiscountScale", "PayAmount", "IPAddress", "ApplyDate" };
            PagerParameters pager = new PagerParameters("ShareDetailInfo", orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 充值记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetPayRecord(string whereQuery, int pageIndex, int pageSize, string[] returnField = null)
        {
            string orderQuery = "ORDER By ApplyDate DESC";
            returnField = returnField == null ? new string[16] { "DetailID", "OperUserID", "ShareID", "UserID", "GameID", "Accounts", "SerialID", "OrderID", "CardTypeID", "Currency", "BeforeCurrency", "OrderAmount", "DiscountScale", "PayAmount", "IPAddress", "ApplyDate" } : returnField;
            PagerParameters pager = new PagerParameters("ShareDetailInfo", orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

       

        #endregion

        #region 推广中心

        /// <summary>
        /// 获取用户推广信息
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Message GetUserSpreadInfo(int userID)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));

            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<RecordSpreadInfo>(Database, "NET_PW_GetUserSpreadInfo", prams);
        }

        /// <summary>
        /// 用户推广结算信息
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="userID"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message GetUserSpreadBalance(int balance, int userID, string ip)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("dwBalance", balance));

            prams.Add(Database.MakeInParam("strClientIP", ip));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_SpreadBalance", prams);
        }

        /// <summary>
        /// 推广记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetSpreaderRecord(string whereQuery, int pageIndex, int pageSize)
        {
            string orderQuery = "ORDER By CollectDate DESC";
            string[] returnField = new string[8] { "RecordID", "UserID", "Score", "TypeID", "ChildrenID", "InsureScore", "CollectDate", "CollectNote" };
            PagerParameters pager = new PagerParameters(RecordSpreadInfo.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 单个用户下所有被推荐人的推广信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataSet GetUserSpreaderList(int userID, int pageIndex, int pageSize)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("dwPageIndex", pageIndex));
            prams.Add(Database.MakeInParam("dwPageSize", pageSize));

            DataSet ds = new DataSet();
            Database.RunProc("NET_PW_GetAllChildrenInfoByUserID", prams, out ds);

            return ds;
        }

        /// <summary>
        /// 获取单个结算总额
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public long GetUserSpreaderTotal(string sWhere)
        {
            string sql = "SELECT SUM(Score) AS Score FROM RecordSpreadInfo ";
            if(sWhere != "" && sWhere != null)
            {
                sql += sWhere;
            }
            RecordSpreadInfo spreader = Database.ExecuteObject<RecordSpreadInfo>(sql);
            return spreader == null ? 0 : spreader.Score;
        }

        /// <summary>
        /// 获取推广配置实体
        /// </summary>
        /// <returns></returns>
        public GlobalSpreadInfo GetGlobalSpreadInfo()
        {
            string sql = "SELECT TOP 1 * FROM GlobalSpreadInfo ORDER BY ID DESC";
            GlobalSpreadInfo model = Database.ExecuteObject<GlobalSpreadInfo>(sql);
            return model;
        }

        #endregion

        #region 银行操作

        /// <summary>
        /// 金币存入
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="TradeScore"></param>
        /// <param name="minTradeScore"></param>
        /// <param name="clientIP"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public Message InsureIn(int userID, Int64 TradeScore, string clientIP, string note)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));

            parms.Add(Database.MakeInParam("dwSwapScore", TradeScore));

            parms.Add(Database.MakeInParam("strClientIP", clientIP));
            parms.Add(Database.MakeInParam("strCollectNote", note));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<GameScoreInfo>(Database, "NET_PW_InsureIn", parms);
        }

        /// <summary>
        /// 金币取出
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="insurePass"></param>
        /// <param name="TradeScore"></param>
        /// <param name="minTradeScore"></param>
        /// <param name="clientIP"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public Message InsureOut(int userID, string insurePass, Int64 TradeScore, string clientIP, string note)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwUserID", userID));
            parms.Add(Database.MakeInParam("strInsurePass", insurePass));

            parms.Add(Database.MakeInParam("dwSwapScore", TradeScore));

            parms.Add(Database.MakeInParam("strClientIP", clientIP));
            parms.Add(Database.MakeInParam("strCollectNote", note));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<GameScoreInfo>(Database, "NET_PW_InsureOut", parms);
        }

        /// <summary>
        /// 金币转账
        /// </summary>
        /// <param name="srcUserID"></param>
        /// <param name="insurePass"></param>
        /// <param name="dstUserID"></param>
        /// <param name="TradeScore"></param>
        /// <param name="minTradeScore"></param>
        /// <param name="clientIP"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public Message InsureTransfer(int srcUserID, string insurePass, int dstUserID, Int64 TradeScore, string clientIP, string note)
        {
            var parms = new List<DbParameter>();
            parms.Add(Database.MakeInParam("dwSrcUserID", srcUserID));
            parms.Add(Database.MakeInParam("strInsurePass", insurePass));
            parms.Add(Database.MakeInParam("dwDstUserID", dstUserID));

            parms.Add(Database.MakeInParam("dwSwapScore", TradeScore));

            parms.Add(Database.MakeInParam("strClientIP", clientIP));
            parms.Add(Database.MakeInParam("strCollectNote", note));
            parms.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_InsureTransfer", parms);
        }

        /// <summary>
        /// 游戏记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetInsureTradeRecord(string whereQuery, int pageIndex, int pageSize)
        {
            string orderQuery = "ORDER By CollectDate DESC";
            string[] returnField = new string[16] { "RecordID", "KindID", "ServerID", "SourceUserID", "SourceGold", "SourceBank", "TargetUserID", "TargetGold", "TargetBank", "SwapScore", "Revenue", "IsGamePlaza", "TradeType", "ClientIP", "CollectDate", "CollectNote" };
            PagerParameters pager = new PagerParameters(RecordInsure.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        #endregion

        #region 获取金币信息

        /// <summary>
        /// 根据用户ID得到金币信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public GameScoreInfo GetTreasureInfo2(int UserID)
        {
            string sqlQuery = string.Format("SELECT * FROM GameScoreInfo(NOLOCK) WHERE UserID={0}", UserID);
            GameScoreInfo score = Database.ExecuteObject<GameScoreInfo>(sqlQuery);

            return score;
        }

        #endregion

        #region 财富排名

        /// <summary>
        /// 财富排名
        /// </summary>
        /// <returns></returns>
        public IList<GameScoreInfo> GetGameScoreInfoOrderByScore()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT TOP 10 UserID, Score, InsureScore ")
                    .Append(" FROM GameScoreInfo ")
                    .Append(" ORDER BY Score DESC,InsureScore DESC ");
            return Database.ExecuteObjectList<GameScoreInfo>(sqlQuery.ToString());
        }
        public DbHelper GetDbHelper()
        {
            return Database;
        }
        /// <summary>
        /// 财富排行
        /// </summary>
        /// <returns></returns>
        public DataSet GetScoreRanking(int num)
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat("SELECT TOP {0} A.UserID,Score,NickName,Nullity,GameID,WinCount,LostCount ", num)
                    .Append(" FROM GameScoreInfo AS A LEFT JOIN RYAccountsDBLink.RYAccountsDB.dbo.AccountsInfo AS B ")
                    .Append(" ON A.UserID = B.UserID WHERE B.Nullity=0 AND Score>0 AND IsAndroid=0 ORDER BY A.Score DESC ");
            return Database.ExecuteDataset(sqlQuery.ToString());
        }

        #endregion

        #region 货币信息

        /// <summary>
        /// 获取货币实体
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserCurrencyInfo GetUserCurrencyInfo(int userId)
        {
            string sqlQuery = "SELECT * FROM UserCurrencyInfo WHERE UserID=@UserID";
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("UserID", userId));
            return Database.ExecuteObject<UserCurrencyInfo>(sqlQuery, prams);
        }

        #endregion

        #region 会员操作

        /// <summary>
        /// 负分清零
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message ClearGameScore(int userID, string ip)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));

            prams.Add(Database.MakeInParam("strClientIP", ip));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ResetGameScore", prams);
        }

        /// <summary>
        /// 逃跑清零
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message ClearGameFlee(int userID, string ip)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));

            prams.Add(Database.MakeInParam("strClientIP", ip));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_ResetGameFlee", prams);
        }

        #endregion

        #region 游戏记录

        /// <summary>
        /// 每桌游戏记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetDrawInfoRecord(string whereQuery, int pageIndex, int pageSize)
        {
            string orderQuery = "ORDER By InsertTime DESC";
            string[] returnField = new string[13] { "DrawID", "KindID", "ServerID", "TableID", "UserCount", "AndroidCount", "Waste", "Revenue", "UserMedal", "StartTime", "ConcludeTime", "InsertTime", "DrawCourse" };
            PagerParameters pager = new PagerParameters(RecordDrawInfo.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 每桌详细记录
        /// </summary>
        /// <param name="whereQuery"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagerSet GetDrawScoreRecord(string whereQuery, int pageIndex, int pageSize)
        {
            string orderQuery = "ORDER By InsertTime DESC";
            string[] returnField = new string[9] { "DrawID", "UserID", "ChairID", "Score", "Grade", "Revenue", "UserMedal", "PlayTimeCount", "InsertTime" };
            PagerParameters pager = new PagerParameters(RecordDrawScore.Tablename, orderQuery, whereQuery, pageIndex, pageSize);

            pager.Fields = returnField;
            pager.CacherSize = 2;

            return GetPagerSet2(pager);
        }

        #endregion

        #region 公共

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pkey">排序或分组</param>
        /// <param name="whereQuery">查询条件</param>
        public PagerSet GetList(string tableName, int pageIndex, int pageSize, string pkey, string whereQuery)
        {
            PagerParameters pager = new PagerParameters(tableName, pkey, whereQuery, pageIndex, pageSize);
            pager.CacherSize = 2;
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pkey">排序或分组</param>
        /// <param name="whereQuery">查询条件</param>
        /// <param name="whereQuery">字段</param>
        public PagerSet GetList(string tableName, int pageIndex, int pageSize, string pkey, string whereQuery, string[] fields)
        {
            PagerParameters pager = new PagerParameters(tableName, pkey, whereQuery, pageIndex, pageSize, fields);
            pager.CacherSize = 2;
            return GetPagerSet2(pager);
        }

        /// <summary>
        /// 根据sql语句获取数据
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public DataSet GetDataSetByWhere(string sqlQuery)
        {
            return Database.ExecuteDataset(sqlQuery);
        }
//         protected void GetScoreRank(HttpContext context)
//         {
//             SqlCommand cmd = new SqlCommand();
//             cmd.Connection = FacadeManage.aideTreasureFacade.GetDbHelper().ExecuteScalar;
//             cmd.CommandText = "GSP_GP_AddAccountEmail";
//             cmd.Parameters.Add(new SqlParameter("@dwUserID", newPhoneLoginInfo.dwUserID));
//             cmd.Parameters.Add(new SqlParameter("@szTitle", newPhoneLoginInfo.szTitle));
//             cmd.Parameters.Add(new SqlParameter("@nType", newPhoneLoginInfo.nType));
//             cmd.Parameters.Add(new SqlParameter("@nSatus", newPhoneLoginInfo.nSatus));
//             cmd.Parameters.Add(new SqlParameter("@szMessage", newPhoneLoginInfo.szMessage));
//             cmd.CommandType = CommandType.StoredProcedure;
//             cmd.ExecuteNonQuery();
//         }
        public DataSet GetRank(DateTime start, DateTime end, int UserId)
        {
            var prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dtBeginTime", start.ToString()));
            prams.Add(Database.MakeInParam("dtEndTime", end.ToString()));
            prams.Add(Database.MakeInParam("nUserId", UserId));
            return Database.ExecuteDataset(CommandType.StoredProcedure, "NET_PW_CalculateRanking", prams.ToArray());
        }
        /// <summary>
        /// 根据sql获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public T GetEntity<T>(string commandText, List<DbParameter> parms)
        {
            return Database.ExecuteObject<T>(commandText, parms);
        }

        /// <summary>
        /// 根据sql获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public T GetEntity<T>(string commandText)
        {
            return Database.ExecuteObject<T>(commandText);
        }

        #endregion

        #region 任务系统

        /// <summary>
        /// 每日签到
        /// </summary>
        /// <param name="detialInfo"></param>
        /// <param name="receipt"></param>
        public Message WriteCheckIn(int userID, string strClientIP)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("strClientIP", strClientIP));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_WriteCheckIn", prams);
        }
        #endregion

        #region 会员卡

        /// <summary>
        /// 获取会员卡
        /// </summary>
        /// <returns></returns>
        public DataSet GetMemberCardList()
        {
            string sql = "SELECT * FROM MemberCard";
            return Database.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取会员卡实体
        /// </summary>
        /// <returns></returns>
        public MemberCard GetMemberCard(int cardID)
        {
            string sql = "SELECT * FROM MemberCard WHERE CardID=@CardID";
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("CardID", cardID));
            return Database.ExecuteObject<MemberCard>(sql, prams);
        }

        /// <summary>
        /// 购买会员卡，平台币兑换成金币
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="goodsID"></param>
        /// <param name="num"></param>
        /// <param name="insurePassword"></param>
        /// <returns></returns>
        public Message CurrencyExchange(int userID, int cardID, int num, string insurePassword, string IP)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("dwCardID", cardID));
            prams.Add(Database.MakeInParam("dwNum", num));
            prams.Add(Database.MakeInParam("strInsurePassword", insurePassword));
            prams.Add(Database.MakeInParam("strClientIP", IP));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            Message msg = MessageHelper.GetMessage(Database, "WSP_PW_CurrencyExchange", prams);
            return msg;
        }
        #endregion

        #region 游戏信息

        /// <summary>
        /// 获取游戏总局排行
        /// </summary>
        /// <param name="number"></param>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public IList<UserGameInfo> GetUserGameGrandTotalRank(int number, int dateID)
        {
            string sql = string.Format("SELECT TOP({0}) * FROM UserGameInfo_Line WHERE DateID=@DateID AND LineGrandTotal>0 ORDER BY LineGrandTotal DESC", number);

            List<DbParameter> param = new List<DbParameter>();
            param.Add(Database.MakeInParam("DateID", dateID));

            return Database.ExecuteObjectList<UserGameInfo>(sql, param);
        }

        /// <summary>
        /// 获取游戏最大赢排行
        /// </summary>
        /// <param name="number"></param>
        /// <param name="kindID"></param>
        /// <returns></returns>
        public IList<UserGameInfo> GetUserGameWinMaxRank(int number, int dateID)
        {
            string sql = string.Format("SELECT TOP({0}) * FROM UserGameInfo_Line WHERE DateID=@DateID AND LineWinMax>0 ORDER BY LineWinMax DESC", number);

            List<DbParameter> param = new List<DbParameter>();
            param.Add(Database.MakeInParam("DateID", dateID));

            return Database.ExecuteObjectList<UserGameInfo>(sql, param);
        }

        #endregion

        #region 代理商管理

        /// <summary>
        /// 获取代理商下级玩家贡献税收分成金币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Int64 GetChildRevenueProvide(int userID)
        {
            string sqlQuery = string.Format("SELECT ISNULL(SUM(AgentRevenue),0) FROM RecordUserRevenue WHERE UserID= {0}", userID);
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            if (obj == null || obj.ToString().Length <= 0)
                return 0;
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// 获取代理商下级玩家贡献的充值分成金币
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Int64 GetChildPayProvide(int userID)
        {
            string sqlQuery = string.Format("SELECT ISNULL(SUM(Score),0) FROM RecordAgentInfo WHERE ChildrenID= {0} AND TypeID=1", userID);
            object obj = Database.ExecuteScalar(CommandType.Text, sqlQuery);
            if (obj == null || obj.ToString().Length <= 0)
                return 0;
            return Convert.ToInt64(obj);
        }

        /// <summary>
        /// 获取代理分成详情
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataSet GetAgentFinance(int userID)
        {
            var prams = new List<DbParameter>();

            prams.Add(Database.MakeInParam("dwUserID", userID));

            return Database.ExecuteDataset(CommandType.StoredProcedure, "WSP_PM_GetAgentFinance", prams.ToArray());
        }

        /// <summary>
        /// 代理结算
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="userID"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message GetAgentBalance(int balance, int userID, string ip)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("dwBalance", balance));

            prams.Add(Database.MakeInParam("strClientIP", ip));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_AgentBalance", prams);
        }
        #endregion

        #region 转盘管理

        /// <summary>
        /// 获取转盘公共配置
        /// </summary>
        /// <returns></returns>
        public LotteryConfig GetLotteryConfig()
        {
            string sqlQuery = string.Format("SELECT * FROM LotteryConfig(NOLOCK) WHERE ID=1");
            LotteryConfig config = Database.ExecuteObject<LotteryConfig>(sqlQuery);

            return config;
        }

        /// <summary>
        /// 获取转盘配置
        /// </summary>
        /// <returns></returns>
        public IList<LotteryItem> GetLotteryItem()
        {
            string sql = "SELECT * FROM LotteryItem";
            return Database.ExecuteObjectList<LotteryItem>(sql);
        }

        /// <summary>
        /// 获取用户转盘信息
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Message GetLotteryUserInfo(int userID, string password)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("strLogonPass", password));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<LotteryUserInfo>(Database, "NET_PW_LotteryUserInfo", prams);
        }

        /// <summary>
        /// 抽奖
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="userID"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message GetLotteryStart(int userID, string password, string ip)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("strLogonPass", password));
            prams.Add(Database.MakeInParam("strClientIP", ip));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessageForObject<LotteryReturn>(Database, "NET_PW_LotteryStart", prams);
        }
        #endregion

        #region 分享

        /// <summary>
        /// 分享
        /// </summary>
        /// <param name="balance"></param>
        /// <param name="userID"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        public Message SharePresent(int userID, string password, string machineID, string ip)
        {
            List<DbParameter> prams = new List<DbParameter>();
            prams.Add(Database.MakeInParam("dwUserID", userID));
            prams.Add(Database.MakeInParam("strPassword", password));
            prams.Add(Database.MakeInParam("strMachineID", machineID));
            prams.Add(Database.MakeInParam("strClientIP", ip));
            prams.Add(Database.MakeOutParam("strErrorDescribe", typeof(string), 127));

            return MessageHelper.GetMessage(Database, "NET_PW_SharePresent", prams);
        }
        #endregion
    }
}
