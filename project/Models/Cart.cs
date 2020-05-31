using project.Forms;
using project.Forms.FormChildsOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project.Models
{
    class Cart
    {
      
        DataClasses1DataContext db = new DataClasses1DataContext();
        public order getTableId(int id)
        {
            order or = null;
            var order = from o in db.orders
                        where o.table_id == id
                        where o.status == 0
                        select o;
            foreach (var item in order)
            {
                db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.orders);
                or = new order();
                or.id = item.id;
                or.table_id = item.table_id;
                or.create_at = item.create_at;
                or.discount = item.discount;
                or.emp_id = item.id;
                or.total_price = item.total_price;
                or.status = item.status;

            }
            return or;
        }
        public int getOrderId(int table_id)
        {
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.orders);
            int id = 0;
            var sql = from o in db.orders
                        where o.table_id == table_id
                        where o.status == 0
                        select o;
            if (sql.Count() > 0)
            {
                foreach (var item in sql)
                {
                    id = item.id;
                }
            }
            return id;
        }
     public List<order_detail> getDsOrder(int order_id)
        {
           
            List<order_detail> lsDetails= new List<order_detail>();
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.order_details);
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.products);
            var sql = from d in db.order_details
                      join p in db.products on d.pro_id equals p.id
                      where d.order_id == order_id
                      select new
                      {
                          pro_id = d.pro_id,
                          order_id = d.order_id,
                          quantity = d.quantity,
                          name = p.name,
                          price = p.price
                      };
            if(sql.Count()>0) {
            foreach (var item in sql)
            {
                order_detail order = new order_detail();

                order.pro_id = item.pro_id;
                order.order_id = item.order_id;
                order.price = item.price;
                order.quantity = item.quantity;
                lsDetails.Add(order);
            }
            }
            return lsDetails;

        }
        public int CheckLsProduct(int order_id, int table_id)
        {
            db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, db.orders);
            int n = 0;
            var sql = from o in db.orders
                      join d in db.order_details on o.id equals d.order_id
                      where o.table_id == table_id
                      where o.status == 0
                      where d.order_id == order_id
                      select new { d.pro_id } ;
            if(sql != null) { 
            foreach (var item in sql)
            {
                n++;
            }
            }
            return n;
        }
        public int deletePro(int pro_id, int id, int table_id)
        {
            int check = 0;
          
                var sql = from d in db.order_details
                          where d.pro_id == pro_id
                          where d.order_id == id
                          select d;
         
                foreach (var item in sql)
                {
               
                db.order_details.DeleteOnSubmit(item);
                       
            }
            try
            {
                db.SubmitChanges();
                check = 1;
            }
            catch (Exception)
            {

                throw;
            }
           

            if (CheckLsProduct(id,table_id) == 0)
                {
                    check = 2;
                }
          
            
            return check;
          
        }
        public void CancelOrder(int order_id)
        {
            
                var sql = from o in db.orders
                          where o.id == order_id
                          select o;
                foreach (var item in sql)
                {
                    db.orders.DeleteOnSubmit(item);
                }
            try
            {
                db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }
      
       
    }
}
