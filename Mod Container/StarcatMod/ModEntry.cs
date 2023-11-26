using System;
using System.Threading;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace StarcatMod
{
    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        /// Custom Console Commands
        // Test Command For Changing Fish Type
        const string setNextFish = "set_next_fish";
        const string setNextHelp = $"""
            Sets the fish that will be caught the next time the player fishes.

            Usage: {setNextFish} [fishId]
            - "fishId" represents the id of the fish to catch.
            """;

        // Lists Important Fish And Their Ids To The Console
        const string getFishIds = "get_fish_ids";
        const string getFishHelp = $"""
            Prints a list of all important fish and their ids.
            """;

        // Lists Baits And Their Ids To The Console
        const string getBaitIds = "get_bait_ids";
        const string getBaitHelp = $"""
            Prints a list of all baits and their ids.
            """;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            // Add Console Commands
            helper.ConsoleCommands.Add("set_next_fish", "setNextHelp", this.SetNextFish);
            //helper.ConsoleCommands.Add(getFishIds, getFishHelp, GetFishIds);
            //helper.ConsoleCommands.Add(getBaitIds, getBaitHelp, GetBaitIds);

            // Prints All Inputs To Console
            helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }


        /*********
        ** Private methods
        *********/

        // Method For Console Command "set_next_fish"
        private void SetNextFish(string cmd, string[] args)
        {
            // TEST
            this.Monitor.Log("TEST TEST TEST");

            // Check That An Id Argument Has Been Passed In
            if (args != null && args.Length > 0)
            {
                // Check That The Id Is An Integer
                if (int.TryParse(args[0].Trim(), out int id)) 
                {
                    GuaranteeFish(id);
                }
                else
                {
                    this.Monitor.Log("Invalid Id Was Entered");
                }
            }
        }

        // Method That Gaurantees A Certain Fish Will Be Caught On The Next Attempt
        private void GuaranteeFish(int fishId)
        {
            this.Monitor.Log($"This will guarantee that the next fish caught is {fishId}");
        }

        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            // print button presses to the console window
            this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
        }
    }
}
