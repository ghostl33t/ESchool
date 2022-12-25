using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Models.Domain;
using server.Models.DTOs.ClassDepartment;
using server.Repositories.Interfaces;
using server.Services.ResponseService;
using server.Validations.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ClassDepartmentController : Controller
    {
        private readonly IClassDepartment _classDepartmentRepository;
        private readonly IClassDepartmentValidations _classDepartmentValidations;
        private readonly IResponseService _functions;
        private readonly IMapper _mapper;
        public ClassDepartmentController(IClassDepartment IClassDepartment, IClassDepartmentValidations IClassDepartmentValidations, IResponseService functions, IMapper mapper)
        {
            this._classDepartmentRepository = IClassDepartment;
            this._classDepartmentValidations = IClassDepartmentValidations;
            this._functions = functions;
            this._mapper = mapper;
        }
        [Route("get-all")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var classList = await _classDepartmentRepository.GetAllClassDepartmentsAsync();
            if(classList != null)
            {
                var classListDTO = _mapper.Map<List<GetClassDepartment>>(classList);
                return await _functions.Response(200, classListDTO);
            }
            return await _functions.Response(400, "No data found!");
        }
        [Route("get-by-id/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(long Id )
        {
            var classd = await _classDepartmentRepository.GetClassDepartmentByIdAsync(Id);
            if(classd != null)
            {
                var classLDTO = _mapper.Map<GetClassDepartment>(classd);
                return await _functions.Response(200, classLDTO);
            }
            return await _functions.Response(400,"No data found!");
        }
        [Route("get-students-inclass/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetStudentsInClassAsync(long Id)
        {
            var studentsInClassList = await _classDepartmentRepository.GetStudentsPerClassDetailsAsync(Id);
            int code = 400;
            if(studentsInClassList != null)
            {
                code = 200;
            }
            return await _functions.Response(code, ((studentsInClassList != null) ? studentsInClassList : "No data found!") );//studentsInClassList);
        }
        [Route("create-department")]
        [HttpPost]
        public async Task<IActionResult> Create(Models.DTOs.ClassDepartment.PostClassDepartment newClassDTO)
        {
            if (await _classDepartmentValidations.Validation(newClassDTO) == true)
            {
                var newclass = _mapper.Map<ClassDepartment>(newClassDTO);
                var res = await _classDepartmentRepository.CreateClassDepartmentAsync(newclass);
            }
            return await _functions.Response(_classDepartmentValidations.code, _classDepartmentValidations.validationMessage);
        }
        [Route("update-department")]
        [HttpPatch]
        public async Task<IActionResult> Update(Models.DTOs.ClassDepartment.PatchClassDepartment classdep)
        {
            if (await _classDepartmentValidations.Validation(classdep) == true)
            {
                var classDepartmentExist = _mapper.Map<ClassDepartment>(classdep);
                var res = await _classDepartmentRepository.ModifyClassDepartmentAsync(classDepartmentExist);
            }
            return await _functions.Response(_classDepartmentValidations.code, _classDepartmentValidations.validationMessage);
        }
        [Route("delete-department/{Id}/{AdministratorId}")]
        [HttpPatch]
        public async Task<IActionResult> Delete(long Id,long AdministratorId)
        {
            if (await _classDepartmentValidations.Validation(Id, AdministratorId) == true)
            {
                var res = await _classDepartmentRepository.DeleteClassDepartmentAsync(Id, AdministratorId);
            }
            return await _functions.Response(_classDepartmentValidations.code, _classDepartmentValidations.validationMessage);
        }
}
}
