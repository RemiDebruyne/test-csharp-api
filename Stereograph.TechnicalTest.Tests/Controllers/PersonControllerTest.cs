using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Threading.Tasks;
using Stereograph.TechnicalTest.Api.Repositories;
using Stereograph.TechnicalTest.Api.Controllers;
using Stereograph.TechnicalTest.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Moq;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;

namespace Stereograph.TechnicalTest.Tests.Controllers
{
    public class PersonControllerTest
    {

        #region GetAll Tests
        [Fact]
        public async Task PersonController_GetAll_ReturnOKAsync()
        {
            //Arrange
            var repository = new Mock<IRepository<Person>>();
            repository.Setup(repository => repository.GetAll()).ReturnsAsync(new List<Person>());
            var controller = new PersonController(repository.Object);

            //Act
            var actionResult = await controller.GetAll();

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);

        }

        [Fact]
        public async Task PersonController_GetAll_ReturnAllPersons()
        {
            //Arrange
            var personList = new List<Person>()
            {
                new Person(),
                new Person(),
                new Person(),
                new Person()
            };

            var repository = new Mock<IRepository<Person>>();
            repository.Setup(repository => repository.GetAll()).ReturnsAsync(personList);
            var controller = new PersonController(repository.Object);

            //Act
            var actionResult = await controller.GetAll() as OkObjectResult;

            //Assert
            var resultList = actionResult.Value as List<Person>;
            Assert.Equal(personList.Count, resultList.Count);
        }

        #endregion

        #region GetById Tests
        [Fact]
        public async Task PersonController_GetById_ReturnOkAsync()
        {
            //Arrange
            var repository = new Mock<IRepository<Person>>();
            int testId = 5;
            repository.Setup(repository => repository.Get(p => p.Id == testId)).ReturnsAsync(new Person() { Id = testId });
            var controller = new PersonController(repository.Object);

            //Act
            var actionResult = await controller.GetById(testId);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);

        }

        [Fact]
        public async Task PersonController_GetById_ReturnBadRequest_When_PersonId_IsDifferent_From_ArgumentId()
        {
            //Arrange

            int fakeId = 7;
            var repository = new Mock<IRepository<Person>>();
            repository.Setup(repository => repository.Get(p => p.Id == 5)).ReturnsAsync(new Person() { Id = 5 });

            var controller = new PersonController(repository.Object);

            //Act
            var actionResult = await controller.GetById(fakeId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task PersonController_GetById_ReturnBadRequest_When_Person_IsNull()
        {
            //Arrange

            int fakeId = 9999;
            var repository = new Mock<IRepository<Person>>();
            repository.Setup(repository => repository.Get(p => p.Id == 5)).ReturnsAsync(new Person() { Id = 5 });

            var controller = new PersonController(repository.Object);

            //Act
            var actionResult = await controller.GetById(fakeId);

            //Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Fact]
        public async Task PersonController_GetById_ReturnPerson_Where_PersonId_EqualTo_ArgumentId()
        {
            //Arrange
            int testId = 5;
            var expectedPerson = new Person { Id = testId };
            var repository = new Mock<IRepository<Person>>();
            repository.Setup(repository => repository.Get(p => p.Id == testId)).ReturnsAsync(new Person() { Id = testId });
            var controller = new PersonController(repository.Object);

            //Act
            var actionResult = await controller.GetById(testId) as OkObjectResult;

            //Assert
            Person resultValue = actionResult.Value as Person;
            Assert.Equal(expectedPerson.Id, resultValue.Id);

        }
        #endregion

        #region Add Tests
        [Fact]
        public async Task PersonController_Add_ReturnCreatedAtActionResult()
        {
            //Arrange
            var person = new Person();
            var repository = new Mock<IRepository<Person>>();
            repository.Setup(repository => repository.Add(person)).ReturnsAsync(person);
            var controller = new PersonController(repository.Object);

            //Act
            var actionResult = await controller.Add(person);

            //Assert
            Assert.IsType<CreatedAtActionResult>(actionResult);
        }

        #endregion

        #region Put Tests
        [Fact]
        public async Task PersonController_Update_Return_OkObjectResult()
        {
            //Arrange
            int testId = 5;
            Person person = new Person() { Id = 5};
            Person newPerson = new Person() { Id = 5, FirstName = "BIM" , LastName ="BOOM"};
            var repository = new Mock<IRepository<Person>>();
            repository.Setup(repository => repository.Get(p => p.Id == testId)).ReturnsAsync(person);
            repository.Setup(repository => repository.Update(newPerson)).ReturnsAsync(newPerson);
            var controller = new PersonController(repository.Object);

            //Act
            var actionResult = await controller.Update(testId, newPerson);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);

        }
        [Fact]
        public async Task PersonController_Update_Return_NotFoundResult_When_NoPersonFound_WithRouteId()
        {
            //Arrange
            int testId = 1500;
            Person person  = null;
            var repository = new Mock<IRepository<Person>>();
            repository.Setup(repository => repository.Get(p => p.Id == testId)).ReturnsAsync(person);
            var controller = new PersonController(repository.Object);

            //Act
            var actionResult = await controller.Update(testId, person);

            //Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public async Task PersonController_Update_Return_BadRequestResult_When_PersonId_And_ArgumentId_Are_Different()
        {
            //Arrange
            int testId = 123;
            Person person = new Person() { Id = 5};
            var repository = new Mock<IRepository<Person>>();
            repository.Setup(repository => repository.Get(p => p.Id == testId)).ReturnsAsync(person);
            var controller = new PersonController(repository.Object);

            //Act
            var actionResult = await controller.Update(testId, person);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public async Task PersonController_Update_Return_Person()
        {
            //Arrange
            int testId = 5;
            Person person = new Person() { Id = 5 };
            Person newPerson = new Person() { Id = 5, FirstName = "BIM", LastName = "BOOM" };
            var repository = new Mock<IRepository<Person>>();
            repository.Setup(repository => repository.Get(p => p.Id == testId)).ReturnsAsync(person);
            repository.Setup(repository => repository.Update(newPerson)).ReturnsAsync(newPerson);
            var controller = new PersonController(repository.Object);

            //Act
            var actionResult = await controller.Update(testId, newPerson) as OkObjectResult;

            //Assert
            var resultValue = actionResult.Value as Person;
            Assert.Equal(newPerson, resultValue);
        }
        #endregion
        #region Delete Tests
        [Fact]
        public async Task PersonController_Delete_Return_OkObjectResult()
        {
            //Arrange
            int testId = 5;
            Person person = new Person() { Id = 5 };
            var repository = new Mock<IRepository<Person>>();
            repository.Setup(repository => repository.Get(p => p.Id == testId)).ReturnsAsync(person);
            var controller = new PersonController(repository.Object);

            //Act
            var actionResult = await controller.Delete(testId);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);

        }

        [Fact]
        public async Task PersonController_Delete_Return_BadRequestResult_When_NoPersonFound_From_RouteId()
        {
            //Arrange
            int testId = 5;
            Person person = new Person() { Id = testId };
            var repository = new Mock<IRepository<Person>>();
            repository.Setup(repository => repository.Get(p => p.Id == testId)).ReturnsAsync(person);
            var controller = new PersonController(repository.Object);

            //Act
            var actionResult = await controller.Delete(testId);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);

        }
        #endregion
    }
}
