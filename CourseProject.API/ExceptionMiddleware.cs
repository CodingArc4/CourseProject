using Courseproject.Business.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CourseProject.API
{
    public class ExceptionMiddleware
    {
        private RequestDelegate Next { get; }

        public ExceptionMiddleware(RequestDelegate next)
        {
            Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }

            catch(AddressNotFoundException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Address for id {ex.Id} not found!",
                    Type = "Error"
                };
            }


            catch (DependendEmployeeExistException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = string.Empty,
                    Instance = "",
                    Title = $"Dependend employees {JsonSerializer.Serialize(ex.Employees.Select(e=> e.Id))}",
                    Type = "Error"
                };
            }

            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var problemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = JsonSerializer.Serialize(ex.Errors),
                    Instance = "",
                    Title = "Validation Error",
                    Type = "Error"
                };
            }
            
            catch (Exception ex) { 
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var ProblemDetails = new ProblemDetails()
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message,
                    Instance = "",
                    Title = "Internal Server Error - something went wrong.",
                    Type = "Error"
                };

                var problemDetailSerializer = JsonSerializer.Serialize(ProblemDetails);
                await context.Response.WriteAsync(problemDetailSerializer);
            }
        }
    }
}
