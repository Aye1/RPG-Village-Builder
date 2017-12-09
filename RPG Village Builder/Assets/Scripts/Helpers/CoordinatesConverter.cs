using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class CoordinatesConverter
    {
        public static int cellSize = 8;
        public static Vector2 RealToMapCoordinates(Vector2 pos)
        {
            if (pos == null)
            {
                return new Vector2(-1,-1);
            }
            return new Vector2(pos.x /cellSize, pos.y / cellSize);
        }
        public static Vector3 RealToMapCoordinates(Vector3 pos)
        {
            if (pos == null)
            {
                return new Vector3(-1, -1, -1);
            }
            return new Vector3((int)(pos.x / cellSize), (int)(pos.y / cellSize), (int)(pos.z/ cellSize));
        }
    }
}