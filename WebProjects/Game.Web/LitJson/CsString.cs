using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LitJson;
using System.Security.Cryptography;
using System.IO;
using System.IO.Compression;
using System.Data;

public class CsString
{
    static string SqlStr = @"like|and|or|exec|execute|insert|select|delete|update|alter|create|drop|count|\*|chr|char|asc|mid|substring|master|truncate|declare|xp_cmdshell|restore|backup|net +user|net +localgroup +administrators";

    public static bool ProcessSqlStr(string inputString)
    {
        //         try
        //         {
        //             if ((inputString != null) && (inputString != String.Empty))
        //             {
        //                 string str_Regex = @"\b(" + SqlStr + @")\b";
        //                 Regex Regex = new Regex(str_Regex, RegexOptions.IgnoreCase);
        //                 if (true == Regex.IsMatch(inputString))
        //                     return false;
        //             }
        //         }
        //         catch
        //         {
        //             return false;
        //         }
        return true;
    }
    public static string DecodeOutputString(string outputstring)
    {
        //要替换的敏感字
        try
        {
            if ((outputstring != null) && (outputstring != String.Empty))
            {
                string str_Regex = @"\[\b(" + SqlStr + @")\b\]";
                Regex Regex = new Regex(str_Regex, RegexOptions.IgnoreCase);
                MatchCollection matches = Regex.Matches(outputstring);
                for (int i = 0; i < matches.Count; i++)
                    outputstring = outputstring.Replace(matches[i].Value, matches[i].Value.Substring(1, matches[i].Value.Length - 2));
            }
        }
        catch
        {
            return "";
        }
        return outputstring;
    }
    public CsString(string _Value)
    {
        Data = _Value;
    }
    public CsString()
    {
        Data = "";
    }
    public CsString Fill(int _Value)
    {
        Data += string.Format("{0},", _Value);
        return this;
    }
    public CsString Fill(uint _Value)
    {
        Data += string.Format("{0},", _Value);
        return this;
    }
    public CsString Fill(short _Value)
    {
        Data += string.Format("{0},", _Value); ;
        return this;
    }
    public CsString Fill(bool _Value)
    {
        Data += string.Format("{0},", _Value ? 1 : 0);
        return this;
    }
    public CsString Fill(ushort _Value)
    {
        Data += string.Format("{0},", _Value); ;
        return this;
    }
    public CsString Fill(char _Value)
    {
        Data += string.Format("{0},", _Value); ;
        return this;
    }
    public CsString Fill(byte _Value)
    {
        Data += string.Format("{0},", _Value); ;
        return this;
    }
    public CsString Fill(byte[] _Value)
    {
        Data += string.Format("{0},", Convert.ToBase64String(_Value)); ;
        return this;
    }
    public CsString Fill(Int64 _Value)
    {
        Data += string.Format("{0},", _Value); ;
        return this;
    }
    public CsString Fill(UInt64 _Value)
    {
        Data += string.Format("{0},", _Value); ;
        return this;
    }
    public CsString Fill(float _Value)
    {
        Data += string.Format("{0},", _Value); ;
        return this;
    }
    public CsString Fill(double _Value)
    {
        Data += string.Format("{0},", _Value); ;
        return this;
    }
    public CsString Fill(string _Value)
    {
        if (_Value == null)
            _Value = "";
        Data += " '";
        Data += _Value;
        Data += "',";
        return this;
    }
    public CsString FillJson(string _Value)
    {
        if (_Value == null)
            _Value = "";
        else
            _Value = _Value.Replace("\\u", "\\\\u");
        Data += " '";
        Data += _Value;
        Data += "',";
        return this;
    }
    public CsString FillEx(string _Value)
    {
        if (_Value == null)
            _Value = "";
        Data += "'";
        Data += _Value;
        Data += "'";
        return this;
    }
    public CsString FillEx(int _Value)
    {
        Data += _Value.ToString();
        return this;
    }
    public CsString FillEx(uint _Value)
    {
        Data += _Value.ToString();
        return this;
    }
    public CsString FillEx(float _Value)
    {
        Data += _Value.ToString();
        return this;
    }
    public CsString FillEx(double _Value)
    {
        Data += _Value.ToString();
        return this;
    }
    public CsString FillJsonEx(string _Value)
    {
        if (_Value == null)
            _Value = "";
        else
            _Value = _Value.Replace("\\u", "\\\\u");
        Data += "'";
        Data += _Value;
        Data += "'";
        return this;
    }
    public CsString EndInsertJson(string _Value)
    {
        if (_Value == null)
            _Value = "";
        else
            _Value = _Value.Replace("\\u", "\\\\u");
        Data += "'";
        Data += _Value;
        Data += "')";
        return this;
    }
    public CsString EndInsert(string _Value)
    {
        if (_Value == null)
            _Value = "";
        Data += "'";
        Data += _Value;
        Data += "')";
        return this;
    }
    public CsString EndInsert(int _Value)
    {
        Data += _Value;
        Data += ")";
        return this;
    }
    public static string SqlString(string str)
    {
        return string.Format("'{0}'", str);
    }
    public static CsString InstertSql(string TableName, string colum, string _value)
    {
        CsString newStr = new CsString("INSERT INTO " + TableName + " (" + colum + ") " + "VALUES(" + _value + ")");
        return newStr;
    }
    public static CsString UpdateSql(string TableName)
    {
        CsString newStr = new CsString("UPDATE " + TableName + " SET ");
        return newStr;
    }
    //--------------------------------
    public static CsString operator <<(CsString My, int _Value)
    {
        My.Data += string.Format("{0},", _Value);
        return My;
    }
    public static CsString operator +(CsString My, CsString Other)
    {
        My.Data += Other.Data;
        return My;
    }
    public static CsString operator +(CsString My, string _Value)
    {
        My.Data += _Value;
        return My;
    }
    public static CsString operator +(CsString My, int _Value)
    {
        My.Data += _Value + " ";
        return My;
    }
    public static CsString operator +(CsString My, uint _Value)
    {
        My.Data += _Value + " ";
        return My;
    }
    public static CsString operator +(CsString My, short _Value)
    {
        My.Data += _Value + " ";
        return My;
    }
    public static CsString operator +(CsString My, byte _Value)
    {
        My.Data += _Value + " ";
        return My;
    }
    public static CsString operator +(CsString My, char _Value)
    {
        My.Data += _Value + " ";
        return My;
    }
    public static CsString operator +(CsString My, float _Value)
    {
        My.Data += _Value + " ";
        return My;
    }
    public string Data;
    public string GetStr() { return Data; }
    public override string ToString() { return Data; }
    public string JsonString()
    {
        Data = Data.Replace("\\u", "\\\\u");
        return Data;
    }
}

public class CsTxtToCn
{
    class CsRemoveString
    {
        public int startIndex = 0;
        public int endIndex = 0;
        public string Strs = "";
    }
    public static bool IsQQNum(char _char)
    {
        if (
            _char == '０' ||
            _char == '１' ||
            _char == '２' ||
            _char == '３' ||
            _char == '４' ||
            _char == '５' ||
            _char == '６' ||
            _char == '７' ||
            _char == '８' ||
            _char == '９'||
            _char == '0' ||
            _char == '1' ||
            _char == '2' ||
            _char == '3' ||
            _char == '4' ||
            _char == '5' ||
            _char == '6' ||
            _char == '7' ||
            _char == '8' ||
            _char == '9'
            )
            return true;
        return false;
    }
    public static bool IsQQNumString(string txt, int startIndex, out int Len)
    {
        Len = IsQQNumString(txt, startIndex);
        return Len != -1;
    }
    public static int IsQQNumString(string txt, int startIndex)
    {
        int i = 0;
        for (i = 0; i < txt.Length - startIndex&&i<12; i++)
        {
            if (!IsQQNum(txt[startIndex + i]))
                break;
        }
        if (i <= 5)
            return -1;
        return i;
    }
    public static string Convert(string txt, out int len)
    {
        len = 0;
        if (txt == null)
            return null;

        txt = txt.Replace("www", "");
        txt = txt.Replace("http", "");
        txt = txt.Replace("net", "");
        txt = txt.Replace("版主", "");
        txt = txt.Replace("div", "");

        txt = txt.Replace("\n\n", "\n");
        txt = txt.Replace("\n\n\n", "\n");
        txt = txt.Replace("\n\n\n\n", "\n");
        txt = txt.Replace("\n\n\n\n\n", "\n");

        txt = txt.Replace("\r\n\r\n", "\n");
        txt = txt.Replace("\r\n", "\n");
        txt = txt.Replace("\r\n\r\n\r\n", "\n");
        txt = txt.Replace("\r\n\r\n\r\n\r\n", "\n");
        txt = txt.Replace("\r\n\r\n\r\n\r\n\r\n", "\n");
        
        List<CsRemoveString> removes = new List<CsRemoveString>();
        CsRemoveString newCsRemoveString = null;
//         string newTxt = "";
//         for (int i = 0; i < txt.Length; i++)
//         {
//             if (txt[i] == '<')
//             {
//                 newCsRemoveString = new CsRemoveString();
//                 newCsRemoveString.startIndex = i;
//                 newCsRemoveString.Strs+=txt[i];
//             }
//             else if (txt[i] == '>')
//             {
//                 if (newCsRemoveString != null)
//                 {
//                     newCsRemoveString.Strs+=txt[i];
//                     newCsRemoveString.endIndex = i;
//                     newCsRemoveString = null;
//                 }
//             }
//             if (newCsRemoveString == null)
//             {
//                 newCsRemoveString.Strs += txt[i];
//                 newTxt += txt[i];
//             }
//         }
//         txt = newTxt;
        int _Len = 0;
        string txtString = "";
        int QQNum = -1;
        txt += "   ";
        for (int i = 0; i < txt.Length - 1; i++)
        {
            if (txt[i] == '<' || txt[i] == '>' ||
                txt[i] == '/' || txt[i] == '\\' ||
                txt[i] == 'Ｃ' ||
                txt[i] == 'Ｗ' ||
                txt[i] == 'Ｏ' ||
                txt[i] == 'Ｍ' ||
                txt[i] == 'c' || 
                txt[i] == 'o' || 
                txt[i] == 'm' || 
                txt[i] == 'w' || 
                txt[i] == 'Ｎ' ||
                txt[i] == 'Ｅ' ||
                txt[i] == 'Ｔ' ||
                txt[i] == 'Ｂ' ||
                txt[i] == 'Ｚ' ||
                txt[i] == 'Ａ' ||
                txt[i] == 'Ｅ' ||
                txt[i] == 'Ｏ' ||
                txt[i] == 'Ｕ' ||
                txt[i] == 'Ｉ' ||
                txt[i] == ' ' ||
                txt[i] == '　' ||             

                txt[i] == 'Ｑ' ||

                txt[i] == '$' || txt[i] == '}' ||
                txt[i] == '{' || txt[i] == '}' ||
                txt[i] == '%' || txt[i] == '|' ||
                txt[i] == '#' || txt[i] == '@' ||
                txt[i] == '&' || txt[i] == '^' ||
                txt[i] == '~' || txt[i] == '-' ||
                txt[i] == '=' || txt[i] == '+' ||
                txt[i] == '^' || 
                txt[i] == '*' || txt[i] == '`' ||
                txt[i] == '《' || txt[i] == '》' ||
                 txt[i] == '　' || txt[i] == ' ')
            {
                continue;
            }
            else if (txt[i] == '(') txtString += "（";
            else if (txt[i] == ')') txtString += "）";
            else if (txt[i] == '[') txtString += "【";
            else if (txt[i] == ']') txtString += "】";
            else if (txt[i] == '?') txtString += "？";
            else if (txt[i] == ',') txtString += "，";
            else if (txt[i] == '.') txtString += "。";
            else if (txt[i] == '!') txtString += "！";

            else if (txt[i] == '0') txtString += "０";
            else if (txt[i] == '1') txtString += "１";
            else if (txt[i] == '2') txtString += "２";
            else if (txt[i] == '3') txtString += "３";
            else if (txt[i] == '4') txtString += "４";
            else if (txt[i] == '5') txtString += "５";
            else if (txt[i] == '6') txtString += "６";
            else if (txt[i] == '7') txtString += "７";
            else if (txt[i] == '8') txtString += "８";
            else if (txt[i] == '9') txtString += "９";
            else if (IsQQNumString(txt, i, out QQNum))
            {
                i += QQNum-1;
                continue;
            }
            else if (txt[i] == '\n')
            {
//                 if (txt[i - 1] == '。' || txt[i - 1] == '？' || txt[i - 1] == '”' || txt[i - 1] == '！')
                txtString += "\n　　";
            }
            else
            {
                txtString += txt[i];
                _Len++;
            }
        }
        len = _Len;
        txt = txt.Replace("：\"", "：“");
        txt = txt.Replace("。\"", "。”");
        return "　　" + txtString;
    }
}





