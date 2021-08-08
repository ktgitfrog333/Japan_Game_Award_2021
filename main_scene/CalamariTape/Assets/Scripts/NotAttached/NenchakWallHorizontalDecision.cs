using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const.Tag;

namespace Controller.WallHorizontal
{
    /// <summary>
    /// ネンチャク用の壁判定（横）
    /// </summary>
    public static class NenchakWallHorizontalDecision
    {
        /// <summary>
        /// 衝突したオブジェクトが透明ブロックかの判定
        /// </summary>
        /// <param name="gameObject">オブジェクト</param>
        /// <returns>透明ブロックオブジェクト情報</returns>
        public static GameObject CheckClear(GameObject gameObject)
        {
            GameObject result = null;
            if (gameObject != null && gameObject.tag.Equals(TagManager.CLEAR_HORIZONTAL_WALL))
            {
                result = gameObject;
            }

            return result;
        }

        /// <summary>
        /// オブジェクトから透明ブロック情報が取得可能かどうか
        /// </summary>
        /// <param name="gameObject">オブジェクト</param>
        /// <returns>透明ブロックの配置場所</returns>
        public static int ShowDirection(GameObject gameObject)
        {
            int result = -1;
            ClearHorizontalWall wall = gameObject.GetComponent<ClearHorizontalWall>();
            if (wall.Position == (int)WallRunHorizontalMode.TOP || wall.Position == (int)WallRunHorizontalMode.RIGHT || wall.Position == (int)WallRunHorizontalMode.BOTTOM || wall.Position == (int)WallRunHorizontalMode.LEFT)
            {
                result = wall.Position;
            }

            return result;
        }
    }
}
