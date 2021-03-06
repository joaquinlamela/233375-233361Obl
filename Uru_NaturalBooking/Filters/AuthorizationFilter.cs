﻿using BusinessLogicInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly IUserManagement userLogic;

        public AuthorizationFilter(IUserManagement logic)
        {
            userLogic = logic;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string token = context.HttpContext.Request.Headers["token"];
            if (token == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Error. Debe estar logueado para poder realizar esta operación."
                };
                return;
            }
            if (!userLogic.IsLogued(token))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "Error. No esta logueado correctamente."
                };
                return;
            }
        }
    }
}
