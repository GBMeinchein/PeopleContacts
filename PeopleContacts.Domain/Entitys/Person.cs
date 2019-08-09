using PeopleContacts.Domain.Entitys;
using System;
using System.Collections.Generic;

namespace PeopleContacts.Domain
{
    public class Person
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public IList<Contact> Contacts { get; set; }
    }
}
