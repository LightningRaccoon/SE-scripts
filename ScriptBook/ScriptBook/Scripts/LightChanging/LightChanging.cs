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
    partial class LightChanging : MyGridProgram
    {
        // This file contains your actual script.
        //
        // You can either keep all your code here, or you can create separate
        // code files to make your program easier to navigate while coding.
        //
        // In order to add a new utility class, right-click on your project, 
        // select 'New' then 'Add Item...'. Now find the 'Space Engineers'
        // category under 'Visual C# Items' on the left hand side, and select
        // 'Utility Class' in the main area. Name it in the box below, and
        // press OK. This utility class will be merged in with your code when
        // deploying your final script.
        //
        // You can also simply create a new utility class manually, you don't
        // have to use the template if you don't want to. Just do so the first
        // time to see what a utility class looks like.
        // 
        // Go to:
        // https://github.com/malware-dev/MDK-SE/wiki/Quick-Introduction-to-Space-Engineers-Ingame-Scripts
        //
        // to learn more about ingame scripts.

        public LightChanging()
        {

        }

        public void Save()
        {

        }

        private readonly Color _colorRed = new Color(255, 0, 0);
        private readonly Color _colorWhite = new Color(255, 255, 255);

        public void Main(string argument, UpdateType updateSource)
        {
            List<IMyLightingBlock> lights = new List<IMyLightingBlock>();
            GridTerminalSystem.GetBlocksOfType(lights);

            List<IMySoundBlock> soundBlocks = new List<IMySoundBlock>();
            GridTerminalSystem.GetBlocksOfType(soundBlocks);

            foreach (var light in lights)
            {
                if (light.Color == _colorWhite)
                {
                    light.SetValue("Color", _colorRed);
                    light.Intensity = 15;
                    light.BlinkIntervalSeconds = 0.5F;
                    light.BlinkLength = 0.5F;
                    foreach (var soundBlock in soundBlocks)
                    {
                        soundBlock.Range = 500;
                        soundBlock.Volume = 100;
                        soundBlock.SelectedSound = "Alert 1";
                        soundBlock.LoopPeriod = 360;
                        soundBlock.Play();
                    }
                }
                else
                {
                    light.SetValue("Color", _colorWhite);
                    light.Intensity = 13;
                    light.BlinkIntervalSeconds = 0;
                    foreach (var soundBlock in soundBlocks)
                    {
                        soundBlock.Stop();
                    }
                }
            }
        }
    }
}
