using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller.AllmodeState
{
    /// <summary>
    /// カラマリモードの状態変化処理
    /// </summary>
    public static class AllmodeStateConf
    {
        /// <summary>
        /// スケールの大きさに合わせて各パラメータを調整する
        /// </summary>
        /// <param name="min">基準値（初期値/最小値）</param>
        /// <param name="max">最大値</param>
        /// <param name="scale">大きさ</param>
        /// <returns>計算後の値</returns>
        public static float ParameterMatchScale(float min, float max, float scale)
        {
            var v = scale - 1f;
            v = min + ((max - min) * (v / 3));
            return v;
        }
    }
}
