using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MixLib;
using OleDbOperate;

namespace UserModule
{
    public partial class FrmModPwd : Form
    {
        public UserDbAdaHelper m_DbAdaHelper = null;

        public string m_User;
        public string m_Password;

        public FrmModPwd()
        {
            InitializeComponent();
        }

        private void bt_Confirm_Click(object sender, EventArgs e)
        {
            if (tb_NewPwd.Text.Trim().Length < 6)
            {
                MessageBox.Show("�����볤�Ȳ���6λ,���������������룡");
            }
            else if (tb_NewPwd.Text != tb_Confirm.Text)
            {
                MessageBox.Show("ȷ�������������벻���!���������롣");
            }
            else
            {
                if (m_DbAdaHelper.GetUserInfo(tb_User.Text, tb_OldPwd.Text) == null)
                {
                    MessageBox.Show("�����û�����������!���������롣");
                }
                else
                {
                    if (m_DbAdaHelper.ModifyPwd(tb_User.Text, tb_OldPwd.Text, tb_NewPwd.Text) != true)
                    {
                        MessageBox.Show("�޸�����ʧ�ܣ�");
                    }
                    else
                    {
                        m_User = tb_User.Text;
                        
                         
                        MessageBox.Show("�޸�����ɹ���");

                        this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    }
                }
            }
        }

        private void FrmModPwd_Load(object sender, EventArgs e)
        {
            tb_User.Text = m_User;
        }        
    }
}