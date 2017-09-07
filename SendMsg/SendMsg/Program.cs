using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// WeChatMessagePush
/// </summary>
namespace SendMsg
{
    class Program
    {
        static void Main(string[] args)
        {
            string appid = string.Empty;
            string secret = string.Empty;

            //获取token
            var Access_token= GetAccess_token(appid, secret);

            //获取微信公众号内所有关注用户OpenID,这里获取到的是一个集合,下面发送消息的openid我取的我自己示例中也是给自己推送消息
            string url = $"https://api.weixin.qq.com/cgi-bin/user/get?access_token={Access_token}";
            var Userlist = HttpHelper.GetData(url);
            Console.WriteLine(Userlist);
            //假设我的openid为
            string Openid = "oTtLIvlBzKIz8r5mv2hbhCe5eDAY";


            //如果你想要获取单个用户的详细信息
            //string url = $"https://api.weixin.qq.com/cgi-bin/user/info?access_token={Access_token}&openid={Openid}&lang=zh_CN";


            //假设选取的发送模板id为HJHJSKDHFJD-jkfjk21fd4fsd245dkjf
            string template_id = "HJHJSKDHFJD-jkfjk21fd4fsd245dkjf";
            //开始发送消息
            var result = SendMsg(Openid, template_id, "https://www.peyshine.com", Access_token);


            Console.Read();
        }



        /// <summary>
        /// 获取发送消息的模版ID
        /// </summary>
        /// <param name="token"></param>
        /// <param name="template_id_short">例如订单提醒：OPENTM206831784</param>
        /// <returns></returns>
        public static string Gettemplate_id(string token, string template_id_short)
        {
            //获取模版Id
            //{"errcode":0,"errmsg":"ok","template_id":"AL4dD5VlHaMg6Y1-2ShNxlsGVG9_sRtl1XjQFMskBKg"}
            //{"errcode":45026,"errmsg":"template num exceeds limit hint: [G8UEcA0340vr20]"} 模版数量达到上限15个
            string getTempidUrl = "https://api.weixin.qq.com/cgi-bin/template/api_add_template?access_token=" + token;
            var tempMsg = HttpHelper.PostData(getTempidUrl, "{" + '"' + "template_id_short" + '"' + ":" + '"' + template_id_short + '"' + "}");
            templateMsg templateMsg = JsonHelper.JsonToObject<templateMsg>(tempMsg);
            return templateMsg.template_id;
        }

        /// <summary>
        /// 获取Access_token
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static string GetAccess_token(string appid, string secret)
        {
            var url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential";
            var tokenJson = HttpHelper.GetData(url + "&appid=" + appid + "&secret=" + secret);
            token token = JsonHelper.JsonToObject<token>(tokenJson);
            return token.access_token;
        }

        /// <summary>
        /// 设置行业
        /// </summary>
        /// <param name="indu1"></param>
        /// <param name="indu2"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string Set_industry(string indu1, string indu2, string token)
        {
            industry industry = new industry();
            industry.industry_id1 = "1";    //电商
            industry.industry_id2 = "40";   //印刷
            var pdata = JsonHelper.ObjectToJSON(industry);
            HttpHelper.PostData("https://api.weixin.qq.com/cgi-bin/template/api_set_industry?access_token= + token", pdata);
            return "";
        }


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="toUser">发送的Openid</param>
        /// <param name="template_id">模板id</param>
        /// <param name="JumpUrl">点击消息跳转的地址</param>
        /// <param name="token">公众号的Access_token</param>
        /// <returns></returns>
        public static string SendMsg(string toUser, string template_id, string JumpUrl, string token)
        {
            //发送消息
            //1.组装发送对象
            first first = new first();
            first.value = "接单成功，正在火速赶来，请耐心等待(测试数据) ";
            first.color = "#173177";
            keyword1 k1 = new keyword1();
            k1.value = "搭电";//System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            k1.color = "#000";
            keyword2 k2 = new keyword2();
            k2.value = "京A88888";
            k2.color = "#000";
            keyword3 k3 = new keyword3();
            k3.value = "何师傅";
            k3.color = "#000";
            keyword4 k4 = new keyword4();
            k4.value = "13900000000";
            k4.color = "#000";
            Remark remark = new Remark();
            remark.value = "感谢您的使用，如有疑问，请拨打客服电话110";
            remark.color = "#000";

            data data = new data();
            data.first = first;
            data.keyword1 = k1;
            data.keyword2 = k2;
            data.keyword3 = k3;
            data.keyword4 = k4;
            data.remark = remark;

            WeChatSendMsg msg = new WeChatSendMsg();
            msg.touser = toUser;
            msg.template_id = template_id;
            msg.url = JumpUrl;
            msg.data = data;

            var postData = JsonHelper.ObjectToJSON(msg);
            string res = HttpHelper.PostData("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + token, postData);
            return res;
        }


    }



}
