using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Restaurant
{
    public class LptPrinter : IDisposable
    {
        const int OPEN_EXISTING = 3;

        string m_portName = "LPT1";
        //Microsoft.Win32.SafeHandles.SafeFileHandle iPrintHandle;
        IntPtr iPrintHandle = (IntPtr)( - 1);

        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            int Internal;
            int InternalHigh;
            int Offset;
            int OffSetHigh;
            int hEvent;
        } 

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFile(string lpFileName,
        int dwDesiredAccess,
        int dwShareMode,
        int lpSecurityAttributes,
        int dwCreationDisposition,
        int dwFlagsAndAttributes,
        int hTemplateFile);

        [DllImport("kernel32.dll")]
        private static extern bool WriteFile(
          int hFile,
          byte[] lpBuffer,
          int nNumberOfBytesToWrite,
          ref���� int lpNumberOfBytesWritten,
          ref���� OVERLAPPED lpOverlapped
          );
        [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(
          int hObject
          );
        [DllImport("kernel32.dll")]
        private static extern bool ReadFile(
           int hFile,
           out byte[] lpBuffer,
           int nNumberOfBytesToRead,
           ref int lpNumberOfBytesRead,
          ref OVERLAPPED lpOverlapped
         );

        public LptPrinter(string prnPort)
        {
            m_portName = prnPort;
        }
        public string PortName
        {
            get
            {
                return m_portName;
            }
        }
        public void Dispose()
        {
            this.Close();
        }

        public bool IsOpen
        {
            get
            {
                if (iPrintHandle == null || iPrintHandle == ((IntPtr)(-1)))
                {
                    //�򿪴��� IntPtr iHandle = CreateFile(prnPort, 0x40000000, 0, 0, OPEN_EXISTING, 0, 0); 

                    IntPtr iHandle = CreateFile(m_portName, 0x50000000, 0, 0, OPEN_EXISTING, 0, 0);
                    if (iHandle.ToInt32() != -1)
                    {
                        iPrintHandle = iHandle;
                    }
                }

                if (iPrintHandle != null && iPrintHandle != ((IntPtr)(-1)))
                    return true;
                else
                    return false;
            }
        }
        
        public void Close()
        {
            if (iPrintHandle != null && iPrintHandle != ((IntPtr)(-1)))
            {
                CloseHandle((int)iPrintHandle);
            }
        }
        void Open()
        {
            if (iPrintHandle == null|| iPrintHandle == ((IntPtr)(-1)))
            {
                //�򿪴��� IntPtr iHandle = CreateFile(prnPort, 0x40000000, 0, 0, OPEN_EXISTING, 0, 0); 

                IntPtr iHandle = CreateFile(m_portName, 0x50000000, 0, 0, OPEN_EXISTING, 0, 0);
                if (iHandle.ToInt32() != -1)
                {
                    iPrintHandle = iHandle;
                }
            }
        }
        public void DisplayMessage(string Msg)
        {
            Open();

            if (iPrintHandle == null || iPrintHandle == ((IntPtr)(-1))) return;

            FileStream fs = new FileStream(iPrintHandle, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);

            //��Ǯ�� Chr(27)+Chr(112)+Chr(0)+Chr(25)+Chr(255);{���ƴ�ӡ����Ǯ��ָ��}
            sw.Write(((char)31).ToString() + ((char)16).ToString() + ((char)1).ToString() + Msg);
            sw.Close();
            fs.Close();

            Close();
        }
        /// <summary>
        /// 
        /// </summary>
        public void OpenCashbox()
        {
            //^m       ������ֽ
            //^i       ��ȫ��ֽ
            //^p       ������Ǯ���ź� 
            //^r       �л���ӡ��ɫΪ��ɫ
            //^b       �л���ӡ��ɫΪ��ɫ
            //����е�^Ϊ�ɴ�ӡ�ַ�,���ǿ����ַ��ı�� 

            //��lpt1���������ַ�����70����m,i,p,r,b���档
            //1B   70   00   15   20   00   00   0D   0A 

            if (iPrintHandle == null||iPrintHandle == ((IntPtr)(-1))) return;

            //Microsoft.Win32.SafeHandles.SafeFileHandle PrintHandle;

            FileStream fs = new FileStream(iPrintHandle, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);

            //��Ǯ�� Chr(27)+Chr(112)+Chr(0)+Chr(25)+Chr(255);{���ƴ�ӡ����Ǯ��ָ��}
            sw.Write(((char)27).ToString() + "p" + ((char)0).ToString() + ((char)60).ToString() + ((char)255).ToString());
            sw.Close();
            fs.Close();

            Close();
        }
    }
}
