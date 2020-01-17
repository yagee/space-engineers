using Sandbox.ModAPI.Ingame;
using System;
using System.Collections.Generic;

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
