namespace MauiAccelerator.Core.Constants;

public static class AppConstants
{
    public const string ApiLoggingCategory = "Api Logging";
    
    public const int DefaultTimeoutInSeconds = 60;
    
    public const string AppSettingsNamespace = "MauiAccelerator.appsettings.json";
    
    public static class Alerts
    {
        public const string Yes = "Yes";
    
        public const string No = "No";
    
        public const string OK = "OK";
        
        public static class Errors
        {
            public const string ErrorHeading = "Error";

            public const string LoginErrorContent = "Email and password needs to be set";
            
            public const string LoginFailedErrorContent = "Email or password is not correct. Please try it again.";

            public const string TodoItemErrorHeading = "Name Required";

            public const string TodoItemErrorContent = "Please enter a name for the todo item.";
        }
    }

    public static class Connectivity
    {
        public const string ConnectionErrorMessage = "Internet connection appears to be offline";
    }

    public static class Navigation
    {
        public const string NavStringFormat = "{0}{1}";
        
        public const string RootPagePrefix = "///";
        
        public const string NavBackPrefix = "..";
    }

    public static class NavParameters
    {
        public const string ItemKey = "Item";
    }

    public static class SecureStorage
    {
        public const string AccessTokenKey = "access_token";
        
        public const string RefreshTokenKey = "refresh_token";

        public const string ExpiryDateKey = "exp";
    }
}