using ForgetMeNot.App.Utils;

namespace ForgetMeNot.App.Api
{
    public class Organization
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public Address address { get; set; }
        public Hours hours { get; set; }
        public object website { get; set; }
        public object distance { get; set; }

        public class Address
        {
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string postcode { get; set; }
            public string country { get; set; }

        }

        public class Hours
        {
            public object monday { get; set; }
            public object tuesday { get; set; }
            public object wednesday { get; set; }
            public object thursday { get; set; }
            public object friday { get; set; }
            public object saturday { get; set; }
            public object sunday { get; set; }

        }

        public class Self
        {
            public string href { get; set; }

        }

    }

    public class OrganizationResponse
    {
        public Organization organization { get; set; }

    }
}
