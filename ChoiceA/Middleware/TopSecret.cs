using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ChoiceA.Middleware
{
    public class TopSecret
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public TopSecret(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        // собственно RequestDelegate
        //
        public async Task Invoke(HttpContext context)
        {

            var name = context.Request.Path.Value.Split("/").Last();

            if (context.User.Identity.IsAuthenticated && name.EndsWith(".secret"))
            {
                await context.Response.SendFileAsync(
                    _env.WebRootFileProvider.GetFileInfo("secret.secret")
                    );
            }
            else
            {
                await _next(context);
            }
        }
    }

    static class TopSecretExtentions
    {
        public static IApplicationBuilder UseTopSecret(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TopSecret>();
        }
    }
}
