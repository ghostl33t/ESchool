using Microsoft.AspNetCore.Mvc;

namespace server.Services.ResponseService
{
    public interface IResponseService
    {
        public Task<IActionResult> Response(int code, object returnobj);

    }
}
