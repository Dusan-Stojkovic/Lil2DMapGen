using System.Collections.Generic;
using UnityEngine;

namespace MapGen
{
  public class Road : Prefab
  {
    public Road() { }
    public Road(int rS, bool x, bool y, int o)
     : base(Color.black, o)
    {
      roadSide = rS;
      openAxis = (x, y);
    }
    public readonly int roadSide;
    protected override bool compatibilityTest(int length, int width)
    {
      if (openAxis.x == true)
      {
        if (roadSide < width)
        {
          Area.Width = roadSide;
          Area.Length = length;
          return true;
        }
        else
        {
          return false;
        }
      }
      else
      {
        if (roadSide < length)
        {
          Area.Width = width;
          Area.Length = roadSide;
          return true;
        }
        else
        {
          return false;
        }
      }
    }
    protected override void updatePositions(List<int> positions, Chunk mapBit)
    {
      if (Pos.x == mapBit.Pos.x)
      {
        positions[0] = -1;
        positions[1] = -1;
        positions[2] = -1;
        positions[4] = -1;
        positions[5] = -1;
        positions[6] = -1;
      }
      if (Pos.y == mapBit.Pos.y)
      {
        positions[0] = -1;
        positions[2] = -1;
        positions[3] = -1;
        positions[4] = -1;
        positions[6] = -1;
        positions[7] = -1;
      }
    }
    protected override int offsetAmount(int position)
    {
      return 0;
    }
  }
}