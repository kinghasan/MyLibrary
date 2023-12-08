using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Analysis
{
    public static class AnalysisManager
    {
        public static AnalysisBase _instance;
        public static AnalysisBase Instance
        {
            get
            {
                if (_instance == null)
                {
#if SuperSonic
                    _instance = new AnalysisBase
#else
                    _instance = new AnalysisBase();
#endif
                }
                return _instance;
            }
        }

		#region UserID
		public static string UserID
		{
			get
			{
				if (string.IsNullOrEmpty(_userID))
				{
					_userID = SystemInfo.deviceUniqueIdentifier;
				}
				return _userID;
			}
		}
		private static string _userID;

		public static string AnalysisUserID
		{
			get
			{
				var analysisUserID = UserID + "_" + RegisterTime + "+" + DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
				return analysisUserID;
			}
		}
		#endregion

		#region Setting
		// 第一次启动游戏的时间
		public static string RegisterTime
		{
			get
			{
				var str = PlayerPrefs.GetString("FirstLaunchTime", "");
				if (string.IsNullOrEmpty(str))
				{
					str = DateTime.UtcNow.AddHours(8).ToString("yyyyMMdd");
					PlayerPrefs.SetString("FirstLaunchTime", str);
				}
				return str;
			}
		}
		#endregion

		#region Setting
		// 第一次启动游戏的版本
		public static string RegisterVersion
		{
			get
			{
				var str = PlayerPrefs.GetString("FirstLaunchVersion", "");
				if (string.IsNullOrEmpty(str))
				{
					str = Application.version;
					PlayerPrefs.SetString("FirstLaunchVersion", str);
				}
				return str;
			}
		}
		#endregion

		#region Setting
		// 第一次启动游戏的时间
		public static string EventTime
		{
			get
			{
				return DateTime.UtcNow.AddHours(8).ToString("yyyyMMdd-HHmmss");
			}
		}
		#endregion
	}
}
