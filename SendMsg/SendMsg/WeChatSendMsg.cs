using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

/// <summary>
/// 实体
/// </summary>
namespace SendMsg
{
    public class WeChatSendMsg
    {
        public string touser { set; get; }
        public string template_id { set; get; }
        public string url { set; get; }
        public data data;
    }
    public class data
    {
        public first first;
        public keyword1 keyword1;
        public keyword2 keyword2;
        public keyword3 keyword3;
        public keyword4 keyword4;
        public Remark remark;
    }
    public class first
    {
        public string value { set; get; }
        public string color { set; get; }
    }
    public class keyword1
    {
        public string value { set; get; }
        public string color { set; get; }
    }
    public class keyword2
    {
        public string value { set; get; }
        public string color { set; get; }
    }
    public class keyword3
    {
        public string value { set; get; }
        public string color { set; get; }
    }
    public class keyword4
    {
        public string value { set; get; }
        public string color { set; get; }
    }
    public class Remark
    {
        public string value { set; get; }
        public string color { set; get; }
    }







    //行业
    public class industry
    {
        public string industry_id1 { set; get; }
        public string industry_id2 { set; get; }
    }
    //获取模版id接受类
    public class templateMsg
    {
        public string errcode { set; get; }
        public string errmsg { set; get; }
        public string template_id { set; get; }
    }
    //消息模版
    public class MsgTemp
    {
        public int LocOperatorInfo_ID { set; get; }
        public string template_id { set; get; }
        public int type { set; get; }
    }
    //Token对象
    public class token
    {
        public string access_token { set; get; }
    }
    //微信消息推送后的返回值
    public class resultSendMsg
    {
        public string errcode { set; get; }
        public string errmsg { set; get; }
        public string msgid { set; get; }
    }

}
