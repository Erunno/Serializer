using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Serializer;

namespace UnreflectedSerializer
{
    class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
    }

    class Country
    {
        public string Name { get; set; }
        public int AreaCode { get; set; }
    }

    class PhoneNumber
    {
        public Country Country { get; set; }
        public int Number { get; set; }
    }

    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address HomeAddress { get; set; }
        public Address WorkAddress { get; set; }
        public Country CitizenOf { get; set; }
        public PhoneNumber MobilePhone { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            RootDescriptor<Person> rootDesc = GetPersonDescriptor();

            var czechRepublic = new Country { Name = "Czech Republic", AreaCode = 420 };
            var person = new Person
            {
                FirstName = "Pavel",
                LastName = "Jezek",
                HomeAddress = new Address { Street = "Patkova", City = "Prague" },
                WorkAddress = new Address { Street = "Malostranske namesti", City = "Prague" },
                CitizenOf = czechRepublic,
                MobilePhone = new PhoneNumber { Country = czechRepublic, Number = 123456789 }
            };

            rootDesc.Serialize(Console.Out, person);
        }

        static RootDescriptor<Person> GetPersonDescriptor()
        {
            //Address
            var addressDes = new RootDescriptor<Address>("Address");
            addressDes.AddNewDescriptor(new SimpleTypeDescriptor<Address, string>("Street", a => a.Street));
            addressDes.AddNewDescriptor(new SimpleTypeDescriptor<Address, string>("City", a => a.City));

            //Country
            var countryDes = new RootDescriptor<Country>("Country");
            countryDes.AddNewDescriptor(new SimpleTypeDescriptor<Country, string>("Name", c => c.Name));
            countryDes.AddNewDescriptor(new SimpleTypeDescriptor<Country, int>("AreaCode", c => c.AreaCode));

            //PhoneNumber
            var phoneNumberDesc = new RootDescriptor<PhoneNumber>("PhoneNumber");
            phoneNumberDesc.AddNewDescriptor(new SimpleTypeDescriptor<PhoneNumber, int>("Number", pn => pn.Number));
            phoneNumberDesc.AddNewDescriptor(new ComplexPropertyDescriptor<PhoneNumber, Country>("Country",countryDes, pn => pn.Country));

            //Person
            var personDesc = new RootDescriptor<Person>("Person");
            personDesc.AddNewDescriptor(new SimpleTypeDescriptor<Person, string>("FirstName", p => p.FirstName));
            personDesc.AddNewDescriptor(new SimpleTypeDescriptor<Person, string>("LastName", p => p.LastName));
            personDesc.AddNewDescriptor(new ComplexPropertyDescriptor<Person, Address>("HomeAddress", addressDes, p => p.HomeAddress));
            personDesc.AddNewDescriptor(new ComplexPropertyDescriptor<Person, Address>("WorkAddress", addressDes, p => p.WorkAddress));
            personDesc.AddNewDescriptor(new ComplexPropertyDescriptor<Person, Country>("CitizenOf", countryDes, p => p.CitizenOf));
            personDesc.AddNewDescriptor(new ComplexPropertyDescriptor<Person, PhoneNumber>("MobilePhone", phoneNumberDesc, p => p.MobilePhone));

            return personDesc;
        }
    }
}
