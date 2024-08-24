using System.Collections.Generic;
using UnityEngine;

namespace MapGen
{
  public partial class Map
  {
    public Map(Texture2D tex, int length, int width, byte seed = 0)
    {
      Chunk c = new Chunk(length,
       width, seed);
      List<Prefab> uAssets = testModel();        //init Assets
      List<Chunk> uChunks = new List<Chunk>();  //emptyChunks
      uChunks.Add(c);
      _texture = tex;
      c.AssetSetup += OnAssetSetup;
      paintMap(length, width, Color.white);   
      c.placePrefabs(uAssets, uChunks);
    }
    public static Texture2D _texture;
    private void paintMap(int length, int width, Color c1, int x = 0, int y = 0)
    {
      for (int i = y; i < y + width; i++)
      {
        for (int j = x; j < x + length; j++)
        {
	 	    _texture.SetPixel( i , j , c1);
        }
      }
    }
    public void OnAssetSetup(object sender, ChunkEventArgs e)
    {
      Prefab p = new Prefab();
      if (e.data is Prefab)
      {
        p = (Prefab)e.data;
        paintMap(p.Area.Length, p.Area.Width, p.Color, p.Pos.x, p.Pos.y);
	 	_texture.Apply();
      }
    }
  }
}
