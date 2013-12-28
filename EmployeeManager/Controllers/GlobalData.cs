using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManager.Controllers
{
    public static class GlobalData
    {
        public static Models.User CurrentUser { get; set; }
    }
}