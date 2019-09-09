using System;

namespace okLims.Models
{
    public class ErrorViewModel
    {
        public string EventId { get; set; }

        public bool ShowEventId => !string.IsNullOrEmpty(EventId);
    }
}