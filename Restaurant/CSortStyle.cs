using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant
{
    public class CSortStyle
    {
        static public string GetSort(int iSortStyle)
        {
            /*
             ̨��
             * ̨��
             * ����
             * ̨��
             * ̨ID
             
             */
            switch (iSortStyle)
            {
                case 0:
                    return "TableNO";//̨��
                case 1:
                    return "TableName";//̨��
                case 2:
                    return "DeptName";//����
                case 3:
                    return "TableClassifyID";//̨��
                case 4:
                    return "DiningTableID";//̨ID
                default:
                    return "DiningTableID";
            }
        }
    }
}
