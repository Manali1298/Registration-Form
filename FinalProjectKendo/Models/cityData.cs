using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProjectKendo.Models
{
    [Table("mg_city", Schema = "public")]

    public class cityData
    {
        [Key]
        public int city_id { get; set; }
        public int state_id { get; set; }
        public string city_name { get; set; }
    }
}