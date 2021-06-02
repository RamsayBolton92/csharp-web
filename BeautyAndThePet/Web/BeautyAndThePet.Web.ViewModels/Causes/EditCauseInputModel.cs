namespace BeautyAndThePet.Web.ViewModels.Causes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using BeautyAndThePet.Data.Models;
    using BeautyAndThePet.Data.Models.Enumerations;
    using BeautyAndThePet.Services.Mapping;

    public class EditCauseInputModel : IMapFrom<Cause>, IValidatableObject
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public double Funds { get; set; }

        [Display(Name = "Bank account")]
        public string BankAccount { get; set; }

        [Display(Name = "Account owner")]
        public string AccountOwner { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartOfPeriod { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndOfPeriod { get; set; }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            if (this.StartOfPeriod > this.EndOfPeriod)
            {
                yield return new ValidationResult("End date should be later than start date");
            }
        }
    }
}
