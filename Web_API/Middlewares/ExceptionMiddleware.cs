using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Claims;
using Web_API.Utilities;
using Infrastructure.Utilities;

namespace Web_API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }
        //Catch all operations into the API
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomException ex)
            {
                await HandleExceptionAsync(httpContext, $"{ex.GetType()}. {ex?.Message} | {ex?.InnerException} | {ex?.InnerException?.Message}", ex?.Message ?? "", ex?.StatusCode ?? HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, $"{ex.GetType()}. {ex?.Message} | {ex?.InnerException} | {ex?.InnerException?.Message}", "Ha ocurrido un error inesperado. Favor de comunicarse con un administrador.", HttpStatusCode.InternalServerError);
             
            }
        }


        //Model the error's information to then return it
        private async Task HandleExceptionAsync(HttpContext context, string message, string messageForUser, HttpStatusCode status)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            await context.Response.WriteAsync(new ErrorResponse()
            {
                StatusCode = context.Response.StatusCode,
                Message = messageForUser,
            }.ToString());
        }

        
    }
}
