using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("tb_m_type_statuses")]
    public class TypeStatus
    {
        [Key]
        [StringLength(16)]
        public string TypeStatusId { get; set; }
        [StringLength(64)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual ICollection<Status> Status { get; set; }

    }
}