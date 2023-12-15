using StardewModdingAPI;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace SvFishingMod
{
    [DataContract]
    public class Settings
    {
        private static Settings _localInstance = null;
        private int _overrideFishType = -1;

        private Settings() { }

        public static Settings DefaultEnabled
        {
            get
            {
                Settings output = new Settings()
                {
                    DisableMod = false,
                };


                return output;
            }
        }

        public static Settings DefaultDisabled
        {
            get
            {
                Settings output = new Settings()
                {
                    DisableMod = true,
                };

                return output;
            }
        }

        public static string ConfigFilePath { get; set; } = null;
        public static IModHelper HelperInstance { get; set; } = null;
        public static IMonitor MonitorInstance { get; set; } = null;

        public static Settings Local
        {
            get
            {
                if (_localInstance == null)
                {
                    if (string.IsNullOrWhiteSpace(ConfigFilePath))
                        _localInstance = Settings.DefaultEnabled;
                    else
                        _localInstance = LoadFromFile(ConfigFilePath);
                }

                return _localInstance;
            }
            set
            {
                _localInstance = value;
            }
        }
        [DataMember] public bool DisableMod { get; set; } = false;

        [DataMember]
        public int OverrideFishType
        {
            get
            {
                if (_overrideFishType == -1)
                    return -1;
                if (_overrideFishType < 128)
                    return 128;

                return _overrideFishType;
            }
            set
            {
                _overrideFishType = value;
            }
        }

        public static Settings LoadFromFile()
        {
            if (HelperInstance == null)
                throw new NullReferenceException("No SMAPI Mod Helper defined before loading settings file.");

            return LoadFromFile(ConfigFilePath);
        }

        public static Settings LoadFromFile(string filename)
        {
            Settings output;

            try
            {
                output = HelperInstance.Data.ReadJsonFile<Settings>(ConfigFilePath);
                if (output == null)
                    output = new Settings();
                output.SaveToFile(filename);
                MonitorInstance.Log(string.Format("Settings loaded using SMAPI from {0}", filename), LogLevel.Trace);
            }
            catch (Exception ex)
            {
                if (MonitorInstance != null)
                    MonitorInstance.Log(string.Format("[SvFishingMod] Unable to load settings from specified filename {0}", filename, ex.GetType().Name, ex.Message), LogLevel.Error);
                output = new Settings(); // Load defaults
            }

            return output;
        }

        public void SaveToFile()
        {
            SaveToFile(ConfigFilePath);
        }

        public void SaveToFile(string filename)
        {
            if (HelperInstance == null)
                throw new NullReferenceException("No SMAPI Mod Helper defined before saving settings file.");

            HelperInstance.Data.WriteJsonFile<Settings>(filename, this);
        }
    }
}