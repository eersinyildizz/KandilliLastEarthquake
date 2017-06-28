using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Content.Models
{
    public class BaseModel
    {
        [Key]
        public int Key { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
