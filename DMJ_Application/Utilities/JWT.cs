﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMJ_Application.Utilities
{
    public class JWT
    {
        public string key { get; set; }
        public string issuer { get; set; }
        public string audience { get; set; }
        public int expireIn { get; set; }

    }
}
