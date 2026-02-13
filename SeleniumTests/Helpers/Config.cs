using System;
using DotNetEnv;

namespace SeleniumTests.Helpers
{
    public static class Config
    {
        static Config()
        {
            Env.Load();
        }

        // LIVE Config--------------------------------------------------------------------------------------------------------------------

        public static string LiveOnlineApp => Environment.GetEnvironmentVariable("LIVE_URL") + ":3001/Account/Login?statusCode=0";
        public static string LiveRegistration => Environment.GetEnvironmentVariable("LIVE_URL") + ":3001";
        public static string LiveWebPortal => Environment.GetEnvironmentVariable("LIVE_URL") + ":3000";
        public static string LivePTRAX => Environment.GetEnvironmentVariable("LIVE_URL") + ":3007";
        public static string LiveBPAS => Environment.GetEnvironmentVariable("LIVE_URL") + ":3010";
        public static string LiveSYSMAN => Environment.GetEnvironmentVariable("LIVE_URL") + ":3013";

        // TEST Config--------------------------------------------------------------------------------------------------------------------------

        public static string TESTOnlineApp => Environment.GetEnvironmentVariable("TEST_URL") + ":1024/Account/Login?statusCode=0";
        public static string TESTRegistration => Environment.GetEnvironmentVariable("TEST_URL") + ":1024";
        public static string TESTWebPortal => Environment.GetEnvironmentVariable("TEST_URL") + ":1025";
        public static string TESTPTRAX => Environment.GetEnvironmentVariable("TEST_URL") + ":1023";
        public static string TESTBPAS => Environment.GetEnvironmentVariable("TEST_URL") + ":1027";
        public static string TESTSYSMAN => Environment.GetEnvironmentVariable("TEST_URL") + ":1026";
    }
}