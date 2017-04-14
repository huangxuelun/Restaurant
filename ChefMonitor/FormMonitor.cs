using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MixLib;
using DataGridViewAutoFilter;
using OleDbOperate;
using System.Data.OleDb;

namespace ChefMonitor
{
    public partial class FormMonitor : Form
    {
        BindingSource bindingSource1;
        CPaginationQuery PaginationQuery;
        Font Font12 = new Font("Times New Roman", 11, FontStyle.Bold);
        Font Font8 = new Font("Times New Roman", 8, FontStyle.Bold);

        int iCurPageIndex = 0;

        public FormMonitor()
        {
            InitializeComponent();

            //CCommonDataSet.G_Instance.Load(Application.StartupPath + "\\Common.dat");

            dgvMain.MouseWheel += new MouseEventHandler(dgvMain_MouseWheel);
            this.MouseWheel += new MouseEventHandler(FormWatch_MouseWheel);

            
        }
        void InitOrderdishesList()
        {
            dgvMain.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn ColumnBillListID = new DataGridViewTextBoxColumn();
            ColumnBillListID.DataPropertyName = "BillListID";
            ColumnBillListID.Name = "ColumnBillListID";
            ColumnBillListID.HeaderText = "BillListID";
            ColumnBillListID.Visible = false;
            ColumnBillListID.Width = 30;
            ColumnBillListID.FillWeight = 40F;
            ColumnBillListID.ReadOnly = true;
            ColumnBillListID.DisplayIndex = 0;
            dgvMain.Columns.Add(ColumnBillListID);

            DataGridViewTextBoxColumn ColumnGoodsID2 = new DataGridViewTextBoxColumn();
            ColumnGoodsID2.DataPropertyName = "GoodsID";
            ColumnGoodsID2.Name = "ColumnGoodsID2";
            ColumnGoodsID2.HeaderText = "GoodsID";
            ColumnGoodsID2.Visible = false;
            ColumnGoodsID2.Width = 30;
            ColumnGoodsID2.FillWeight = 40F;
            ColumnGoodsID2.ReadOnly = true;
            ColumnGoodsID2.DisplayIndex = 1;
            dgvMain.Columns.Add(ColumnGoodsID2);


            DataGridViewTextBoxColumn ColumnBillID = new DataGridViewTextBoxColumn();
            ColumnBillID.DataPropertyName = "BillID";
            ColumnBillID.Name = "ColumnBillID";
            ColumnBillID.HeaderText = "BillID";
            ColumnBillID.Width = 70;
            ColumnBillID.DisplayIndex = 2;
            ColumnBillID.Visible = false;
            dgvMain.Columns.Add(ColumnBillID);

            DataGridViewTextBoxColumn ColumnLongGoodsNO2 = new DataGridViewTextBoxColumn();
            ColumnLongGoodsNO2.DataPropertyName = "LongGoodsNO";
            ColumnLongGoodsNO2.Name = "ColumnLongGoodsNO2";
            ColumnLongGoodsNO2.HeaderText = "������";
            ColumnLongGoodsNO2.Width = 80;
            ColumnLongGoodsNO2.DisplayIndex = 3;
            dgvMain.Columns.Add(ColumnLongGoodsNO2);

            DataGridViewYyFilterTextBoxColumn ColumnGoodsName = new DataGridViewYyFilterTextBoxColumn();
            ColumnGoodsName.DataSource = CGlobalInstance.Instance.DbAdaHelper.GetDataTable("SELECT DISTINCT GoodsName FROM tBillList where (RecordTime > DATEADD(Day, - 1, GETDATE())) or State<3");
            ColumnGoodsName.UpdateFilterEvent += OnUpdateFilter;
            ColumnGoodsName.DataPropertyName = "GoodsName";
            ColumnGoodsName.Name = "ColumnGoodsName";
            ColumnGoodsName.HeaderText = "������Ŀ";
            ColumnGoodsName.Width = 120;
            ColumnGoodsName.DisplayIndex = 4;
            
            dgvMain.Columns.Add(ColumnGoodsName);

            DataGridViewTextBoxColumn ColumnGoodsNumber = new DataGridViewTextBoxColumn();
            ColumnGoodsNumber.DataPropertyName = "GoodsNumber";
            ColumnGoodsNumber.Name = "ColumnGoodsNumber";
            ColumnGoodsNumber.HeaderText = "����";
            ColumnGoodsNumber.Width = 30;
            ColumnGoodsNumber.DisplayIndex = 5;
            dgvMain.Columns.Add(ColumnGoodsNumber);

            DataGridViewTextBoxColumn ColumnUnit = new DataGridViewTextBoxColumn();
            ColumnUnit.DataPropertyName = "Unit";
            ColumnUnit.Name = "ColumnUnit";
            ColumnUnit.HeaderText = "��λ";
            ColumnUnit.Width = 50;
            ColumnUnit.DisplayIndex = 6;
            dgvMain.Columns.Add(ColumnUnit);

            DataGridViewTextBoxColumn ColumnOrderTime = new DataGridViewTextBoxColumn();
            ColumnOrderTime.DataPropertyName = "OrderTime";
            ColumnOrderTime.Name = "ColumnOrderTime";
            ColumnOrderTime.HeaderText = "�ͳ�ʱ��";
            ColumnOrderTime.Width = 120;
            ColumnOrderTime.DisplayIndex = 7;
            ColumnOrderTime.ReadOnly = true;
            dgvMain.Columns.Add(ColumnOrderTime);

            DataGridViewTextBoxColumn ColumnRecordTime = new DataGridViewTextBoxColumn();
            ColumnRecordTime.DataPropertyName = "RecordTime";
            ColumnRecordTime.Name = "ColumnRecordTime";
            ColumnRecordTime.HeaderText = "�µ�ʱ��";
            ColumnRecordTime.Width = 120;
            ColumnRecordTime.DisplayIndex = 8;
            ColumnRecordTime.ReadOnly = true;
            dgvMain.Columns.Add(ColumnRecordTime);

            DataGridViewTextBoxColumn ColumnServingTime = new DataGridViewTextBoxColumn();
            ColumnServingTime.DataPropertyName = "ServingTime";
            ColumnServingTime.Name = "ColumnServingTime";
            ColumnServingTime.HeaderText = "����ʱ��";
            ColumnServingTime.Width = 120;
            ColumnServingTime.DisplayIndex = 9;
            ColumnServingTime.ReadOnly = true;
            dgvMain.Columns.Add(ColumnServingTime);
            

            DataGridViewTextBoxColumn ColumnUnitPrice2 = new DataGridViewTextBoxColumn();
            ColumnUnitPrice2.DataPropertyName = "UnitPrice";
            ColumnUnitPrice2.Name = "ColumnUnitPrice2";
            ColumnUnitPrice2.HeaderText = "����";
            ColumnUnitPrice2.Width = 80;
            ColumnUnitPrice2.DefaultCellStyle.Format = "0.00";
            ColumnUnitPrice2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ColumnUnitPrice2.Visible = false;
            ColumnUnitPrice2.DisplayIndex = 10;
            dgvMain.Columns.Add(ColumnUnitPrice2);

            DataGridViewCheckBoxColumn ColumnIsCurrentPrice2 = new DataGridViewCheckBoxColumn();
            ColumnIsCurrentPrice2.DataPropertyName = "IsCurrentPrice";
            ColumnIsCurrentPrice2.Name = "ColumnIsCurrentPrice2";
            ColumnIsCurrentPrice2.HeaderText = "ʱ��";
            ColumnIsCurrentPrice2.TrueValue = "1";
            ColumnIsCurrentPrice2.FalseValue = "0";
            ColumnIsCurrentPrice2.Visible = false;
            ColumnIsCurrentPrice2.ReadOnly = true;
            ColumnIsCurrentPrice2.Width = 50;
            ColumnIsCurrentPrice2.DisplayIndex = 11;
            dgvMain.Columns.Add(ColumnIsCurrentPrice2);

            DataGridViewYyFilterTextBoxColumn ColumnGoodsClassifyName = new DataGridViewYyFilterTextBoxColumn();
            ColumnGoodsClassifyName.DataSource = CGlobalInstance.Instance.DbAdaHelper.GetDataTable("SELECT GoodsClassifyName FROM tGoodsClassify");
            ColumnGoodsClassifyName.UpdateFilterEvent += OnUpdateFilter;
            ColumnGoodsClassifyName.DataPropertyName = "GoodsClassifyName";
            ColumnGoodsClassifyName.Name = "ColumnGoodsClassifyName";
            ColumnGoodsClassifyName.HeaderText = "����";
            ColumnGoodsClassifyName.Width = 70;
            ColumnGoodsClassifyName.DisplayIndex = 12;           

            dgvMain.Columns.Add(ColumnGoodsClassifyName);

            DataGridViewTextBoxColumn ColumnRemark = new DataGridViewTextBoxColumn();
            ColumnRemark.DataPropertyName = "Remark";
            ColumnRemark.Name = "ColumnRemark";
            ColumnRemark.HeaderText = "��ע";
            ColumnRemark.Width = 80;
            ColumnRemark.DisplayIndex = 13;
            dgvMain.Columns.Add(ColumnRemark);

            DataGridViewComboBoxColumn ColumnType = new DataGridViewComboBoxColumn();
            ColumnType.DataPropertyName = "Type";//
            ColumnType.DisplayMember = "Name";
            ColumnType.ValueMember = "Value";
            ColumnType.DataSource = CGlobalInstance.Instance.dtBillType;

            ColumnType.Name = "ColumnType";
            ColumnType.HeaderText = "����";//�㵥���˵�������,����
            ColumnType.Width = 80;
            ColumnType.DisplayIndex = 14;
            dgvMain.Columns.Add(ColumnType);


            DataGridViewComboBoxColumn ColumnState = new DataGridViewComboBoxColumn();
            ColumnState.DataPropertyName = "State";
            ColumnState.Name = "ColumnState";
            ColumnState.DisplayMember = "Name";
            ColumnState.ValueMember = "Value";
            ColumnState.DataSource = CGlobalInstance.Instance.dtBillState;
            ColumnState.HeaderText = "״̬";//0�䵥��1�Ŷ��У�3���ϲ�
            ColumnState.Width = 80;
            ColumnState.DisplayIndex = 15;
            dgvMain.Columns.Add(ColumnState);

            DataGridViewTextBoxColumn ColumnDeductEmployeeNO = new DataGridViewTextBoxColumn();
            ColumnDeductEmployeeNO.DataPropertyName = "DeductEmployeeNO";
            ColumnDeductEmployeeNO.Name = "ColumnDeductEmployeeNO";
            ColumnDeductEmployeeNO.HeaderText = "���Ա��";
            ColumnDeductEmployeeNO.Width = 80;
            ColumnDeductEmployeeNO.DisplayIndex = 16;
            dgvMain.Columns.Add(ColumnDeductEmployeeNO);

            DataGridViewYyFilterTextBoxColumn ColumnDeptName = new DataGridViewYyFilterTextBoxColumn();
            ColumnDeptName.DataSource = CGlobalInstance.Instance.DbAdaHelper.GetDataTable("SELECT DISTINCT DeptName FROM tDiningDept ");
            ColumnDeptName.UpdateFilterEvent += OnUpdateFilter;
            ColumnDeptName.DataPropertyName = "DeptName";
            ColumnDeptName.Name = "ColumnDeptName";
            ColumnDeptName.HeaderText = "����";//0�䵥��1�Ŷ��У�3���ϲ�
            ColumnDeptName.Width = 80;
            ColumnDeptName.DisplayIndex = 17;
            dgvMain.Columns.Add(ColumnDeptName);

            DataGridViewTextBoxColumn ColumnBillTime = new DataGridViewTextBoxColumn();
            ColumnBillTime.DataPropertyName = "BillTime";
            ColumnBillTime.Name = "ColumnBillTime";
            ColumnBillTime.HeaderText = "��̨ʱ��";//0�䵥��1�Ŷ��У�3���ϲ�
            ColumnBillTime.Width = 80;
            ColumnBillTime.DisplayIndex = 18;
            dgvMain.Columns.Add(ColumnBillTime);

            DataGridViewAutoFilterTextBoxColumn ColumnTableNO = new DataGridViewAutoFilterTextBoxColumn();
            ColumnTableNO.DataPropertyName = "TableNO";
            ColumnTableNO.Name = "ColumnTableNO";
            ColumnTableNO.HeaderText = "̨��";
            ColumnTableNO.Width = 120;
            ColumnTableNO.DisplayIndex = 19;
            dgvMain.Columns.Add(ColumnTableNO);

            DataGridViewAutoFilterTextBoxColumn ColumnTableName = new DataGridViewAutoFilterTextBoxColumn();
            ColumnTableName.DataPropertyName = "TableName";
            ColumnTableName.Name = "ColumnTableName";
            ColumnTableName.HeaderText = "̨��";
            ColumnTableName.Width = 120;
            ColumnTableName.DisplayIndex = 20;
            dgvMain.Columns.Add(ColumnTableName);

        }
        private void FormMonitor_Load(object sender, EventArgs e)
        {
            tmDataRefresh.Enabled = false;
            MixLib.CAppOption.LoadData();

            this.Text = CAppOption.WindowTitle;

            CGlobalInstance.Instance.DbAdaHelper.ConnectString = MixLib.CAppOption.m_sDbConnectString;

            if (!CGlobalInstance.Instance.DbAdaHelper.IsOpen)
            {
                ToolStripMenuItemConnect.PerformClick();

                this.Close();
                return;
            }
            else
            {
                //������ݿ������ļ�
                CGlobalInstance.Instance.DbAdaHelper.ExecUpdate();
            }

            InitOrderdishesList();

            PaginationQuery = new CPaginationQuery((COleDbOperate)CGlobalInstance.Instance.DbAdaHelper, "vOrderdishesList", "BillListID");
            PaginationQuery.PageRowNum = CAppOption.PageRowCount;
            PaginationQuery.Sorter = CSortStyle.GetSort(CAppOption.iSortStyle);

            //��Ʊ����,��ˮ����,���ʺ�,ɽˮ����
            CAppOption.WindowStyleMan.SetStyle(this, CAppOption.iFormStyle);
            SetShowStyle("ȫ��");

            tabControlList.TabPages.Clear();
            tabControlList.TabPages.Add("tpAll", "ȫ��");
            DataTable dtList = CGlobalInstance.Instance.dtBillState;
            if (dtList != null)
            {
                foreach (DataRow dr in dtList.Rows)
                {
                    tabControlList.TabPages.Add("tpState_" + dr["Value"].ToString(), dr["Name"].ToString());
                }
            }            

            

            DataRefresh();
            ShowPageInfo();

            //������ʱ��
            tmDataRefresh.Enabled = true;
        }

        private void FormMonitor_FormClosed(object sender, FormClosedEventArgs e)
        {
            MixLib.CAppOption.SaveData();
        }

        void SetShowStyle(string ClassifyName)
        {
            bool bRet=MixFunc.DataGridViewHelper.LoadStyle(dgvMain, ClassifyName);            
        }

        void dgvMain_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta >= 120)
            {
                PageUp();
            }
            else if (e.Delta <= -120)
            {
                PageDown();
            }
        }
        void FormWatch_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta >= 120)
            {
                PageUp();
            }
            else if (e.Delta <= -120)
            {
                PageDown();
            }
        }
        void PageUp()
        {
            iCurPageIndex--;
            DataRefresh();
            ShowPageInfo();
        }

        void PageDown()
        {
            iCurPageIndex--;
            DataRefresh();
            ShowPageInfo();
        }

        void ShowPageInfo()
        {
            int pageindex = iCurPageIndex;
            int iPageCount = PaginationQuery.PageCount;
            if (iPageCount != 0) pageindex = pageindex % iPageCount;//���¼���pageindex
            if (pageindex < 0) pageindex = iPageCount + pageindex;

            toolStripLabelPageInfo.Text = string.Format("��{0}ҳ ��{1}ҳ", iPageCount, pageindex + 1);
        }

        private void toolStripButtonConnect_Click(object sender, EventArgs e)
        {
            MSDASC.DataLinks mydlg = new MSDASC.DataLinks();
            CDataAdaHelper OleCon = new CDataAdaHelper();

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
                    CGlobalInstance.Instance.WriteErrorLog(YyLogger.LogSeverity.error, "�������ݿ�����", ex);
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
                catch (Exception ex)
                {
                    MessageBox.Show("������Ч,�޷��������ݿ⣨ע����ѡ�����������룩��");
                    CGlobalInstance.Instance.WriteErrorLog(YyLogger.LogSeverity.error, "�������ݿ�����", ex);
                }
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
        public void WriteErrorLog(string source, Exception ex)
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

        private void tmDataRefresh_Tick(object sender, EventArgs e)
        {
            //������С����ʱ��ִ��ˢ��
            if (this.WindowState == FormWindowState.Minimized || !CGlobalInstance.Instance.DbAdaHelper.IsOpen)
                return;

            DataRefresh();

            SetStatusBar();
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

                dgvMain.AutoGenerateColumns = false;
                DataTable dt = PaginationQuery.GetDataTable(iCurPageIndex);
                if (dt != null)
                {
                    bindingSource1.DataSource = dt;

                    dgvMain.DataSource = bindingSource1;
                }
                else
                {
                    dgvMain.Rows.Clear();
                }

                btUp.Visible = PaginationQuery.IsMultiPage;
                btDown.Visible = PaginationQuery.IsMultiPage;

                if (string.IsNullOrEmpty(bindingSource1.Filter)) btShowAll.Visible = false;
                else btShowAll.Visible = true;                

            }
            catch (Exception ex)
            {
                tmDataRefresh.Enabled = false;

                System.Windows.Forms.MessageBox.Show(ex.Message);
                this.WriteErrorLog("�����Զ�����", ex);                

                tmDataRefresh.Enabled = true;
            }
        }
        int GetCurrentState()
        {
            int State = -1;
            if (tabControlList.SelectedTab != null)
            {
                string[] NameList = tabControlList.SelectedTab.Name.Split('_');
                try
                {
                    if (NameList.Length > 1)
                        State = Convert.ToInt32(NameList[1]);
                }
                catch
                {
                }
            }
            return State;
        }
        private void OnUpdateFilter(object sender, EventArgs e)
        {
            iCurPageIndex = 0;

            int State = -1;
            State=GetCurrentState();
            if (State!=-1)
            {
                PaginationQuery.Filter = string.Format("State={0}", State);
            }
            else
            {
                PaginationQuery.Filter = "";

            }
            if (bindingSource1 != null)
            {
                if (!string.IsNullOrEmpty(bindingSource1.Filter))
                {
                    if (string.IsNullOrEmpty(PaginationQuery.Filter))
                    {
                        //�հ� LEN(ISNULL(CONVERT([{0}],'System.String'),''))=0 
                        //LEN(ISNULL(CONVERT(varchar(50), ProductName), '')) > 0

                        //�ǿհ� LEN(ISNULL(CONVERT([{0}],'System.String'),''))>0
                        PaginationQuery.Filter = TranFilter(bindingSource1.Filter);
                    }
                    else
                    {
                        PaginationQuery.Filter += (" AND " + TranFilter(bindingSource1.Filter));
                    }
                }
            }
            PaginationQuery.RePaginationQuery();

            DataRefresh();
            ShowPageInfo();
        }
        string TranFilter(string Filter)
        {
            string NewStr = "";
            int iPos = -1, iPos2 = -1, iPos3 = -1;
            string[] stringSeparators = new string[] { " AND " };

            string[] strList = Filter.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            foreach (string tmp in strList)
            {
                iPos = tmp.IndexOf("CONVERT(");
                if (iPos > 0)
                {
                    string tmp2 = "", tmp3 = "";
                    iPos += 8;
                    iPos2 = tmp.IndexOf(",", iPos);
                    iPos3 = tmp.LastIndexOf(")") + 1;
                    tmp2 = tmp.Substring(iPos, iPos2 - iPos);
                    tmp3 = string.Format("LEN(ISNULL(CONVERT(varchar(50), {0}), ''))", tmp2);

                    tmp3 += tmp.Substring(iPos3, tmp.Length - iPos3);

                    NewStr += (tmp3 + " AND ");
                }
                else
                {
                    NewStr += (tmp + " AND ");
                }
            }
            return NewStr.Substring(0, NewStr.Length - 4);
        }
        private void tabControlList_SelectedIndexChanged(object sender, EventArgs e)
        {
            iCurPageIndex = 0;           

            if (bindingSource1 != null) bindingSource1.Filter = "";//�����Ϊ�˽������ Lgahero Xie

            int State = -1;
            State = GetCurrentState();
            if (State != -1)
            {
                PaginationQuery.Filter = string.Format("State={0}", State);
            }
            else
            {
                PaginationQuery.Filter = "";

            }

            string ClassifyName = "";
            if (tabControlList.SelectedTab != null)
            {
                ClassifyName = tabControlList.SelectedTab.Text;
            }
            SetShowStyle(ClassifyName);

            PaginationQuery.RePaginationQuery();

            DataRefresh();
            ShowPageInfo();


            DataGridViewYyFilterTextBoxColumn tmpColumnGoodsName = dgvMain.Columns["ColumnGoodsName"] as DataGridViewYyFilterTextBoxColumn;
            if (tmpColumnGoodsName != null)
            {
                ((DataGridViewYyFilterColumnHeaderCell)tmpColumnGoodsName.HeaderCell).ResetDropDown();
            }

            DataGridViewYyFilterTextBoxColumn tmpColumnGoodsClassifyName = dgvMain.Columns["ColumnGoodsClassifyName"] as DataGridViewYyFilterTextBoxColumn;
            if (tmpColumnGoodsClassifyName != null)
            {
                ((DataGridViewYyFilterColumnHeaderCell)tmpColumnGoodsClassifyName.HeaderCell).ResetDropDown();
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

        private void FormMonitor_SizeChanged(object sender, EventArgs e)
        {
            btUp.Left = dgvMain.Right - 100;
            btUp.Top = dgvMain.Bottom - 5;
            btUp.Width = 80;

            btDown.Left = dgvMain.Right - 100;
            btDown.Top = dgvMain.Bottom + 20;
            btDown.Width = 80;

            btShowAll.Left = dgvMain.Right - 200;
            btShowAll.Top = dgvMain.Bottom + 20;
            btShowAll.Width = 80;
        }

        private void btShowAll_Click(object sender, EventArgs e)
        {
            DataGridViewYyFilterColumnHeaderCell.RemoveFilter(dgvMain);
            OnUpdateFilter(this, new EventArgs());
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            iCurPageIndex--;

            DataRefresh();
            ShowPageInfo();
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            iCurPageIndex++;

            DataRefresh();
            ShowPageInfo();
        }

        private void toolStripButtoncooked_Click(object sender, EventArgs e)
        {
            long BillListID=0;
            int State = 0;
            if (dgvMain.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow dr in dgvMain.SelectedRows)
                {
                    BillListID = Convert.ToInt64(dr.Cells["ColumnBillListID"].Value);
                    State = Convert.ToInt32(dr.Cells["ColumnState"].Value);

                    if (State == 1) CGlobalInstance.Instance.DbAdaHelper.UpdateBillListState(BillListID, 2);//����
                }
            }
            else
            {
                if (dgvMain.CurrentRow == null) return;

                DataGridViewRow dr = dgvMain.CurrentRow;
                BillListID = Convert.ToInt64(dr.Cells["ColumnBillListID"].Value);
                State = Convert.ToInt32(dr.Cells["ColumnState"].Value);

                if (State == 1) CGlobalInstance.Instance.DbAdaHelper.UpdateBillListState(BillListID, 2);//����
            }
        }

        private void toolStripButtonServing_Click(object sender, EventArgs e)
        {
            //�����Ƿ���ԱȨ��
            long BillListID = 0;
            int State = 0;
            if (dgvMain.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow dr in dgvMain.SelectedRows)
                {
                    BillListID = Convert.ToInt64(dr.Cells["ColumnBillListID"].Value);
                    State = Convert.ToInt32(dr.Cells["ColumnState"].Value);

                    if (State == 2) CGlobalInstance.Instance.DbAdaHelper.UpdateBillListState(BillListID, 3);//�ϲ�
                }
            }
            else
            {
                if (dgvMain.CurrentRow == null) return;

                DataGridViewRow dr = dgvMain.CurrentRow;
                BillListID = Convert.ToInt64(dr.Cells["ColumnBillListID"].Value);
                State = Convert.ToInt32(dr.Cells["ColumnState"].Value);

                if (State == 2) CGlobalInstance.Instance.DbAdaHelper.UpdateBillListState(BillListID, 3);//�ϲ�
            }           
        }

        private void toolStripButtonDeliver_Click(object sender, EventArgs e)
        {
            //�����Ƿ���ԱȨ��
            long BillListID = 0;
            int State = 0;
            if (dgvMain.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow dr in dgvMain.SelectedRows)
                {
                    BillListID = Convert.ToInt64(dr.Cells["ColumnBillListID"].Value);
                    State = Convert.ToInt32(dr.Cells["ColumnState"].Value);

                    if (State == 0) CGlobalInstance.Instance.DbAdaHelper.UpdateBillListState(BillListID, 1);//�ͳ�
                }
            }
            else
            {
                if (dgvMain.CurrentRow == null) return;

                DataGridViewRow dr = dgvMain.CurrentRow;
                BillListID = Convert.ToInt64(dr.Cells["ColumnBillListID"].Value);
                State = Convert.ToInt32(dr.Cells["ColumnState"].Value);

                if (State == 0) CGlobalInstance.Instance.DbAdaHelper.UpdateBillListState(BillListID, 1);//�ͳ�
            }
        }

        private void toolStripButtonReturn_Click(object sender, EventArgs e)
        {
            //�����Ƿ���ԱȨ��
            long BillListID = 0;
            int State = 0;
            if (dgvMain.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow dr in dgvMain.SelectedRows)
                {
                    BillListID = Convert.ToInt64(dr.Cells["ColumnBillListID"].Value);
                    State = Convert.ToInt32(dr.Cells["ColumnState"].Value);

                    if (State> 0) CGlobalInstance.Instance.DbAdaHelper.UpdateBillListState(BillListID, State-1);//�ϲ�
                }
            }
            else
            {
                if (dgvMain.CurrentRow == null) return;

                DataGridViewRow dr = dgvMain.CurrentRow;
                BillListID = Convert.ToInt64(dr.Cells["ColumnBillListID"].Value);
                State = Convert.ToInt32(dr.Cells["ColumnState"].Value);

                if (State > 0) CGlobalInstance.Instance.DbAdaHelper.UpdateBillListState(BillListID, State - 1);//�ϲ�
            }
        }

        private void toolStripButtonStyle_Click(object sender, EventArgs e)
        {
            string ClassifyName = "";
            if (tabControlList.SelectedTab != null)
            {
                ClassifyName = tabControlList.SelectedTab.Text;
            }

            if (string.IsNullOrEmpty(ClassifyName)) return;

            FormColumn dlg = new FormColumn();
            dlg.dgvMainSet = dgvMain;
            dlg.ShowDialog();

            MixFunc.DataGridViewHelper.SaveStyle(dgvMain, ClassifyName);  
        }
    }
}