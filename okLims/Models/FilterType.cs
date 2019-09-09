using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class FilterType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int FilterID { get; set; }
        public string filterType { get; set; }

        public static FilterType[] GetFilterTypes()
        {
            FilterType fedbatch = new FilterType
            {
                FilterID = 1,
                filterType = "FedBatch"
               
            };
            FilterType fedbatchmod = new FilterType
            {
                FilterID = 2,
                filterType = "FedBatch Mod"
            };
            FilterType atfsingle = new FilterType
            {
                FilterID = 3,
                filterType = "ATF Single"
            };
            FilterType atft = new FilterType
            {
                FilterID = 4,
                filterType = "ATF T"
            };
            FilterType atfmod = new FilterType
            {
                FilterID = 5,
                filterType = "ATF Mod"
            };
            return new FilterType[] { fedbatch, fedbatchmod, atfsingle, atft, atfmod };
        }
    }
}
