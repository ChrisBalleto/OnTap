﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnTap.Models
{
    public class FeedMessage
    {
        public int Id { get; set; }

        public string FromName { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime? Created { get; set; }

    }
}