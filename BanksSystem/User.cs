using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanksSystem
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public double Sum { get; set; } // Переменная для хранения суммы
        [ForeignKey("Person")]
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
