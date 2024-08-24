using System.Collections.Generic;
using System;
using UnityEngine;

namespace MapGen
{
  public partial class Chunk
  {
    public void placePrefabs(List<Prefab> uAssets, List<Chunk> mapBits)
    {
      if (uAssets.Count == 0)
      {
        return;
      }
      else
      {
        Prefab asset = uAssets[0];
        uAssets.Remove(asset);
        Chunk mapBit = mapBits[0];
        try
        {
          asset.getStartLocation(mapBit);
          asset.FillCutOrder(mapBit);
          enqueChunks(getChunks(mapBit, asset), mapBits);
          mapBits.Remove(mapBit);
          OnAssetSetup(asset);
          placePrefabs(uAssets, mapBits);
        }
        catch (Exception ex)
        {
          Debug.Log(ex.Message);
          uAssets.Remove(asset);
          placePrefabs(uAssets, mapBits);
        }
      }
    }
    protected int calibrateRange((int L, int R) range, int side)
    {
      range.L += side;
      range.R -= side;
      if (range.L > range.R)
      {
        return range.R;
      }
      else
      {
        return calibrateRange(range, side);
      }
    }
    private List<Chunk> getChunks(Chunk mapBit, Prefab asset)
    {
      Chunk temp;
      List<Chunk> chunkList = new List<Chunk>();
      for (int i = 0; i < asset.cutOrder.GetLength(0); i++)
      {
        temp = new Chunk(asset.cutOrder[i].endX - asset.cutOrder[i].startX,
          asset.cutOrder[i].endY - asset.cutOrder[i].startY,
          asset.cutOrder[i].startX, asset.cutOrder[i].startY);
        temp.openAxis = asset.openAxis;
        chunkList.Add(temp);
      }
      return chunkList;
    }
    private void enqueChunks(List<Chunk> currentSplits, List<Chunk> chunkList)
    {
      foreach (Chunk currentSplit in currentSplits)
      {
        chunkList.Add(currentSplit);
      }
      chunkList.Sort();
    }
  }
}
