﻿using Microsoft.AspNetCore.Mvc;
using server.Repositories.Interfaces;

namespace server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CDSPController : Controller
    {
        private readonly ICDSP ICDSP;
        public CDSPController(ICDSP ICDSP)
        {
            this.ICDSP = ICDSP;
        }
        //vraca listu profesora i predmeta koji predaju odredjenom odjeljenju
        [HttpGet]
        [Route("get-class-details/{Id}")]
        public async Task<IActionResult> GetClassDetails(long Id)
        {
            return Ok(await this.ICDSP.GetClassDetails(Id));
        }
        //vraca listu predmeta i razreda kojima profesor predaje
        [HttpGet]
        [Route("get-professor-subject-details/{Id}")]
        public async Task<IActionResult> GetProfessorSubjectDetails(long Id)
        {
            return Ok(await this.ICDSP.GetProfessorSubjectDetails(Id));
        }
    }
}
