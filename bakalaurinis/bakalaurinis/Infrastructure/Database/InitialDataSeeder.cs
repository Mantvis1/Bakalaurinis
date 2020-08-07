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
                      TextTemplate = "You got inivtation from [user] to work [work]!"
                  },
                  new MessageTemplate
                  {
                      Id = 5,
                      TitleTemplate = "Invitation declined",
                      TextTemplate = "[user] declined your invitation to work [work]!"
                  },
                   new MessageTemplate
                   {
                       Id = 6,
                       TitleTemplate = "Invitation accepted",
                       TextTemplate = "[user] accepted your invitation to work [work]!"
                   },
                  new MessageTemplate
                  {
                      Id = 7,
                      TitleTemplate = "You have declined invitation",
                      TextTemplate = "You have declined [user]'s invitation to work [work]!"
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
