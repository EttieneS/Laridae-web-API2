using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Laeridae_API.Models
{
    public class Teachers
    {
        public Teachers() { }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int? Active { get; set; }
        
    }
}