using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using KalumManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace KalumManagement.Helpers
{
    public class ErrorFilterResponseException : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is SqlException)
            {
                ErrorResponse error = new ErrorResponse()
                {
                    TipoError = "COM",
                    HttpStatusCode = "503",
                    Mensaje = "Error en el servicio legado de la base de datos de sql server"
                };
                context.Result = new ObjectResult(503)
                {
                    StatusCode = 503,
                    Value = error
                };
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
          Console.WriteLine("...");
        }
    }
}