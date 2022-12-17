using Microsoft.AspNetCore.Mvc;
namespace server.Other
{
    public interface IFunctions 
    {
        public Task<IActionResult> Response(int code, object returnobj);
        
    }
}
