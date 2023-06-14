namespace BlogManagementSystem.Data
{
    public class BlogList
    {

        static dbAccess con;
        static OracleConnection aOracleConnection;

        static BindingList<BlogModel> Blogs=new BindingList<BlogModel>();

        public static BindingList<BlogModel> GetAll()
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return GetData(CmdTrans, aOracleConnection);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Close();
            }
        }

        public static BindingList<BlogModel> GetData(OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @"SELECT
                                *
                            FROM
                                TblBlog
                            ";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {

                    Blogs.Add(

                        new BlogModel() { Id= Convert.ToInt32(dr[0].ToString()),
                            title= dr[1].ToString()!,
                            content= dr[2].ToString()!,
                            dateAdded= DateTime.Parse(dr[3].ToString()!),
                            imagePath= dr[4].ToString(),
                            UserId= Convert.ToInt32(dr[5].ToString()),
                            Category = dr[6].ToString()
                        }
                    );

                }

                return Blogs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        static void Open()
        {
            con = new dbAccess();
            aOracleConnection = con.get_con();
        }



        static void Close()
        {
            con.Close(aOracleConnection);
        }
    }
}
