using UnityEngine;

namespace Case_1
{
    public static class GridExtensions
    {
        public static Vector2Int GetGridPosFromWorld(this Vector3 worldPos, float size)
        {
            int x = Mathf.FloorToInt((worldPos.x / size));
            int y = Mathf.FloorToInt((worldPos.z / size));

            return new Vector2Int(x, y);
        }
    }
}