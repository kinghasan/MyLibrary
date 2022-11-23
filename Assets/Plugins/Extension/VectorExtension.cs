using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aya.Extension
{
    public static class VectorExtension
    {
        public static bool Inside(this Vector3 self, Vector3 selfSize, Vector3 target, Vector3 targetSize)
        {
            var selfMinX = self.x - selfSize.x;
            var selfMaxX = self.x + selfSize.x;
            var targetMinX = target.x - targetSize.x;
            var targetMaxX = target.x + targetSize.x;
            if (targetMinX < selfMaxX && targetMaxX > selfMinX || targetMaxX > selfMinX && targetMinX < selfMaxX)
            {
                var selfMinY = self.y - selfSize.y;
                var selfMaxY = self.y + selfSize.y;
                var targetMinY = target.y - targetSize.y;
                var targetMaxY = target.y + targetSize.y;
                if (targetMinY < selfMaxY && targetMaxY > selfMinY || targetMaxY > selfMinY && targetMinY < selfMaxY)
                {
                    var selfMinZ = self.z - selfSize.z;
                    var selfMaxZ = self.z + selfSize.z;
                    var targetMinZ = target.z - targetSize.z;
                    var targetMaxZ = target.z + targetSize.z;
                    if (targetMinZ < selfMaxZ && targetMaxZ > selfMinZ || targetMaxZ > selfMinZ && targetMinZ < selfMaxZ)
                        return true;
                }
            }
            return false;
        }

        public static bool Inside(this Vector2 self, Vector2 selfSize, Vector2 target, Vector2 targetSize)
        {
            var selfMinX = self.x - selfSize.x;
            var selfMaxX = self.x + selfSize.x;
            var targetMinX = target.x - targetSize.x;
            var targetMaxX = target.x + targetSize.x;
            if (targetMinX < selfMaxX && targetMaxX > selfMinX || targetMaxX > selfMinX && targetMinX < selfMaxX)
            {
                var selfMinY = self.y - selfSize.y;
                var selfMaxY = self.y + selfSize.y;
                var targetMinY = target.y - targetSize.y;
                var targetMaxY = target.y + targetSize.y;
                if (targetMinY < selfMaxY && targetMaxY > selfMinY || targetMaxY > selfMinY && targetMinY < selfMaxY)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 2D检测线段是否和包围盒相交
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static bool Inside(Vector2 start, Vector2 end, Rect rect)
        {
            var inside = start.x < rect.xMax && start.y < rect.yMax && end.x > rect.xMin && end.y > rect.yMin;
            return inside;
        }

        public static List<Vector3> GetRandomPosition(Vector3 center, Vector3 range, int count = 1)
        {
            var positionList = new List<Vector3>();
            for (var i = 0; i < count; i++)
            {
                var x = Random.Range(range.x * -1, range.x);
                var y = Random.Range(range.y * -1, range.y);
                var z = Random.Range(range.z * -1, range.z);
                var pos = new Vector3(x, y, z);
                pos += center;
                positionList.Add(pos);
            }
            return positionList;
        }

        public static Vector3 ToScreenPosition(this Vector3 worldPos)
        {
            var result = Camera.main.WorldToScreenPoint(worldPos);
            return result;
        }
    }
}
