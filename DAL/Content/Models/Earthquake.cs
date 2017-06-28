using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Content.Models
{
    [Table("Earthquake")]
    public class Earthquake : BaseModel
    {
        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Depth { get; set; }

        public double Magnitude { get; set; }

        public string Address { get; set; }
    }
}
