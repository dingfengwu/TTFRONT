using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
public class RetureCode
{
    public int code = 0;
    public string msg = "";
}

public class LoginReturnCode:RetureCode
{
    public string gameName { get; set; }

    public int kindId { get; set; }
}

public class CodeData
{
    public string CountryCode;
    public string PhoneNum;
    public string CodeNum;
    public string Data;
    public DateTime CreateTime = DateTime.Now;
    public bool HasExp()
    {
        return (DateTime.Now - CreateTime).TotalMinutes >= 30;
    }
}
public class PhoneCode
{
    public static PhoneCode GPhoneCode = new PhoneCode();
    public Dictionary<string, CodeData> Codes = new Dictionary<string, CodeData>();
    public void Add(string phone, CodeData code)
    {
        lock (Codes)
        {
            Codes.Remove(phone);
            Codes.Add(phone, code);
        }
    }
    public CodeData CheckCode(string CountryCode, string PhoneNum, string CodeNum)
    {
        lock (Codes)
        {
            if (Codes.ContainsKey(CountryCode + PhoneNum))
            {
                CodeData d = Codes[CountryCode + PhoneNum];
                if (d.CodeNum == CodeNum)
                {
                    Codes.Remove(CountryCode + PhoneNum);
                    if (!d.HasExp())
                        return d;
                }
            }
            return null;
        }
    }
    public CodeData FindCode(string CountryCode, string PhoneNum)
    {
        lock (Codes)
        {
            if (Codes.ContainsKey(CountryCode + PhoneNum))
            {
                CodeData d = Codes[CountryCode + PhoneNum];
                if (!d.HasExp())
                    return d;
            }
            return null;
        }
    }
}
public class CommonTools
{
    public static string GetRequest(HttpContext context)
    {
        Stream sm = context.Request.InputStream;
        using (StreamReader inputData = new StreamReader(sm))
        {
            string DataString = inputData.ReadToEnd();
            return DataString;
        }
    }
    public static void SendStringToClient(HttpContext hd, System.Object obj)
    {
        hd.Response.ContentType = "text/plain;charset=UTF-8";
        hd.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
    }
    public static int StringToInt(object x)
    {
        if (x == null)
            return 0;
        int o = 0;
        int.TryParse(x.ToString(), out o);
        return 0;
    }
    public static void SendStringToClient(HttpContext hd, string obj)
    {
        hd.Response.ContentType = "text/plain;charset=UTF-8";
        hd.Response.Write(obj);
    }
    public static void SendStringToClient(HttpContext hd, int codeId, string Msg)
    {
        RetureCode code = new RetureCode();
        code.code = codeId;
        code.msg = Msg;
        SendStringToClient(hd, code);
    }

    static Random rd = new Random();
    public static string CreatePassWord(int num)
    {
        string str = "";
        for (int i = 0; i < num; i++)
        {
            str += (char)(((int)'0') + rd.Next(10));
        }
        return str;
    }
    public static string GetMD5Hash(string fileName)
    {
        try
        {
            MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(fileName));
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(ms);
            ms.Close();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
        }
    }
}