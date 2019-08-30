using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class RequestState
    {
        [Key]
        public int StateId { get; set; }
        public string State { get; set; }
        public int RequestFK { get; set; }
        [ForeignKey("RequestFK")]
        public ICollection<Request> Requests { get; set; }
        IList<RequestState> states = new List<RequestState>
            {
                new RequestState(){StateId = 0, State= "Submitted"},
                new RequestState(){StateId = 1, State="Opened"},
                new RequestState(){StateId = 2, State="Completed"},
                new RequestState(){StateId = 3, State="Deleted"}
        };


    }
}