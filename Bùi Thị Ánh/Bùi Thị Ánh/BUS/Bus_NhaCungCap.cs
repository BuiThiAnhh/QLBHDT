using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class Bus_NhaCungCap
    {
        DAL_Nhacungcap dal_ncc = new DAL_Nhacungcap();
        public DataTable getdata()
        {
            return dal_ncc.getData();
        }
    }
}
