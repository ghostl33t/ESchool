using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs.ClassDepartmentSubjectProfessor;
using server.Repositories.Interfaces;
using server.Services.ResponseService;
using server.Validations.Interfaces;

namespace server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CDSPController : Controller
    {
        private readonly ICDSP _cdsp;
        private readonly IResponseService _functions;
        private readonly ICDSPValidations _cdspValidatons;
        private readonly IMapper _mapper;
        public CDSPController(ICDSP cdsp, IResponseService functions, ICDSPValidations cdspValidations, IMapper mapper)
        {
            this._cdsp = cdsp;
            this._functions = functions;
            this._cdspValidatons = cdspValidations;
            this._mapper = mapper;
        }
        [HttpPost]
        [Route("create-cdsp")]
        public async Task<IActionResult> CreateCDSPAsync(PostCDSP newcdspdto)
        {
            var newcdsp = _mapper.Map<ClassDepartmentSubjectProfessor>(newcdspdto);
            if(await _cdspValidatons.Validate(newcdsp.CreatedById,newcdsp.SubjectID,newcdsp.ProfessorId,newcdsp.ClassDepId) == true)
            {
                var res = await _cdsp.CreateCDSP(newcdsp);
            }
            return await _functions.Response(_cdspValidatons.code, _cdspValidatons.validationMessage);
        }
        [HttpPatch]
        [Route("update-cdsp/{Id}")]
        public async Task<IActionResult> ModifyCDSPAsync(long Id, PatchCDSP cdspdto)
        {
            var cdsp = _mapper.Map<ClassDepartmentSubjectProfessor>(cdspdto);
            if (await _cdspValidatons.Validate(cdsp.CreatedById, cdsp.SubjectID, cdsp.ProfessorId, cdsp.ClassDepId) == true)
            {
                var res = await _cdsp.ModifyCDSP(Id,cdsp);
            }
            return await _functions.Response(_cdspValidatons.code, _cdspValidatons.validationMessage);
        }
        [HttpPatch]
        [Route("delete-cdsp/{Id}/{administratorId}")]
        public async Task<IActionResult> DeleteCDSPAsync(long Id, long administratorId)
        {
            if (await _cdspValidatons.Validate(Id,administratorId) == true)
            {
                var res = await _cdsp.DeleteCDSP(Id,administratorId);
            }
            return await _functions.Response(_cdspValidatons.code, _cdspValidatons.validationMessage);
        }
        //vraca listu profesora i predmeta koji predaju odredjenom odjeljenju
        [HttpGet]
        [Route("get-class-details/{Id}")]
        public async Task<IActionResult> GetClassDetailsAsync(long Id)
        {
            var result =  await this._cdsp.GetClassDetails(Id);
            if(result.Count == 0 || result == null)
            {
                return await _functions.Response(400, "Data not found!");
            }
            return await _functions.Response(200, result);
        }
        //vraca listu predmeta i razreda kojima profesor predaje
        [HttpGet]
        [Route("get-professor-subject-details/{Id}")]
        public async Task<IActionResult> GetProfessorSubjectDetailsAsync(long Id)
        {
            var result = await this._cdsp.GetProfessorSubjectDetails(Id);
            if (result.Count == 0 || result == null)
            {
                return await _functions.Response(400, "Data not found!");
            }
            return await _functions.Response(200, result);
        }
    }
}
