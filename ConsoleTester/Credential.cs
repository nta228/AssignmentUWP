﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTester
{
    public class Credential
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string account_id { get; set; }
        public string expire_time { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public int status { get; set; }

    }
}
