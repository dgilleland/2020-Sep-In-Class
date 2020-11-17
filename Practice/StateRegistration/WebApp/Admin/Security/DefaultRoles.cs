using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApp.Admin.Security
{
    public static class DefaultRoles
    {
        public static string AdminRole => ConfigurationManager.AppSettings["adminRole"];
        public static string DefaultRole = ConfigurationManager.AppSettings["defaultRole"];
    }
}