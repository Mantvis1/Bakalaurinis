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
                    TitleTemplate = "Veiklos sukūrimas",
                    TextTemplate = "Jūs sukūrėte nauja veiklą [activity]!"
                },
                new MessageTemplate
                {
                    Id = 2,
                    TitleTemplate = "Veiklos šalinimas",
                    TextTemplate = "Jūs pašalinote veiklą [activity]!"
                },
                  new MessageTemplate
                  {
                      Id = 3,
                      TitleTemplate = "Tvarkataščio generavimas atliktas",
                      TextTemplate = "Sistema atliko naują tvarkaraščio generavimą!"
                  },
                  new MessageTemplate
                  {
                      Id = 4,
                      TitleTemplate = "Naujas kvietimas į renginį",
                      TextTemplate = "Vartotojas [user] pakvietė jus i veiką [activity]!"
                  },
                  new MessageTemplate
                  {
                      Id = 5,
                      TitleTemplate = "Kvietimas atmestas",
                      TextTemplate = "Vartotojas [user] atmetė jūsų pakvietimą į renginį [activity]!"
                  },
                   new MessageTemplate
                   {
                       Id = 6,
                       TitleTemplate = "Kvietimas priimtas",
                       TextTemplate = "Vartotojas [user] priėmė jūsų pakvietimą į renginį [activity]!"
                   }
                );
        }
    }
}
