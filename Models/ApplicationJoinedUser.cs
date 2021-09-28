using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JOB_FINDER.Models
{
    public class ApplicationJoinedUser
    {
        public APPLICATION applicationDetails { get; set; }
        public USER userDetails { get; set; }
    }
}