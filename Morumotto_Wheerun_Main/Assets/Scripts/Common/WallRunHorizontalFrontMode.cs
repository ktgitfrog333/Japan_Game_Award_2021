using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.WallHorizontal
{
    /// <summary>
    /// 横にある壁に対して横方向へ入力すると登るモードオプション
    /// </summary>
    public enum WallRunHorizontalFrontMode
    {
        /// <summary>1：右方向入力で登り、左方向で下りる</summary>
        RIGHT_IS_FRONT = 1,
        /// <summary>-1：左方向入力で登り、右方向で下りる</summary>
        LEFT_IS_FRONT = -1
    }
}
