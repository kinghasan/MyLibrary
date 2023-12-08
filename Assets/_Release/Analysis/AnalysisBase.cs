using System.Collections.Generic;

namespace Aya.Analysis
{
    public class AnalysisBase
    {
        #region Init & End

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// 结束
        /// </summary>
        public virtual void End()
        {
        }

        #endregion

        #region Info

        /// <summary>
        /// 设置帐号
        /// </summary>
        /// <param name="uid">唯一标识</param>
        /// <param name="nickName">昵称</param>
        /// <param name="type">账户类型</param>
        public virtual void SetAccount(string uid, string nickName, AccountType type = AccountType.Default)
        {
        }

        /// <summary>
        /// 设置服务器
        /// </summary>
        /// <param name="server">服务器</param>
        public virtual void SetServer(string server)
        {
        }

        /// <summary>
        /// 设置等级
        /// </summary>
        /// <param name="level">等级</param>
        public virtual void SetLevel(int level)
        {
        }

        /// <summary>
        /// 设置性别
        /// </summary>
        /// <param name="gender">性别类型</param>
        public virtual void SetGender(GenderType gender)
        {
        }

        /// <summary>
        /// 设置性别
        /// </summary>
        /// <param name="age"></param>
        public virtual void SetAge(int age)
        {
        }
        #endregion

        #region Item

        /// <summary>
        /// 购买物品
        /// </summary>
        /// <param name="itemName">物品名称</param>
        /// <param name="amount">数量</param>
        /// <param name="currency">消费虚拟币</param>
        public virtual void Purchase(string itemName, int amount, double currency)
        {
        }

        /// <summary>
        /// 使用物品
        /// </summary>
        /// <param name="itemName">物品名称</param>
        /// <param name="amount">数量</param>
        public virtual void Use(string itemName, int amount)
        {
        }

        #endregion

        #region Level

        public virtual void LevelStart(string level)
        {
        }

        public virtual void LevelCompleted(string level)
        {
        }

        public virtual void LevelFailed(string level)
        {
        }

        #endregion

        #region Mission

        /// <summary>
        /// 任务开始
        /// </summary>
        /// <param name="mission">任务名</param>
        /// <param name="type">任务类型</param>
        public virtual void MissionStart(string mission, MissionType type = MissionType.Unknown)
        {
        }

        /// <summary>
        /// 任务完成
        /// </summary>
        /// <param name="mission">任务名称</param>
        public virtual void MissionCompleted(string mission)
        {
        }

        /// <summary>
        /// 任务失败
        /// </summary>
        /// <param name="mission">任务名称</param>
        /// <param name="reason">失败原因</param>
        public virtual void MissionFailed(string mission, string reason)
        {
        }

        #endregion

        #region Event

        /// <summary>
        /// 自定义事件
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="args">参数列表</param>
        public void Event(string eventID, params object[] args)
        {
            // #if !UNITY_EDITOR
            var preArgs = new[]
            {
                "UserID", AnalysisManager.UserID,
                "RegisterTime", AnalysisManager.RegisterTime,
                "EventTime", AnalysisManager.EventTime,
                "RegisterVersion",AnalysisManager.RegisterVersion,
                //"ABID",ABTestSetting.Ins.Version.ToString()
            };
            var newArgs = new List<object>();
            newArgs.AddRange(preArgs);
            newArgs.AddRange(args);
            // Event(eventID + "_" + ABTestSetting.Ins.Version, AnalysisUtil.ParamToDic(newArgs.ToArray()));
            // #endif
        }

        /// <summary>
        /// 自定义事件
        /// </summary>
        /// <param name="eventID">事件ID</param>
        /// <param name="add"></param>
        /// <param name="args">事件参数</param>
        public virtual void Event(string eventID, Dictionary<string, object> args)
        {
        }

#if GoogleAnalytic
		public virtual void Event(string category, string action, string lable, int value) 
		{
		}
#endif

        #endregion

    }
}
