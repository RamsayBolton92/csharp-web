namespace BeautyAndThePet.Data.Models
{
    using System;

    using BeautyAndThePet.Data.Common.Models;

    public class PetImage : BaseModel<string>
    {
        public PetImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int PetId { get; set; }

        public virtual Pet Pet { get; set; }

        public string Extension { get; set; }

        //// The contents of the image is in the file system

        public string RemoteImageUrl { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
