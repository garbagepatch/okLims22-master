using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using okLims.Data;
using okLims.Helpers;
using okLims.Models;
using okLims.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace okLims.Controllers.api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/RequestCalendar")]
    public class RequestCalendarController: Controller
    {
        ApplicationDbContext db;
       public RequestCalendarController ( ApplicationDbContext _db)
        {
            db = _db;
        }
        [HttpGet]
        public IActionResult LoadData()  // Here we get the Start and End Date and based on that can filter the data and return to Scheduler
        {
            var data = db.Request.ToList();
            return Ok(data);
        }
        [HttpPost("[action]")]
        public IActionResult UpdateData(EditParams param)
        {
            if (param.action == "insert" || (param.action == "batch" && param.added != null)) // this block of code will execute while inserting the appointments
            {
                var value = (param.action == "insert") ? param.value : param.added[0];
                int intMax = db.Request.Select(x => x.RequestId).DefaultIfEmpty(0).Max();
                DateTime startTime = (value.Start);
                DateTime endTime = (value.End);
                Request appointment = new Request()
                {
                    RequestId = intMax + 1,
                    Start = startTime,
                    End = endTime,
                 Title = value.Title,
                 AllDay = value.AllDay,
                 Description = value.Description,
                 FilterID = value.FilterID,
                 SizeID = value.SizeID,
                 LaboratoryId = value.LaboratoryId
                };
                db.Request.Add(appointment);
                db.SaveChanges();
            }
            if (param.action == "update" || (param.action == "batch" && param.changed != null)) // this block of code will execute while updating the appointment
            {
                var value = (param.action == "update") ? param.value : param.changed[0];
                var filterData = db.Request.Where(c => c.RequestId == (value.RequestId));
                if (filterData.Count() > 0)
                {
                    DateTime startTime = (value.Start);
                    DateTime endTime = (value.End);
                      Request appointment = db.Request.Single(A => A.RequestId ==(value.RequestId));
                    appointment.Start = startTime;
                    appointment.End = endTime;
                    appointment.AllDay = value.AllDay;
                    appointment.Title = value.Title;
                    appointment.Description = value.Description;
                 appointment.FilterID = value.FilterID;
                    appointment.SizeID = value.SizeID;
                    appointment.LaboratoryId = value.LaboratoryId;


                }
                db.SaveChanges();
            }
            if (param.action == "remove" || (param.action == "batch" && param.deleted != null)) // this block of code will execute while removing the appointment
            {
                if (param.action == "remove")
                {
                    int key = Convert.ToInt32(param.key);
                   Request appointment = db.Request.Where(c => c.RequestId == key).FirstOrDefault();
                    if (appointment != null) db.Request.Remove(appointment);
                }
                else
                {
                    foreach (var apps in param.deleted)
                    {
                        Request appointment = db.Request.Where(c => c.RequestId == apps.RequestId).FirstOrDefault();
                        if (apps != null) db.Request.Remove(appointment);
                    }   
                }
                db.SaveChanges();
            }
            var data = db.Request.ToList();
            return Ok(data);
        }

        public class EditParams
        {
            public string key { get; set; }
            public string action { get; set; }
            public List<Request> added { get; set; }
            public List<Request> changed { get; set; }
            public List<Request> deleted { get; set; }
            public Request value { get; set; }
        }
    }
}