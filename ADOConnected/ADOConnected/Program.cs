using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOConnected
{
    class teach
    { }
    abstract class pp
    {
      public   void nonn()
        {
            Console.WriteLine(  "non static");
        }
      public   static void mm()
        {
            Console.WriteLine("Static");
        }
    }
    class Program:pp
    {
       // SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=SSPI;Initial Catalog=Library;");
            
        SqlConnection con = new SqlConnection(
            ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        SqlCommand cmd;

        static Dictionary<string, DateTime> flightSchedule = new Dictionary<string, DateTime>()
        {
            {"111A",Convert.ToDateTime("13:54:10") },
             {"222B",Convert.ToDateTime("11:30:10") },
              {"333C",Convert.ToDateTime("21:50:30") }

        };
      

        public void Display()
        {
            nonn();
            mm();
            con.Open();
            //LinqToSQLDataContext db = new LinqToSQLDataContext(connectString);
            cmd = new SqlCommand("sel_books", con);
            cmd.CommandType = CommandType.StoredProcedure;
        
            SqlDataReader   dr = cmd.ExecuteReader();
            Console.WriteLine(dr.FieldCount );
            //for (int i = 0;i<dr.FieldCount;i++)
                while (dr.Read())
            {
                Console.Write(dr[0].ToString()+"\t");
                Console.Write(dr[1].ToString() + "\t");
                Console.Write(dr[2].ToString() + "\t");
                Console.Write(dr[3].ToString());

                Console.WriteLine();
            }
            dr.Close();
            con.Close();
        }


        protected void Binsert_Click(int bid,string bknm,int bpri,int bqty)
        {      
            con.Open();
            cmd= new SqlCommand("book_insert",con);
             cmd.Connection = con;               
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter id = new SqlParameter();
            id.ParameterName = "@id";
            id.Value = bid;
            
            SqlParameter bnm = new SqlParameter();
            bnm.ParameterName = "@bknm";   
            bnm.Value = bknm;           

            SqlParameter pri = new SqlParameter();
            pri.ParameterName = "@pri"; 
            pri.Value = bpri;
           
            SqlParameter qty = new SqlParameter();
            //qty.ParameterName = "@qty"; 
            //qty.Value = bqty;
            cmd.Parameters.Add("@qty", SqlDbType.Int);
            cmd.Parameters["@qty"].Value = bqty;

            cmd.Parameters.Add(id);
            cmd.Parameters.Add(bnm);
            cmd.Parameters.Add(pri);
            //cmd.Parameters.Add(qty);

            //cmd.CommandText = "INSERT INTO book VALUES (@id,@bnm,@pri,@qty)";
            
            cmd.ExecuteNonQuery();
            Console.WriteLine("One record inserted:");
            con.Close();
            Display();
        }

        protected void Bdelete_Click()
        {
            con.Open();
            Console.WriteLine("Enter Id");
            int id = Convert.ToInt32(Console.ReadLine());
            cmd = new SqlCommand("DELETE FROM book where bookid='" + id + "'", con);
            cmd.ExecuteNonQuery();
            Console.WriteLine("one record Delete:");
            con.Close();
            Display();
        }
        // ' " +bnm+  "'
        protected void Bupdate_Click()
        {
            con.Open();
            Console.WriteLine("Enter Id of book u want to update");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter book nm that needs change");
            string bm = Console.ReadLine();
            Console.WriteLine("Update the price of book");
                int pr=int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new qty");
            int qty = int.Parse(Console.ReadLine());           

            //update book set booknm="sddf",qty=30 where bookid=100

            string abc = "UPDATE book SET booknm ='" + bm + "',price='"+pr+"',qty='"+qty+"' " +
                "WHERE bookid = '" + id + "'";
            SqlCommand cmd = new SqlCommand(abc , con);
            cmd.ExecuteNonQuery();
            Console.WriteLine("one record updated:");
            con.Close();
            Display();
        }

        protected void BDisplay_Click()
        {
            Display();
        }
        
        protected void Bsearch_Click()
        {      
                   
            con.Open();
            Console.WriteLine("Enter Book ID");
            int aa = Convert.ToInt32(Console.ReadLine());
            string abc = "SELECT * FROM book where bookid='" + aa + "'";
            cmd = new SqlCommand(abc, con);
            Console.WriteLine("one record found:");
         SqlDataReader   dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Console.Write(dr[0].ToString() + "\t");
                Console.Write(dr[1].ToString() + "\t");
                Console.Write(dr[2].ToString() + "\t");
                Console.Write(dr[3].ToString());
                Console.WriteLine();
            }
            dr.Close();
            con.Close();
        }

        protected void BTotalRec_Click()
        {
          
            con.Open();
            cmd = new SqlCommand("select Count(*) from book", con);
            int a = (int)cmd.ExecuteScalar();
            Console.WriteLine("Total Record:--> " + a.ToString());
            con.Close();
        }

        public static string flightstatus(string flightno)
        {
            
            DateTime currtm = DateTime.Now;
            foreach (KeyValuePair<string, DateTime> de in flightSchedule)
            {
               
                if (flightno ==de.Key )
                {
                    
                    if(currtm <de.Value )
                    {
                        return "Time to Flight "+(de.Value-currtm).ToString() ;
                    }
                    else
                    {
                        return "flight left";
                    }
                }
                else
                {
                    return "Flight not found";
                }
            }
            return "";
        }
        static void Main(string[] args)
        {
            
            
            Program p = new Program();
           /* Console.WriteLine("Enter BookId");
            int bid = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Bookname");
            string bknm = Console.ReadLine();

            Console.WriteLine("Enter Book price");
            int bpri = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Book Qty");
            int bqty = int.Parse(Console.ReadLine());*/
            p.Display();

           // p.Binsert_Click(bid,bknm,bpri,bqty);


           /* p.Bupdate_Click();
            p.Bdelete_Click();*/
            //p.BTotalRec_Click();
            Console.ReadKey();
        }
    }
}
