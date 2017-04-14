using System;
using System.Collections.Generic;
using System.Text;

namespace ChefMonitor
{
    public class CSortStyle
    {
        static public string GetSort(int iSortStyle)
        {
            /*
             �ͳ�ʱ��
             * ����
             * ����
             * �µ�ʱ��
             * ����,�ͳ�ʱ��
             * ����,�µ�ʱ��
             * ̨��,�ͳ�ʱ��
             * ̨��,�µ�ʱ��
             * ����,̨��,�ͳ�ʱ��
             * ����,̨��,�µ�ʱ��
             * ����,̨��,����ʱ��
             */
            switch (iSortStyle)
            {
                case 0:
                    return "OrderTime";//�ͳ�ʱ��
                    break;
                case 1:
                    return "GoodsName";//����
                    break;
                case 2:
                    return "Type";//����
                case 3:
                    return "RecordTime";//�µ�ʱ��
                case 4:
                    return "DeptName,OrderTime";//����,�ͳ�ʱ��
                case 5:
                    return "DeptName,RecordTime";//����,�µ�ʱ��
                case 6:
                    return "TableName,OrderTime"; //̨��,�ͳ�ʱ��   
                case 7:
                    return "TableName,RecordTime"; //̨��,�µ�ʱ�� 
                case 8:
                    return "DeptName,TableName,OrderTime";//����,̨��,�ͳ�ʱ��   
                case 9:
                    return "DeptName,TableName,RecordTime";//����,̨��,�µ�ʱ�� 
                case 10:
                    return "DeptName,TableName,ServingTime";//����,̨��,����ʱ��
                default:
                    return "OrderTime";
            }
        }
    }
}
