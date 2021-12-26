using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace School
{
    public class DataLib
    {
        public DBContext db = new DBContext();
        public static string cs = "Data Source = (localdb)\\ProjectsV13; Initial Catalog = Lis; Integrated Security = True; Connect Timeout = 30; Encrypt = False;";
        //public static string cs = System.Configuration.ConfigurationManager.ConnectionStrings["byEntity"].ConnectionString;
        //string c = db.Database.GetDbConnection().ConnectionString;
        //public static bool IsDuplicate(string table, string key, string value)
        //{
        //    string query;
        //    // for Check Duplicate Records
        //    query = "Select * from " + table + " where " + key + " ='" + value.Trim() + "'";
        //    SqlConnection Con = new SqlConnection(cs);
        //    Con.Open();
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter Adp = new SqlDataAdapter(query, Con);
        //    Adp.Fill(dt);
        //    Con.Close();
        //    if (dt.Rows.Count > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public static bool IsUsed(int value, string table, string key)
        //{
        //    string query;
        //    // for use in deponding table 
        //    query = "Select " + key + " from " + table + " where " + key + " =" + value;
        //    SqlConnection Con = new SqlConnection(cs);
        //    Con.Open();
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter Adp = new SqlDataAdapter(query, Con);
        //    Adp.Fill(dt);
        //    Con.Close();
        //    if (dt.Rows.Count > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}        
        //public static string GetCellItems(string Query)
        //{
        //    SqlConnection Con = new SqlConnection(cs);
        //    Con.Open();
        //    SqlDataAdapter Adp = new SqlDataAdapter(Query, Con);
        //    DataTable t = new DataTable();
        //    Adp.Fill(t);
        //    Con.Close();
        //    if (t.Rows.Count > 0)
        //    {
        //        return t.Rows[0].ItemArray[0].ToString();
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
        //public static DataTable GetQueryTable(string Query)
        //{
        //    SqlConnection Con = new SqlConnection(cs);
        //    Con.Open();
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter Adp = new SqlDataAdapter(Query, Con);
        //    Adp.Fill(dt);
        //    Con.Close();
        //    return dt;
        //}

        //public static DataRow GetQueryRow(string Query)
        //{
        //    SqlConnection Con = new SqlConnection(cs);
        //    Con.Open();
        //    SqlDataAdapter Adp = new SqlDataAdapter(Query, Con);
        //    DataTable t = new DataTable();
        //    Adp.Fill(t);
        //    DataRow dr = t.Rows[0];
        //    return dr;
        //}
        //public static List<string> GetQueryList(string Query)
        //{
        //    SqlConnection Con = new SqlConnection(cs);
        //    Con.Open();
        //    SqlDataAdapter Adp = new SqlDataAdapter(Query, Con);
        //    DataTable t = new DataTable();
        //    Adp.Fill(t);
        //    List<string> list = new List<string>();
        //    for (int i = 0; i < t.Columns.Count; i++)
        //    {
        //        list.Add(t.Rows[0].ItemArray[i].ToString());
        //    }
        //    t.Dispose();
        //    Con.Close();
        //    return list;
        //}
        //public static DataTable GetQueryTableWithSchema(string Query)
        //{
        //    SqlConnection Con = new SqlConnection(cs);           
        //    SqlCommand cmd = new SqlCommand(Query, Con);
        //    Con.Open();
        //    //
        //    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    DataTable dtSchema = dr.GetSchemaTable();
        //    DataTable dt = new DataTable();
        //    // You can also use an ArrayList instead of List<> 
        //    List<DataColumn> listCols = new List<DataColumn>();
        //    if (dtSchema != null)
        //    {
        //        foreach (DataRow drow in dtSchema.Rows)
        //        {
        //            string columnName = System.Convert.ToString(drow["ColumnName"]);
        //            DataColumn column = new DataColumn(columnName, (Type)(drow["DataType"]));
        //            column.Unique = (bool)drow["IsUnique"];
        //            column.AllowDBNull = (bool)drow["AllowDBNull"];
        //            column.AutoIncrement = (bool)drow["IsAutoIncrement"];
        //            listCols.Add(column);
        //            dt.Columns.Add(column);
        //        }

        //    }

        //    // Read rows from DataReader and populate the DataTable 

        //    while (dr.Read())
        //    {
        //        DataRow dataRow = dt.NewRow();
        //        for (int i = 0; i < listCols.Count; i++)
        //        {
        //            dataRow[((DataColumn)listCols[i])] = dr[i];
        //        }

        //        dt.Rows.Add(dataRow);
        //    }
        //    return dt;

        //}
        //public static DataTable GetQueryTable(string Query, string ConnectiongString)
        //{
        //    SqlConnection Con = new SqlConnection(ConnectiongString);
        //    Con.Open();
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter Adp = new SqlDataAdapter(Query, Con);
        //    Adp.Fill(dt);
        //    Con.Close();
        //    return dt;
        //}
        //public static IHtmlString DropDownWithID(string ID,string Query)
        //{
        //    DataTable dt = DataLib.GetQueryTable(Query);
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<select id = '"+ ID +"' name = '"+ ID + "' class='form-control'>");
        //    sb.Append("<option value = '0'>Select</option>");
        //    if (dt.Rows.Count>0)
        //    {
        //        for(int i=0;i<dt.Rows.Count;i++)
        //        {
        //            sb.Append("<option value = " + dt.Rows[i].ItemArray[0].ToString() + " >" + dt.Rows[i].ItemArray[1].ToString() + "</option>");
        //        }
        //    }
        //    sb.Append("</select>");
        //    var str = new HtmlString(sb.ToString());
        //    return new MvcHtmlString(str.ToHtmlString());
        //}
        //public static IHtmlString QueryDataTableToHTMLTable(string Query)
        //{
        //    //
        //   // DataTable dt = new DataTable();
        //    DataTable dt = DataLib.GetQueryTableWithSchema(Query);            
        //    //
        //    StringBuilder sb = new StringBuilder();          
        //    sb.Append("<div class='dt-responsive table-responsive'>");
        //    sb.Append("<table id = 'cbtn-selectors' class='table table-striped table-bordered nowrap'>");
        //    sb.Append("<thead>");
        //    sb.Append("<tr>");
        //    if (dt.Columns.Count > 0)
        //    {
        //        foreach (DataColumn column in dt.Columns)
        //        {
        //            sb.Append("<th>" + column.ColumnName + "</th>");

        //        }
        //    }
        //    sb.Append("</tr>");
        //    sb.Append("</thead>");
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            sb.Append("<tr>");
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //               // string t = dt.Columns[j].DataType.Name.ToString();
        //                try
        //                {
        //                    int len = dt.Rows[i].ItemArray[j].ToString().Length;
        //                    string str1 = dt.Rows[i].ItemArray[j].ToString().Replace("₹","").Trim();
        //                    if (Convert.ToDecimal(str1) >0)
        //                    {
        //                        sb.Append("<td class='text-right'>" + dt.Rows[i].ItemArray[j].ToString() + "</td>");
        //                    }
        //                    else
        //                    {
        //                        sb.Append("<td class='text-left'>" + dt.Rows[i].ItemArray[j].ToString() + "</td>");
        //                    }
        //                }
        //                catch
        //                {
        //                    sb.Append("<td class='text-left'>" + dt.Rows[i].ItemArray[j].ToString() + "</td>");
        //                }
        //            }
        //            sb.Append("</tr>");

        //        }
        //    }
        //    sb.Append("<tbody>");
        //    sb.Append("</tbody>");
        //    sb.Append("</table>");
        //    sb.Append("</div");

        //    var str = new HtmlString(sb.ToString());
        //    return new MvcHtmlString(str.ToHtmlString());
        //}
        //public static IHtmlString QueryDataTableToHTMLTableWithButton(string Query,string Controller)
        //{
        //    //
        //    DataTable dt = new DataTable();
        //    dt = DataLib.GetQueryTable(Query);
        //    //
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<div class='dt-responsive table-responsive'>");
        //    sb.Append("<table id = 'cbtn-selectors' class='table table-striped table-bordered nowrap'>");
        //    sb.Append("<thead>");
        //    sb.Append("<tr>");
        //    if (dt.Columns.Count > 0)
        //    {
        //        foreach (System.Data.DataColumn column in dt.Columns)
        //        {
        //            sb.Append("<th>" + column.ColumnName + "</th>");
        //        }
        //        sb.Append("<th>Edit</th>");
        //        sb.Append("<th>Del</th>");
        //    }
        //    sb.Append("</tr>");
        //    sb.Append("</thead>");
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            sb.Append("<tr>");
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //                sb.Append("<td>" + dt.Rows[i].ItemArray[j].ToString() + "</td>");
        //            }
        //            sb.Append("<td>");
        //            sb.Append("<a href = /" + Controller + "/Edit/"+ dt.Rows[i].ItemArray[0].ToString() + " class='btn btn-success btn-mini btn-outline-primary'>");
        //            sb.Append("<i class='icofont icofont-ui-edit'></i></a>");
        //            sb.Append("</td>");

        //            sb.Append("<td>");
        //            sb.Append("<a href = /" + Controller + "/Delete/" + dt.Rows[i].ItemArray[0].ToString() + " class='btn btn-danger btn-mini btn-outline-primary'>");
        //            sb.Append("<i class='icofont icofont-ui-close'></i></a>");
        //            sb.Append("</td>");

        //            sb.Append("</tr>");
        //        }

        //    }
        //    sb.Append("<tbody>");
        //    sb.Append("</tbody>");
        //    sb.Append("</table>");
        //    sb.Append("</div");

        //    var str = new HtmlString(sb.ToString());
        //    return new MvcHtmlString(str.ToHtmlString());
        //}
        //public static IHtmlString QueryDataTableToHTMLTableWithButtonImage(string Query, string Controller,string FiledName)
        //{
        //    DataTable dt = DataLib.GetQueryTable(Query);
        //    //
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<div class='dt-responsive table-responsive'>");
        //    sb.Append("<table id = 'cbtn-selectors' class='table table-striped table-bordered nowrap'>");
        //    sb.Append("<thead>");
        //    sb.Append("<tr>");
        //    if (dt.Columns.Count > 0)
        //    {
        //        foreach (System.Data.DataColumn column in dt.Columns)
        //        {
        //            sb.Append("<th>" + column.ColumnName + "</th>");
        //        }
        //        sb.Append("<th>Edit</th>");
        //        sb.Append("<th>Del</th>");
        //    }
        //    sb.Append("</tr>");
        //    sb.Append("</thead>");
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            sb.Append("<tr>");
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //                sb.Append("<td>" + dt.Rows[i].ItemArray[j].ToString() + "</td>");
        //            }

        //            sb.Append("<td>");
        //            sb.Append("<a href = /" + Controller + "/Edit/" + dt.Rows[i].ItemArray[0].ToString() + " class='btn btn-success btn-mini btn-outline-primary'>");
        //            sb.Append("<i class='icofont icofont-user-alt-3'></i></a>");
        //            sb.Append("</td>");

        //            sb.Append("<td>");
        //            sb.Append("<a href = /" + Controller + "/Delete/" + dt.Rows[i].ItemArray[0].ToString() + " class='btn btn-danger btn-mini btn-outline-primary'>");
        //            sb.Append("<i class='icofont icofont-user-alt-3'></i></a>");
        //            sb.Append("</td>");

        //            sb.Append("</tr>");
        //        }




        //    }
        //    sb.Append("<tbody>");
        //    sb.Append("</tbody>");
        //    sb.Append("</table>");
        //    sb.Append("</div");

        //    var str = new HtmlString(sb.ToString());
        //    return new MvcHtmlString(str.ToHtmlString());
        //}
        public static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        //public static DataTable ConvertXSLXtoDataTable(string strFilePath)
        //{
        //    string connString = "";
        //    string extension = System.IO.Path.GetExtension(strFilePath);
        //    //Connection String to Excel Workbook  
        //    if (extension.Trim() == ".xls")
        //    {
        //        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        //        //DataTable dt = Utility.ConvertXSLXtoDataTable(path1, connString);
        //        //ViewBag.Data = dt;
        //    }
        //    else if (extension.Trim() == ".xlsx")
        //    {
        //        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strFilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //        //DataTable dt = Utility.ConvertXSLXtoDataTable(path1, connString);
        //        //ViewBag.Data = dt;
        //    }

        //    OleDbConnection oledbConn = new OleDbConnection(connString);
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        oledbConn.Open();
        //        using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
        //        {
        //            OleDbDataAdapter oleda = new OleDbDataAdapter();
        //            oleda.SelectCommand = cmd;
        //            DataSet ds = new DataSet();
        //            oleda.Fill(ds);
        //            dt = ds.Tables[0];
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    finally
        //    {
        //        oledbConn.Close();
        //    }
        //    return dt;
        //}        
        //public static bool CheckForDatabaseConnection()
        //{
        //    try
        //    {
        //        SqlConnection conn = new SqlConnection(DataLib.cs);
        //        conn.Open();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //public static bool MinimumEnvoiremt()
        //{
        //    if (NetLib.CheckForInternetConnection() == true)
        //    {
        //        if (DataLib.CheckForDatabaseConnection() == true)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            HttpContext.Current.Response.Redirect("Database.aspx");
        //        }
        //    }
        //    else
        //    {
        //        HttpContext.Current.Response.Redirect("Internet.aspx");
        //    }
        //    return false;
        //}
        public static string DataTableColumnSum(DataTable dt, string Column)
        {
            if (dt.Rows.Count > 0)
            {
                object sumObject;
                sumObject = dt.Compute("Sum(" + Column + ")", string.Empty);
                return sumObject.ToString();
            }
            else
            {
                return "0";
            }

        }
        public static double DataTableColumnSumDouble(DataTable dt, string Column)
        {
            if (dt.Rows.Count > 0)
            {
                object sumObject;
                sumObject = dt.Compute("Sum(" + Column + ")", string.Empty);
                return Convert.ToDouble(sumObject.ToString());
            }
            else
            {
                return 0;
            }

        }

        public static DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();


            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type IcolType = GetProperty.PropertyType;

                        if ((IcolType.IsGenericType) && (IcolType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            IcolType = IcolType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, IcolType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo p in columns)
                {
                    dr[p.Name] = p.GetValue(Record, null) == null ? DBNull.Value : p.GetValue
                    (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }

        //

    }
}