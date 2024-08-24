using UnityEngine;

namespace MapGen
{
  public partial class Prefab : Chunk
  {
    /*
      Prefab is a class that expresses any upgrade of the base chunk class
      right now it is the main splitter of chunks 
      which means that it has the base logic implemented into it.
     */
    public Prefab() { }
    public Prefab(Color c, int o)
    {
      Color = c;
      orientation = orientations[o];
    }
    public Prefab(int length, int width, Color c, int o)
      : base(length, width, 0, 0)
    {
      Color = c;
      orientation = orientations[o];
    }
    public Color Color;

    //The default schema of chunk splitting
    protected static readonly (byte startX, byte endX, byte startY, byte endY)[] cutLocations
     = new (byte, byte, byte, byte)[]
    {
      (0,1,2,3),    //top-left
      (0,1,1,2),    //left
      (0,1,0,1),    //bottom-left
      (1,2,0,1),    //bottom
      (2,3,0,1),    //bottom-right
      (2,3,1,2),    //right
      (2,3,2,3),    //top-right
      (1,2,2,3)     //top
    };
    public (int startX, int endX, int startY, int endY)[] cutOrder;
    //For 4-sided prefabs
    protected byte orientation;
    protected static readonly byte[] orientations = { 0, 2, 4, 6 };

  }
}
