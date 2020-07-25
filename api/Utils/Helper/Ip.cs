namespace API.Utils.Helper
{
    /// <summary>
    /// Ip.
    /// </summary>
    public class Ip
    {
        /// <summary>
        /// Gets the IP Address.
        /// </summary>
        /// <returns>The IP Address.</returns>
        public string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
