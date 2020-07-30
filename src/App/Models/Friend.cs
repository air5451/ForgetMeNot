using ForgetMeNot.App.Api;

namespace ForgetMeNot.App.Models
{
    public class Friend
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string Details { get; set; }

        public string Image { get; set; }

        public string Breed { get; set; }

        public string Published { get; set; }

        public string SortProperty => Breed; 

        public string Organization { get; set; }

        public Contact Contacts { get; set; }

        public class Address
        {
            public string Address1 { get; set; }
            public object Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Postcode { get; set; }
            public string Country { get; set; }
        }

        public class Contact
        {
            public string Email { get; set; }
            public string Phone { get; set; }
            public Address Address { get; set; }
        }
    }
}
