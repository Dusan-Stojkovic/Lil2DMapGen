using System;
using System.Collections.Generic;
using System.Linq;

namespace MapGen
{
  public partial class Prefab
  {
    protected virtual bool compatibilityTest(int length, int width)
    {
      if (length > Area.Length && width > Area.Width)
      {
        return true;
      }
      else
      {
        return false;
      }
    }
    public void getStartLocation(Chunk mapBit)
    {
      if (compatibilityTest(mapBit.Area.Length, mapBit.Area.Width))
      {
        Pos = mapBit.Pos;
        (int L, int R) rangeX = (mapBit.Pos.x, mapBit.Pos.x + mapBit.Area.Length);
        (int L, int R) rangeY = (mapBit.Pos.y, mapBit.Pos.y + mapBit.Area.Width);
        if (openAxis.x)
        {
          Pos.y = calibrateRange(rangeY, Area.Width);
          mapBit.openAxis = openAxis;
        }
        else if (openAxis.y)
        {
          Pos.x = calibrateRange(rangeX, Area.Length);
          mapBit.openAxis = openAxis;
        }
        else
        {
          Pos.x = calibrateRange(rangeX, Area.Length);
          Pos.y = calibrateRange(rangeY, Area.Width);
        }
      }
      else
      {
        Exception ex = new Exception("Prefab.getStartLocation: Chunk too small for prefab.");
        throw ex;
      }
    }
    protected virtual void updatePositions(List<int> positions, Chunk mapBit)
    {
      if (Pos.x == mapBit.Pos.x)
      {
        positions[0] = -1;
        positions[1] = -1;
        positions[2] = -1;
      }
      if (Pos.y == mapBit.Pos.y)
      {
        positions[2] = -1;
        positions[3] = -1;
        positions[4] = -1;
      }
      if (Pos.x + Area.Length == mapBit.Pos.x + mapBit.Area.Length)
      {
        positions[4] = -1;
        positions[5] = -1;
        positions[6] = -1;
      }
      if (Pos.y + Area.Width == mapBit.Pos.y + mapBit.Area.Width)
      {
        positions[6] = -1;
        positions[7] = -1;
        positions[0] = -1;
      }
    }
    protected void setupSplit(Chunk mapBit)
    {
      List<int> positions = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
      updatePositions(positions, mapBit);
      cutOrder = new (int, int, int, int)[(from n in positions
                                                where n != -1
                                                select n).Count()];
      setupOrientation(positions);
    }
    public void FillCutOrder(Chunk mapBit)
    {
      setupSplit(mapBit);  //Prepare cutOrder
      for (int i = 0; i < cutOrder.GetLength(0); i++)
      {
        updateValue(mapBit.Pos.x, mapBit.Area.Length, Pos.x, Area.Length, ref cutOrder[i].startX);
        updateValue(mapBit.Pos.x, mapBit.Area.Length, Pos.x, Area.Length, ref cutOrder[i].endX);
        updateValue(mapBit.Pos.y, mapBit.Area.Width, Pos.y, Area.Width, ref cutOrder[i].startY);
        updateValue(mapBit.Pos.y, mapBit.Area.Width, Pos.y, Area.Width, ref cutOrder[i].endY);
      }
    }
    protected void updateValue(int cPoint, int cEdge, int pos, int area, ref int position)
    {
      switch (position)
      {
        case 0:
          position = cPoint + offsetAmount(position);
          break;
        case 1:
          position = pos + offsetAmount(position);
          break;
        case 2:
          position = pos + area + offsetAmount(position);
          break;
        case 3:
          position = cPoint + cEdge + offsetAmount(position);
          break;
      }
    }
    protected void setupOrientation(List<int> positions)
    {
      int i = 0;
      int clock = orientation;
      do
      {
        if (clock == positions.Count)
        {
          clock = 0;
        }
        else if (positions[clock] == -1)
        {
          clock++;
        }
        else
        {
          cutOrder[i] = (cutLocations[clock].startX,
            cutLocations[clock].endX, cutLocations[clock].startY,
            cutLocations[clock].endY);
          i++;
          clock++;
        }
      } while (clock != orientation);
    }
    protected virtual int offsetAmount(int position)
    {
      if (position == 1 || position == 3)
        return -3;
      else
        return 3;
    }
  }
}