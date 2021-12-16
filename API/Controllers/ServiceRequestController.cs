using Core.Dtos.FleetCarRequest;
using API.Specifications;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController : BaseApiController
    {
        private readonly IGenericRepository<FleetCarRequest> _FleetCarRequestRepo;
        private readonly IMapper _mapper;

        public ServiceRequestController(IGenericRepository<FleetCarRequest> FleetCarRequestRepo,IMapper mapper)
        {
            _FleetCarRequestRepo = FleetCarRequestRepo;
            _mapper = mapper;
        }

        //[HttpPost("GetSrvRequest")]
        //public async Task<ActionResult<FleetCarRequestDto>> GetSrvRequest([FromBody] FleetCarRequestCDto SrvReqParams)
        //{
        //    bool isSupplier = SrvReqParams.isSupplier;
        //    int srvManNo = SrvReqParams.srvManNo;
        //    int fleetCarNo = SrvReqParams.fleetCarNo;

        //    var spec = new FleetCarRequestSpecification(srvManNo);
        //    var SrvReq = await _FleetCarRequestRepo.GetEntityWithSpec(spec);

        //    FleetCarRequestDto retObj = new FleetCarRequestDto();
        //    retObj.callno = SrvReq.CallNo;
        //    return Ok(retObj);
        //}
        [HttpPost("GetSrvRequest")]
        public async Task<ActionResult<IReadOnlyList<FleetCarRequestDto>>> GetSrvRequest([FromBody] FleetCarRequestCDto SrvReqParams)
        {
            bool isSupplier = SrvReqParams.isSupplier;
            int srvManNo = SrvReqParams.srvManNo;
            int fleetCarNo = SrvReqParams.fleetCarNo;
            var spec = new FleetCarRequestSpecification(srvManNo);
            var SrvReqs = await _FleetCarRequestRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<FleetCarRequestDto>>(SrvReqs);
            return Ok(data);
        }
    }
}
