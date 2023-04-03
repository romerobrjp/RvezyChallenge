﻿using Business.Services.Interfaces;
using Infra.Data.Models;
using Infra.Data.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ListingController : ControllerBase
{
  private readonly IListingService _listingService;
  private readonly ILogger<ListingController> _logger;

  public ListingController(IListingService listingService, ILogger<ListingController> logger)
  {
    _listingService = listingService;
    _logger = logger;
  }

  [HttpGet("ping")]
  [Produces("text/plain")]
  public IActionResult Ping()
  {
    Console.WriteLine(">> pong");
    return Ok("pong");
  }

  // GET: api/<ListingController>
  [HttpGet]
  public async Task<IActionResult> Get()
  {
    IEnumerable<Listing> responseData = await _listingService.GetAll();
    return Ok(responseData);
  }

  // GET api/<ListingController>/5
  [HttpGet("{id}")]
  public async Task<IActionResult> Get(long id)
  {
    Listing responseData = await _listingService.GetById(id);
    return Ok(responseData);
  }

  // POST api/<ListingController>
  [HttpPost]
  public async Task<IActionResult> Post([FromBody] ListingCreateRequest requestData)
  {
    try
    {
      Listing result = await _listingService.Create(requestData);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }

  // PUT api/<ListingController>/5
  [HttpPut("{id}")]
  public async Task<IActionResult> Put(long id, [FromBody] ListingUpdateRequest requestData)
  {
    try
    {
      Listing result = await _listingService.Update(id, requestData);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }

  // DELETE api/<ListingController>/5
  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(long id)
  {
    try
    {
      bool result = await _listingService.Delete(id);
      return Ok(result);
    }
    catch (Exception ex)
    {
      return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
    }
  }
}