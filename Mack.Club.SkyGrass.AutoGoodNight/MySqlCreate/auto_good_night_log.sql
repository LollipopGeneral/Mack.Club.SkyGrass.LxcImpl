# Host: 120.79.45.128  (Version 5.7.20-0ubuntu0.16.04.1)
# Date: 2019-01-03 22:44:03
# Generator: MySQL-Front 6.1  (Build 1.26)


#
# Structure for table "auto_good_night_log"
#

CREATE TABLE `auto_good_night_log` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `from_qq` bigint(20) unsigned NOT NULL DEFAULT '0',
  `from_group` bigint(20) unsigned NOT NULL DEFAULT '0',
  `add_time` bigint(20) unsigned NOT NULL DEFAULT '0',
  `message` varchar(128) NOT NULL DEFAULT '',
  `status` int(11) unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='晚安式消息日志';
