using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace API.Models
{
    [Table("tb_m_detail_schedule_interviews")]
    public class DetailScheduleInterview
    {
        [Key]
        public int DetailScheduleInterviewId { get; set; }

        [ForeignKey("ScheduleInterview")]
        public string ScheduleInterviewId { get; set; }
        [JsonIgnore]
        public virtual ScheduleInterview ScheduleInterview { get; set; }
        [StringLength(64)]
        public string EmailCandidate { get; set; }
        [StringLength(64)]
        public string EmailCustomer { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public typeLocation TypeLocation { get; set; }
    }
    public enum typeLocation {
        //[EnumMember(Value = "ONLINE")]
        Online,
        //[EnumMember(Value = "OFFLINE")]
        Offline 
    }
}