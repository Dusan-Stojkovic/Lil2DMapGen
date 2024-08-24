using System;

namespace MapGen
{
  public partial class ChunkEventArgs : EventArgs
  {
    public Chunk data { get; set; }
  }
  public partial class Chunk : IComparable
  {
    public Chunk() { }
    public Chunk(int length, int width, int seed = 0)
    {
      if (seed == 0)
        rnd = new System.Random();
      else
        rnd = new System.Random(seed);
      Area.Length = length;
      Area.Width = width;
      Pos.x = 0;
      Pos.y = 0;
    }
    public Chunk(int length, int width, int x, int y)
    {
      Area.Length = length;
      Area.Width = width;
      Pos.x = x;
      Pos.y = y;
    }
    public static System.Random rnd;
    public (bool x, bool y) openAxis = (false, false);
    public (int x, int y) Pos;
    public (int Length, int Width) Area;
    public int getArea()
    {
      return Area.Length * Area.Width;
    }
    public int CompareTo(object obj)
    {
      Chunk chunkToCompare = obj as Chunk;
      if (chunkToCompare == null) return 1;
      else if (countAxis() == chunkToCompare.countAxis())
      {
        if (getArea() > chunkToCompare.getArea())
        {
          return -1;
        }
        else if (getArea() < chunkToCompare.getArea())
        {
          return 1;
        }
        else
        {
          return 0;
        }
      }
      else if (countAxis() > chunkToCompare.countAxis())
      {
        return -1;
      }
      else
      {
        return 1;
      }
    }
    public event EventHandler<ChunkEventArgs> AssetSetup;
    protected virtual void OnAssetSetup(Chunk c)
    {
      if (AssetSetup != null)
        AssetSetup(this, new ChunkEventArgs() { data = c });
    }
    protected int countAxis()
    {
      int count = 0;
      if (openAxis.x == true)
        ++count;
      if (openAxis.y == true)
        ++count;
      return count;
    }
  }
}