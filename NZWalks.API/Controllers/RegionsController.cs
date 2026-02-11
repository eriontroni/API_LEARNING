using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase

    {
        public readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        //GET all regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database-Domain Models

            //var regionsDomain = await dbContext.Regions.ToListAsync();
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map Domain Models to DTOs
            /*var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });

            }*/
            //PA AUTOMAPPER


            //Map Domain Models to DTOs ME AUTOMAPPER

            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);


            //GET ALL REGIONS
            //https:localhost:portnumber/api/regions

            //Return DTOs
            return Ok(regionsDto);
        }


        //GET single region by id
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region=dbContext.Regions.Find(id);
            //var region = dbContext.Regions.Find(id);
            //GET region domain Model From Database
            //var regionDomain =await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null) return NotFound();// nese ska kthen 404 not found
            /*//Map/Convert Region domain Model to RegionDto
            var regionDto = new RegionDto()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            }; PA AUTOMAPPER*/

            var regionDto = mapper.Map<List<RegionDto>>(regionDomain);
            //return DTO back to client =- Best Practice
            return Ok(regionDto);// nese ka bon return 200 OK

        }



        //POST To Create New Region
        //POST https:localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDtocs)
        {
            //VALIDATIONS:
            /*if (ModelState.IsValid)
            {*/

            /*//Map or Convert Dto to Domain Model
            var regiondomainModel = new Region
            {
                Code = addRegionRequestDtocs.Code,
                Name = addRegionRequestDtocs.Name,
                RegionImageUrl = addRegionRequestDtocs.RegionImageUrl
            }; PA AUTOMAPPER*/
            var regiondomainModel = mapper.Map<Region>(addRegionRequestDtocs); //MAP ME AUTOMAPPER
                                                                               //Use Domain Model to create Region
            regiondomainModel = await regionRepository.CreateAsync(regiondomainModel);

            /*//Map Domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regiondomainModel.Id,
                Code = regiondomainModel.Code,
                Name = regiondomainModel.Name,
                RegionImageUrl = regiondomainModel.RegionImageUrl
            }; MAP BACK PA AUTOMAPPER*/
            var regionDto = mapper.Map<RegionDto>(regiondomainModel);
            return CreatedAtAction(nameof(GetById), new { id = regiondomainModel.Id }, regionDto);
            /*}
            else
            {
                return BadRequest(ModelState);
            }*/
        }
   


        //PUT to Update region
        //PUT https:localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel] // Ne vend te validimit me IF
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //VALIDATION:
            /* if (ModelState.IsValid)
             {*/
            //Check If Region exists
            /*            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
                        if (regionDomainModel == null) return NotFound();*/

            /*//MAP DTO to Domain model
            var regionDomainModel = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            }; PA AUTOMAPPEr*/
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            //Map Dto to Domain Model
            /*regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();*/
            //-I KENA QIT N REPOSITORY QETO NALT

            /*//Convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };*/
            var regionDto = mapper.Map<RegionDto>(regionDomainModel); //Map back to dto me automapper

            return Ok(regionDto);//Dmth me qet funksion po ja ndrrojm 3 parametra regjionit, jo veq nja
            /*}
            else
            {
                return BadRequest(ModelState);
            }*/
        }


        //Delete region
        //DELETE: https:localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            /*var regionDomainModel = await  dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);*/
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null) return NotFound();

            /*//Delete
            dbContext.Regions.Remove(regionDomainModel); // REMOVE NUK KA ASYNC METOD
            await dbContext.SaveChangesAsync();*/
            //return deleted region back
            //OSHT FSHI NREPOSITORY
            //Map Domain model To Do
            /*var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };PA AUTOMAPPER */
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }
    }

}
