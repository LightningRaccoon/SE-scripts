using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
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
    partial class MiningBotControl : MyGridProgram
    {

        public MiningBotControl()
        {

        }

        public void Save()
        {

        }

        private string panelName = "debugLCD";
        private string thrustGroupName = "Forward";

        List<IMyThrust> thrusters = new List<IMyThrust>();
        List<IMyBlockGroup> groups = new List<IMyBlockGroup>();


        public void Main(string argument, UpdateType updateSource)
        {

            IMyBlockGroup ThrusterGroup = GridTerminalSystem.GetBlockGroupWithName(thrustGroupName);
            List<IMyThrust> thrusters = new List<IMyThrust>();
            ThrusterGroup.GetBlocksOfType(thrusters, thruster => thruster.Enabled);

            List<IMyShipDrill> drills = new List<IMyShipDrill>();
            GridTerminalSystem.GetBlocksOfType(drills, drill =>  drill.Enabled);

            foreach(var drill in drills)
            {
                drill.Enabled = true;
                //drill.ApplyAction()
            }

            if (thrusters.Count > 0 )
            {
                WriteDataToPanel(panelName);
            }
            

            foreach (var thrust in thrusters)
            {
                thrust.Enabled = true;
                thrust.ThrustOverridePercentage = 0.02F;
            }


            List<IMyTimerBlock> timers = new List<IMyTimerBlock>();
            GridTerminalSystem.GetBlocksOfType(timers);

            timers[0].TriggerDelay = 10F;
            timers[0].StartCountdown();

            foreach (var thrust in thrusters)
            {
                thrust.ThrustOverridePercentage = 0F;
            }

            //WriteDataToPanel(panelName);

            //foreach (var thrust in thrusters)
            //{
            //    if (thrust.CurrentThrustPercentage.Equals(100F) && thrust.MaxThrust > 2000000)
            //    {
            //        thrust.ThrustOverridePercentage = 0F;
            //    }
            //    else if (thrust.MaxThrust > 2000000)
            //    {
            //        thrust.ThrustOverridePercentage = 100F;
            //    }
            //}
        }

        void WriteDataToPanel(string panelName)
        {
            IMyTextPanel lcdPanel = GridTerminalSystem.GetBlockWithName(panelName) as IMyTextPanel;

            if (lcdPanel != null)
            {
                List<string> dataList = new List<string>();
                dataList.Append("**** Found groups: " + groups.Count.ToString() + ", " + groups[0].Name + " ****\n");
                dataList.Append("-------------------------------\n");
                foreach (var thrust in thrusters)
                {
                    dataList.Append(thrust.CustomName + "\n");
                }

                lcdPanel.WriteText("");

                foreach (string dataItem in dataList)
                {
                    lcdPanel.WriteText(dataItem + "\n", true);
                }
            }
        }
    }
}
