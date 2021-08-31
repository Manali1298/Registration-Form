using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProjectKendo.Models
{
    [Table("mg_state", Schema = "public")]

    public class stateData
    {
        [Key]
        public int state_id { get; set; }
        public string state_name { get; set; }
    }
}