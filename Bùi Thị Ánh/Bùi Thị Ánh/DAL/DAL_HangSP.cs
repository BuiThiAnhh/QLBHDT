using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Entities;
namespace DAL
{
    public class DAL_HangSP
    {

        DBConnect connect = new DBConnect();
        public DataTable getData()
        {
            return connect.getData("Select * from HangSanPham");
        }
    }
}
