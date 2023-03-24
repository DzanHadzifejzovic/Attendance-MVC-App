﻿using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace FIsrtMVCapp.Filters
{
    public class HandleErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            context.HttpContext.Response.Body.Write(Encoding.UTF8.GetBytes("Hey you got error"));
        }
    }
}
