namespace BeautyAndThePet.Web.ViewModels.Causes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BeautyAndThePet.Web.Infrastructure;
    using Microsoft.AspNetCore.Http;

    public class CreateCauseInputModel : IValidatableObject
    {
        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Funds { get; set; }

        [Required]
        [Display(Name = "Bank Account")]
        public string BankAccount { get; set; }

        [Required]
        [Display(Name = "Account owner")]
        public string AccountOwner { get; set; }

        public string Creator { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Start of campaign")]
        public DateTime StartOfPeriod { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "End of campaign")]
        public DateTime EndOfPeriod { get; set; }

        [Required]
        public IEnumerable<IFormFile> Images { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartOfPeriod > this.EndOfPeriod)
            {
                yield return new ValidationResult("End date should be later than start date");
            }
        }
    }
}
