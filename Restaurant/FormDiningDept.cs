using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Restaurant
{
    public partial class FormDiningDept : Form
    {
        public int m_DiningDeptID = 0;
        public string m_DeptName = "";

        public int iOperate = 0;

        public FormDiningDept()
        {
            InitializeComponent();
        }

        private void FormDiningDept_Load(object sender, EventArgs e)
        {
            tbDiningDeptID.Text = m_DiningDeptID.ToString();
            tbDeptName.Text = m_DeptName.ToString();
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            if (tbDiningDeptID.Text == "" || tbDeptName.Text == "")
            {
                MessageBox.Show("���ű�š����Ʋ��ܿ�!");
                return;
            }

            if (tbDiningDeptID.Text != "")
            {
                try{
                    m_DiningDeptID = Convert.ToInt32(tbDiningDeptID.Text);
                }catch{
                    MessageBox.Show("���ű������������!");
                }
            }
            else
            {
                m_DiningDeptID = 0;
            }

            m_DeptName = tbDeptName.Text;

            if (iOperate == 0)
            {
                if (CGlobalInstance.Instance.DbAdaHelper.AddDiningDept(m_DiningDeptID, m_DeptName)!=true)
                {
                    MessageBox.Show("���ű�ſ����ظ�,��������������!");
                    return;
                }
            }
            else
            {
                CGlobalInstance.Instance.DbAdaHelper.UpdateDiningDept(m_DiningDeptID, m_DeptName);
            }

            this.DialogResult = DialogResult.OK;

        }
    }
}