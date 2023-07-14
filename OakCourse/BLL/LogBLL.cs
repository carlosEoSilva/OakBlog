using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class LogBLL
    {
        LogDAO dao = new LogDAO();

        public static void AddLog(int Processtype, string TableName, int ProcessID)
        {
            LogDAO.AddLog(Processtype, TableName, ProcessID);
        }

        public List<LogDTO> GetLogs()
        {
            return dao.GetLogs();
        }
    }
}
