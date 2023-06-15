namespace BlogManagementSystem.Data
{
    public class BlogList
    {

        static dbAccess con;
        static OracleConnection aOracleConnection;

        static List<BlogModel> Blogs { get; set; }=new List<BlogModel>() ;

        public static List<BlogModel> GetAll()
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

        static List<BlogModel> GetData(OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            try
            {
                Blogs.Clear();
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
                            Category = dr[6].ToString(),
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



        public static BlogModel Get(int Id)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return GetRowData(Id,CmdTrans, aOracleConnection);
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

        static BlogModel GetRowData(int Id,OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @$"SELECT
                                *
                            FROM
                                TblBlog
                            where Id={Id}
                            ";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow dr = dt.Rows[0];
                return new BlogModel()
                        {
                            Id = Convert.ToInt32(dr[0].ToString()),
                            title = dr[1].ToString()!,
                            content = dr[2].ToString()!,
                            dateAdded = DateTime.Parse(dr[3].ToString()!),
                            imagePath = dr[4].ToString(),
                            UserId = Convert.ToInt32(dr[5].ToString()),
                            Category = dr[6].ToString(),
                        };


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }





        public static void Insert(BlogModel blog)
        {

            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                InsertData(blog,CmdTrans, aOracleConnection);
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

        static void InsertData(BlogModel blog,OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @$"insert into TblBlog(Id,title,content,dateAdded,imagePath,category,userid) values(TblBlog_Seq_Id.NextVal,'{blog.title}','{blog.content}',:dateAdd,'{blog.imagePath}','{blog.Category}',{blog.UserId})";
                cmd.CommandText = cmdText;
                cmd.Parameters.Add(new OracleParameter(":dateAdd",blog.dateAdded));
                cmd.ExecuteNonQuery();
                CmdTrans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public static void Update(BlogModel blog)
        {

            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                UpdateData(blog, CmdTrans, aOracleConnection);
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

        static void UpdateData(BlogModel blog, OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @$"update TblBlog
                                    set title='{blog.title}',content='{blog.content}',
                                    dateAdded=:dateAdd,imagePath='{blog.imagePath}',category='{blog.Category}',userid={blog.UserId}
                                    where Id={blog.Id}";

                cmd.CommandText = cmdText;
                cmd.Parameters.Add(new OracleParameter(":dateAdd", blog.dateAdded));
                cmd.ExecuteNonQuery();
                CmdTrans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static void Delete(int Id)
        {

            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                deleteData(Id, CmdTrans, aOracleConnection);
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

        static void deleteData(int Id, OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @$"delete from TblBlog
                                where Id={Id}";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                CmdTrans.Commit();
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
