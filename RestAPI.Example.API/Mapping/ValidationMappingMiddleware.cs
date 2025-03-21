﻿using FluentValidation;
using RestAPI.Example.Contract.Response;
using System.Net;

namespace RestAPI.Example.API.Mapping
{
    public class ValidationMappingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var validationFailureResponse = new ValidationFailureResponse
                    (ex.Errors.Select(e => new ValidationResponse(e.PropertyName, e.ErrorMessage)));

                await context.Response.WriteAsJsonAsync(validationFailureResponse);
            }
        }
    }
}
