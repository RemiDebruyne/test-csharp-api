using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Threading.Tasks;
using Stereograph.TechnicalTest.Api.Repositories;
using FakeItEasy;
using Stereograph.TechnicalTest.Api.Controllers;
using Stereograph.TechnicalTest.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace Stereograph.TechnicalTest.Tests.Controllers
{
    public class PersonControllerTest
    {
        IRepository<Person> _repository;

        public PersonControllerTest()
        {
            _repository = A.Fake<IRepository<Person>>();
        }

        [Fact]
        public async Task PersonController_GetAllPerson_ReturnOKAsync()
        {
            //Arrange

            var controller = new PersonController(_repository);


            //Act
            var actionResult = await controller.GetAll();

            //Assert
            actionResult.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public async Task PersonController_GetPersonById_ReturnOKAsync()
        {
            //Arrange
            var controller = new PersonController(_repository);
            int testId = 5;

            //Act
            var actionResult = await controller.GetById(testId);

            //Assert
            actionResult.Should().BeOfType(typeof (OkObjectResult));
        }

        [Fact]
        public async Task PersonController_Add_ReturnCreatedAtAction()
        {
            //Arrange
            var person = A.Fake<Person>();
            var controller = new PersonController(_repository);

            //Act
            var actionResult = await controller.Add(person);

            //Assert
            actionResult.Should().BeOfType(typeof(CreatedAtActionResult));

        }


    }
}
