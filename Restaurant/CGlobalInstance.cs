using System;
using System.Collections.Generic;
using System.Text;
using YyLogger;
using UserModule;
using System.Data;

namespace Restaurant
{
    public class CGlobalInstance
    {
        public CDbAdaHelper DbAdaHelper = new CDbAdaHelper();
        public UserDbAdaHelper m_UserDbAdaHelper = null;
        public CFileLogger FileLogger = new CFileLogger("AppLog");

        public DataTable dtBillState;
        public DataTable dtBillType;
        public DataTable dtOnCreditType;
        public DataTable dtAssociator;
        /// <summary>
        /// ȫ�ַ���ʵ��
        /// </summary>
        static CGlobalInstance g_Instance = new CGlobalInstance();

        CGlobalInstance()
        {
            m_UserDbAdaHelper = new UserDbAdaHelper(DbAdaHelper);
            DbAdaHelper.OnErrorEvent += new OleDbOperate.OnErrorEventHandler(Db_OnErrorEvent);

            dtBillState = new DataTable("dtState");
            dtBillState.Columns.Add("Name", System.Type.GetType("System.String"));
            dtBillState.Columns.Add("Value", System.Type.GetType("System.Int32"));

            DataRow dr2 = dtBillState.NewRow();
            dr2["Name"] = "δ�ͳ�";
            dr2["Value"] = 0;
            dtBillState.Rows.Add(dr2);

            dr2 = dtBillState.NewRow();
            dr2["Name"] = "���ͳ�";
            dr2["Value"] = 1;
            dtBillState.Rows.Add(dr2);

            dr2 = dtBillState.NewRow();
            dr2["Name"] = "���ϲ�";
            dr2["Value"] = 2;
            dtBillState.Rows.Add(dr2);

            dr2 = dtBillState.NewRow();
            dr2["Name"] = "���ϲ�";
            dr2["Value"] = 3;
            dtBillState.Rows.Add(dr2);

            dtBillType = new DataTable("Type");//�㵥���˵�������,����
            dtBillType.Columns.Add("Name", System.Type.GetType("System.String"));
            dtBillType.Columns.Add("Value", System.Type.GetType("System.Int32"));

            DataRow dr = dtBillType.NewRow();
            dr["Name"] = "�㵥";
            dr["Value"] = 0;
            dtBillType.Rows.Add(dr);

            dr = dtBillType.NewRow();
            dr["Name"] = "�˵�";
            dr["Value"] = 1;
            dtBillType.Rows.Add(dr);

            dr = dtBillType.NewRow();
            dr["Name"] = "����";
            dr["Value"] = 2;
            dtBillType.Rows.Add(dr);

            dr = dtBillType.NewRow();
            dr["Name"] = "����";
            dr["Value"] = 3;
            dtBillType.Rows.Add(dr);

            dtOnCreditType = new DataTable("OpType");
            dtOnCreditType.Columns.Add("Name", System.Type.GetType("System.String"));
            dtOnCreditType.Columns.Add("Value", System.Type.GetType("System.Int32"));

            DataRow drA = dtOnCreditType.NewRow();
            drA["Name"] = "����";
            drA["Value"] = 0;
            dtOnCreditType.Rows.Add(drA);

            drA = dtOnCreditType.NewRow();
            drA["Name"] = "����";
            drA["Value"] = 1;
            dtOnCreditType.Rows.Add(drA);

            dtAssociator = new DataTable("OpType");
            dtAssociator.Columns.Add("Name", System.Type.GetType("System.String"));
            dtAssociator.Columns.Add("Value", System.Type.GetType("System.Int32"));

            DataRow drB = dtAssociator.NewRow();
            drB["Name"] = "��ֵ";
            drB["Value"] = 0;
            dtAssociator.Rows.Add(drB);

            drB = dtAssociator.NewRow();
            drB["Name"] = "�ۿ�";
            drB["Value"] = 1;
            dtAssociator.Rows.Add(drB);
        }

        /// <summary>
        /// ȫ�ַ���ʵ����
        /// </summary>
        public static CGlobalInstance Instance
        {
            get
            {
                
                return g_Instance;
            }
        }

        void Db_OnErrorEvent(OleDbOperate.DbErrorEventArgs e)
        {
            WriteErrorLog(LogSeverity.error,e.ExtraMsg, e.Ex);
        }
        public void WriteErrorLog(LogSeverity Level, string source, Exception Ex)
        {
            FileLogger.WriteLog(Level, source, Ex.Message + ":" + Ex.StackTrace);
        }
    }
}
