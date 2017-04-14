using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Restaurant
{
    public partial class FormAssociatorClassify : Form
    {
        BindingSource bs;
        public FormAssociatorClassify()
        {
            InitializeComponent();

            InitView();
        }
        void InitView()
        {
            dgvMain.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn ColumnClassifyID = new DataGridViewTextBoxColumn();
            ColumnClassifyID.DataPropertyName = "ClassifyID";
            ColumnClassifyID.Name = "ColumnClassifyID";
            ColumnClassifyID.HeaderText = "���";
            ColumnClassifyID.Width = 30;
            ColumnClassifyID.FillWeight = 40F;
            ColumnClassifyID.ReadOnly = true;
            ColumnClassifyID.DisplayIndex = 0;
            dgvMain.Columns.Add(ColumnClassifyID);

            DataGridViewTextBoxColumn ColumnClassifyName = new DataGridViewTextBoxColumn();
            ColumnClassifyName.DataPropertyName = "ClassifyName";
            ColumnClassifyName.Name = "ColumnClassifyName";
            ColumnClassifyName.HeaderText = "��Ա����";
            ColumnClassifyName.Width = 70;
            ColumnClassifyName.DisplayIndex = 1;
            dgvMain.Columns.Add(ColumnClassifyName);

            DataGridViewCheckBoxColumn ColumnIsTimeLimit = new DataGridViewCheckBoxColumn();
            ColumnIsTimeLimit.DataPropertyName = "IsTimeLimit";
            ColumnIsTimeLimit.Name = "ColumnIsTimeLimit";
            ColumnIsTimeLimit.HeaderText = "ʱ��";
            ColumnIsTimeLimit.Width = 70;
            ColumnIsTimeLimit.TrueValue = 1;
            ColumnIsTimeLimit.FalseValue = 0;
            ColumnIsTimeLimit.DisplayIndex = 2;
            dgvMain.Columns.Add(ColumnIsTimeLimit);

            DataGridViewTextBoxColumn ColumnTimeLimit = new DataGridViewTextBoxColumn();
            ColumnTimeLimit.DataPropertyName = "TimeLimit";
            ColumnTimeLimit.Name = "ColumnTimeLimit";
            ColumnTimeLimit.HeaderText = "ʱ��(�죩";
            ColumnTimeLimit.Width = 70;
            ColumnTimeLimit.DisplayIndex = 3;
            dgvMain.Columns.Add(ColumnTimeLimit);

            DataGridViewTextBoxColumn ColumnAgioRate = new DataGridViewTextBoxColumn();
            ColumnAgioRate.DataPropertyName = "AgioRate";
            ColumnAgioRate.Name = "ColumnAgioRate";
            ColumnAgioRate.HeaderText = "�ۿ�";
            ColumnAgioRate.Width = 70;
            ColumnAgioRate.DisplayIndex = 4;
            dgvMain.Columns.Add(ColumnAgioRate);

            DataGridViewTextBoxColumn ColumnIntegralRadix = new DataGridViewTextBoxColumn();
            ColumnIntegralRadix.DataPropertyName = "IntegralRadix";
            ColumnIntegralRadix.Name = "ColumnIntegralRadix";
            ColumnIntegralRadix.HeaderText = "���ֻ���";
            ColumnIntegralRadix.Width = 70;
            ColumnIntegralRadix.DisplayIndex = 5;
            dgvMain.Columns.Add(ColumnIntegralRadix);
        }
        private void FormAssociatorClassify_Load(object sender, EventArgs e)
        {
            bs = new BindingSource();

            ShowAssociatorClassify();

            groupBox1.Enabled = false;
        }
        void ShowAssociatorClassify()
        {
            bs.DataSource = CGlobalInstance.Instance.DbAdaHelper.GetAssociatorClassify();

            dgvMain.DataSource = bs;
        }

        private void toolStripButtonDel_Click(object sender, EventArgs e)
        {
            if (dgvMain.CurrentRow != null)
            {
                int ClassifyID = Convert.ToInt32(dgvMain.CurrentRow.Cells["ColumnClassifyID"].Value);

                if (CGlobalInstance.Instance.DbAdaHelper.DeleteAssociatorClassify(ClassifyID))
                {
                    ShowAssociatorClassify();
                }
            }
        }

        int m_CurrentID = 0;
        public int m_TableNO = 0;
        public int m_iOperate = -1;

        private void dgvMain_SelectionChanged(object sender, EventArgs e)
        {
            if (m_iOperate == 1 || m_iOperate == -1) return;

            ShowCurrentData();
        }

        void ShowCurrentData()
        {
            if (dgvMain.CurrentRow != null)
            {
                int CurrentID = Convert.ToInt32(dgvMain.CurrentRow.Cells["ColumnClassifyID"].Value);

                if (CurrentID != m_CurrentID)
                {
                    tbClassifyName.Text = dgvMain.CurrentRow.Cells["ColumnClassifyName"].Value.ToString();
                    int IsTimeLimit = Convert.ToInt32(dgvMain.CurrentRow.Cells["ColumnIsTimeLimit"].Value);
                    if (IsTimeLimit == 1)
                        cbIsTimeLimit.Checked = true;
                    else
                        cbIsTimeLimit.Checked = false;

                    tbTimeLimit.Text = dgvMain.CurrentRow.Cells["ColumnTimeLimit"].Value.ToString();

                    tbAgioRate.Text = dgvMain.CurrentRow.Cells["ColumnAgioRate"].Value.ToString();
                    tbIntegralRadix.Text = dgvMain.CurrentRow.Cells["ColumnIntegralRadix"].Value.ToString();
                    tbRemark.Text = dgvMain.CurrentRow.Cells["ColumnRemark"].Value.ToString();

                    groupBox1.Enabled = false;
                    m_CurrentID = CurrentID;
                }
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            m_CurrentID = 0;

            tbClassifyName.Text = "";
            cbIsTimeLimit.Checked = false;
            tbTimeLimit.Text = "365";

            tbAgioRate.Text = "0.98";
            tbIntegralRadix.Text = "100";
            tbRemark.Text="";

            m_iOperate = 1;
            groupBox1.Enabled = true;
            btAdd.Enabled = false;
            btMod.Enabled = false;
        }

        private void btMod_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            m_iOperate = 2;

            ShowCurrentData();

            btAdd.Enabled = false;
            btMod.Enabled = false;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;

            _tagAssociatorClassify data = new _tagAssociatorClassify();

            data.ClassifyID = m_CurrentID;
            data.ClassifyName = tbClassifyName.Text;
            data.TimeLimit = Convert.ToInt32(tbTimeLimit.Text);
            data.IntegralRadix = Convert.ToInt32(tbIntegralRadix.Text);
            data.AgioRate = Convert.ToDouble(tbAgioRate.Text);
            data.Remark = tbRemark.Text;

            if (cbIsTimeLimit.Checked == true)
            {
                data.IsTimeLimit = 1;
            }
            else
            {
                data.IsTimeLimit = 0;
            }

            bool bRet = false;
            if (m_iOperate == 1)
            {
                bRet = CGlobalInstance.Instance.DbAdaHelper.AddAssociatorClassify(data);                
            }
            else
            {
                bRet = CGlobalInstance.Instance.DbAdaHelper.UpdateAssociatorClassify(data);
            }
            if (bRet == true)
            {
                ShowAssociatorClassify();
                m_iOperate = 0;
            }
            else
            {
                MessageBox.Show("���ݿ������󣬱���ʧ��!");
            }

            btAdd.Enabled = true;
            btMod.Enabled = true;
        }

    }
}