namespace BeautyAndThePet.Data.Models
{
    using System;

    using BeautyAndThePet.Data.Common.Models;

    public class CauseImage : BaseModel<string>
    {
        public CauseImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int CauseId { get; set; }

        public virtual Cause Cause { get; set; }

        public string Extension { get; set; }

        //// The contents of the image is in the file system

        public string RemoteImageUrl { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
