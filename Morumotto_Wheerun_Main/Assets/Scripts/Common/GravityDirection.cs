using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.Gravity
{
    /// <summary>
    /// 重力の方向
    /// </summary>
    public enum GravityDirection
    {
        /// <summary>-1：空中</summary>
        AIR = -1,
        /// <summary>0：横向きの壁</summary>
        VERTICAL = 0,
        /// <summary>1：縦向きの壁（左）</summary>
        HORIZONTAL_LEFT = 1,
        /// <summary>2：縦向きの壁（右）</summary>
        HORIZONTAL_RIGHT = 2,
    }
}
