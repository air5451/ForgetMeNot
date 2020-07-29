using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace ForgetMeNot.App.Droid
{
    [Activity]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataHost = "auth",
        DataScheme = "msal9aeaf003-678b-464f-b587-ae2516be77ee")]
    public class MsalActivity : BrowserTabActivity
    {
    }
}