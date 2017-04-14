using System;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using YyLogger;
using MixFunc;

namespace MixLib
{
	/// <summary>
	/// ����ѡ��
	/// </summary>
	public class CAppOption
	{
		//ȫ�ֱ���
		public static Int64   m_lUserID=-1;//��¼֮�����
		public static string m_sUserName="";//��¼֮�����
        public static string m_sUserAlias="";//��¼֮�����
        public static string m_sUserWorkID="";//��¼֮�����

		public static string m_sUser="";
		public static string m_sPwd="";
        public static string m_sDbConnectString = @"Provider=SQLOLEDB.1;Password=13902410869;Persist Security Info=True;User ID=sa;Initial Catalog=OAManage;Data Source=192.168.1.143\\OAMANAGEDB;Use Procedure for Prepare=1;Auto Translate=True;Packet Size=4096;Workstation ID=localhost;Use Encryption for Data=False;Tag with column collation when possible=False";//Integrated Security=SSPI;Packet Size=4096;Data Source=OAMANAGE\OAMANAGEDB;Tag with column collation when possible=False;Initial Catalog=OAManage;Use Procedure for Prepare=1;Auto Translate=True;Persist Security Info=False;Provider=""SQLOLEDB.1"";Workstation ID=localhost;Use Encryption for Data=False";
		public static bool m_bSavePwd;//��Ҫʹ���ϴα��������

        public static int iAppStyle = 0;//Ӧ�����ͣ�0�ֲģ�1
        public static string WindowTitle = "�ϲ˼���ϵͳ";
        public static int iFormStyle = 0;
        public static int PageRowCount = 22;
        public static string NumberFormat;
        public static string MoneyFormat;
        public static int iSortStyle = 0;

        public static string Idiograph = "";
        public static string RegisterUser = "";
        public static string RegisterCode = "";
        public static bool bRegister = false;
        public static CWindowStyleMan WindowStyleMan = new CWindowStyleMan(System.Windows.Forms.Application.StartupPath + "\\Option.dat");

        public CAppOption()
		{
			//��ע����������
			LoadData();
            
		}

		 ~CAppOption()
		{
			//��������
			SaveData();
            
		}

		/// <summary>
		/// ��������
		/// </summary>
		public static void LoadData()
		{
            string szOptionFile;
			szOptionFile =System.Windows.Forms.Application.StartupPath+"\\Option.dat";
            if (System.IO.File.Exists(szOptionFile))
            {
                System.Text.StringBuilder szValue = new System.Text.StringBuilder("",256);                

                int iRet ;
                iRet=PlatformInvokeKernel32.GetPrivateProfileString("App", "AppStyle", iAppStyle.ToString(), szValue, 256, szOptionFile);
                try
                {
                    iAppStyle = Convert.ToInt32(szValue.ToString());
                }
                catch
                {
                }

                iRet = PlatformInvokeKernel32.GetPrivateProfileString("Windows", "Style", iFormStyle.ToString(), szValue, 256, szOptionFile);

                try
                {
                    iFormStyle = Convert.ToInt32(szValue.ToString());
                }
                catch
                {
                }
                iRet = PlatformInvokeKernel32.GetPrivateProfileString("Windows", "SortStyle", iSortStyle.ToString(), szValue, 256, szOptionFile);

                try
                {
                    iSortStyle = Convert.ToInt32(szValue.ToString());
                }
                catch
                {
                }
                iRet = PlatformInvokeKernel32.GetPrivateProfileString("Windows", "NumberFormat", NumberFormat, szValue, 256, szOptionFile);

                NumberFormat = szValue.ToString();
                if (NumberFormat == "")
                {
                    NumberFormat = "0.00";
                }

                iRet = PlatformInvokeKernel32.GetPrivateProfileString("Windows", "MoneyFormat", MoneyFormat, szValue, 256, szOptionFile);

                MoneyFormat = szValue.ToString();
                if (MoneyFormat == "")
                {
                    MoneyFormat = "0.00";
                }

                iRet = PlatformInvokeKernel32.GetPrivateProfileString("Windows", "PageRowCount", PageRowCount.ToString(), szValue, 256, szOptionFile);

                try
                {
                    PageRowCount = Convert.ToInt32(szValue.ToString());
                    if (PageRowCount < 10)
                    {
                        PageRowCount = 22;
                    }
                }
                catch
                {
                    PageRowCount = 22;
                }

                iRet = PlatformInvokeKernel32.GetPrivateProfileString("App", "Idiograph", "", szValue, 256, szOptionFile);
                Idiograph = szValue.ToString();
                
                //ע����
                iRet = PlatformInvokeKernel32.GetPrivateProfileString("Register", "Code", "", szValue, 256, szOptionFile);
                RegisterCode = szValue.ToString();

                iRet = PlatformInvokeKernel32.GetPrivateProfileString("Register", "User", "", szValue, 256, szOptionFile);
                RegisterUser = szValue.ToString();

                string[] MacList=CGeneralFunction.GetMac();
                ChefMonitor.CGlobalInstance.Instance.FileLogger.WriteLog(YyLogger.LogSeverity.info, "���ü���", string.Format("Mac:{0}", MacList[0]));

                if (MacList.Length > 0)
                {
                    SimpleScrypt SScrypt = new SimpleScrypt();
                    if (SScrypt.Encrypt(MacList[0]) == RegisterCode )
                    {
                        bRegister = true;
                    }
                    if (!string.IsNullOrEmpty(RegisterUser) && SScrypt.Encrypt(RegisterUser) == RegisterCode)
                    {
                        bRegister = true;
                    }
#if DEBUG
                    if (bRegister==false)
                    {
                        ChefMonitor.CGlobalInstance.Instance.FileLogger.WriteLog(YyLogger.LogSeverity.info, "���ü���", string.Format("RegisterCode:{0}", SScrypt.Encrypt(MacList[0])));
                        ChefMonitor.CGlobalInstance.Instance.FileLogger.WriteLog(YyLogger.LogSeverity.info, "���ü���", string.Format("RegisterCode:{0}", SScrypt.Encrypt(RegisterUser)));
                    }
#endif
                }
            }
            WindowStyleMan.LoadData();
            

			RegistryKey oRegSoft;
			oRegSoft=Registry.LocalMachine.OpenSubKey("SOFTWARE",true);
			if(oRegSoft==null)
			{
				//����
				oRegSoft=Registry.LocalMachine.CreateSubKey("SOFTWARE");
			}

			RegistryKey oRegTemp=oRegSoft.OpenSubKey(Application.ProductName,true);
			if(oRegTemp==null)
			{
				oRegTemp=oRegSoft.CreateSubKey(Application.ProductName);
			}
			m_sUser=(string)oRegTemp.GetValue("User","Admin");

            m_sPwd = "";//20090110�޸ģ�����
			//m_sPwd=Decrypt((string)oRegTemp.GetValue("Pwd",""));

            m_sDbConnectString = CYyScrypt.Decrypt((string)oRegTemp.GetValue("DbConnect", CYyScrypt.Encrypt(m_sDbConnectString)));
			m_bSavePwd=Convert.ToBoolean(oRegTemp.GetValue("SavePwd",true));
		
		}

		/// <summary>
		/// ��������
		/// </summary>
		public static void SaveData()
		{
            string szOptionFile;
            szOptionFile = System.Windows.Forms.Application.StartupPath + "\\Option.dat";

            
            int iRet=0;
            iRet = PlatformInvokeKernel32.WritePrivateProfileString("App", "AppStyle", iAppStyle.ToString(), szOptionFile);
            iRet = PlatformInvokeKernel32.WritePrivateProfileString("App", "Idiograph", Idiograph,  szOptionFile);


            iRet = PlatformInvokeKernel32.WritePrivateProfileString("Windows", "Style", iFormStyle.ToString(), szOptionFile);
            iRet = PlatformInvokeKernel32.WritePrivateProfileString("Windows", "SortStyle", iSortStyle.ToString(), szOptionFile);
            iRet = PlatformInvokeKernel32.WritePrivateProfileString("Windows", "NumberFormat", NumberFormat, szOptionFile);
            iRet = PlatformInvokeKernel32.WritePrivateProfileString("Windows", "MoneyFormat", MoneyFormat, szOptionFile);
            iRet = PlatformInvokeKernel32.WritePrivateProfileString("Windows", "PageRowCount", PageRowCount.ToString(), szOptionFile);

            WindowStyleMan.SaveData();

			RegistryKey oRegSoft;
			oRegSoft=Registry.LocalMachine.OpenSubKey("SOFTWARE",true);
			if(oRegSoft==null)
			{
				//����
				oRegSoft=Registry.LocalMachine.CreateSubKey("SOFTWARE");
			}

			RegistryKey oRegTemp=oRegSoft.OpenSubKey(Application.ProductName,true);
			if(oRegTemp==null)
			{
				oRegTemp=oRegSoft.CreateSubKey(Application.ProductName);
			}
            oRegTemp.SetValue("DbConnect", CYyScrypt.Encrypt(m_sDbConnectString));

			oRegTemp.SetValue("User",m_sUser);
            oRegTemp.SetValue("Pwd", CYyScrypt.Encrypt(m_sPwd));            
			oRegTemp.SetValue("SavePwd",m_bSavePwd);
			oRegTemp.Flush();
		}
				
	}
}
