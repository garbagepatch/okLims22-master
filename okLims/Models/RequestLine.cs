using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class RequestLine
    {
        public int RequestLineId { get; set; }
        public Request Request { get; set; }
        public int RequestId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public int ControllerID { get; set; }
        public int SizeID { get; set; }
        public int FilterID { get; set; }
      
        public int LaboratoryId { get; set; }

        public string SpecialDetails { get; set; }
        public string RequesterEmail { get; set; }
       

    }
}
