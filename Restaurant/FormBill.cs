using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataGridViewAutoFilter;

namespace Restaurant
{
    public partial class FormBill : Form
    {
        BindingSource bs;

        public _tagBill m_Bill=new _tagBill();
        public FormBill()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FormBill_Load(object sender, EventArgs e)
        {
            tbBillNO.Text = m_Bill.BillNO;
            tbTableNO.Text = m_Bill.TableNO.ToString();
            tbTableName.Text = m_Bill.TableName;

            dgvMain.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn ColumnPrearrangeID = new DataGridViewTextBoxColumn();
            ColumnPrearrangeID.DataPropertyName = "PrearrangeID";
            ColumnPrearrangeID.Name = "ColumnPrearrangeID";
            ColumnPrearrangeID.HeaderText = "Ԥ�����";
            //ColumnPrearrangeID.Visible = false;
            ColumnPrearrangeID.Width = 30;
            ColumnPrearrangeID.FillWeight = 40F;
            ColumnPrearrangeID.ReadOnly = true;
            ColumnPrearrangeID.DisplayIndex = 0;
            dgvMain.Columns.Add(ColumnPrearrangeID);

            DataGridViewTextBoxColumn ColumnClientName = new DataGridViewTextBoxColumn();
            ColumnClientName.DataPropertyName = "ClientName";
            ColumnClientName.Name = "ColumnClientName";
            ColumnClientName.HeaderText = "�ͻ�";
            ColumnClientName.Width = 70;
            ColumnClientName.DisplayIndex = 1;
            dgvMain.Columns.Add(ColumnClientName);

            DataGridViewTextBoxColumn ColumnClientNumber = new DataGridViewTextBoxColumn();
            ColumnClientNumber.DataPropertyName = "ClientNumber";
            ColumnClientNumber.Name = "ColumnClientNumber";
            ColumnClientNumber.HeaderText = "����";
            ColumnClientNumber.Width = 70;
            ColumnClientNumber.DisplayIndex = 2;
            dgvMain.Columns.Add(ColumnClientNumber);

            DataGridViewAutoFilterTextBoxColumn ColumnPhone = new DataGridViewAutoFilterTextBoxColumn();
            ColumnPhone.DataPropertyName = "Phone";
            ColumnPhone.Name = "ColumnPhone";
            ColumnPhone.HeaderText = "�绰";
            ColumnPhone.Width = 70;
            ColumnPhone.DisplayIndex = 3;
            dgvMain.Columns.Add(ColumnPhone);

            DataGridViewTextBoxColumn ColumnRemark = new DataGridViewTextBoxColumn();
            ColumnRemark.DataPropertyName = "Remark";
            ColumnRemark.Name = "ColumnRemark";
            ColumnRemark.HeaderText = "��ע";
            ColumnRemark.Width = 70;
            ColumnRemark.DisplayIndex = 4;
            dgvMain.Columns.Add(ColumnRemark);

            DataGridViewTextBoxColumn ColumnOrderTime = new DataGridViewTextBoxColumn();
            ColumnOrderTime.DataPropertyName = "OrderTime";
            ColumnOrderTime.Name = "ColumnOrderTime";
            ColumnOrderTime.HeaderText = "Ԥ��ʱ��";
            ColumnOrderTime.Width = 120;
            ColumnOrderTime.DisplayIndex = 5;
            dgvMain.Columns.Add(ColumnOrderTime);

            DataGridViewAutoFilterTextBoxColumn ColumnTableNO = new DataGridViewAutoFilterTextBoxColumn();
            ColumnTableNO.DataPropertyName = "TableNO";
            ColumnTableNO.Name = "ColumnTableNO";
            ColumnTableNO.HeaderText = "̨��";
            ColumnTableNO.Width = 120;
            ColumnTableNO.DisplayIndex = 6;
            dgvMain.Columns.Add(ColumnTableNO);

            DataGridViewAutoFilterTextBoxColumn ColumnTableName = new DataGridViewAutoFilterTextBoxColumn();
            ColumnTableName.DataPropertyName = "TableName";
            ColumnTableName.Name = "ColumnTableName";
            ColumnTableName.HeaderText = "̨��";
            ColumnTableName.Width = 120;
            ColumnTableName.DisplayIndex = 7;
            dgvMain.Columns.Add(ColumnTableName);

            DataGridViewAutoFilterTextBoxColumn ColumnEmployeeNO = new DataGridViewAutoFilterTextBoxColumn();
            ColumnEmployeeNO.DataPropertyName = "EmployeeNO";
            ColumnEmployeeNO.Name = "ColumnEmployeeNO";
            ColumnEmployeeNO.HeaderText = "����Ա��";
            ColumnEmployeeNO.Width = 120;
            ColumnEmployeeNO.DisplayIndex = 8;
            dgvMain.Columns.Add(ColumnEmployeeNO);

            DataGridViewComboBoxColumn ColumnState = new DataGridViewComboBoxColumn();
            ColumnState.DataPropertyName = "State";
            ColumnState.Name = "ColumnState";
            ColumnState.HeaderText = "״̬";
            ColumnState.Width = 60;
            ColumnState.DisplayIndex = 9;
            ColumnState.Visible = false;
            dgvMain.Columns.Add(ColumnState);

            DataGridViewTextBoxColumn ColumnRecordTime = new DataGridViewTextBoxColumn();
            ColumnRecordTime.DataPropertyName = "RecordTime";
            ColumnRecordTime.Name = "ColumnRecordTime";
            ColumnRecordTime.HeaderText = "��¼ʱ��";
            ColumnRecordTime.Width = 120;
            ColumnRecordTime.DisplayIndex = 10;
            dgvMain.Columns.Add(ColumnRecordTime);

            DataGridViewTextBoxColumn ColumnDiningTableID = new DataGridViewTextBoxColumn();
            ColumnDiningTableID.DataPropertyName = "DiningTableID";
            ColumnDiningTableID.Name = "ColumnDiningTableID";
            ColumnDiningTableID.HeaderText = "̨ID";
            ColumnDiningTableID.Width = 120;
            ColumnDiningTableID.DisplayIndex = 11;
            ColumnDiningTableID.Visible = false;
            dgvMain.Columns.Add(ColumnDiningTableID);

            ColumnState.DataSource = CGlobalInstance.Instance.DbAdaHelper.GetPrearrangeState();
            ColumnState.DisplayMember = "ItemName";
            ColumnState.ValueMember = "ItemValue";

            timer1.Enabled = true;

            bs = new BindingSource();

            ShowPrearrange();

        }

        void ShowPrearrange()
        {
            //int TableNO = 0;
            //TableNO = Convert.ToInt32(tbTableNO.Text);

            bs.DataSource = CGlobalInstance.Instance.DbAdaHelper.GetPrearrangeByTableNO(m_Bill.TableNO);
            
            dgvMain.DataSource = bs;

            m_PrearrangeID = 0;
            m_TableNO = 0;
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbClientNumber.Text))
            {
                MessageBox.Show("��������Ϊ��!");
                return;
            }

            m_Bill.TableNO = Convert.ToInt32(tbTableNO.Text);
            m_Bill.BillNO = tbBillNO.Text;
            m_Bill.ClientName = tbClientName.Text;
            m_Bill.ClientNumber = Convert.ToInt32(tbClientNumber.Text);
            m_Bill.BillTime = DateTime.Now;
            m_Bill.Remark = tbRemark.Text;
            if (dgvMain.RowCount > 0)
            {
                if ( m_TableNO == 0)
                {
                    if (MessageBox.Show("��û��ѡ��Ԥ���ͻ���ȷ����������̨��", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }else  if (m_TableNO != m_Bill.TableNO)
                {
                    if (MessageBox.Show("��û��ѡ��Ԥ���ͻ���ȷ����������̨��", "��ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }
            }
            if (CGlobalInstance.Instance.DbAdaHelper.AddBill(m_Bill))
            {
                //��Ϊ���
                if (m_Bill.TableNO == m_TableNO)
                    CGlobalInstance.Instance.DbAdaHelper.UpdatePrearrange(m_PrearrangeID, 1);

                int State = (int)YyTableCtrl.YyDiningTable.YyTableState.Using;
                CGlobalInstance.Instance.DbAdaHelper.UpdateDiningTableState(m_Bill.TableNO, State);

                this.DialogResult = DialogResult.OK;
            }
        }

        private void tbTableNO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbTableNO.Text))
            {
                m_Bill.TableNO = Convert.ToInt32(tbTableNO.Text);
                tbTableName.Text = CGlobalInstance.Instance.DbAdaHelper.GetDiningTableName(m_Bill.TableNO);

                ShowPrearrange();
            }
            else
            {
                tbTableName.Text = "";
            }
        }
        long m_PrearrangeID = 0;
        int m_TableNO = 0;
        private void dgvMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMain.CurrentRow != null)
            {
                tbClientName.Text = dgvMain.CurrentRow.Cells["ColumnClientName"].Value.ToString();
                tbClientNumber.Text = dgvMain.CurrentRow.Cells["ColumnClientNumber"].Value.ToString();
                tbTableNO.Text = dgvMain.CurrentRow.Cells["ColumnTableNO"].Value.ToString();
                tbTableName.Text = dgvMain.CurrentRow.Cells["ColumnTableName"].Value.ToString();
                tbRemark.Text = dgvMain.CurrentRow.Cells["ColumnRemark"].Value.ToString();
                m_PrearrangeID = Convert.ToInt64(dgvMain.CurrentRow.Cells["ColumnPrearrangeID"].Value);
                m_TableNO = Convert.ToInt32(tbTableNO.Text);

                lblPrompt.Text = "��ѡ����Ԥ����ţ�" + m_PrearrangeID.ToString();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tbBillTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}