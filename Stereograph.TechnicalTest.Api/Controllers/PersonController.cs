using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Stereograph.TechnicalTest.Api.Entities;
using Stereograph.TechnicalTest.Api.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            return BadRequest($"Person with id: {id} was not found");
        }

        if (person.Id != id)
        {
            return BadRequest("Something went wrong");
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
        Person personFromDb = await _repository.Get(person => person.Id == id);
        if (personFromDb == null)
            return NotFound();


        if (person.Id != id)
            return BadRequest();

        Person updatedPerson = await _repository.Update(person);

        if (updatedPerson == null)
            return BadRequest();

        return Ok(updatedPerson);
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
