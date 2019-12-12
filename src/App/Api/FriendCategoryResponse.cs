using System.Collections.Generic;

namespace ForgetMeNot.App.Api.Types
{

    public class Self
    {
        public string href { get; set; }
    }

    public class Breeds
    {
        public string href { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public Breeds breeds { get; set; }
    }

    public class Type
    {
        public string name { get; set; }
        public List<object> coats { get; set; }
        public List<string> colors { get; set; }
        public List<string> genders { get; set; }
        public Links _links { get; set; }
    }

    public class FriendCategoryResponse
    {
        public List<Type> types { get; set; }
    }
}
