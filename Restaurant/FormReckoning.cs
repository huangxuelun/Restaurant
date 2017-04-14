using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using YyPrint;

namespace Restaurant
{
    public partial class FormReckoning : Form
    {
        _tagOnCredit m_OnCredit = null;
        _tagAssociatorParam m_Associator = null;
        double m_Account = 0;
        double m_RealAccount = 0;
        double m_ReceAccount = 0;
        double m_Discount = 0;

        public _tagBill m_Bill = new _tagBill();

        BindingSource bsBillList;

        public FormReckoning()
        {
            InitializeComponent();

            
        }

        void InitBillList()
        {
            dgvBillList.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn ColumnBillListID = new DataGridViewTextBoxColumn();
            ColumnBillListID.DataPropertyName = "BillListID";
            ColumnBillListID.Name = "ColumnBillListID";
            ColumnBillListID.HeaderText = "BillListID";
            ColumnBillListID.Visible = false;
            ColumnBillListID.Width = 30;
            ColumnBillListID.FillWeight = 40F;
            ColumnBillListID.ReadOnly = true;
            ColumnBillListID.DisplayIndex = 0;
            dgvBillList.Columns.Add(ColumnBillListID);

            DataGridViewTextBoxColumn ColumnGoodsID2 = new DataGridViewTextBoxColumn();
            ColumnGoodsID2.DataPropertyName = "GoodsID";
            ColumnGoodsID2.Name = "ColumnGoodsID2";
            ColumnGoodsID2.HeaderText = "GoodsID";
            ColumnGoodsID2.Visible = false;
            ColumnGoodsID2.Width = 30;
            ColumnGoodsID2.FillWeight = 40F;
            ColumnGoodsID2.ReadOnly = true;
            ColumnGoodsID2.DisplayIndex = 1;
            dgvBillList.Columns.Add(ColumnGoodsID2);


            DataGridViewTextBoxColumn ColumnBillID = new DataGridViewTextBoxColumn();
            ColumnBillID.DataPropertyName = "BillID";
            ColumnBillID.Name = "ColumnBillID";
            ColumnBillID.HeaderText = "BillID";
            ColumnBillID.Width = 70;
            ColumnBillID.DisplayIndex = 2;
            ColumnBillID.Visible = false;
            dgvBillList.Columns.Add(ColumnBillID);

            DataGridViewTextBoxColumn ColumnLongGoodsNO2 = new DataGridViewTextBoxColumn();
            ColumnLongGoodsNO2.DataPropertyName = "LongGoodsNO";
            ColumnLongGoodsNO2.Name = "ColumnLongGoodsNO2";
            ColumnLongGoodsNO2.HeaderText = "������";
            ColumnLongGoodsNO2.Width = 80;
            ColumnLongGoodsNO2.DisplayIndex = 4;
            dgvBillList.Columns.Add(ColumnLongGoodsNO2);

            DataGridViewTextBoxColumn ColumnGoodsName2 = new DataGridViewTextBoxColumn();
            ColumnGoodsName2.DataPropertyName = "GoodsName";
            ColumnGoodsName2.Name = "ColumnGoodsName2";
            ColumnGoodsName2.HeaderText = "������Ŀ";
            ColumnGoodsName2.Width = 120;
            ColumnGoodsName2.DisplayIndex = 3;
            dgvBillList.Columns.Add(ColumnGoodsName2);

            DataGridViewTextBoxColumn ColumnGoodsNumber = new DataGridViewTextBoxColumn();
            ColumnGoodsNumber.DataPropertyName = "GoodsNumber";
            ColumnGoodsNumber.Name = "ColumnGoodsNumber";
            ColumnGoodsNumber.HeaderText = "����";
            ColumnGoodsNumber.Width = 50;
            ColumnGoodsNumber.DisplayIndex = 4;
            dgvBillList.Columns.Add(ColumnGoodsNumber);


            DataGridViewTextBoxColumn ColumnUnit2 = new DataGridViewTextBoxColumn();
            ColumnUnit2.DataPropertyName = "Unit";
            ColumnUnit2.Name = "ColumnUnit2";
            ColumnUnit2.HeaderText = "��λ";
            ColumnUnit2.Width = 50;
            ColumnUnit2.DisplayIndex = 5;
            dgvBillList.Columns.Add(ColumnUnit2);

            DataGridViewTextBoxColumn ColumnUnitPrice2 = new DataGridViewTextBoxColumn();
            ColumnUnitPrice2.DataPropertyName = "UnitPrice";
            ColumnUnitPrice2.Name = "ColumnUnitPrice2";
            ColumnUnitPrice2.HeaderText = "����";
            ColumnUnitPrice2.DefaultCellStyle.Format = "0.00";
            ColumnUnitPrice2.Width = 80;
            ColumnUnitPrice2.DisplayIndex = 6;
            dgvBillList.Columns.Add(ColumnUnitPrice2);

            DataGridViewTextBoxColumn ColumnWorthiness = new DataGridViewTextBoxColumn();
            ColumnWorthiness.DataPropertyName = "Worthiness";
            ColumnWorthiness.Name = "ColumnWorthiness";
            ColumnWorthiness.HeaderText = "С��";
            ColumnWorthiness.DefaultCellStyle.Format = "0.00";
            ColumnWorthiness.Width = 80;
            ColumnWorthiness.DisplayIndex = 7;
            dgvBillList.Columns.Add(ColumnWorthiness);
            

            DataGridViewCheckBoxColumn ColumnIsCurrentPrice2 = new DataGridViewCheckBoxColumn();
            ColumnIsCurrentPrice2.DataPropertyName = "IsCurrentPrice";
            ColumnIsCurrentPrice2.Name = "ColumnIsCurrentPrice2";
            ColumnIsCurrentPrice2.HeaderText = "ʱ��";
            ColumnIsCurrentPrice2.TrueValue = "1";
            ColumnIsCurrentPrice2.FalseValue = "0";
            ColumnIsCurrentPrice2.Visible = true;
            ColumnIsCurrentPrice2.ReadOnly = true;
            ColumnIsCurrentPrice2.Width = 50;
            ColumnIsCurrentPrice2.DisplayIndex = 8;
            dgvBillList.Columns.Add(ColumnIsCurrentPrice2);

            DataGridViewTextBoxColumn ColumnGoodsClassifyName = new DataGridViewTextBoxColumn();
            ColumnGoodsClassifyName.DataPropertyName = "GoodsClassifyName";
            ColumnGoodsClassifyName.Name = "ColumnGoodsClassifyName";
            ColumnGoodsClassifyName.HeaderText = "����";
            ColumnGoodsClassifyName.Width = 70;
            ColumnGoodsClassifyName.DisplayIndex = 9;
            dgvBillList.Columns.Add(ColumnGoodsClassifyName);


            DataGridViewTextBoxColumn ColumnRemark = new DataGridViewTextBoxColumn();
            ColumnRemark.DataPropertyName = "Remark";
            ColumnRemark.Name = "ColumnRemark";
            ColumnRemark.HeaderText = "��ע";
            ColumnRemark.Width = 80;
            ColumnRemark.DisplayIndex = 10;
            dgvBillList.Columns.Add(ColumnRemark);

            DataGridViewTextBoxColumn ColumnDeductEmployeeNO = new DataGridViewTextBoxColumn();
            ColumnDeductEmployeeNO.DataPropertyName = "DeductEmployeeNO";
            ColumnDeductEmployeeNO.Name = "ColumnDeductEmployeeNO";
            ColumnDeductEmployeeNO.HeaderText = "���Ա��";
            ColumnDeductEmployeeNO.Width = 80;
            ColumnDeductEmployeeNO.DisplayIndex = 11;
            dgvBillList.Columns.Add(ColumnDeductEmployeeNO);

            
            DataGridViewComboBoxColumn ColumnType = new DataGridViewComboBoxColumn();
            ColumnType.DataPropertyName = "Type";//
            ColumnType.DisplayMember = "Name";
            ColumnType.ValueMember = "Value";
            ColumnType.DataSource = CGlobalInstance.Instance.dtBillType;

            ColumnType.Name = "ColumnType";
            ColumnType.HeaderText = "����";//�㵥���˵�������,����
            ColumnType.Width = 80;
            ColumnType.DisplayIndex = 12;
            dgvBillList.Columns.Add(ColumnType);

            
            DataGridViewComboBoxColumn ColumnState = new DataGridViewComboBoxColumn();
            ColumnState.DataPropertyName = "State";
            ColumnState.Name = "ColumnState";
            ColumnState.DisplayMember = "Name";
            ColumnState.ValueMember = "Value";
            ColumnState.DataSource = CGlobalInstance.Instance.dtBillState; ;
            ColumnState.HeaderText = "״̬";//0�䵥��1�Ŷ��У�3���ϲ�
            ColumnState.Width = 80;
            ColumnState.DisplayIndex = 12;
            dgvBillList.Columns.Add(ColumnState);            
        }
        EventHandler SelectedValueChangedHandler = null;
        void SetOnCreditDataSource()
        {
            if (SelectedValueChangedHandler != null)
            {
                cbOnCredit.SelectedValueChanged -= SelectedValueChangedHandler;
            }
            int OnCreditID = 0;
            if (m_OnCredit != null) OnCreditID=m_OnCredit.OnCreditID;

            DataTable dtOnCredit = CGlobalInstance.Instance.DbAdaHelper.GetOnCredit();
            DataRow dr = dtOnCredit.NewRow();
            dr["OnCreditID"] = 0;
            dr["ClientName"] = "";
            dtOnCredit.Rows.Add(dr);

            cbOnCredit.DisplayMember = "ClientName";
            cbOnCredit.ValueMember = "OnCreditID";
            cbOnCredit.DataSource = dtOnCredit;

            cbOnCredit.SelectedValue = OnCreditID;

            if (SelectedValueChangedHandler == null)
            {
                SelectedValueChangedHandler = new EventHandler(cbOnCredit_SelectedValueChanged);
            }
            cbOnCredit.SelectedValueChanged += SelectedValueChangedHandler;
        }
        private void FormReckoning_Load(object sender, EventArgs e)
        {
            InitBillList();

            SetOnCreditDataSource();

            tbBillNO.Text = m_Bill.BillNO;
            tbTableNO.Text = m_Bill.TableNO.ToString();
            tbTableName.Text = m_Bill.TableName;
            tbClientName.Text = m_Bill.ClientName;
            tbClientNumber.Text = m_Bill.ClientNumber.ToString();
            tbBillTime.Text = m_Bill.BillTime.ToString("yyyy-MM-dd hh:mm");
            tbRemark.Text = m_Bill.Remark;

            bsBillList = new BindingSource();
            ShowBillList();
        }
        void ShowBillList()
        {
            DataTable dtBillListSrc = CGlobalInstance.Instance.DbAdaHelper.GetBillListOrderType(this.m_Bill.BillID);

            DataTable dtBillList=dtBillListSrc.Copy();

            int ipos=0,iType=0;
            double SumWorthiness = 0;
            bool bRT = false;
            foreach (DataRow dr in dtBillListSrc.Rows)
            {
                iType=Convert.ToInt32(dr["Type"]);

                if (iType >= 2 && bRT==false)
                {
                    DataRow drSumH = dtBillList.NewRow();
                    drSumH["GoodsName"] = "    �ϼ�(��)";
                    drSumH["Worthiness"] = SumWorthiness;
                    dtBillList.Rows.InsertAt(drSumH, ipos);
                    m_Account = SumWorthiness;
                    SumWorthiness = 0;
                    bRT = true;
                }

                if (!Convert.IsDBNull(dr["Worthiness"]))
                    SumWorthiness += Convert.ToDouble(dr["Worthiness"]);
                ipos++;

                
            }

            DataRow drSum = dtBillList.NewRow();
            drSum["GoodsName"] = "    �ϼ�(��)";
            drSum["Worthiness"] = SumWorthiness;
            dtBillList.Rows.Add(drSum);

            if (bRT==false) m_Account = SumWorthiness;

            ShowAccount();

            bsBillList.DataSource = dtBillList;
            dgvBillList.DataSource = bsBillList;
        }
               

        void ShowAccount()
        {
            if (m_Associator != null )
            {
                //&& m_Associator.EndDate<DateTime.Now.Date.AddDays(1)
                m_ReceAccount = m_Account * m_Associator.AgioRate;
                m_Discount = m_Account - m_ReceAccount;
                tbDiscount.Text = m_Discount.ToString("0.00");
            }
            else
            {
                m_ReceAccount = m_Account;
                tbDiscount.Text = "";
            }
            m_RealAccount=m_ReceAccount  ;
            tbAccount.Text = m_Account.ToString("0.00");
            tbReceAccount.Text = m_ReceAccount.ToString("0.00");
            tbRealAccount.Text = tbReceAccount.Text;

            if (CRunSetting.ClientPromptPort.IndexOf("COM") > 0)
            {
                ComPrinter Printer = new ComPrinter(CRunSetting.ClientPromptPort);
                Printer.DisplayMessage("�����ܶ�:" + tbAccount.Text + " Ӧ��:" + tbReceAccount.Text);
            }
        }
        private void cbOnCredit_SelectedValueChanged(object sender, EventArgs e)
        {
            GetSelectedOnCredit();
        }
        void GetOnCreditItemInfo(int OnCreditID)
        {
            m_OnCredit = CGlobalInstance.Instance.DbAdaHelper.GetOnCreditItem(OnCreditID);
            if (m_OnCredit != null)
            {
                tbCompanyB.Text = m_OnCredit.Company;
                tbAddress.Text = m_OnCredit.Address;
                tbPhone.Text = m_OnCredit.Phone;
                tbRemarkB.Text = m_OnCredit.Remark;

                tbOnCreditLimit.Text = m_OnCredit.OnCreditLimit.ToString();
                tbOnCreditSum.Text = m_OnCredit.OnCreditSum.ToString();
                tbSurplusB.Text = m_OnCredit.Surplus.ToString();
            }
            else
            {
                tbCompanyB.Text = "���ݻ�ȡ�쳣";
                tbAddress.Text = "";
                tbPhone.Text = "";
                tbRemarkB.Text = "";

                tbOnCreditLimit.Text = "";
                tbOnCreditSum.Text = "";
                tbSurplusB.Text = "";
            }
        }
        void GetSelectedOnCredit()
        {
            if (cbOnCredit.SelectedItem != null)
            {
                DataRowView dr = (System.Data.DataRowView)cbOnCredit.SelectedItem;
                if (!string.IsNullOrEmpty(cbOnCredit.Text))
                {
                    int OnCreditID = Convert.ToInt32(dr["OnCreditID"]);
                    GetOnCreditItemInfo(OnCreditID);
                }
                else
                {
                    tbCompanyB.Text = dr["Company"].ToString();
                    tbAddress.Text = dr["Address"].ToString();
                    tbPhone.Text = dr["Phone"].ToString();
                    tbRemarkB.Text = dr["Remark"].ToString();

                    tbOnCreditLimit.Text = dr["OnCreditLimit"].ToString();
                    tbOnCreditSum.Text = dr["OnCreditSum"].ToString();
                    tbSurplusB.Text = dr["Surplus"].ToString();
                }                
            }
            else
            {
                m_OnCredit = null;
                tbCompanyB.Text = "";
                tbAddress.Text = "";
                tbPhone.Text = "";
                tbRemarkB.Text = "";

                tbOnCreditLimit.Text = "";
                tbOnCreditSum.Text = "";
                tbSurplusB.Text = "";
            }
        }
        
        private void tbCardID_TextChanged(object sender, EventArgs e)
        {
            if (tbCardPwd.Text != "")
            {
                GetAssociatorData();
            }
        }
        void GetAssociatorData()
        {
            m_Associator = CGlobalInstance.Instance.DbAdaHelper.GetAssociatorItem(tbCardID.Text, tbCardPwd.Text);
            if (m_Associator != null)
            {
                tbAssociator.Text = m_Associator.Associator;
                tbIdentityCard.Text = m_Associator.IdentityCard;
                tbCompany.Text = m_Associator.Company;
                tbSurplus.Text = m_Associator.Surplus.ToString();
                tbIntegral.Text = m_Associator.Integral.ToString();

                tbAgioRate.Text = m_Associator.AgioRate.ToString();

                if (m_Associator.IsTimeLimit != 1)
                {
                    tbEndDate.Text = DateTime.MaxValue.ToString();
                    tbEndDate.Visible = false;
                }
                else
                {
                    tbEndDate.Text = m_Associator.EndDate.ToString();
                    tbEndDate.Visible = true;

                    if (m_Associator.EndDate < DateTime.Now.Date.AddDays(1))
                    {
                        lblPrompt.Text = "���ѵ���,����ʹ�û���!";
                    }
                }

                ShowAccount();

                btDeposit.Visible = true;
            }
        }
        private void tbCardPwd_TextChanged(object sender, EventArgs e)
        {
            if (tbCardID.Text != "")
            {
                GetAssociatorData();
            }
        }

        private void tbRealAccount_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(tbRealAccount.Text))
            {
                try
                {
                    m_RealAccount = Convert.ToDouble(tbRealAccount.Text);
                }
                catch
                {
                }
            }else
            {
                m_RealAccount=0;
            }
        }

        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            double Getback = 0;
            try
            {
                Getback = Convert.ToDouble(tbInput.Text) - m_RealAccount;
                tbGetback.Text = Getback.ToString("0.00");
            }
            catch
            {
                tbGetback.Text = "�������!";
            }
        }
        void UpdateBill()
        {
            m_Bill.Account = m_Account;
            m_Bill.Discount = m_Discount;
            m_Bill.ReceAccount = m_ReceAccount;
            m_Bill.RealAccount = m_RealAccount;
            m_Bill.Remark = tbRemark.Text;
            m_Bill.PrintTime = DateTime.Now;
            m_Bill.Deduct = 0;//�д��޸�

            m_Bill.Checkout = 1;
        }

        private void btreckoning_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbInput.Text))
            {
                if (m_Associator == null && m_OnCredit == null)
                {
                    MessageBox.Show("������ʵ���ֽ��ٽ��н���!");
                    return;
                }

                if(m_OnCredit == null)
                {
                    if (m_Associator != null && m_Associator.Surplus < m_RealAccount)
                    {
                        MessageBox.Show("��Ա�����㣬���ȳ�ֵ!");
                        return;
                    }
                }
            }

            UpdateBill();

            
            //����Ǯ��
            if (CRunSetting.AutoOpenCashbox==1)
            {
                if (CRunSetting.CashboxPort.IndexOf("LPT") > 0)
                {
                    LptPrinter Printer = new LptPrinter(CRunSetting.CashboxPort);
                    Printer.OpenCashbox();
                }
                else
                {
                    ComPrinter Printer = new ComPrinter(CRunSetting.CashboxPort);
                    Printer.OpenCashbox();
                }
            }
            //��һ����Ҫ�Ż�����
            if (CGlobalInstance.Instance.DbAdaHelper.UpdateBillB(m_Bill))
            {
                string Employee = "";//�д��޸�
                if (string.IsNullOrEmpty(tbInput.Text))
                {
                    bool bDeduct = false;
                    bool bOnCredit = false;
                    if (m_Associator != null && m_Associator.Surplus > m_RealAccount)
                    {
                        bDeduct = CGlobalInstance.Instance.DbAdaHelper.AssociatorDeduct(m_Associator.CardID, m_Associator.Surplus, m_RealAccount, Employee, tbRemark.Text);
                        if (bDeduct == true)
                        {
                            MessageBox.Show("��Ա���ʳɹ�!");
                        }
                        else
                        {
                            MessageBox.Show("��Ա����ʧ��!��Ҫ�رս��ʴ��ڣ�����������ٽ��У�");
                            return;
                        }
                    }
                    if (bDeduct == false && m_OnCredit != null)
                    {
                        if (CGlobalInstance.Instance.DbAdaHelper.OnCreditDo(m_OnCredit.OnCreditID, m_OnCredit.OnCreditSum, m_RealAccount, Employee, tbRemark.Text))
                        {
                            bOnCredit = true;
                            MessageBox.Show("���ʳɹ�!");
                        }
                        else
                        {
                            MessageBox.Show("����ʧ��!��Ҫ�رս��ʴ��ڣ����������ٽ��У�");
                            return;
                        }
                    }
                    CGlobalInstance.Instance.DbAdaHelper.UpdateBillListAllState(m_Bill.BillID,3);//���ϲ�
                }
                //�����Ա����
                if (m_Associator != null)
                {
                    int Integral = (int)m_RealAccount / m_Associator.IntegralRadix;
                    CGlobalInstance.Instance.DbAdaHelper.AssociatorIntegral(m_Associator.CardID, Integral);
                }

                int State = (int)YyTableCtrl.YyDiningTable.YyTableState.Reckoning;
                if (CGlobalInstance.Instance.DbAdaHelper.UpdateDiningTableState(m_Bill.TableNO, State))
                {
                    //��ӡ�ʵ�
                    if (MessageBox.Show("��Ҫ��ӡ�ʵ���", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        PrintBill();
                    }

                    //�Զ���̨,�����ʺ�Ϊ����״̬
                    if (CRunSetting.AutoClearTable==1)
                    {
                        State = (int)YyTableCtrl.YyDiningTable.YyTableState.Idle;
                        if (CGlobalInstance.Instance.DbAdaHelper.UpdateDiningTableState(m_Bill.TableNO, State))
                        {
                        }
                    }
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show("�����쳣!��������!");
            }
        }
        string GetPrintBillMsg()
        {
            string strBlank3 = "   ";
            string Msg = "", strValue = "", strValue2 = "";
            Msg += strBlank3 + "��ӡʱ��:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\r\n";
            strValue = m_Bill.ClientName;
            strValue = strValue.PadRight(12, ' ');
            strValue2 = m_Bill.ClientNumber.ToString();
            strValue2 = strValue2.PadRight(12, ' ');
            Msg += strBlank3 + "��������:" + strValue + "��������:" + strValue2 + "\r\n";
            strValue = m_Bill.TableName;
            strValue = strValue.PadRight(12, ' ');
            strValue2 = m_Bill.BillNO.ToString();
            strValue2 = strValue2.PadRight(12, ' ');
            Msg += strBlank3 + "��̨����:" + strValue + "�ʵ����:" + strValue2 + "\r\n";

            strValue = m_Bill.Employee.ToString();
            strValue = strValue.PadRight(12, ' ');
            strValue2 =  m_Bill.BillTime.ToString("yyyy-MM-dd HH:mm");
            strValue2 = strValue2.PadRight(12, ' ');
            Msg += strBlank3 + "����Ա��:" + strValue + "��̨ʱ��:" + strValue2 + "\r\n";

            Msg += strBlank3 + "---------------------------------------------------" + "\r\n";
            Msg += strBlank3 + "  ��Ŀ             ����     ����      ���   " + "\r\n";
            Msg += strBlank3 + "---------------------------------------------------" + "\r\n";

            foreach (DataGridViewRow dr in dgvBillList.Rows)
            {
                Msg += strBlank3;
                strValue = dr.Cells["ColumnGoodsName2"].Value.ToString();                
                int iCount=MixFunc.ChinaSpell.CountChineseLetter(strValue);
                string temp = "";
                temp = temp.PadLeft(16 - iCount - strValue.Length, ' ');
                strValue += temp;
                Msg += strValue;

                strValue = dr.Cells["ColumnUnitPrice2"].FormattedValue.ToString();
                strValue = strValue.PadLeft(8, ' ');
                Msg += strValue;

                strValue = dr.Cells["ColumnGoodsNumber"].Value.ToString();
                strValue = strValue.PadLeft(6, ' ');
                Msg += strValue;

                strValue = dr.Cells["ColumnWorthiness"].FormattedValue.ToString();
                strValue = strValue.PadLeft(10, ' ');
                Msg += strValue;

                strValue = dr.Cells["ColumnType"].FormattedValue.ToString();
                strValue = strValue.PadLeft(6, ' ');
                Msg += strValue;
                

                Msg += "\r\n";
            }

            Msg += strBlank3 + "---------------------------------------------------" + "\r\n";

            strValue = m_Bill.Account.ToString("0.00");
            strValue = strValue.PadRight(12, ' ');
            strValue2 = m_Bill.Discount.ToString("0.00");
            strValue2 = strValue2.PadRight(12, ' ');
            Msg += strBlank3 + "���ѽ��:" + strValue + "�ۿ۽��:" + strValue2 + "\r\n";

            strValue = m_Bill.ReceAccount.ToString("0.00");
            strValue = strValue.PadRight(12, ' ');
            strValue2 = m_Bill.RealAccount.ToString("0.00");
            strValue2 = strValue2.PadRight(12, ' ');
            Msg += strBlank3 + "Ӧ�ս��:" + strValue + "ʵ�ս��:" + strValue2 + "\r\n";

            return Msg;
        }
        bool PrintPreviewBill()
        {
            //�����ı���ӡ

            CTextPrint TextPrint = new CTextPrint(GetPrintBillMsg());
            //TextPrint.m_PrintFont.Name = "����";
            TextPrint.PrinterName = CRunSetting.BillPrinter;
            TextPrint.PaperName = CRunSetting.BillMode;

            TextPrint.PrintPreview();

            return true;
        }
        bool PrintBill()
        {
            //�����ı���ӡ

            CTextPrint TextPrint = new CTextPrint(GetPrintBillMsg());
            //TextPrint.m_PrintFont.Name = "����";

            TextPrint.PrinterName = CRunSetting.BillPrinter;
            TextPrint.PaperName = CRunSetting.BillMode;
            TextPrint.Print();

            return true;
        }
        private void btPrint_Click(object sender, EventArgs e)
        {
            UpdateBill();
            PrintBill();
        }

        private void btPrintPreview_Click(object sender, EventArgs e)
        {
            UpdateBill();
            PrintPreviewBill();
        }

        private void btDeposit_Click(object sender, EventArgs e)
        {
            GetAssociatorData();

            FormDeposit dlg = new FormDeposit();

            dlg.CardID = m_Associator.CardID;
            dlg.Associator = m_Associator.Associator;
            double Surplus = m_Associator.Surplus;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string Employee = "";//�д��޸�
                string Remark = dlg.Remark;

                if (CGlobalInstance.Instance.DbAdaHelper.AssociatorDeposit(dlg.CardID, Surplus, dlg.Value, Employee, Remark))
                {
                    MessageBox.Show("��ֵ�ɹ�!");
                }
                else
                {
                    MessageBox.Show("��ֵʧ��!");
                }
                GetAssociatorData();
            }
            
        }

        private void btRepayment_Click(object sender, EventArgs e)
        {
            if (m_OnCredit==null)
            {
                return;
            }
            FormRepayment dlg = new FormRepayment();
            dlg.ClientName = m_OnCredit.ClientName;
            dlg.Company = m_OnCredit.Company;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string Employee = "";//�д��޸�
                string Remark = dlg.Remark;

                if (CGlobalInstance.Instance.DbAdaHelper.OnCreditRepayment(m_OnCredit.OnCreditID, m_OnCredit.OnCreditSum, -dlg.Value, Employee, Remark))
                {
                    MessageBox.Show("����ɹ�!");
                }
                else
                {
                    MessageBox.Show("����ʧ��!");
                }
                int OnCreditID = m_OnCredit.OnCreditID;

                SetOnCreditDataSource();

                GetOnCreditItemInfo(OnCreditID);
            }
        }

        private void dgvBillList_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (Convert.IsDBNull(dgvBillList.Rows[e.RowIndex].Cells["ColumnBillListID"].Value))
                dgvBillList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
        }

        private void dgvBillList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //e.
        }

        private void tbInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = MixFunc.CommConvert.DBCcaseNumber(e.KeyChar);
        }

        private void tbRealAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = MixFunc.CommConvert.DBCcaseNumber(e.KeyChar);   
        }

        
    }
}