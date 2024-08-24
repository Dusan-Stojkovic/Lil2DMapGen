using System.Collections.Generic;
using UnityEngine;

namespace MapGen
{
  public partial class Map
  {
    private static readonly (int count, int side1, int side2, Color col)[] prefabs = 
      new (int count, int side1, int side2, Color c)[]
    {
      (16, 16, 16, Color.red),
      (8, 16, 32, Color.blue),
      (4, 32, 32, Color.yellow),
      (2, 32, 64, Color.green),
      (1, 64, 64, Color.magenta)
    };
    private static (int side, bool x, bool y)[] roads =
      new (int side, bool x, bool y)[]
    {
      (4, false, true),
      (4, true, false),
      (4, false, true),
      (8, true, false),
      (8, false, true),
      (16, true, false),
      (16, false, true)
    };
    public List<Prefab> testModel()
    {
      int i = 0;
      int orientation = 0;
      List<Prefab> assets = new List<Prefab>();
      foreach ((int n, int s1, int s2, Color c) in prefabs)
      {
        for (i = 0; i < n; i++)
        {
          orientation = Chunk.rnd.Next(0, 3);
          if(orientation % 2 != 0)
            assets.Add(new Prefab(s2, s1,
              c, orientation));
          else
            assets.Add(new Prefab(s1, s2, 
              c, orientation));
        }
      }
      foreach((int side, bool x, bool y) in roads)
      {
        orientation = Chunk.rnd.Next(0, 3);
        assets.Add(new Road(side, x, y, orientation));
      }
      assets.Sort();
      return assets;
    }
  }
}
