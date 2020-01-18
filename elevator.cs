using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
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
  partial class Program : MyGridProgram
  {
    List<IMyPistonBase> pistons;

    IMyBlockGroup pistonGroup;

    float floorHeight;

    public Program()
    {
      pistons = new List<IMyPistonBase>();
      pistonGroup = GridTerminalSystem.GetBlockGroupWithName("ElevatorPistons");
      pistonGroup.GetBlocksOfType<IMyPistonBase>(pistons);
    }

    public void Main(string argument)
    {
      if (argument != "")
        floorHeight = Convert.ToSingle(argument);

      Runtime.UpdateFrequency = UpdateFrequency.None;
      foreach (IMyPistonBase piston in pistons)
      {
        float currentHeight = piston.CurrentPosition;
        float pistonVelocity = floorHeight - currentHeight;
        if (Math.Abs(pistonVelocity) > 0.05)
        {
          Runtime.UpdateFrequency = UpdateFrequency.Update1;
        }
        else
        {
          piston.Velocity = 0;
        }
        piston.Velocity = ConstrainNum(pistonVelocity, -1, 1);
      }
    }

    float ConstrainNum(float Num, float Min, float Max)
    {
      return Math.Max(Math.Min(Max, Num), Min);
    }
  }
}
