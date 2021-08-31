using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProjectKendo.Models
{
   
        [Table("mg_datauser", Schema = "public")]
        public class UserData
        {
            [Key]
        public int u_id { get; set; }
        public string u_name { get; set; }
        public string u_email { get; set; }
        public int u_mob { get; set; }
        //public string state { get; set; }
        //public string city { get; set; }
        public string u_password { get; set; }
       // public int u_confirmpass { get; set; }



    }
}