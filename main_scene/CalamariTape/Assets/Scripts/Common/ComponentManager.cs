using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Const.Component
{
    /// <summary>
    /// コンポネント名を管理
    /// </summary>
    public static class ComponentManager
    {
        /// <summary>MoveVerticalWallコンポーネント</summary>
        public static readonly string MOVE_WALLS = "MoveWalls";
        /// <summary>CalamariStateコンポーネント</summary>
        public static readonly string CALAMARI_STATE = "CalamariState";
        /// <summary>回し車の制御値</summary>
        public static readonly string MARMOT_HEALTH = "MarmotHealth";
        /// <summary>回し車</summary>
        public static readonly string MARMOT_WHEEL = "MarmotWheel";
        /// <summary>キャラクターコントローラー</summary>
        public static readonly string CHARACTER_CONTROLLER = "CharacterController";
        /// <summary>カラマリモードの操作</summary>
        public static readonly string CALAMARI_MOVE_CONTROLLER = "CalamariMoveController";
        /// <summary>ネンチャクモードの操作</summary>
        public static readonly string NENCHAK_MOVE_CONTROLLER = "NenchakMoveController";
        /// <summary>ツルツルモードの操作</summary>
        public static readonly string TSURUTSURU_MOVE_CONTROLLER = "TsuruTsuruMoveController";
        /// <summary>ベルトコンベア上でプレイヤーを動かす</summary>
        public static readonly string CONVEYOR_MOVE_CHARACTER = "ConveyorMoveCharacter";
        /// <summary>回転リング</summary>
        public static readonly string CIRCLE_RING = "CircleRing";
    }
}
