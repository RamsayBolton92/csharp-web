// ReSharper disable VirtualMemberCallInConstructor
namespace BeautyAndThePet.Data.Models
{
    using System;
    using System.Collections.Generic;

    using BeautyAndThePet.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Pets = new HashSet<Pet>();
            this.PetImages = new HashSet<Image>();
            this.SentMessages = new HashSet<SentMessage>();
            this.ReceivedMessages = new HashSet<ReceivedMessage>();
            this.Ads = new HashSet<Ad>();
        }

        public int MyProperty { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }

        public virtual ICollection<Image> PetImages { get; set; }

        public virtual ICollection<ReceivedMessage> ReceivedMessages { get; set; }

        public virtual ICollection<SentMessage> SentMessages { get; set; }

        public virtual ICollection<Ad> Ads { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
