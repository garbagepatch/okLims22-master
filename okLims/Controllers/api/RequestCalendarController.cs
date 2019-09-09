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
    public class RequestCalendar : Controller
    {
      private readonly  ApplicationDbContext db;
        private readonly IEmailSender _emailSender;

        public RequestCalendar(ApplicationDbContext _db, IEmailSender emailSender)
        {
            db = _db;
            _emailSender = emailSender;
        }
        [HttpGet]
        public async Task<IActionResult> GetCalendarEvents()
        {
            List<Request> Items = await db.Request

          .ToListAsync();

            int Count = Items.Count();

            return Ok(new { Items, Count });
        }

    
    [HttpGet("[action]")]
        public async Task <IActionResult> GetCalendarEvents(string start, string end)
        {
            Request events = await db.Request
                .Where(s => s.Start == start)
                .Where(e => e.End == end)
                  .FirstOrDefaultAsync();

            return Ok(events);
        }
        [HttpPost("[action]")]
        public  IActionResult UpdateEvent([FromBody] CrudViewModel<Request> payload)
        {
            Request request = payload.value;
            db.Update(request);
            db.SaveChanges();
            return Ok(request);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddEvent([FromBody] CrudViewModel<Request> payload)
        {
            Request Request = payload.value;
            db.Request.Add(Request);
           db.SaveChanges();
            await _emailSender.SendEmailAsync(Request.RequesterEmail, "Order Received", "thank you");

            this.UpdateEvents(Request.EventId);

            return Ok(Request); 

      
        }

     

        [HttpPost("[action]")]
        public IActionResult DeleteEvent([FromBody]CrudViewModel<Request> payload)
        {
            Request Request = db.Request
                .Where(x => x.EventId == (int)payload.key)
                     .FirstOrDefault();

            db.Request.Remove(Request);

            db.SaveChanges();
            return Ok(Request);
        }



        private SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-okLims5-10F86EC1-A7B9-40B3-A943-2C9C114B0BDE;MultipleActiveResultSets=False;TrustServerCertificate=False;Connection Timeout=30;");
            conn.Open();

            return conn;
        }
       

        private void CloseConnection(SqlConnection conn)
        {
            conn.Close();
        }

        public List<Event> GetCalendarEvent(string start, string end)
        {
            List<Event> events = new List<Event>();

            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"select
                                                            event_id
                                                            ,title
                                                            ,[description]
                                                            ,event_start
                                                            ,event_end
                                                            ,all_day
                                                        from
                                                            [Events]
                                                        where
                                                            event_start between @start and @end", conn)
                {
                    CommandType = CommandType.Text
                })
                {
                    cmd.Parameters.Add("@start", SqlDbType.VarChar).Value = start;
                    cmd.Parameters.Add("@end", SqlDbType.VarChar).Value = end;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            events.Add(new Request()
                            {
                                EventId = Convert.ToInt32(dr["event_id"]),
                                Title = Convert.ToString(dr["title"]),
                                Description = Convert.ToString(dr["description"]),
                                Start = Convert.ToString(dr["event_start"]),
                                End = Convert.ToString(dr["event_end"])
                               
                            });
                        }
                    }
                }
            }

            return events;
        }


        private void UpdateEvents(int EventId)
        {
            try
            {
                Request Request = new Request();
                Request = db.Request
                    .Where(x => x.EventId.Equals(EventId))
                    .FirstOrDefault();
                if (Request != null)
                {
                    List<RequestLine> lines = new List<RequestLine>();
                    lines = db.RequestLine.Where(x => x.EventId.Equals(EventId)).ToList();
                    //update master data by its lines                                       
                    db.Update(Request);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string AddEvents(Event evt, out int eventId)
        {
            string message = "";
            SqlConnection conn = GetConnection();
            SqlTransaction trans = conn.BeginTransaction();
            eventId = 0;

            try
            {
                SqlCommand cmd = new SqlCommand(@"insert into [Events]
                                                (
	                                                title
	                                                ,[description]
	                                                ,event_start
	                                                ,event_end
	                                                ,all_day
                                                )
                                                values
                                                (
	                                                @title
	                                                ,@description
	                                                ,@start
	                                                ,@end
	                                                ,@allDay
                                                );
                                                select scope_identity()", conn, trans)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = evt.Title;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = evt.Description;
                cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = evt.Start;
                cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = Helper.ToDBNullOrDefault(evt.End);
                cmd.Parameters.Add("@allDay", SqlDbType.Bit).Value = evt.AllDay;

                eventId = Convert.ToInt32(cmd.ExecuteScalar());

                trans.Commit();
            }
            catch (Exception exp)
            {
                trans.Rollback();
                message = exp.Message;
            }
            finally
            {
                CloseConnection(conn);
            }

            return message;
        }

        public string DeleteEvents(int eventId)
        {
            string message = "";
            SqlConnection conn = GetConnection();
            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(@"delete from 
	                                                [Events]
                                                where
	                                                event_id=@eventId", conn, trans)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add("@eventId", SqlDbType.Int).Value = eventId;
                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception exp)
            {
                trans.Rollback();
                message = exp.Message;
            }
            finally
            {
                CloseConnection(conn);
            }

            return message;
        }
    }
}

