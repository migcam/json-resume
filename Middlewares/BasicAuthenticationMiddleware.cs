using System;
using System.Text;
using System.Threading.Tasks;
using json_resume.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace json_resume.Middlewares
{
    public class BasicAuthenticationMiddleware
    {
        public RequestDelegate _next { get; set; }
        public readonly BasicAuthenticationService _authenticationService;

        public BasicAuthenticationMiddleware(RequestDelegate next,
        BasicAuthenticationService authenticationService)
        {
            _next = next;
            _authenticationService = authenticationService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //request

            if(context.Request.Method == "POST"||
                context.Request.Method == "PUT"||
                context.Request.Method == "PATCH"||
                context.Request.Method == "DELETE")
                {
                    if(!context.Request.Headers.TryGetValue(HeaderNames.Authorization,out var auth))
                        throw new System.Exception();

                    string header = context.Request.Headers[HeaderNames.Authorization];
                    string[] headers =header.Split(' ');

                    if(headers[0] != "Basic")
                        throw new System.Exception();

                    byte[] credentialBytes = Convert.FromBase64String(headers[1]);
                    string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');

                    string username = credentials[0];
                    string password = credentials[1];

                    if(!_authenticationService.users.ContainsKey(username) ||
                    _authenticationService.users[username]  != password)
                        throw new System.Exception();

                }
            
            await _next(context);

            //response
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static void UseBasicAuthentication(this IApplicationBuilder app){
            app.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }

}