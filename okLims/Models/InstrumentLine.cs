using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class InstrumentLine
    {
        [Key]
        public int InstrumentLineId { get; set; }
        public int InstrumentId { get; set; }

        public Instrument Instrument { get; set; }
        public string InstrumentHistory { get; set; }
        public string InstrumentName { get; set; }
        public DateTimeOffset CalibrationDue { get; set; }
        public DateTimeOffset CalibrationDate { get; set; }
        public int CalibrationLength { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public int MaintenanceInterval { get; set; }

    }
}
