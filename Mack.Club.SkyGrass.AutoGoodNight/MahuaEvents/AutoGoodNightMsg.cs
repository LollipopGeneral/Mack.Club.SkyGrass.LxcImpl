using LxcLibrary.Cache.MySql.lxc_pro_db.auto_good_night_log;
using LxcLibrary.CommonUtils;
using Newbe.Mahua;
using Newbe.Mahua.Logging;
using Newbe.Mahua.MahuaEvents;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Mack.Club.SkyGrass.AutoGoodNight.MahuaEvents
{
    /// <summary>
    /// 群消息接收事件
    /// </summary>
    public class AutoGoodNightMsg
        : IGroupMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;
        private const string dbConnString = "Database=***;Data Source=***;User ID=***;Password=***;CharSet=utf8mb4;SslMode=None;Connect Timeout=60;";

        public AutoGoodNightMsg(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessGroupMessage(GroupMessageReceivedContext context)
        {
            // 只上报#开头的晚安式消息
            string message = context.Message;
            message = message.Trim();

            if (message.StartsWith("#"))
            {
                ulong from_qq = Convert.ToUInt64(context.FromQq);
                ulong from_group = Convert.ToUInt64(context.FromGroup);
                ulong add_time = Convert.ToUInt64(UnixTimestamp.GetTimeStampLong());

                string nick_name = string.Empty;
                try
                {
                    nick_name = _mahuaApi.GetGroupMemberInfo(context.FromGroup, context.FromQq).NickName;
                }
                catch
                {
                    nick_name = from_qq.ToString();
                }

                AutoGoodNightLogCache cache = new AutoGoodNightLogCache();

                cache.Init(dbConnString);

                try
                {
                    string localPath = AppDomain.CurrentDomain.BaseDirectory;

                    localPath = Path.Combine(localPath, "data", "image");

                    // 图片酷Q码的转换
                    message = _replaceMsg(localPath, message);

                    // Emojid酷Q码的转换，暂时调整到获取的时候在转换
                    // message = CqpEmoji.EmojiStringToNormalString(message);

                    cache.InsertLog(from_qq, from_group, message, add_time, nick_name);
                }
                catch (Exception e)
                {
                    ILog Logger = Newbe.Mahua.Logging.LogProvider.For<string>();
                    Logger.Debug(e.StackTrace);
                }
            }
        }

        private string _replaceMsg(string localPath, string msg)
        {
            Regex reg = new Regex(@"\[CQ:image,file=.*\]");
            string cqCode = reg.Match(msg).Value;
            if(string.IsNullOrEmpty(cqCode) || cqCode == " ")
            {
                return msg;
            }
            
            string fileName = cqCode.Replace("[CQ:image,file=", "").Replace("]", "");
            string cqImgName = $"{fileName}.cqimg";

            string url = _imageToUrl(localPath, cqCode);
            string ret = string.Empty;

            if (string.IsNullOrEmpty(url))
            {
                ret = msg.Replace(cqCode, "");
            }
            else
            {
                ret = msg.Replace(fileName, url);
            }

            return ret;
        }

        private string _imageToUrl(string localPath, string cqCode)
        {
            string fileName = cqCode.Replace("[CQ:image,file=", "").Replace("]", "");
            string cqImgName = $"{fileName}.cqimg";
            string filePath = Path.Combine(localPath, cqImgName);
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                string url = string.Empty;
                foreach (var item in lines)
                {
                    if (item.StartsWith("url="))
                    {
                        url = item.Replace("url=", "");
                        url = url.Substring(0, url.IndexOf("?"));
                        break;
                    }
                }
                return url;
            }
            else
            {
                return string.Empty;
            }
        }
    }    
}
