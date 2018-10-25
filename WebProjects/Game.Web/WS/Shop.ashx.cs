using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Game.Utils;
using Game.Facade;
using Game.Entity.NativeWeb;
using Game.Kernel;
using Game.Entity.Accounts;
using System.Text.RegularExpressions;

namespace Game.Web.Ashx
{
    /// <summary>
    /// 商城系统
    /// </summary>
    public class Shop : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            string action = GameRequest.GetQueryString("action").ToLower();
            switch(action)
            {
                case "buyaward":
                    BuyAward(context);
                    break;
                case "returnaward":
                    ReturnAward(context);
                    break;
                case "mobilegetawardlist":
                    MobileGetAwardList(context);
                    break;
                case "mobilegetawardinfo":
                    MobileGetAwardInfo(context);
                    break;
                case "mobilebuyaward":
                    MobileBuyAward(context);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 购买商品
        /// </summary>
        /// <param name="context"></param>
        public void BuyAward(HttpContext context)
        {
            Message msg = new Message();
            AjaxJsonValid ajaxJson = new AjaxJsonValid();

            //判断登录
            if(!Fetch.IsUserOnline())
            {
                ajaxJson.code = 1;
                ajaxJson.msg = "请先登录";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //获取参数
            int typeID = GameRequest.GetQueryInt("TypeID", 0);
            int awardID = GameRequest.GetFormInt("awardID", 0);           //商品ID
            int counts = GameRequest.GetFormInt("counts", 0);             //购买数量
            string compellation = TextFilter.FilterScript(GameRequest.GetFormString("name"));      //真实姓名
            string mobilePhone = TextFilter.FilterScript(GameRequest.GetFormString("phone"));      //移动电话
            int province = GameRequest.GetFormInt("province", -1);        //省份
            int city = GameRequest.GetFormInt("city", -1);                //城市
            int area = GameRequest.GetFormInt("area", -1);                //地区
            string dwellingPlace = TextFilter.FilterScript(GameRequest.GetFormString("address"));  //详细地址

            //验证奖品
            if(awardID == 0)
            {
                ajaxJson.msg = "非常抱歉，你所选购的商品不存在！";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //验证数量
            if (counts <= 0)
            {
                ajaxJson.msg = "请输入正确的兑换数量！";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }
            if (counts > 100)
            {
                ajaxJson.msg = "兑换数量不能超过100！";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            AwardInfo awardInfo = FacadeManage.aideNativeWebFacade.GetAwardInfo(awardID);
            int needInfo = awardInfo.NeedInfo;
            int qqValue = (int)AppConfig.AwardNeedInfoType.QQ号码;
            int nameValue = (int)AppConfig.AwardNeedInfoType.真实姓名;
            int phoneValue = (int)AppConfig.AwardNeedInfoType.手机号码;
            int addressValue = (int)AppConfig.AwardNeedInfoType.收货地址及邮编;

            //验证真实姓名
            if((needInfo & nameValue) == nameValue)
            {
                msg = CheckingRealNameFormat(compellation, false);
                if(!msg.Success)
                {
                    ajaxJson.msg = "请输入正确的收件人";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
            }

            //验证手机号
            if((needInfo & phoneValue) == phoneValue)
            {
                msg = CheckingMobilePhoneNumFormat(mobilePhone, false);
                if(!msg.Success)
                {
                    ajaxJson.msg = "请输入正确的手机号码";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
            }

            //验证地址邮编
            if((needInfo & addressValue) == addressValue)
            {
                if(province == -1)
                {
                    ajaxJson.msg = "请选择省份";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                if(city == -1)
                {
                    ajaxJson.msg = "请选择城市";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                if(area == -1)
                {
                    ajaxJson.msg = "请选择地区";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }
                if(string.IsNullOrEmpty(dwellingPlace))
                {
                    ajaxJson.msg = "请输入详细地址";
                    context.Response.Write(ajaxJson.SerializeToJson());
                    return;
                }                
            }

            //验证用户
            UserInfo userInfo = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(Fetch.GetUserCookie().UserID, 0, "").EntityList[0] as UserInfo;

            //验证余额
            int totalAmount = awardInfo.Price * counts;     //总金额
            if(totalAmount > userInfo.UserMedal)
            {
                ajaxJson.msg = "很抱歉！您的元宝数不足，不能兑换该奖品";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //验证库存
            if(awardInfo.Inventory <= 0)
            {
                ajaxJson.msg = "很抱歉！奖品的库存数不足，请更新其他奖品或者等待补充库存";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //扣除奖牌
            userInfo.UserMedal = userInfo.UserMedal - totalAmount;

            //更新奖牌
            AwardOrder awardOrder = new AwardOrder();
            awardOrder.UserID = userInfo.UserID;
            awardOrder.AwardID = awardID;
            awardOrder.AwardPrice = awardInfo.Price;
            awardOrder.AwardCount = counts;
            awardOrder.TotalAmount = totalAmount;
            awardOrder.Compellation = compellation;
            awardOrder.MobilePhone = mobilePhone;
            awardOrder.QQ = "";
            awardOrder.Province = province;
            awardOrder.City = city;
            awardOrder.Area = area;
            awardOrder.DwellingPlace = dwellingPlace;
            awardOrder.PostalCode = "";
            awardOrder.BuyIP = Utility.UserIP;

            msg = FacadeManage.aideNativeWebFacade.BuyAward(awardOrder);
            if(msg.Success)
            {
                ajaxJson.SetValidDataValue(true);
                ajaxJson.msg = "恭喜您！兑换成功";
                awardOrder = msg.EntityList[0] as AwardOrder;
                if (typeID == 0)
                {
                    ajaxJson.AddDataItem("uri", "/Shop/Order.aspx?param=" + awardOrder.AwardID);
                }
                else
                {
                    ajaxJson.AddDataItem("uri", "/Mobile/Shop/Order.aspx?param=" + awardOrder.AwardID);
                }
                context.Response.Write(ajaxJson.SerializeToJson());
            }
            else
            {
                ajaxJson.msg = msg.Content;
                context.Response.Write(ajaxJson.SerializeToJson());
            }
        }

        /// <summary>
        /// 申请退货
        /// </summary>
        /// <param name="context"></param>
        public void ReturnAward(HttpContext context)
        {
            Message msg = new Message();
            AjaxJsonValid ajaxJson = new AjaxJsonValid();

            //判断登录
            if(!Fetch.IsUserOnline())
            {
                ajaxJson.code = 1;
                ajaxJson.msg = "请先登录";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //验证订单号
            int orderID = GameRequest.GetQueryInt("orderid", 0);           //订单号
            if(orderID == 0)
            {
                return;
            }

            AwardOrder awardOrder = FacadeManage.aideNativeWebFacade.GetAwardOrder(orderID, Fetch.GetUserCookie().UserID);
            if(awardOrder == null)
            {
                ajaxJson.msg = "申请退货失败，订单不存在";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }
            if(awardOrder.OrderStatus != 1 && awardOrder.OrderStatus != 2)
            {
                ajaxJson.msg = "此订单暂不允许退货";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //验证商品是否允许退货
            AwardInfo awardInfo = FacadeManage.aideNativeWebFacade.GetAwardInfo(awardOrder.AwardID);
            if(!awardInfo.IsReturn)
            {
                ajaxJson.msg = "此商品属于不予退货服务的产品范畴";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            awardOrder.OrderStatus = (int)AppConfig.AwardOrderStatus.申请退货;
            FacadeManage.aideNativeWebFacade.UpdateAwardOrderStatus(awardOrder);
            ajaxJson.SetValidDataValue(true);
            ajaxJson.msg = "申请退货成功，请等待客服审核";
            context.Response.Write(ajaxJson.SerializeToJson());
            return;
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="context"></param>
        public void MobileGetAwardList(HttpContext context)
        {
            AjaxJsonValid ajv = new AjaxJsonValid();
            int page = GameRequest.GetQueryInt("page", 1);
            int pageSize = GameRequest.GetQueryInt("pageSize", 20);
            string orderBy = "ORDER BY SortID DESC";
            string where = " WHERE Nullity=0";
            PagerSet pagerSet = FacadeManage.aideNativeWebFacade.GetList(AwardInfo.Tablename, page, pageSize, orderBy, where);
            IList<AwardInfo> list = DataHelper.ConvertDataTableToObjects<AwardInfo>(pagerSet.PageSet.Tables[0]);
            if (list != null)
            {
                foreach (AwardInfo item in list)
                {
                    item.SmallImage = Fetch.GetUploadFileUrl(item.SmallImage);
                    item.BigImage = Fetch.GetUploadFileUrl(item.BigImage);
                }
            }
            ajv.AddDataItem("List", list);
            ajv.AddDataItem("RecordCount", pagerSet.RecordCount);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 移动获取奖品
        /// </summary>
        /// <param name="context"></param>
        public void MobileGetAwardInfo(HttpContext context)
        {
            int awardid = GameRequest.GetQueryInt("awardID", 0);

            AjaxJsonValid ajv = new AjaxJsonValid();

            if (awardid == 0)
            {
                ajv.msg = "缺少参数奖品ID";
                context.Response.Write(ajv.SerializeToJson());
                return;
            }

            AwardInfo awardInfo = FacadeManage.aideNativeWebFacade.GetAwardInfo(awardid);
            if (awardInfo != null)
            {
                awardInfo.SmallImage = Fetch.GetUploadFileUrl(awardInfo.SmallImage);
                awardInfo.BigImage = Fetch.GetUploadFileUrl(awardInfo.BigImage);
            }
            ajv.AddDataItem("Data", awardInfo);
            ajv.SetValidDataValue(true);
            context.Response.Write(ajv.SerializeToJson());
        }

        /// <summary>
        /// 移动购买奖品
        /// </summary>
        /// <param name="context"></param>
        public void MobileBuyAward(HttpContext context)
        {
            Message msg = new Message();
            AjaxJsonValid ajaxJson = new AjaxJsonValid();

            int userID = GameRequest.GetFormInt("userID", 0);                //用户标识
            string signature = GameRequest.GetFormString("signature");       //签名
            string time = GameRequest.GetFormString("time");                 //过期时间

            //验证签名
            Message message = FacadeManage.aideAccountsFacade.CheckUserSignature(userID, time, signature);
            if (!message.Success)
            {
                ajaxJson.msg = message.Content;
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //获取参数
            int awardID = GameRequest.GetFormInt("awardID", 0);           //商品ID
            int counts = GameRequest.GetFormInt("counts", 0);             //购买数量
            string compellation = TextFilter.FilterScript(GameRequest.GetFormString("name"));      //真实姓名
            string mobilePhone = TextFilter.FilterScript(GameRequest.GetFormString("phone"));      //移动电话
            int province = GameRequest.GetFormInt("province", -1);        //省份
            int city = GameRequest.GetFormInt("city", -1);                //城市
            int area = GameRequest.GetFormInt("area", -1);                //地区
            string dwellingPlace = TextFilter.FilterScript(GameRequest.GetFormString("address"));  //详细地址  

            //验证奖品
            if (awardID == 0)
            {
                ajaxJson.msg = "非常抱歉，你所选购的商品不存在！";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //验证数量
            if (counts <= 0)
            {
                ajaxJson.msg = "请输入正确的兑换数量！";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }
            if (counts > 99)
            {
                ajaxJson.msg = "每次兑换的数量最多为 99 件";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            AwardInfo awardInfo = FacadeManage.aideNativeWebFacade.GetAwardInfo(awardID);

            //验证真实姓名
            msg = CheckingRealNameFormat(compellation, false);
            if (!msg.Success)
            {
                ajaxJson.msg = "请输入正确的收件人";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //验证手机号
            msg = CheckingMobilePhoneNumFormat(mobilePhone, false);
            if (!msg.Success)
            {
                ajaxJson.msg = "请输入正确的手机号码";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //验证地址
            if (province == -1)
            {
                ajaxJson.msg = "请选择省份";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }
            if (city == -1)
            {
                ajaxJson.msg = "请选择城市";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }
            if (area == -1)
            {
                ajaxJson.msg = "请选择地区";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }
            if (string.IsNullOrEmpty(dwellingPlace))
            {
                ajaxJson.msg = "请输入详细地址";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //  防止数据溢出，商品单价不能超过2000万
            if (awardInfo.Price > 20000000)
            {
                ajaxJson.msg = "很抱歉，该商品暂停兑换！";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //验证用户
            UserInfo userInfo = FacadeManage.aideAccountsFacade.GetUserGlobalInfo(userID, 0, "").EntityList[0] as UserInfo;

            //验证余额
            int totalAmount = awardInfo.Price * counts;     //总金额
            if (totalAmount > userInfo.UserMedal)
            {
                ajaxJson.msg = "很抱歉！您的元宝数不足，不能兑换该奖品";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //验证库存
            if (awardInfo.Inventory <= counts)
            {
                ajaxJson.msg = "很抱歉！奖品的库存数不足，请更新其他奖品或者等待补充库存";
                context.Response.Write(ajaxJson.SerializeToJson());
                return;
            }

            //扣除奖牌
            userInfo.UserMedal = userInfo.UserMedal - totalAmount;

            //更新奖牌
            AwardOrder awardOrder = new AwardOrder();
            awardOrder.UserID = userInfo.UserID;
            awardOrder.AwardID = awardID;
            awardOrder.AwardPrice = awardInfo.Price;
            awardOrder.AwardCount = counts;
            awardOrder.TotalAmount = totalAmount;
            awardOrder.Compellation = compellation;
            awardOrder.MobilePhone = mobilePhone;
            awardOrder.QQ = "";
            awardOrder.Province = province;
            awardOrder.City = city;
            awardOrder.Area = area;
            awardOrder.DwellingPlace = dwellingPlace;
            awardOrder.PostalCode = "";
            awardOrder.BuyIP = Utility.UserIP;

            msg = FacadeManage.aideNativeWebFacade.BuyAward(awardOrder);
            if (msg.Success)
            {
                ajaxJson.SetValidDataValue(true);
                ajaxJson.msg = "恭喜您！兑换成功";
                awardOrder = msg.EntityList[0] as AwardOrder;
                context.Response.Write(ajaxJson.SerializeToJson());
            }
            else
            {
                ajaxJson.msg = msg.Content;
                context.Response.Write(ajaxJson.SerializeToJson());
            }
        }

        /// <summary>
        /// 验证真实姓名
        /// </summary>
        /// <param name="realName"></param>
        /// <returns></returns>
        public static Message CheckingRealNameFormat(string realName, bool isAllowNull)
        {
            Message msg = new Message();
            if (isAllowNull && string.IsNullOrEmpty(realName))
            {
                return msg;
            }
            if (!isAllowNull && string.IsNullOrEmpty(realName))
            {
                msg.Success = false;
                msg.Content = "真实姓名不能为空";
                return msg;
            }
            if (realName.Length > 16)
            {
                msg.Success = false;
                msg.Content = "真实姓名太长";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证QQ号码
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="isAllowNull"></param>
        /// <returns></returns>
        public static Message CheckingQQFormat(string qq, bool isAllowNull)
        {
            Message msg = new Message();
            if (isAllowNull && string.IsNullOrEmpty(qq))
            {
                return msg;
            }
            if (!isAllowNull && string.IsNullOrEmpty(qq))
            {
                msg.Success = false;
                msg.Content = "QQ号码不能为空";
                return msg;
            }
            Regex reg = new Regex(@"^\d{4,20}$");
            if (!string.IsNullOrEmpty(qq) && !reg.IsMatch(qq))
            {
                msg.Success = false;
                msg.Content = "QQ号码格式不对";
                return msg;
            }
            return msg;
        }

        /// <summary>
        /// 验证移动电话号码
        /// </summary>
        /// <param name="mobilePhoneNum"></param>
        /// <returns></returns>
        public static Message CheckingMobilePhoneNumFormat(string mobilePhoneNum, bool isAllowNull)
        {
            Message msg = new Message();
            if (isAllowNull && string.IsNullOrEmpty(mobilePhoneNum))
            {
                return msg;
            }
            if (!isAllowNull && string.IsNullOrEmpty(mobilePhoneNum))
            {
                msg.Success = false;
                msg.Content = "电话号码不能为空";
                return msg;
            }

            Regex isMobile = new Regex(@"^\d{11}$", RegexOptions.Compiled);
            if (!isMobile.IsMatch(mobilePhoneNum))
            {
                msg.Success = false;
                msg.Content = "移动电话格式不正确";
                return msg;
            }
            return msg;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}