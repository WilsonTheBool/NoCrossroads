using System;
using System.Collections.Generic;
using UnityEngine;

public class MathAdd
{
    public static Vector3Int[] GetAllPositionInCircle(Vector3Int center, int radius)
    {
        List<Vector3Int> positions = new List<Vector3Int>(radius * radius);
        for(int x = center.x - radius; x <= center.x + radius; x++)
        {
            for (int y = center.y - radius; y <= center.y + radius; y++)
            {
                double dx = x - center.x;
                double dy = y - center.y;
                double distanceSquared = dx * dx + dy * dy;
                double radiusSquared = radius * radius;

                if (distanceSquared <= radiusSquared)
                {
                    positions.Add(new Vector3Int(x, y, 0));
                }
            }
        }

        return positions.ToArray();
    }
}

