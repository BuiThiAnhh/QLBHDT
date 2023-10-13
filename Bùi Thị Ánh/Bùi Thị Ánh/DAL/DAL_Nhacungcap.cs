using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Nhacungcap: DBConnect
    {
        DBConnect connect = new DBConnect();
        public DataTable getData()
        {
            return connect.getData("Select * from NhaCungCap");
        }

    }
}
