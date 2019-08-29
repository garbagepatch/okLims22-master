using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class FilterSize
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SizeID { get; set; }
        public string filterSize { get; set; }
        public int RequestFK { get; set; }
    public ICollection<Request> Requests { get; set; }
    public static FilterSize[] GetFilterSizes()
    {
            FilterSize thirtykda = new FilterSize
        {
            SizeID = 1,
            filterSize = "30 kDa"
        };
            FilterSize seventyfivekda = new FilterSize
            {
                SizeID = 2,
                filterSize = "75 kDa"
            };
            FilterSize twomicron = new FilterSize
            {
                SizeID = 3,
                filterSize = "0.2 um"
            };
            FilterSize fortyfivemicron = new FilterSize
            {
                SizeID = 4,
                filterSize = "0.45 um"
            };
            return new FilterSize[] { thirtykda, seventyfivekda, twomicron, fortyfivemicron };
        }

}
}
