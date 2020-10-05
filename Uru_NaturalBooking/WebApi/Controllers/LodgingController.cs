﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicException;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApi.Controllers
{
    [Route("api/lodging")]
    [ApiController]
    public class LodgingController : ControllerBase
    {

        private readonly ILodgingManagement lodgingManagement;

        public LodgingController(ILodgingManagement lodgingLogic)
        {
            lodgingManagement = lodgingLogic;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<Lodging> lodgings = lodgingManagement.GetAllLoadings();
                if (lodgings == null)
                {
                    return NotFound("No se pudo encontrar hospedajes"); 
                }
                return Ok(LodgingModelForResponse.ToModel(lodgings));
            }catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Lodging lodging = lodgingManagement.GetLodgingById(id);
                if (lodging == null)
                {
                    return NotFound("El hospedaje solicitado no fue encontrado");
                }
                return Ok(LodgingModelForResponse.ToModel(lodging));
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] LodgingModelForRequest lodgingModel)
        {
            try
            {
                Lodging lodging = lodgingManagement.Create(LodgingModelForRequest.ToEntity(lodgingModel), lodgingModel.TouristSpotId);
                return CreatedAtRoute("Get", new { id = lodging.Id }, LodgingModelForRequest.ToModel(lodging));
            }
            catch (ExceptionBusinessLogic e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] LodgingModelForRequest lodgingModel)
        {
            try
            {
                Lodging lodging = lodgingManagement.UpdateLodging(LodgingModelForRequest.ToEntity(lodgingModel)); 
                return CreatedAtRoute("Get", new { id = lodging.Id }, LodgingModelForRequest.ToModel(lodging));
            }
            catch (ExceptionBusinessLogic e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {

                lodgingManagement.RemoveLodging(id);
                return NoContent();
            }
            catch (ExceptionBusinessLogic e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}