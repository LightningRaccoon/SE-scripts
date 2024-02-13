using Sandbox.Game.EntityComponents;
using Sandbox.Game.GameSystems;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    public class LCD_Control : MyGridProgram
    {
        public LCD_Control()
        {

        }

        public void Save()
        {

        }

        private string panelName = "MyLCD";
        private string thrustGroupName = "Forward";
        public void Main(string argument, UpdateType updateSource)
        {
            IMyBlockGroup group = GridTerminalSystem.GetBlockGroupWithName(panelName);
            List<IMyTextPanel> panels = new List<IMyTextPanel>();
            group.GetBlocksOfType(panels, panel => panel.Enabled);

            foreach (var panel in panels)
            {
                panel.BackgroundColor = Color.LightBlue;
                panel.WriteText("Booting up debug....");
            }

            IMyBlockGroup groupT = GridTerminalSystem.GetBlockGroupWithName(thrustGroupName);
            List<IMyThrust> thrusters = new List<IMyThrust>();
            group.GetBlocksOfType(thrusters, thruster => thruster.Enabled);

            if (thrusters.Count > 0)
            {
                panels[0].BackgroundColor = Color.Gray;
                panels[0].WriteText("Found thrusters");
            }

            foreach (var thrust in thrusters)
            {
                thrust.Enabled = true;
                thrust.ThrustOverridePercentage = 0.5F;
            }


            List<IMyTimerBlock> timers = new List<IMyTimerBlock>();
            GridTerminalSystem.GetBlocksOfType(timers);

            timers[0].TriggerDelay = 5F;
            timers[0].StartCountdown();

        }
    }
}