﻿using Azure;
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Core.Application.Exceptions;
using System.Net;

namespace LinkDev.Talabat.APIs.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Logic Executed with the Request

                await _next(httpContext);

                // Logic Executed with the Response

                //if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                //{
                //    var response = new ApiResponse((int)HttpStatusCode.NotFound, $"The request endpoint: {httpContext.Request.Path} is not found");
                //    await httpContext.Response.WriteAsync(response.ToString());
                //}


            }
            catch (Exception ex)
            {
                #region Logging : TODO
                if (_env.IsDevelopment())
                {
                    // Development Mode

                    _logger.LogError(ex, ex.Message);
                }
                else
                {
                    // Production Mode
                    // Log Exception Details in DB || File

                } 
                #endregion

                await HandleExceptionAsync(httpContext, ex);
            }

        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ApiResponse response;

            switch (ex)
            {
                case NotFoundException:
                    httpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiResponse(404, ex.Message);

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                case ValidationException validationException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiValidationErrorResponse(ex.Message) { Errors = (IEnumerable<ApiValidationErrorResponse.ValidationError>) validationException.Errors };

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                case BadRequestException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiResponse(400, ex.Message);

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                case UnAuthorizedException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    httpContext.Response.ContentType = "application/json";

                    response = new ApiResponse(401, ex.Message);

                    await httpContext.Response.WriteAsync(response.ToString());
                    break;

                default:

                    response = _env.IsDevelopment() ?
                        new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString())
                        : 
                        new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);


                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpContext.Response.ContentType = "application/json";


                    await httpContext.Response.WriteAsync(response.ToString());
                    break;
            }
        }
    }
}
