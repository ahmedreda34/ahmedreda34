using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace WebApplication4
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            /*string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
             using (SqlConnection con = new SqlConnection(cs))
             {

                 SqlCommand cmd = new SqlCommand();
                 cmd.Connection=con;
                 con.Open();

                 cmd.CommandText = "delete from firstt where id =4";
                  int totalrowaffect = cmd.ExecuteNonQuery();
                 Response.Write(" total delete=" + totalrowaffect.ToString()+"<br/>");
                 cmd.CommandText = "insert into firstt values (4 ,'zena','banhaa') ";
                  totalrowaffect = cmd.ExecuteNonQuery();
                 Response.Write(" total insert=" + totalrowaffect.ToString()+"<br/>");
                 cmd.CommandText = "update firstt set namee ='reda' where id = 5";
                 totalrowaffect = cmd.ExecuteNonQuery();
                 Response.Write(" total update=" + totalrowaffect.ToString()+"<br/>");
             }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using
                (SqlConnection con = new SqlConnection(cs))
            {
                
                SqlCommand cmd = new SqlCommand("spname", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@namee" , TextBox1.Text + "%");
                con.Open();
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using(SqlConnection con =new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("empol", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue ("@name", TextBox2.Text);
                cmd.Parameters.AddWithValue("@gender", DropDownList1.SelectedValue);
                cmd.Parameters.AddWithValue("@salary", TextBox1.Text);
                SqlParameter outputparameter = new SqlParameter();
                outputparameter.ParameterName="employeeid";
                outputparameter.SqlDbType = System.Data.SqlDbType.Int;
                outputparameter.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(outputparameter);
                con.Open();
                cmd.ExecuteNonQuery();
                string empid = outputparameter.Value.ToString();
                Label4.Text = "employee_id =" + empid;
            }
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using(SqlConnection con =new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("select * from product;select*from categorie", con);
                con.Open();
                using(SqlDataReader sdr = cmd.ExecuteReader()) {
                    GridView1.DataSource = sdr;
                    GridView1.DataBind();
                    while (sdr.NextResult())
                    {
                        GridView2.DataSource = sdr;
                        GridView2.DataBind();
                    }

                }
            }
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using(SqlConnection con =new SqlConnection(cs))
            {
                SqlDataAdapter sq = new SqlDataAdapter("spcatr", con);
                sq.SelectCommand.CommandType = CommandType.StoredProcedure;
                sq.SelectCommand.Parameters.AddWithValue("@catid", TextBox1.Text);
                DataSet ds = new DataSet();
                sq.Fill(ds);
                GridView1.DataSource=(ds);
                GridView1.DataBind();

            }
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter sq = new SqlDataAdapter("spget", con);
                sq.SelectCommand.CommandType = CommandType.StoredProcedure;
                
                DataSet ds = new DataSet();
                sq.Fill(ds);
                ds.Tables[0].TableName = "pr";
                ds.Tables[1].TableName = "ct";
                GridView1.DataSource = ds.Tables["pr"];
                GridView1.DataBind();
                GridView2.DataSource = ds.Tables["ct"];
                
                GridView2.DataBind();

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Cache["data"] == null)
            {
                string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlDataAdapter da = new SqlDataAdapter("select * from categorie", con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    Cache["data"] = ds;
                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                }
                Label1.Text = "data loaded from database";

            }
            else
            {
                GridView1.DataSource = (DataSet)Cache["data"];
                GridView1.DataBind();
                Label1.Text = "data loaded from cache";
            };
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                string sql_query = "select * from student where id =" + TextBox1.Text;
                SqlDataAdapter da = new SqlDataAdapter(sql_query, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "student");
                ViewState["sqlquery"] = sql_query;
                ViewState["dataset"] = ds;
                if (ds.Tables["student"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["student"].Rows[0];
                    TextBox2.Text = dr["name"].ToString();

                    TextBox3.Text = dr["totalmarks"].ToString();
                    DropDownList1.SelectedValue = dr["gender"].ToString();

                }
                else
                {
                    Label5.ForeColor = System.Drawing.Color.Red;
                    Label5.Text = "no student recorded with id=" + TextBox1.Text;
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlDataAdapter da = new SqlDataAdapter((string)ViewState["sqlquery"], con);
                SqlCommandBuilder build = new SqlCommandBuilder(da);
                DataSet ds = (DataSet)ViewState["dataset"];
                if (ds.Tables["student"].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables["student"].Rows[0];
                    dr["name"] = TextBox2.Text;
                    dr["gender"] = DropDownList1.SelectedValue;
                    dr["totalmarks"] = TextBox3.Text;
                }
                int rowupdate = da.Update(ds, "student");
                if (rowupdate > 0)
                {
                    Label5.ForeColor = System.Drawing.Color.Red;
                    Label5.Text = rowupdate.ToString()+"rows update";
                }
                else
                {
                    Label5.ForeColor = System.Drawing.Color.Green;
                    Label5.Text = "no rows to update";
                }
            }
            }*/
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con =new SqlConnection(cs))
            {
                string strquery = "select * from student";
                SqlDataAdapter da = new SqlDataAdapter(strquery, con);
                DataSet ds = new DataSet();
                da.Fill(ds,"student");
                ds.Tables["student"].PrimaryKey = new DataColumn[] { ds.Tables["student"].Columns["id"] };
                Cache.Insert("DATASET", ds,null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                GridView1.DataSource = ds;
                GridView1.DataBind();
                Label1.Text = "data loaded from database";
            }
            
            
        }private void getdata()
        {
            if (Cache["DATASET"] != null)
            {
                DataSet ds = (DataSet)Cache["DATASET"];
                GridView1.DataSource = ds;
                GridView1.DataBind();
                
            }
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            getdata();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if(Cache["DATASET"] != null)
            {
                DataSet ds = (DataSet)Cache["DATASET"];
                DataRow dr = ds.Tables["student"].Rows.Find(e.Keys["id"]);
                dr["name"] = e.NewValues["name"];
                dr["gender"] = e.NewValues["gender"];
                dr["totalmarks"] = e.NewValues["totalmarks"];
                Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                GridView1.EditIndex = -1;
                getdata();
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Cache["DATASET"] != null)
            {
                DataSet ds = (DataSet)Cache["DATASET"];
                DataRow dr = ds.Tables["student"].Rows.Find(e.Keys["id"]);
                dr.Delete();
                Cache.Insert("DATASET", ds, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
              
                getdata();
            }
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            getdata();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            
                string sqlquery = "select * from student";
                SqlDataAdapter da = new SqlDataAdapter(sqlquery, con);
                DataSet ds = (DataSet)Cache["DATASET"];
                
                string cmmd = "update student set name =@name ,gender=@gender,totalmarks=@totalmarks where id =@id";
                SqlCommand updatecommand = new SqlCommand(cmmd, con);
                updatecommand.Parameters.Add("@name", SqlDbType.NVarChar, 50,"name");
                updatecommand.Parameters.Add("@gender", SqlDbType.NVarChar, 20,"gender");
                updatecommand.Parameters.Add("@totalmarks",SqlDbType.Int,0,"totalmarks");
                updatecommand.Parameters.Add("@id", SqlDbType.Int, 0, "id");
            da.UpdateCommand = updatecommand ;
                string deletecommand = "delete from student where id =@id";
                SqlCommand deletecommands = new SqlCommand(deletecommand, con);
                deletecommands.Parameters.Add("@id", SqlDbType.Int, 0, "id");
                da.DeleteCommand = deletecommands;  
                da.Update(ds, "student");
                Label1.Text = "data updated to database";
            
        }

       
    }
}

