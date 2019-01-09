﻿using Newbe.Mahua;

namespace Mack.Club.SkyGrass.AutoGoodNight
{
    /// <summary>
    /// 本插件的基本信息
    /// </summary>
    public class PluginInfo : IPluginInfo
    {
        /// <summary>
        /// 版本号，建议采用 主版本.次版本.修订号 的形式
        /// </summary>
        public string Version { get; set; } = "1.0.2";

        /// <summary>
        /// 插件名称
        /// </summary>

        public string Name { get; set; } = "灵性草-自动化晚安式";

        /// <summary>
        /// 作者名称
        /// </summary>
        public string Author { get; set; } = "棒棒糖将军";

        /// <summary>
        /// 插件Id，用于唯一标识插件产品的Id，至少包含 AAA.BBB.CCC 三个部分
        /// </summary>
        public string Id { get; set; } = "Mack.Club.SkyGrass.AutoGoodNight";

        /// <summary>
        /// 插件描述
        /// </summary>
        public string Description { get; set; } = "自动化晚安式上报消息插件，只包含去掉前导空白字符后，以#开头的消息";
    }
}
