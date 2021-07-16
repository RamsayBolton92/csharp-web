namespace BeautyAndThePet.Web.ViewModels.Users
{
    using System.Collections.Generic;

    public class UserInfoViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string CreatedOn { get; set; }

        public int PetsCount { get; set; }

        public int AdsCount { get; set; }

        public int CausesCount { get; set; }

        public Dictionary<int, Dictionary<string, string>> PetsDictionary { get; set; }

        public Dictionary<string, string> AdsDictionary { get; set; }

        public Dictionary<string, string> CausesDictionary { get; set; }
    }
}
