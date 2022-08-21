namespace ForgetMeNot.App.LogOn
{
    public class B2CConstants
    {
        // Azure AD B2C Coordinates
        public static string Tenant = "forgetmenotusers.onmicrosoft.com";
        public static string AzureADB2CHostname = "forgetmenotusers.b2clogin.com";
        public static string ClientID = "f374b42b-95f4-4892-81a2-464a5b38d731";
        public static string PolicySignUpSignIn = "B2C_1_SignUpAndLogIn";
        public static string PolicyEditProfile = "B2C_1_edit_profile";
        public static string PolicyResetPassword = "B2C_1_reset";

        public static string[] Scopes = { "https://forgetmenotusers.onmicrosoft.com/api/read" };

        public static string AuthorityBase = $"https://{AzureADB2CHostname}/tfp/{Tenant}/";
        public static string AuthoritySignInSignUp = $"{AuthorityBase}{PolicySignUpSignIn}";
        public static string AuthorityEditProfile = $"{AuthorityBase}{PolicyEditProfile}";
        public static string AuthorityPasswordReset = $"{AuthorityBase}{PolicyResetPassword}";
        public static string IOSKeyChainGroup = "com.microsoft.adalcache";
    }
}
