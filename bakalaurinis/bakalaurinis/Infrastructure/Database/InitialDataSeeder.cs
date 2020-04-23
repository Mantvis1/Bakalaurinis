using bakalaurinis.Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace bakalaurinis.Infrastructure.Database
{
    public static class InitialDataSeeder
    {
        internal static void CreateMessageTemplates(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageTemplate>().HasData(
                new MessageTemplate
                {
                    Id = 1,
                    TitleTemplate = "Work created",
                    TextTemplate = "You created new work [work]!"
                },
                new MessageTemplate
                {
                    Id = 2,
                    TitleTemplate = "Work deleted",
                    TextTemplate = "You deleted work [work]!"
                },
                  new MessageTemplate
                  {
                      Id = 3,
                      TitleTemplate = "Schedule generation complete",
                      TextTemplate = "The system performed a new schedule generation!"
                  },
                  new MessageTemplate
                  {
                      Id = 4,
                      TitleTemplate = "New invitation received",
                      TextTemplate = "Vartotojas [user] pakvietė jus i veiką [work]!"
                  },
                  new MessageTemplate
                  {
                      Id = 5,
                      TitleTemplate = "Invitation declined",
                      TextTemplate = "Vartotojas [user] atmetė jūsų pakvietimą į renginį [work]!"
                  },
                   new MessageTemplate
                   {
                       Id = 6,
                       TitleTemplate = "Invitation accepted",
                       TextTemplate = "Vartotojas [user] priėmė jūsų pakvietimą į renginį [work]!"
                   },
                  new MessageTemplate
                  {
                      Id = 7,
                      TitleTemplate = "You have declined invitation",
                      TextTemplate = "Jūs atmetėte kvietimą vartotojo [user] pakvietimą į renginį [work]!"
                  },
                   new MessageTemplate
                   {
                       Id = 8,
                       TitleTemplate = "You have accepted invitation",
                       TextTemplate = "You have accepted [user]'s invitation to work [work]!"
                   },
                  new MessageTemplate
                  {
                      Id = 9,
                      TitleTemplate = "Invitation sent",
                      TextTemplate = "Jūs pakvietėtę [user] į veiką [work]!"
                  }
                );
        }
    }
}
