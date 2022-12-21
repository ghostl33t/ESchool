using Microsoft.AspNetCore.Mvc;

namespace server.Services.ResponseService
{
    public class ResponseService : Controller, IResponseService
    {
        public new async Task<IActionResult> Response(int code, object returnobj)
        {
            switch (code)
            {
                case 200: //Ugl kod ispisa
                    return await Task.FromResult(StatusCode(StatusCodes.Status200OK, returnobj));
                case 201: //Ugl kod kreiranja
                    return await Task.FromResult(StatusCode(StatusCodes.Status201Created, returnobj));
                case 204: //Ako je request proso a ne vraca nista npr kod updatea i deleata u mom slucaju radi polja deleted
                    return await Task.FromResult(StatusCode(StatusCodes.Status204NoContent, returnobj));
                case 400: //Ako request sadrzi neke podatke koji nisu validni npr fali username i slicno
                    return await Task.FromResult(StatusCode(StatusCodes.Status400BadRequest, returnobj));
                case 401: //Ako korisnik nema pravo da izvrsi izmjene ili dobije podatke
                    return await Task.FromResult(StatusCode(StatusCodes.Status401Unauthorized, returnobj));
                case 404: //Ako korisnik trazi neki podatak a podatak ne postoji vrati mu 404
                    return await Task.FromResult(StatusCode(StatusCodes.Status404NotFound, returnobj));
                case 405: //Ako je pozvana metoda koja je tipa GET a hoce da se pozove kao POST server ce vratiti ovaj status kod
                    return await Task.FromResult(StatusCode(StatusCodes.Status405MethodNotAllowed, returnobj));
                default:
                    return await Task.FromResult(StatusCode(StatusCodes.Status404NotFound, returnobj));
            }
        }
    }
}
