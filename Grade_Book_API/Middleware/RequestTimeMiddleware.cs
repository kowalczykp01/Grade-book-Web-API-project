﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grade_Book_API.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        private readonly Stopwatch _stopWatch;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
            _stopWatch = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
            await next.Invoke(context);
            _stopWatch.Stop();

            var elapsedMilliseconds = _stopWatch.ElapsedMilliseconds;

            if (elapsedMilliseconds > 4000)
                _logger.LogInformation($"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedMilliseconds} miliseconds");
        }
    }
}
