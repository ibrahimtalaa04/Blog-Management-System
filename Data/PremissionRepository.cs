namespace BlogManagementSystem.Data
{
    public class PremissionRepository
    {

        static dbAccess con;
        static OracleConnection aOracleConnection;

        public static bool Check(CheckPremissionViewModel model)
        {
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                return CheckforUser(model, CmdTrans, aOracleConnection);
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

        static bool CheckforUser(CheckPremissionViewModel model, OracleTransaction CmdTrans, OracleConnection aOracleConnection)
        {
            try
            {
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;

                var cmdText = @$"select  screenid,commandid,userid  from tblroles
                                where ScreenId= {model.ScreenId} and userid={model.UserId} and commandid={model.CommandId}";

                cmd.CommandText = cmdText;
                cmd.ExecuteNonQuery();
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);                
                if (dt.Rows.Count>0)
                {

                    return true;

                }
                return false;


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
