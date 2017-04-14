using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UserModule
{
    public partial class FormUser : Form
    {
        public UserDbAdaHelper m_DbAdaHelper = null;

        public string User="";
        public string Pwd = "";
        public FormUser()
        {
            InitializeComponent();
        }

        private void bt_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(editLoginName.Text))
            {
                MessageBox.Show("��¼������Ϊ��!","��ʾ");
                return;
            }
            if (editPwdInput.Text != editPwd.Text||string.IsNullOrEmpty(editPwdInput.Text))
            {
                MessageBox.Show("ȷ�����벻���(����Ϊ��)!","��ʾ");
                return;
            }
            Pwd = editPwdInput.Text;
            User = editLoginName.Text;

            this.DialogResult = DialogResult.OK;
        }
    }
}