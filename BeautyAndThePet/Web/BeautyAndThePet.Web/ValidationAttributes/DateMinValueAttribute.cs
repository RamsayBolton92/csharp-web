namespace BeautyAndThePet.Services
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateMinValueAttribute : ValidationAttribute
    {
        public DateMinValueAttribute(DateTime start)
        {
            this.Start = start;

            this.ErrorMessage = $"Date should be later than {start}";
        }

        public DateTime Start { get; }

        public override bool IsValid(object value)
        {
            if (value is DateTime endValue)
            {
                if (endValue > this.Start)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

