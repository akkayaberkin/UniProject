using System;
using RabbitMQ.Client;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace KocUniversityCourseManagement.Presentation.Middlewares
{
	public class RateLimitingMiddleware
	{
        private readonly RequestDelegate _next;
        private readonly IConnectionMultiplexer _redis;
        private readonly IConnection _rabbitMQConnection;
        private readonly IModel _rabbitMQChannel;

        public RateLimitingMiddleware(RequestDelegate next, IConnectionMultiplexer redis, IConnection rabbitMQConnection)
        {
            _next = next;
            _redis = redis;
            _rabbitMQConnection = rabbitMQConnection;
            _rabbitMQChannel = _rabbitMQConnection.CreateModel();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var remoteIp = context.Connection.RemoteIpAddress.ToString();
            var rateLimitKey = $"ratelimit:{remoteIp}";

            var db = _redis.GetDatabase();
            var rateLimitReached = db.StringIncrement(rateLimitKey) > 10; // saniyede 10 istek
            db.KeyExpire(rateLimitKey, TimeSpan.FromSeconds(1)); 

            if (rateLimitReached)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new
            {
                Path = context.Request.Path,
                Method = context.Request.Method,
                Query = context.Request.QueryString,
                Body = await new StreamReader(context.Request.Body).ReadToEndAsync()
            }));

            _rabbitMQChannel.BasicPublish(exchange: "",
                                          routingKey: "requestQueue",
                                          basicProperties: null,
                                          body: body);

            await context.Response.WriteAsync("Request queued for asynchronous processing.");
        }
    }
}

