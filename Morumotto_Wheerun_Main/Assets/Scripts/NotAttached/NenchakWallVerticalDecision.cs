using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const.Tag;

namespace Controller.WallVertical
{
    /// <summary>
    /// ネンチャク用の壁判定（縦）
    /// </summary>
    public static class NenchakWallVerticalDecision
    {
        /// <summary>
        /// 衝突したオブジェクトが透明ブロックかの判定
        /// </summary>
        /// <param name="gameObject">オブジェクト</param>
        /// <returns>透明ブロックオブジェクト情報</returns>
        public static GameObject CheckClear(GameObject gameObject)
        {
            GameObject result = null;
            if (gameObject != null && gameObject.tag.Equals(TagManager.CLEAR_VERTICAL_WALL))
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
            ClearVerticalWall wall = gameObject.GetComponent<ClearVerticalWall>();
            if (wall.Position == (int)WallRunVerticalMode.TOP || wall.Position == (int)WallRunVerticalMode.RIGHT || wall.Position == (int)WallRunVerticalMode.BOTTOM || wall.Position == (int)WallRunVerticalMode.LEFT)
            {
                result = wall.Position;
            }

            return result;
        }
    }
}
