using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationTest.Models
{
    public class Product
    {
        [Key]
        [Required]
        //[DataType("int")]
        public int ID { get; set; }
        //public string ID { get; set; } = System.Guid.NewGuid().ToString();

        [Required]
        [MaxLength(250)]
        //[DataType("varchar(50)")]
        public string Name { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string CreateAt { get; set; } = (DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss.ffff");
    }
}
