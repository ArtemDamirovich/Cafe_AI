using Cafe_AI.Core.Enums;

namespace Cafe_AI1.Components.Layout
{
    public partial class MainLayout
    {
        private UserRole CurrentUserRole { get; set; } = UserRole.Admin;

        private void SetRole(UserRole role)
        {
            CurrentUserRole = role;
            StateHasChanged();
        }
        private string GetRoleName(UserRole role)
        {
            return role switch
            {
                UserRole.Admin => "Администратор",
                UserRole.Chef => "Повар",
                UserRole.Waiter => "Официант",
                _ => "Гость"
            };
        }
    }
}
