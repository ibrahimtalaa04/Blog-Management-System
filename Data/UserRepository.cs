using Oracle.ManagedDataAccess.Client;
using System.Reflection.Metadata;

namespace BlogManagementSystem.Data
{
    public class UserRepository
    {

        static dbAccess con;
        static OracleConnection aOracleConnection;
        public UserModel User;
        public static void Signup(SignupViewModel model)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                Signup(model,CmdTrans, aOracleConnection);
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

        static void Signup(SignupViewModel model,OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @$"insert into TblUser (Id,firstName,lastName,email,password,imagePath,datejoined)
                                  values(TblUser_Seq_Id.NextVal,'{model.FirstName}','{model.LastName}','{model.Email}','{model.Password}','{model.imagePath}',:joined)";
                cmd.CommandText = cmdText;
                cmd.Parameters.Add(new OracleParameter(":joined", DateTime.Now));
                cmd.ExecuteNonQuery();
                CmdTrans.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static UserModel? GetByEmail(string Email)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return GetByEmail(Email, CmdTrans, aOracleConnection);
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

        static UserModel? GetByEmail(string Email, OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                var cmdText = @$"SELECT
                                *
                            FROM
                                TblUser
                            where email='{Email}'
                            ";
                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    return new UserModel()
                    {
                        Id = Convert.ToInt32(dr[0].ToString()),
                        firstName = dr[1].ToString()!,
                        lastName = dr[2].ToString()!,
                        email = dr[3].ToString()!,
                        password = dr[4].ToString(),
                        imagePath = dr[5].ToString(),
                        dateJoined = DateTime.Parse(dr[6].ToString()!),
                    };

                }
                return null;


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
