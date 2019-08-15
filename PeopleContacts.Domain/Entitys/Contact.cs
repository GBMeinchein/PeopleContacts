using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleContacts.Domain.Entitys
{
    public class Contact
    {
        public long Id { get; set; }
        public string TipoContato { get; set; }
        public string EnderecoContato { get; set; }
        public long PersonId { get; set; }
    }
}
