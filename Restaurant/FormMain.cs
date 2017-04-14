using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MixLib;
using OleDbOperate;
using System.Data.OleDb;
using UserModule;
using MixFunc;

namespace Restaurant
{
    public partial class FormMain : Form
    {
        BindingSource bindingSource1;
        CPaginationQuery PaginationQuery;

        int iCurPageIndex = 0;

        Font Font12 = new Font("Times New Roman", 12, FontStyle.Bold);
        Font Font8 = new Font("Times New Roman", 8, FontStyle.Bold);

        public FormMain()
        {
            InitializeComponent();
        }
        public bool UserLogin()
        {
            FrmLogin LoginDlg = new FrmLogin();
            LoginDlg.m_DbAdaHelper = CGlobalInstance.Instance.m_UserDbAdaHelper;
            LoginDlg.m_sUser = CAppOption.m_sUser;
            LoginDlg.m_bSavePwd = CAppOption.m_bSavePwd;
            if (CAppOption.m_bSavePwd)
            {
                LoginDlg.m_sPwd = CAppOption.m_sPwd;
            }
            if (LoginDlg.ShowDialog() == DialogResult.OK)  
            {
                CAppOption.m_bSavePwd = LoginDlg.m_bSavePwd;

                CAppOption.m_lUserID = LoginDlg.m_UserInfo.UserID;
                CAppOption.m_sUserName = LoginDlg.m_UserInfo.UserName;
                CAppOption.m_sUserWorkID = LoginDlg.m_UserInfo.UserWorkID;
                CAppOption.m_sUserAlias = LoginDlg.m_UserInfo.UserAlias;
                CAppOption.m_sUser = LoginDlg.m_UserInfo.LoginName;
                CAppOption.m_sPwd = LoginDlg.m_UserInfo.Pwd;

                return true;
            }
            else
            {
                return false;
            }

        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                MixLib.CAppOption.LoadData();
                MixLib.CAppOption.LoadKeyNO();
                //����ע����У��

                CRunSetting.LoadData();

    #if TMPRELEASE
                DateTime dt200906=new DateTime(2013,8,30);
                DateTime dt200909=new DateTime(2013,10,1);
                if (DateTime.Now > dt200906)
                {
                    MessageBox.Show("��ʹ�õ�����ʱ�汾,����2011��8��30��ǰ��ϵ�����߸������!");
                }
                else if (DateTime.Now > dt200909)
                {
                    MessageBox.Show("��ʹ�õ�����ʱ�汾,����ϵ�����߸������!");
                    this.Close();
                }
                this.Text = "��Ӱ����ϵͳ V1.03 ��ʱ�汾";
#endif
            
                CGlobalInstance.Instance.DbAdaHelper.ConnectString = MixLib.CAppOption.m_sDbConnectString;
                if (!CGlobalInstance.Instance.DbAdaHelper.IsOpen)
                {
#if TMPRELEASE
                    ToolStripMenuItemConnect.Visible = false;
#else
                ToolStripMenuItemConnect.PerformClick();
#endif
                    MessageBox.Show("���ݿ���ش�������ϵ������!");
                    this.Close();
                    return;
                }
                else
                {
                    //������ݿ������ļ�
                    CGlobalInstance.Instance.DbAdaHelper.ExecUpdate();
                }
                

                if (CGlobalInstance.Instance.DbAdaHelper.IsKVLoad != true)
                {
                    MessageBox.Show("KV����ʧ�ܣ��㻹û��ע�ᣬ���޷�ʹ�ñ�ϵͳ��");
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                CGlobalInstance.Instance.WriteErrorLog(YyLogger.LogSeverity.error, "Main", ex);
            }

            if (UserLogin() != true)
            {
                Application.Exit();
                return;
            }

            PaginationQuery = new CPaginationQuery(CGlobalInstance.Instance.DbAdaHelper, "vDiningTable", "DiningTableID");
            PaginationQuery.PageRowNum = CAppOption.PageRowCount;
            PaginationQuery.Sorter = CSortStyle.GetSort(CAppOption.iSortStyle);

            //��Ʊ����,��ˮ����,���ʺ�,ɽˮ����
            CAppOption.WindowStyleMan.SetStyle(this, CAppOption.iFormStyle);

            tabControlList.TabPages.Clear();
            tabControlList.TabPages.Add("tpAll", "ȫ��");
            DataTable dtList = CGlobalInstance.Instance.DbAdaHelper.GetDiningDept();
            if (dtList != null)
            {
                foreach (DataRow dr in dtList.Rows)
                {
                    tabControlList.TabPages.Add("tpDiningDept_" + dr["DiningDeptID"].ToString(), dr["DeptName"].ToString());
                }
            }

            //SetShowStyle("ȫ��");

            DataRefresh();
            ShowPageInfo();

            //������ʱ��
            tmDataRefresh.Enabled = true;
        }
        
        void OnKeyDown(Keys KeyCode)
        {
            if (KeyCode == Keys.PageDown)
            {
                iCurPageIndex++;
                DataRefresh();
                ShowPageInfo(); 
            }
            else if (KeyCode == Keys.PageUp)
            {
                iCurPageIndex--;
                DataRefresh();
                ShowPageInfo(); 
            } 
        }

        void ShowPageInfo()
        {
            int pageindex = iCurPageIndex;
            int iPageCount = PaginationQuery.PageCount;
            if (iPageCount != 0) pageindex = pageindex % iPageCount;//���¼���pageindex
            if (pageindex < 0) pageindex = iPageCount + pageindex;

            toolStripLabelPageInfo.Text = string.Format("��{0}ҳ ��{1}ҳ", iPageCount, pageindex + 1);
        }
        void DataRefresh()
        {
            if (!CGlobalInstance.Instance.DbAdaHelper.IsOpen) return;

            try
            {
                if (bindingSource1 == null)
                {
                    bindingSource1 = new BindingSource();
                }

                DataTable dt = PaginationQuery.GetDataTable(iCurPageIndex);
                if (dt != null)
                {
                    bindingSource1.DataSource = dt;

                    yyDiningTableView1.DataSource = bindingSource1;
                }                

                btUp.Visible = PaginationQuery.IsMultiPage;
                btDown.Visible = PaginationQuery.IsMultiPage;                
            }
            catch (Exception ex)
            {
                tmDataRefresh.Enabled = false;

                System.Windows.Forms.MessageBox.Show(ex.Message);
                this.WriteErrorLog("�����Զ�����",ex);

                tmDataRefresh.Enabled = true;
            }
        }

        public void WriteAppLog(string Message)
        {
            CGlobalInstance.Instance.FileLogger.WriteLog(YyLogger.LogSeverity.info, "Main", Message);
        }
        public void WriteErrorLog(Exception ex)
        {
            CGlobalInstance.Instance.WriteErrorLog(YyLogger.LogSeverity.error, "Main", ex);
        }
        public void WriteErrorLog(string source,Exception ex)
        {
            CGlobalInstance.Instance.WriteErrorLog(YyLogger.LogSeverity.error, source, ex);
        }

        private void SetStatusBar()
        {
            try
            {
                /*
                TimeSpan span = DateTime.Now - startTime;
                this.toolStripStatusLabelRunTime.Text = string.Format("ϵͳ�Ѿ���������{0}��{1}Сʱ{2}��{3}��",
                    span.Days, span.Hours, span.Minutes, span.Seconds);
                 * */

                int memory = (int)(System.Environment.WorkingSet / 1024);
                this.toolStripStatusLabelMemory.Text = "ʹ���ڴ�:" + memory.ToString("#,###,###") + "K";
#if DEBUG
                if (memory > 80000)
#else
                if (memory > 100000)
#endif
                {
                    GC.Collect();//��������
                    WriteAppLog("ִ����������!");

                    //System.Diagnostics.Process.GetCurrentProcess().MaxWorkingSet = (IntPtr)60000000;//�ֽڵ�λ��80��000kb
                    this.WindowState = FormWindowState.Minimized;
                    System.Threading.Thread.Sleep(200);
                    this.WindowState = FormWindowState.Maximized;
                    //MisFunc.PlatformInvokeKernel32.SetProcessWorkingSetSize();
                }
            }
            catch (Exception Ex)
            {
                WriteErrorLog("SetStatusBar�쳣", Ex);
            }
        }

        private void toolStripButtonTableSet_Click(object sender, EventArgs e)
        {
            FormTableSet dlg = new FormTableSet();

            dlg.ShowDialog();

            PaginationQuery.RePaginationQuery();
            DataRefresh();
            ShowPageInfo();
        }

        private void ToolStripMenuItemConnect_Click(object sender, EventArgs e)
        {
#if TMPRELEASE
            ToolStripMenuItemConnect.Visible = false;
#else
                
            MSDASC.DataLinks mydlg = new MSDASC.DataLinks();
            CDbAdaHelper OleCon = new CDbAdaHelper();

            ADODB._Connection ADOcon;
            bool bEdit = false;

            //OleCon.ConnectString = CAppOption.m_sDbConnectString;
            if (CAppOption.m_sDbConnectString == String.Empty)
            {
                try
                {
                    //Cast the generic object that PromptNew returns to an ADODB._Connection.
                    ADOcon = (ADODB._Connection)mydlg.PromptNew();
                    OleCon.ConnectString = ADOcon.ConnectionString;

                    bEdit = true;
                }
                catch (Exception ex)
                {
                    CGlobalInstance.Instance.WriteErrorLog(YyLogger.LogSeverity.error, "�������ݿ�����", ex);
                }
            }
            else
            {
                ADOcon = new ADODB.ConnectionClass();
                ADOcon.ConnectionString = CAppOption.m_sDbConnectString;
                //set local COM compatible data type
                object oConnection = ADOcon;
                try
                {
                    //prompt user to edit the given connect string
                    if ((bool)mydlg.PromptEdit(ref oConnection))
                    {
                        //����						
                    }
                    OleCon.ConnectString = ADOcon.ConnectionString;

                    bEdit = true;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("�������Ӳ��ɹ�����������������!");
                    CGlobalInstance.Instance.WriteErrorLog(YyLogger.LogSeverity.error,"�������ݿ�����",ex);
                }

            }

            if (bEdit == true)
            {
                try
                {
                    //OleCon.Db.Open();
                    OleCon.Open();

                    if (OleCon.IsOpen)
                    {

                        CAppOption.m_sDbConnectString = OleCon.ConnectString;
                        //OleCon.Db.Close();
                        OleCon.Close();


                        CGlobalInstance.Instance.DbAdaHelper.ConnectString = CAppOption.m_sDbConnectString;
                        CAppOption.SaveData();
                    }
                    else
                    {
                        MessageBox.Show("������Ч,�޷��������ݿ�");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("������Ч,�޷��������ݿ⣨ע����ѡ�����������룩��");
                    CGlobalInstance.Instance.WriteErrorLog(YyLogger.LogSeverity.error, "�������ݿ�����", ex);
                }
            }
#endif
        }

        private void tmDataRefresh_Tick(object sender, EventArgs e)
        {
            DataRefresh();

            SetStatusBar();
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            iCurPageIndex++;

            DataRefresh();
            ShowPageInfo(); 
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            iCurPageIndex--;

            DataRefresh();
            ShowPageInfo(); 
        }

        private void yyDiningTableView1_ItemDoubleClick(object sender, EventArgs e)
        {
            //���ﴦ��
            YyTableCtrl.YyDiningTable Table = (YyTableCtrl.YyDiningTable)sender;
            if(Table.State==YyTableCtrl.YyDiningTable.YyTableState.Idle||Table.State==YyTableCtrl.YyDiningTable.YyTableState.Prearrange) 
            {
                StartBill();
            }
            else if (Table.State == YyTableCtrl.YyDiningTable.YyTableState.Using)
            {            
                Orderdishes();
            }
        }

        private void ToolStripMenuItemWinStyle_Click(object sender, EventArgs e)
        {
            FormStyle dlgStyle = new FormStyle();
            if (dlgStyle.ShowDialog() == DialogResult.Yes)
            {
                MessageBox.Show("������������Ч��");
            }
        }

        

        private void toolStripButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
            //this.Dispose();
        }

        private void tabControlList_SelectedIndexChanged(object sender, EventArgs e)
        {
            iCurPageIndex = 0;

            int DiningDeptID = -1;
            string DiningDeptName = "";
            if (tabControlList.SelectedTab != null)
            {
                DiningDeptName = tabControlList.SelectedTab.Text;

                string[] NameList = tabControlList.SelectedTab.Name.Split('_');
                try
                {
                    if (NameList.Length > 1)
                        DiningDeptID = Convert.ToInt32(NameList[1]);
                }
                catch
                {
                }
            }

            if (bindingSource1 != null) bindingSource1.Filter = "";//�����Ϊ�˽������ Lgahero Xie


            if (DiningDeptID != -1)
            {
                PaginationQuery.Filter = string.Format("DiningDeptID={0}", DiningDeptID);
            }
            else
            {
                PaginationQuery.Filter = "";

            }

            PaginationQuery.RePaginationQuery();

            DataRefresh();
            ShowPageInfo();

        }

        private void toolStripButtonUnused_Click(object sender, EventArgs e)
        {
            if (yyDiningTableView1.CurrentItem!=null)
            {
                YyTableCtrl.YyDiningTable Table = this.yyDiningTableView1.CurrentItem;

                if (Table.State != YyTableCtrl.YyDiningTable.YyTableState.Idle)
                {
                    return;
                }

                int TableNO,State;
                TableNO = Convert.ToInt32(yyDiningTableView1.CurrentItem.TableNO);
                State = (int)YyTableCtrl.YyDiningTable.YyTableState.Unused;
                if (CGlobalInstance.Instance.DbAdaHelper.UpdateDiningTableState(TableNO, State))
                {
                    yyDiningTableView1.CurrentItem.State = YyTableCtrl.YyDiningTable.YyTableState.Unused;
                }
                
            }
        }
        private void toolStripButtonUse_Click(object sender, EventArgs e)
        {
            if (yyDiningTableView1.CurrentItem != null)
            {
                YyTableCtrl.YyDiningTable Table = this.yyDiningTableView1.CurrentItem;

                if (Table.State != YyTableCtrl.YyDiningTable.YyTableState.Unused)
                {
                    return;
                }

                int TableNO, State;
                TableNO = Convert.ToInt32(yyDiningTableView1.CurrentItem.TableNO);
                State = (int)YyTableCtrl.YyDiningTable.YyTableState.Idle;
                if (CGlobalInstance.Instance.DbAdaHelper.UpdateDiningTableState(TableNO, State))
                {
                    yyDiningTableView1.CurrentItem.State = YyTableCtrl.YyDiningTable.YyTableState.Idle;
                }

            }
        }
        private void toolStripButtonPrearrange_Click(object sender, EventArgs e)
        {
            if (yyDiningTableView1.CurrentItem != null)
            {
                int TableNO;
                TableNO = Convert.ToInt32(yyDiningTableView1.CurrentItem.TableNO);

                FormPrearrange dlg = new FormPrearrange();
                dlg.m_TableNO = TableNO;

                dlg.ShowDialog();

                DataRefresh();
                ShowPageInfo();
            }
        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            btUp.Left = tabControlList.Right - 100;
            btUp.Top = tabControlList.Top - 5;
            btUp.Width = 85;

            btDown.Left = tabControlList.Right - 100;
            btDown.Top = tabControlList.Top + 20;
            btDown.Width = 85;            
        }

        private void toolStripButtonGoodsSet_Click(object sender, EventArgs e)
        {
            FormGoodsSet dlg = new FormGoodsSet();

            dlg.ShowDialog();
            dlg.Dispose();
        }
        void StartBill()
        {
            if (this.yyDiningTableView1.CurrentItem == null) return;
            YyTableCtrl.YyDiningTable Table = this.yyDiningTableView1.CurrentItem;
            if (Table.State != YyTableCtrl.YyDiningTable.YyTableState.Idle && Table.State != YyTableCtrl.YyDiningTable.YyTableState.Prearrange)
            {
                return;
            }

            FormBill dlg = new FormBill();

            dlg.m_Bill.TableNO = Convert.ToInt32(this.yyDiningTableView1.CurrentItem.TableNO);
            dlg.m_Bill.TableName = this.yyDiningTableView1.CurrentItem.TableName;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //���
                FormOrderdishes dlgOrderdishes = new FormOrderdishes();
                dlgOrderdishes.m_Bill.TableNO = dlg.m_Bill.TableNO;
                dlgOrderdishes.m_Bill.TableName = dlg.m_Bill.TableName;

                _tagBill data = CGlobalInstance.Instance.DbAdaHelper.GetBillInfoOfA(dlgOrderdishes.m_Bill.TableNO);
                if (data != null)
                {
                    dlgOrderdishes.m_Bill.BillID = data.BillID;
                    dlgOrderdishes.m_Bill.BillTime = data.BillTime;
                    dlgOrderdishes.m_Bill.ClientName = data.ClientName;
                    dlgOrderdishes.m_Bill.ClientNumber = data.ClientNumber;
                    dlgOrderdishes.m_Bill.Remark = data.Remark;
                }

                dlgOrderdishes.ShowDialog();

                //
                DataRefresh();
                ShowPageInfo();
            }
            dlg.Dispose();
        }
        private void toolStripButtonStartBill_Click(object sender, EventArgs e)
        {
            StartBill();
        }
        void Orderdishes()
        {
            if (this.yyDiningTableView1.CurrentItem == null) return;
            YyTableCtrl.YyDiningTable Table = this.yyDiningTableView1.CurrentItem;
            if (Table.State != YyTableCtrl.YyDiningTable.YyTableState.Using) return;

            FormOrderdishes dlgOrderdishes = new FormOrderdishes();
            dlgOrderdishes.m_Bill.TableNO = Convert.ToInt32(this.yyDiningTableView1.CurrentItem.TableNO);
            dlgOrderdishes.m_Bill.TableName = this.yyDiningTableView1.CurrentItem.TableName;

            _tagBill data = CGlobalInstance.Instance.DbAdaHelper.GetBillInfoOfA(dlgOrderdishes.m_Bill.TableNO);
            if (data != null)
            {
                dlgOrderdishes.m_Bill.BillID = data.BillID;
                dlgOrderdishes.m_Bill.BillTime = data.BillTime;
                dlgOrderdishes.m_Bill.ClientName = data.ClientName;
                dlgOrderdishes.m_Bill.ClientNumber = data.ClientNumber;
                dlgOrderdishes.m_Bill.Remark = data.Remark;
            }

            dlgOrderdishes.ShowDialog();
        }
        private void toolStripButtonOrderdishes_Click(object sender, EventArgs e)
        {
            Orderdishes();
        }

        private void toolStripButtonTransfer_Click(object sender, EventArgs e)
        {
            if (this.yyDiningTableView1.CurrentItem == null) return;
            YyTableCtrl.YyDiningTable Table = this.yyDiningTableView1.CurrentItem;
            if (Table.State != YyTableCtrl.YyDiningTable.YyTableState.Using) return;

            if (this.yyDiningTableView1.CurrentItem.State != YyTableCtrl.YyDiningTable.YyTableState.Using)
            {
                MessageBox.Show("��ֻ̨����ʹ���в��ܽ��л�̨��");
                return;
            }

            FormTransfer dlg = new FormTransfer();
            dlg.OldTableNO = Convert.ToInt32(this.yyDiningTableView1.CurrentItem.TableNO);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (CGlobalInstance.Instance.DbAdaHelper.TransferDiningTable(dlg.OldTableNO, dlg.NewTableNO))
                {
                    MessageBox.Show("��̨�ɹ�!");

                    DataRefresh();
                    ShowPageInfo();
                }
                else
                {
                    MessageBox.Show("��̨ʧ�ܣ����ҹ���Ա��ԭ��!");
                }

                
            }

        }

        private void toolStripButtonUnite_Click(object sender, EventArgs e)
        {
            if (this.yyDiningTableView1.CurrentItem == null) return;

            if (this.yyDiningTableView1.CurrentItem.State != YyTableCtrl.YyDiningTable.YyTableState.Using)
            {
                MessageBox.Show("��ֻ̨����ʹ���в��ܽ��в�̨��");
                return;
            }

            FormUnite dlg = new FormUnite();
            dlg.OldTableNO = Convert.ToInt32(this.yyDiningTableView1.CurrentItem.TableNO);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (CGlobalInstance.Instance.DbAdaHelper.UniteDiningTable(dlg.OldTableNO, dlg.NewTableNO))
                {
                    MessageBox.Show("��̨�ɹ�!");

                    DataRefresh();
                    ShowPageInfo();
                }
                else
                {
                    MessageBox.Show("��̨ʧ�ܣ����ҹ���Ա��ԭ��!");
                }


            }
        }

        private void toolStripButtonReckoning_Click(object sender, EventArgs e)
        {
            if (this.yyDiningTableView1.CurrentItem == null) return;

            if (this.yyDiningTableView1.CurrentItem.State == YyTableCtrl.YyDiningTable.YyTableState.Using||this.yyDiningTableView1.CurrentItem.State == YyTableCtrl.YyDiningTable.YyTableState.Holdon)
            {

                if (this.yyDiningTableView1.CurrentItem == null) return;
                YyTableCtrl.YyDiningTable Table = this.yyDiningTableView1.CurrentItem;
                if (Table.State != YyTableCtrl.YyDiningTable.YyTableState.Using) return;

                FormReckoning dlgReckoning = new FormReckoning();

                dlgReckoning.m_Bill.TableNO = Convert.ToInt32(this.yyDiningTableView1.CurrentItem.TableNO);
                dlgReckoning.m_Bill.TableName = this.yyDiningTableView1.CurrentItem.TableName;

                _tagBill data = CGlobalInstance.Instance.DbAdaHelper.GetBillInfoOfA(dlgReckoning.m_Bill.TableNO);
                if (data != null)
                {
                    dlgReckoning.m_Bill.BillID = data.BillID;
                    dlgReckoning.m_Bill.BillTime = data.BillTime;
                    dlgReckoning.m_Bill.ClientName = data.ClientName;
                    dlgReckoning.m_Bill.ClientNumber = data.ClientNumber;
                    dlgReckoning.m_Bill.Remark = data.Remark;
                }

                if (dlgReckoning.ShowDialog() == DialogResult.OK)
                {
                    if (CRunSetting.AutoClearTable == 1)
                    {
                        yyDiningTableView1.CurrentItem.State = YyTableCtrl.YyDiningTable.YyTableState.Idle;
                    }
                    else
                        yyDiningTableView1.CurrentItem.State = YyTableCtrl.YyDiningTable.YyTableState.Reckoning;
                }
            }else{
                MessageBox.Show("��ֻ̨����ʹ���л��������ܽ��н��ʣ�");
                return;
            }

            
        }

        private void toolStripButtonClearTable_Click(object sender, EventArgs e)
        {
            if (yyDiningTableView1.CurrentItem != null && this.yyDiningTableView1.CurrentItem.State == YyTableCtrl.YyDiningTable.YyTableState.Reckoning)
            {
                int TableNO, State;
                TableNO = Convert.ToInt32(yyDiningTableView1.CurrentItem.TableNO);
                State = (int)YyTableCtrl.YyDiningTable.YyTableState.Idle;
                if (CGlobalInstance.Instance.DbAdaHelper.UpdateDiningTableState(TableNO, State))
                {
                    yyDiningTableView1.CurrentItem.State = YyTableCtrl.YyDiningTable.YyTableState.Idle;
                }
            }
        }

        private void ToolStripMenuItemOnCredit_Click(object sender, EventArgs e)
        {
            FormOnCredit dlg = new FormOnCredit();

            dlg.ShowDialog();
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAssociator dlg = new FormAssociator();

            dlg.ShowDialog();
        }

        private void ToolStripMenuItemEmployeeInfo_Click(object sender, EventArgs e)
        {
            FormEmployee PageForm = new FormEmployee();
            PageForm.m_DbAdaHelper = CGlobalInstance.Instance.m_UserDbAdaHelper;
            PageForm.ShowDialog();

            PageForm.Dispose(); 
        }

        private void ToolStripMenuItemGroupInfo_Click(object sender, EventArgs e)
        {
            FormGroupInfo PageForm = new FormGroupInfo();
            PageForm.m_DbAdaHelper = CGlobalInstance.Instance.m_UserDbAdaHelper;
            PageForm.ShowDialog();
            
            PageForm.Dispose();            
        }

        private void ToolStripMenuItemRolesInfo_Click(object sender, EventArgs e)
        {
            FormRolesInfo PageForm = new FormRolesInfo();
            PageForm.m_DbAdaHelper = CGlobalInstance.Instance.m_UserDbAdaHelper;
            PageForm.ShowDialog();

            PageForm.Dispose();       
        }

        private void ToolStripMenuItemLoginMgr_Click(object sender, EventArgs e)
        {
            FormUserMgr PageForm = new FormUserMgr();
            PageForm.m_DbAdaHelper = CGlobalInstance.Instance.m_UserDbAdaHelper;
            PageForm.ShowDialog();

            PageForm.Dispose();
        }

        private void ToolStripMenuItemRunSetting_Click(object sender, EventArgs e)
        {
            FormSysSetting dlg = new FormSysSetting();
            dlg.ShowDialog();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            MixLib.CAppOption.SaveData();
            CRunSetting.SaveData();
        }

        private void ToolStripMenuItemBillQuery_Click(object sender, EventArgs e)
        {
            FormBillQuery dlg = new FormBillQuery();

            dlg.ShowDialog();
        }

        private void ToolStripMenuItemBillListQuery_Click(object sender, EventArgs e)
        {
            FormBillListQuery dlg = new FormBillListQuery();

            dlg.ShowDialog();
        }

        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            FormAbout dlg = new FormAbout();
            dlg.ShowDialog();
        }

        private void ToolStripMenuItemRegister_Click(object sender, EventArgs e)
        {
            FormRegister dlg = new FormRegister();
            string strMac = CGeneralFunction.GetMACaddressOne();

            SimpleScrypt SScrypt = new SimpleScrypt();
            dlg.CodeNO = SScrypt.Encrypt(strMac);
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //ʵ��ע����룬�д��޸�
                MessageBox.Show("�����������������");
                CAppOption.RegKeyNO = dlg.KeyNO;
                CAppOption.SaveData();
                CAppOption.SaveKeyNO();

                Application.Exit();
            }
        }     

        
    }
}