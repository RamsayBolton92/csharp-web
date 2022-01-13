namespace BeautyAndThePet.Web.ViewModels.Causes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Services.Mapping;

    public class CauseViewModel : IMapFrom<Cause>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Funds { get; set; }

        public string BankAccount { get; set; }

        public string AccountOwner { get; set; }

        public DateTime StartOfPeriod { get; set; }

        public DateTime EndOfPeriod { get; set; }

        public string CreatorId { get; set; }

        public string CreatorUserName { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<CauseImage> CauseImages { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Cause, CauseViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x => x.CauseImages.FirstOrDefault().RemoteImageUrl != null ?
                        x.CauseImages.FirstOrDefault().RemoteImageUrl :
                        "/images/causes/" + x.CauseImages.FirstOrDefault().Id + "." + x.CauseImages.FirstOrDefault().Extension));
        }
    }
}
