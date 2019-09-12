using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class RequestLine
    {
        [Key]
        public int RequestLineId { get; set; }
       public int RequestId { get; set; }
     
        public int ControllerID { get; set; }
        public int SizeID { get; set; }
        public int FilterID { get; set; }
      
        public int LaboratoryId { get; set; }
        [ForeignKey("RequestId")]
       public Request Request { get; set; }
        public string RequesterEmail { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
public DateTime End { get; set; }
       

    }
}
