using ForgetMeNot.App.Api;

namespace ForgetMeNot.App.Models
{
    public class Organization
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Addr { get; set; }
        public Hours Hrs { get; set; }
        public object Website { get; set; }
        public object Distance { get; set; }

        public class Address
        {
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Postcode { get; set; }
            public string Country { get; set; }

        }

        public class Hours
        {
            public object Monday { get; set; }
            public object Tuesday { get; set; }
            public object Wednesday { get; set; }
            public object Thursday { get; set; }
            public object Friday { get; set; }
            public object Saturday { get; set; }
            public object Sunday { get; set; }

        }

    }
}
