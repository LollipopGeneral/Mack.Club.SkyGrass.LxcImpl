using LxcLibrary.Cache.MySql.lxc_pro_db.auto_good_night_log;
using LxcLibrary.CommonUtils;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using System;

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

                AutoGoodNightLogCache cache = new AutoGoodNightLogCache();

                cache.Init(dbConnString);

                cache.InsertLog(from_qq, from_group, message, add_time);                
            }
        }
    }    
}
