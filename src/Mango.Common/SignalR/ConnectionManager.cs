using System;
using System.Collections.Generic;
using System.Text;
namespace Mango.Common.SignalR
{
    public class ConnectionManager
    {
        public static List<ConnectionUser> ConnectionUsers { get; set; } = new List<ConnectionUser>();
    }
}
