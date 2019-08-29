using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class ControllerType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ControllerID { get; set; }
        public string controllerType { get; set; }
        public int RequestFK { get; set; }
        public ICollection<Request> Requests { get; set; }
        public static ControllerType[] ControllerTypes()
        {
            ControllerType finesse = new ControllerType
            {
                ControllerID = 1,
                controllerType = "Finesse"

            };
            ControllerType incontrol = new ControllerType
            {
                ControllerID = 2,
                controllerType = "In-Control"
            };
            return new ControllerType[] { finesse, incontrol };
        }
    }
}
