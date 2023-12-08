using System.Collections.Generic;

namespace Aya.Analysis
{
    public class AnalysisBase
    {
        #region Init & End

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public virtual void Init()
        {
        }

        /// <summary>
        /// ����
        /// </summary>
        public virtual void End()
        {
        }

        #endregion

        #region Info

        /// <summary>
        /// �����ʺ�
        /// </summary>
        /// <param name="uid">Ψһ��ʶ</param>
        /// <param name="nickName">�ǳ�</param>
        /// <param name="type">�˻�����</param>
        public virtual void SetAccount(string uid, string nickName, AccountType type = AccountType.Default)
        {
        }

        /// <summary>
        /// ���÷�����
        /// </summary>
        /// <param name="server">������</param>
        public virtual void SetServer(string server)
        {
        }

        /// <summary>
        /// ���õȼ�
        /// </summary>
        /// <param name="level">�ȼ�</param>
        public virtual void SetLevel(int level)
        {
        }

        /// <summary>
        /// �����Ա�
        /// </summary>
        /// <param name="gender">�Ա�����</param>
        public virtual void SetGender(GenderType gender)
        {
        }

        /// <summary>
        /// �����Ա�
        /// </summary>
        /// <param name="age"></param>
        public virtual void SetAge(int age)
        {
        }
        #endregion

        #region Item

        /// <summary>
        /// ������Ʒ
        /// </summary>
        /// <param name="itemName">��Ʒ����</param>
        /// <param name="amount">����</param>
        /// <param name="currency">���������</param>
        public virtual void Purchase(string itemName, int amount, double currency)
        {
        }

        /// <summary>
        /// ʹ����Ʒ
        /// </summary>
        /// <param name="itemName">��Ʒ����</param>
        /// <param name="amount">����</param>
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
        /// ����ʼ
        /// </summary>
        /// <param name="mission">������</param>
        /// <param name="type">��������</param>
        public virtual void MissionStart(string mission, MissionType type = MissionType.Unknown)
        {
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="mission">��������</param>
        public virtual void MissionCompleted(string mission)
        {
        }

        /// <summary>
        /// ����ʧ��
        /// </summary>
        /// <param name="mission">��������</param>
        /// <param name="reason">ʧ��ԭ��</param>
        public virtual void MissionFailed(string mission, string reason)
        {
        }

        #endregion

        #region Event

        /// <summary>
        /// �Զ����¼�
        /// </summary>
        /// <param name="eventID">�¼�ID</param>
        /// <param name="args">�����б�</param>
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
        /// �Զ����¼�
        /// </summary>
        /// <param name="eventID">�¼�ID</param>
        /// <param name="add"></param>
        /// <param name="args">�¼�����</param>
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
