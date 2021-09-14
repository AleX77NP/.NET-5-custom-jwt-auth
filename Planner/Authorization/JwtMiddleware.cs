using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Planner.Data;
using Planner.Helpers;

namespace Planner.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository, JwtService jwtService)
        {

            try
            {
                var jwt = context.Request.Cookies["jwt"];
                var token = jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);

                var user = userRepository.GetById(userId);

                context.Items["User"] = user;
            } catch(Exception e)
            {
                Console.WriteLine(e);
            } finally
            {
                await _next(context);
            }
        }
    }
}
