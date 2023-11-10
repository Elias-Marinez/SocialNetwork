﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Dtos.Account.Response
{
    public abstract class BaseResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
      
        public BaseResponse() 
        {
            HasError = false;
        }
       
    }
}
