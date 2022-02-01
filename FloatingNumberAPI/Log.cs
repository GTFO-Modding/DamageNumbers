using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FloatingTextAPI
{
    public static class Log
    {
        private static readonly ManualLogSource logger;
        static Log()
        {
            logger = new ManualLogSource(Main.MODNAME);
            Logger.Sources.Add(logger);
        }
        public static void Verbose(object msg)
        {
            logger.LogInfo(msg);
        }

        public static void Debug(object msg)
        {
            logger.LogDebug(msg);
        }

        public static void Message(object msg)
        {
            logger.LogMessage(msg);
        }

        public static void Error(object msg)
        {
            logger.LogError(msg);
        }

        public static void Warn(object msg)
        {
            logger.LogWarning(msg);
        }
    }
}
