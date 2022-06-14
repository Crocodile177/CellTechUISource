using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellTechUI.Models
{
    public class CellPhone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Specifications { get; set; }
        public double Price { get; set; }
    }
}