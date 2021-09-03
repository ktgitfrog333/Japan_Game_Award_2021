using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const.Component;

namespace DeadException
{
    /// <summary>
    /// コンポーネントを参照した際の例外を回避
    /// </summary>
    public static class DeadNullReference
    {
        /// <summary>
        /// コンポーネント参照例外を回避
        /// </summary>
        /// <param name="gameObject">オブジェクト</param>
        /// <param name="name">スクリプト名</param>
        /// <returns></returns>
        public static bool CheckReferencedComponent(GameObject gameObject, string name)
        {
            var result = false;
            var message = "";
            try
            {
                if (name.Equals(ComponentManager.MOVE_WALLS))
                {
                    var v = gameObject.GetComponent<MoveWalls>().RigidbodyVelocity;
                    result = true;
                }
                else if (name.Equals(ComponentManager.CALAMARI_STATE))
                {
                    var t = gameObject.GetComponent<CalamariState>()._transform;
                    if (t == null)
                    {
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                }
                else if (name.Equals(ComponentManager.MARMOT_HEALTH))
                {
                    var t = gameObject.GetComponent<MarmotHealth>()._health;
                    result = true;
                }
            }
            catch (NullReferenceException e)
            {
                message = e + "";
                result = false;
            }
            finally
            {
                if (result == false)
                {
                    if (0 < message.Length)
                    {
                        Debug.Log(name + "_Null参照：" + message);
                    }
                    else
                    {
                        Debug.Log(name + "_Null参照");
                    }
                }
            }

            return result;
        }
    }
}
