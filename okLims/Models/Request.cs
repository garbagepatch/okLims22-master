using Microsoft.AspNetCore.Mvc.Rendering;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace okLims.Models
{

   

    public class Request
    {
        [Key]
        public int RequestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool AllDay { get; set; }

        public int ControllerID { get; set; }
       
        public int SizeID { get; set; }
       
        public int FilterID { get; set; }
       
        public int StateId { get; set; }
        [ForeignKey("SizeID")]
        public virtual FilterSize FilterSize { get; set; }
        [ForeignKey("FilterID")]
        public virtual FilterType FilterType { get; set; }
        [ForeignKey("ControllerID")]
        public virtual ControllerType ControllerType {get; set;}
      
        public int LaboratoryId { get; set; }
        
     
        public string RequesterEmail { get; set; }
        [ForeignKey("LaboratoryId")]
        public virtual Laboratory Laboratory { get; set; }
     
       

        public virtual List<RequestLine> RequestLines { get; set; } = new List<RequestLine>();
        [ForeignKey("StateId")]
        public virtual RequestState State { get; set; }
       
    }

}
