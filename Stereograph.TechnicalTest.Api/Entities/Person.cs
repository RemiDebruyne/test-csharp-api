using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stereograph.TechnicalTest.Api.Entities
{
    public class Person
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _address;
        private string _city;

        public Person()
        {

        }

        public Person(string firstName, string lastName, string email, string address, string city)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
            City = city;
        }
        [Key]
        [Ignore]
        public int Id { get => _id; set => _id = value; }
        [Name("first_name")]
        public string FirstName { get => _firstName; set => _firstName = value; }
        [Name("last_name")]
        public string LastName { get => _lastName; set => _lastName = value; }
        [Name("email")]
        public string Email { get => _email; set => _email = value; }
        [Name("address")]
        public string Address { get => _address; set => _address = value; }
        [Name("city")]
        public string City { get => _city; set => _city = value; }
    }
}
