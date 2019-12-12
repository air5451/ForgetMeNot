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
    }
}
