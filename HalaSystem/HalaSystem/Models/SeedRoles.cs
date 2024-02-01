using Microsoft.AspNetCore.Identity;

namespace HalaSystem.Models
{
    public class SeedRoles
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            // Pobranie menedżera ról z dostawcy usług
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            // Tablica z nazwami ról do utworzenia
            string[] roles = new string[] { "Administrator", "Manager" };

            // Inicjalizacja listy nowych ról
            var newrolelist = new List<IdentityRole>();

            // Iteracja przez tablicę ról
            foreach (string role in roles)
            {
                // Sprawdzenie, czy rola "Administrator" już istnieje
                bool exists = await roleManager.RoleExistsAsync(role);

                // Jeśli rola nie istnieje, dodaj ją do listy nowych ról
                if (!exists)
                {
                    newrolelist.Add(new IdentityRole(role));
                }
            }

            // Iteracja przez listę nowych ról i utworzenie ich w systemie Identity
            foreach (var r in newrolelist)
            {
                await roleManager.CreateAsync(r);
            }
        }
    }
}
