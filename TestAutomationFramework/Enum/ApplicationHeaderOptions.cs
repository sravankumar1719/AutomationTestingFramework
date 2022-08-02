using System.Runtime.Serialization;
using TestAutomationFramework.Utils.Custom;

namespace TestAutomationFramework.Enum
{
    public enum ApplicationHeaderOptions
    {
        [EnumMember(Value = "Products")]
        [Locator("Products",  "//button[@id = 'product-menu-toggle']")]
        Products,

        [EnumMember(Value = "Developers")]
        [Locator("Developers", "//button[@id = 'developers-menu-toggle']")]
        Developers,

        [EnumMember(Value = "Live for Teams")]
        [Locator("Live for Teams", "//a[text() = 'Live for Teams']")]
        LiveForTeams,

        [EnumMember(Value = "Pricing")]
        [Locator("Pricing", "//a[text() = 'Pricing']")]
        Pricing,

        [EnumMember(Value = "Sign in")]
        [Locator("Sign in", "//a[text() = 'Sign in']")]
        SignIn,

        [EnumMember(Value = "Free Trial")]
        [Locator("FREE TRIAL", "//a[@id= 'free-trial-link-anchor']")]
        FreeTrail
    }
}