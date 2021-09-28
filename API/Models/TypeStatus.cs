using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_type_statuses")]
    public class TypeStatus
    {
        [Key]
        public int TypeStatusId { get; set; }
        public int NameTypeStatus { get; set; }
        public virtual ICollection<Status> Status { get; set; }
    }
}