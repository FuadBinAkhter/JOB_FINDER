using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JOB_FINDER.Data
{
    public class JOB_FINDERContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public JOB_FINDERContext() : base("name=JOB_FINDERContext")
        {
        }

        public System.Data.Entity.DbSet<JOB_FINDER.Models.COMPANY> COMPANies { get; set; }
        public System.Data.Entity.DbSet<JOB_FINDER.Models.USER> USERs { get; set; }
    }
}
