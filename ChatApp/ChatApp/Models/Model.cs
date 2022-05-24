using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Models
{
    public class Model
    {
        public class CreatedAt
        {
            public int Seconds { get; set; }
            public int Nanoseconds { get; set; }
        }

        public string id { get; set; }
        public string[] contactID { get; set; }
        public string[] contactName { get; set; }
        public string[] contactEmail { get; set; }
        public CreatedAt created_at { get; set; }
    }
}
