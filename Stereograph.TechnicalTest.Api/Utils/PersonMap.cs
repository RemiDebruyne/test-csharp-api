using CsvHelper.Configuration;
using Stereograph.TechnicalTest.Api.Entities;

namespace Stereograph.TechnicalTest.Api.Utils
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap() {
            Map(m => m.FirstName).Name("first_name");
            Map(m => m.LastName).Name("last_name");
            Map(m => m.Email).Name("email");
            Map(m => m.Address).Name("address");
            Map(m => m.City).Name("city");
        }
    }
}
