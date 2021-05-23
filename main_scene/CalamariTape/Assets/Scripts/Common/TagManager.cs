using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Const.Tag
{
    /// <summary>
    /// タグマネージャー
    /// </summary>
    public class TagManager : MonoBehaviour
    {
        /// <summary>前後にある壁</summary>
        public static readonly string VERTICAL_WALL = "VerticalWall";
        /// <summary>左右にある壁</summary>
        public static readonly string HORIZONTAL_WALL = "HorizontalWall";
        /// <summary>止まる壁</summary>
        public static readonly string WALL = "Wall";
    }
}
