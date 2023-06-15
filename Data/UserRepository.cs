using Oracle.ManagedDataAccess.Client;
using System.Reflection.Metadata;

namespace BlogManagementSystem.Data
{
    public class UserRepository
    {

        static dbAccess con;
        static OracleConnection aOracleConnection;

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
