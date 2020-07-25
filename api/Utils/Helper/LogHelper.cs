using System;
using System.Data.Entity;
using System.Threading.Tasks;
using API.DAL;
using API.Models;

namespace API.Utils.Helper
{
    public class LogHelper : DbContext
    {
        private APIContext db = new APIContext();

        public async Task<object> Save(Log log)
        {
            /* Save to log */
            Log logObj = new Log
            {
                Action = log.Action,
                UserID = log.UserID,
                Ip = new Ip().GetIPAddress(),
                CreatedAt = DateTime.Now,
            };

            db.Logs.Add(logObj);
            return await db.SaveChangesAsync();
        }
    }
}
