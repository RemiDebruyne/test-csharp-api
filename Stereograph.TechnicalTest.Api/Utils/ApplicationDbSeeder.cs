using CsvHelper;
using Stereograph.TechnicalTest.Api.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Stereograph.TechnicalTest.Api.Utils
{
    public class ApplicationDbSeeder
    {
        public List<Person> Seed()
        { 
                using (StreamReader reader = new StreamReader("./Ressources/Persons.csv", Encoding.UTF8))
                {
                    CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                    List<Person> record = csv.GetRecords<Person>().ToList();
                    return record;
                };
        }
    }
}
