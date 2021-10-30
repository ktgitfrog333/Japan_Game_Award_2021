using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Conveyor
{
    /// <summary>
    /// ベルトコンベアの移動方向の定義
    /// </summary>
    public enum ConveyorMoveMode
    {
        /// <summary>0：上方向</summary>
        UP = 0,
        /// <summary>1：右方向</summary>
        RIGHT = 1,
        /// <summary>2：下方向</summary>
        DOWN = 2,
        /// <summary>3：左方向</summary>
        LEFT = 3,
        /// <summary>4：前方向</summary>
        FORWARD = 4,
        /// <summary>5：後方向</summary>
        BACK = 5
    }
}
