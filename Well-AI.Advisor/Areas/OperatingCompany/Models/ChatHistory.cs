﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class ChatHistory
    {
        [ScaffoldColumn(false)]
        public int Id
        {
            get;
            set;
        }

        public DateTime? CreateDate
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string JobTitle
        {
            get;
            set;
        }

        public int Unread
        {
            get;
            set;
        }
    }
}
