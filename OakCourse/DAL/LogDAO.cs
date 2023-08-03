using DTO;
using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;

namespace DAL
{
    public class LogDAO
    {
        public static void AddLog(int ProcessType, string TableName, int ProcessID)
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                Log_Table log = new Log_Table();

                log.UserID = UserStatic.UserID;
                log.ProcessType = ProcessType;
                log.ProcessID = ProcessID;
                log.ProcessCategoryType = TableName;
                log.PrecessDate = DateTime.Now;
                log.IPAddress = HttpContext.Current.Request.UserHostAddress;

                db.Log_Table.Add(log);
                db.SaveChanges();
            }
        }

        public List<LogDTO> GetLogs()
        {
            using (POSTDATAEntities db = new POSTDATAEntities())
            {
                List<LogDTO> dtolist = new List<LogDTO>();

                var list = (from log in db.Log_Table
                            join user in db.T_User on log.UserID equals user.ID
                            join process in db.ProcessTypes on log.ProcessType equals process.ID
                            select new
                            {
                                ID = log.ID,
                                UserName = user.Username,
                                TableName = log.ProcessCategoryType,
                                TableID = log.ProcessID,
                                ProcessName = process.ProcessName,
                                ProcessDate = log.PrecessDate,
                                ipAddress = log.IPAddress
                            }).OrderByDescending(x => x.ProcessDate).ToList();

                foreach (var item in list)
                {
                    LogDTO dto = new LogDTO
                    {
                        ID = item.ID,
                        UserName = MeuUtils.ByteParaString(item.UserName),
                        TableName = item.TableName,
                        TableID = item.TableID,
                        ProcessName = item.ProcessName,
                        ProcessDate = item.ProcessDate,
                        IpAddress = item.ipAddress
                    };

                    dtolist.Add(dto);
                }

                return dtolist;
            }
        }
    }
}
