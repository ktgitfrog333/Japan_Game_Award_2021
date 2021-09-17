using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const.Component;

namespace Controller.Gimmicks
{
    /// <summary>
    /// ギミック判定
    /// </summary>
    public static class GimmicksDecision
    {
        /// <summary>
        /// プレイヤーのモードを判定
        /// </summary>
        /// <param name="gameObject">プレイヤー</param>
        /// <returns>再生・停止・-1は例外</returns>
        public static int DecisionCollisionPlayerMode(GameObject gameObject, PlayerManager playerManager, string type)
        {
            var result = -1;
            if (type.Equals(ComponentManager.MARMOT_WHEEL))
            {
                // 回し車　ツルツルモードのみ
                if (gameObject.name.Equals(playerManager._tsurutsuru.name))
                {
                    result = 1;
                }
                else if (gameObject.name.Equals(playerManager._calamari.name) || gameObject.name.Equals(playerManager._nenchak.name))
                {
                    result = 0;
                }
            }
            else if (type.Equals(ComponentManager.CIRCLE_RING))
            {
                // 回転リング　カラマリモードとツルツルモードのみ
                if (gameObject.name.Equals(playerManager._calamari.name) || gameObject.name.Equals(playerManager._tsurutsuru.name))
                {
                    result = 1;
                }
                else if (gameObject.name.Equals(playerManager._nenchak.name))
                {
                    result = 0;
                }
            }

            return result;
        }
    }
}
