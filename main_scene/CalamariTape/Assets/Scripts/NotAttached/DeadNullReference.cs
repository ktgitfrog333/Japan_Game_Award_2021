using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            try
            {
                if (name.Equals("MoveVerticalWall"))
                {
                    var v = gameObject.GetComponent<MoveVerticalWall>().RigidbodyVelocity;
                    result = true;
                }
            }
            catch (NullReferenceException e)
            {
                Debug.Log("Null参照：" + e);
                result = false;
            }

            return result;
        }
    }
}
