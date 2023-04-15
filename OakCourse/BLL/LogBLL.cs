using DAL;

namespace BLL
{
    public class LogBLL
    {
        public static void AddLog(int Processtype, string TableName, int ProcessID)
        {
            LogDAO.AddLog(Processtype, TableName, ProcessID);
        }
    }
}
