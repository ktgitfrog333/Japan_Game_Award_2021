using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.WallHorizontal
{
    /// <summary>
    /// 横にある壁に対して横方向へ入力すると登るモードオプション
    /// </summary>
    public enum WallRunHorizontalMode
    {
        /// <summary>0：上に配置</summary>
        TOP = 0,
        /// <summary>1：右に配置</summary>
        RIGHT = 1,
        /// <summary>2：下に配置</summary>
        BOTTOM = 2,
        /// <summary>3：左に配置</summary>
        LEFT = 3
    }
}
