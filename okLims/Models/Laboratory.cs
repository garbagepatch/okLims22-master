using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Models
{
    public class Laboratory
    {


        public int LaboratoryId { get; set; }
        
        public string LaboratoryName { get; set; }
        public int RequestFK { get; set; }
        public ICollection<Request> Requests { get; set; }
        public static Laboratory[] Laboratories()
        {
            Laboratory first = new Laboratory
            {
                LaboratoryId = 1,
                LaboratoryName = "First Floor"
            };
            Laboratory second = new Laboratory
            {
                LaboratoryId = 2,
                LaboratoryName = "Second Floor"
            };
            Laboratory third = new Laboratory
            {
                LaboratoryId = 3,
                LaboratoryName = "Third Floor"
            };
            Laboratory fourth = new Laboratory
            {
                LaboratoryId = 4,
                LaboratoryName = "fourth floor"
            };
            Laboratory fifth = new Laboratory
            {
                LaboratoryId = 5,
                LaboratoryName = "fifth floor"
            };
            Laboratory sixth = new Laboratory
            {
                LaboratoryId = 6,
                LaboratoryName = "sixth floor"
            };
            Laboratory seventh = new Laboratory
            {
                LaboratoryId = 7,
                LaboratoryName = "seventh floor"
            };
            return new Laboratory[] { first, second, third, fourth, fifth, sixth, seventh };
        }


    }
}
