namespace Aya.Analysis
{
	/// <summary>
	/// ��������
	/// </summary>
	public enum MissionType
	{
		/// <summary>
		/// δ֪
		/// </summary>
		Unknown = -1,
		/// <summary>
		/// ��������
		/// </summary>
		Guide = 0,
		/// <summary>
		/// ÿ��
		/// </summary>
		Daily = 1,
		/// <summary>
		/// ÿ��
		/// </summary>
		Weekly = 2,
		/// <summary>
		/// ÿ��
		/// </summary>
		Monthly = 3,
		/// <summary>
		/// ��ʱ�
		/// </summary>
		Activity = 4,
		/// <summary>
		/// ����/����
		/// </summary>
		Story = 5,
		/// <summary>
		/// �ɾ�/��ս
		/// </summary>
		Challenge = 6,
		/// <summary>
		/// ����
		/// </summary>
		Other = 100
	}

	/// <summary>
	/// �Ա�
	/// </summary>
	public enum GenderType
	{
		/// <summary>
		/// δ֪
		/// </summary>
		Unknown = -1,
		/// <summary>
		/// ����
		/// </summary>
		Male = 0,
		/// <summary>
		/// Ů��
		/// </summary>
		Female = 1
	}

	/// <summary>
	/// �˻�����
	/// </summary>
	public enum AccountType
	{
		/// <summary>
		/// Ĭ������
		/// </summary>
		Default = 0,
		/// <summary>
		/// QQ
		/// </summary>
		QQ = 1,
		/// <summary>
		/// ΢��
		/// </summary>
		WeChat = 2,
		/// <summary>
		/// �ȸ�
		/// </summary>
		Google = 3,
		/// <summary>
		/// ����
		/// </summary>
		Other = 999
	}
}
