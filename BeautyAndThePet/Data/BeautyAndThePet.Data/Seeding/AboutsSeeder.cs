using BeautyAndThePet.Common;
using BeautyAndThePet.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyAndThePet.Data.Seeding
{
    public class AboutsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Abouts.Any())
            {
                return;
            }

            await dbContext.Abouts.AddAsync(new About { Description = GlobalConstants.AboutUs, Rights = GlobalConstants.Rights });
            await dbContext.SaveChangesAsync();
        }
    }
}
