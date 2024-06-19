﻿using Microsoft.AspNetCore.Mvc;
using Stereograph.TechnicalTest.Api.Entities;
using Stereograph.TechnicalTest.Api.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stereograph.TechnicalTest.Api.Controllers;

[Route("api/persons")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IRepository<Person> _repository;

    public PersonController(IRepository<Person> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<Person> personList = await _repository.GetAll();

        return Ok(personList);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        Person person = await _repository.Get(person => person.Id == id);

        if (person == null)
        {
            return BadRequest(new
            {
                Message = $"Person with id: {id} was not found"
            });
        }

        return Ok(person);

    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Person person)
    {
        Person personAddedToDb = await _repository.Add(person);

        if (personAddedToDb == null)
        {
            return BadRequest("Something went wrong");
        }

        return CreatedAtAction(nameof(GetById), new { id = personAddedToDb.Id }, personAddedToDb);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] Person person)
    {
        Person personFromDb = await _repository.Get(p => p.Id == id);

        if (personFromDb == null)
        {
            return BadRequest("No user was found with this id");
        }

        person.Id = id;

        Person personUpdated = await _repository.Update(person);

        if (personUpdated == null)
        {
            return BadRequest(new{ 
                Message = "Something went wrong",
            });
        }

        return Ok(personUpdated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        Person personFromDb = await _repository.Get(p => p.Id == id);

        if (personFromDb == null)
            return BadRequest("Something went wrong");

        await _repository.Delete(id);

        return Ok($"The person with id: {id} was deleted from database successfully");

    }
}
