using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        List<IMyPistonBase> Pistons;
        IMyBlockGroup PistonGroup;

        float FloorHeight;

        public Program()
        {
            Pistons = new List<IMyPistonBase>();
            PistonGroup = GridTerminalSystem.GetBlockGroupWithName("ElevatorPistons");
            PistonGroup.GetBlocksOfType<IMyPistonBase>(Pistons);

            Runtime.UpdateFrequency = UpdateFrequency.Update1;
        }

        public void Main(string argument, UpdateType updateSource)
        {
              
            if (argument != "")
                FloorHeight = Convert.ToSingle(argument);

            Runtime.UpdateFrequency = UpdateFrequency.None;

            foreach (IMyPistonBase Piston in Pistons)
            {
                float CurrentHeight = Piston.CurrentPosition;
                float PistonVelocity = FloorHeight - CurrentHeight;
                if (Math.Abs(PistonVelocity) > 0.01)
                {
                    Runtime.UpdateFrequency = UpdateFrequency.Update1;
                }
                else
                {
                    PistonVelocity = 0;
                }
                Piston.Velocity = ConstrainNum(PistonVelocity, -0.2f, 0.2f);
            }
        }

        float ConstrainNum(float Num, float Min, float Max)
        {
            return Math.Max(Math.Min(Max, Min), Min);
        }
    }
}
