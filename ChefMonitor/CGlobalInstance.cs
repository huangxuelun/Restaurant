using System;
using System.Collections.Generic;
using System.Text;
using YyLogger;
using System.Data;
using System.Data.OleDb;

namespace ChefMonitor
{
    public class CGlobalInstance
    {
        public CDataAdaHelper DbAdaHelper = null;
        public CFileLogger FileLogger = new CFileLogger("AppLog");

        public DataTable dtBillState;
        public DataTable dtBillType;
        /// <summary>
        /// ȫ�ַ���ʵ��
        /// </summary>
        static CGlobalInstance g_Instance = new CGlobalInstance();

        CGlobalInstance()
        {
            DbAdaHelper = new CDataAdaHelper();
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
            WriteErrorLog(LogSeverity.error, e.ExtraMsg, e.Ex);
        }
        public void WriteErrorLog(LogSeverity Level, string source, Exception Ex)
        {
            FileLogger.WriteLog(Level, source, Ex.Message + ":" + Ex.StackTrace);
        }
    }
}
