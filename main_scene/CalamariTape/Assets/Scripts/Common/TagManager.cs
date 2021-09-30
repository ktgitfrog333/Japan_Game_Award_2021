using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Const.Tag
{
    /// <summary>
    /// タグマネージャー
    /// </summary>
    public class TagManager
    {
        /// <summary>前後にある壁</summary>
        public static readonly string VERTICAL_WALL = "VerticalWall";
        /// <summary>左右にある壁</summary>
        public static readonly string HORIZONTAL_WALL = "HorizontalWall";
        /// <summary>止まる壁</summary>
        public static readonly string WALL = "Wall";
        /// <summary>透明ブロック（縦）</summary>
        public static readonly string CLEAR_VERTICAL_WALL = "ClearVerticalWall";
        /// <summary>透明ブロック（横）</summary>
        public static readonly string CLEAR_HORIZONTAL_WALL = "ClearHorizontalWall";
        /// <summary>チュートリアルメッセージ</summary>
        public static readonly string MESSAGE = "Message";
        /// <summary>回し車の制御値</summary>
        public static readonly string MARMOT_HEALTH = "MarmotHealth";
        /// <summary>ベルトコンベア</summary>
        public static readonly string CONVEYOR = "Conveyor";
        /// <summary>滑る床</summary>
        public static readonly string ICE_PLANE = "IcePlane";
        /// <summary>プレイヤー</summary>
        public static readonly string PLAYER = "Player";
        /// <summary>ダッシュパネル</summary>
        public static readonly string DASH_PANEL = "DashPanel";
    }
}
