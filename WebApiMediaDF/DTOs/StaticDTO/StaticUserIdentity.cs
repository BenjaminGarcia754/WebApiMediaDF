using Microsoft.AspNetCore.Identity;

namespace WebApiMediaDF.DTOs.StaticDTO
{
    public static class StaticUserIdentity
    {
        public static WebApiMediaDbContex _context { get; set; }
       
        public static string Username { get; set; }
        
        public static string Password { get; set; }

        public static string Id { get; set; }
    }
}
