using System;
using System.Data.Entity;
using System.Threading.Tasks;
using API.DAL;
using API.Models;

namespace API.Utils.Helper
{
    /// <summary>
    /// Log helper.
    /// </summary>
    public class LogHelper : DbContext
    {
        private APIContext db = new APIContext();

        /// <summary>
        /// Save the specified log.
        /// </summary>
        /// <returns>The save.</returns>
        /// <param name="log">Log.</param>
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
