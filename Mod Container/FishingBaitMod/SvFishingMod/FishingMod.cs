using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SvFishingMod
{
    public sealed partial class FishingMod : Mod
    {
        public override void Entry(IModHelper helper)
        {
            Settings.HelperInstance = helper;
            Settings.MonitorInstance = Monitor;
            Settings.ConfigFilePath = "svfishmod.json";
            Settings.LoadFromFile();

            helper.ConsoleCommands.Add("sv_fishing_debug", "Enables or disables Debug mode on the SvFishingMod.\nUsage: sv_fishing_debug 0|1", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_enabled", "Enables or disables SvFishingMod.\nUsage: sv_fishing_enabled 0|1", HandleCommand);
            helper.ConsoleCommands.Add("sv_fishing_setfish", "Forces the next fishing event to give a fish with the specified id.\nUsage: sv_fishing_setfish <fish_id>\nUse sv_fishing_search to get the id of a given fish by name.\nUse -1 as the fish id to restore original game functionality.", HandleCommand);
       }

        private void HandleCommand(string command, string[] args)
        {
            if (string.Equals(command, "sv_fishing_enabled", StringComparison.OrdinalIgnoreCase))
            {
                if (args != null && args.Length > 0)
                {
                    if (string.Equals(args[0].Trim(), "1", StringComparison.Ordinal))
                        Settings.Local.DisableMod = false;
                    else if (string.Equals(args[0].Trim(), "0", StringComparison.Ordinal))
                        Settings.Local.DisableMod = true;
                }
                return;
            }

            if (string.Equals(command, "sv_fishing_setfish", StringComparison.OrdinalIgnoreCase))
            {
                if (args != null && args.Length > 0)
                {
                    if (int.TryParse(args[0].Trim(), out int fishId))
                    {
                        Settings.Local.OverrideFishType = fishId;
                        Monitor.Log(string.Format("Done. The next reeled fish will be {0}", fishId), LogLevel.Info);
                    }
                    else
                        Monitor.Log("Invalid fish id specified.", LogLevel.Info);
                }
                return;
            }
        }
    }
}