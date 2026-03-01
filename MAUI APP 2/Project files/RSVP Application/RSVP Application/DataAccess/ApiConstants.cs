using System;
using System.Collections.Generic;
using System.Text;

namespace RSVP_Application.DataAccess
{
    internal class ApiConstants
    {
        public const string DeviceUrl = "localhost";
        public const string Port = "57825";

        public static string BaseUrl = $"http://{DeviceUrl}:{Port}/api/";

        // Auth Credentials
        public const string AuthUsername = "Deardorff01";
        public const string AuthPassword = "Password1";
    }
}