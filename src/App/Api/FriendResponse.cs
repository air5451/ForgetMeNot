﻿using System;
using System.Collections.Generic;

namespace ForgetMeNot.App.Api
{
    public class Breeds
    {
        public string primary { get; set; }
        public string secondary { get; set; }
        public bool mixed { get; set; }
        public bool unknown { get; set; }
    }

    public class Colors
    {
        public string primary { get; set; }
        public string secondary { get; set; }
        public object tertiary { get; set; }
    }

    public class Attributes
    {
        public bool spayed_neutered { get; set; }
        public bool house_trained { get; set; }
        public object declawed { get; set; }
        public bool special_needs { get; set; }
        public bool shots_current { get; set; }
    }

    public class Environment
    {
        public bool? children { get; set; }
        public bool? dogs { get; set; }
        public bool? cats { get; set; }
    }

    public class Address
    {
        public string address1 { get; set; }
        public object address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
    }

    public class Contact
    {
        public string email { get; set; }
        public string phone { get; set; }
        public Address address { get; set; }
    }

    public class SelfLink
    {
        public string href { get; set; }
    }

    public class TypeLink
    {
        public string href { get; set; }
    }

    public class OrganizationLink
    {
        public string href { get; set; }
    }

    public class Links
    {
        public SelfLink self { get; set; }
        public TypeLink type { get; set; }
        public OrganizationLink organization { get; set; }
    }

    public class Animal
    {
        public int id { get; set; }
        public string organization_id { get; set; }
        public string url { get; set; }
        public string type { get; set; }
        public string species { get; set; }
        public Breeds breeds { get; set; }
        public Colors colors { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string size { get; set; }
        public string coat { get; set; }
        public Attributes attributes { get; set; }
        public Environment environment { get; set; }
        public List<object> tags { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<Photo> photos { get; set; }
        public string status { get; set; }
        public DateTime status_changed_at { get; set; }
        public DateTime published_at { get; set; }
        public double distance { get; set; }
        public Contact contact { get; set; }
        public Links _links { get; set; }
    }

    public class Next
    {
        public string href { get; set; }
    }

    public class Links2
    {
        public Next next { get; set; }
    }

    public class Pagination
    {
        public int count_per_page { get; set; }
        public int total_count { get; set; }
        public int current_page { get; set; }
        public int total_pages { get; set; }
        public Links2 _links { get; set; }
    }

    public class FriendResponse
    {
        public List<Animal> animals { get; set; }
        public Pagination pagination { get; set; }
    }
}
