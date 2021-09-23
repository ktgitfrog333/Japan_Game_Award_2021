using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const.Layer;
using Const.Tag;
using Controller.Gravity;
using DeadException;
using Const.Component;

namespace Controller.AllmodeState
{
    /// <summary>
    /// 各モードの状態変化処理
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

        /// <summary>
        /// 地面の接触判定
        /// </summary>
        /// <param name="character">キャラクターコントローラー</param>
        /// <param name="transform">オブジェクトの位置・角度・スケール</param>
        /// <param name="registMaxDistance">接触判定の最大距離</param>
        /// <returns>接地状態か否か</returns>
        public static bool IsGrounded(CharacterController character, Transform transform, float registMaxDistance)
        {
            var result = character.isGrounded;

            if (result == false)
            {
                Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * registMaxDistance, Color.green);
                var ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
                foreach (RaycastHit hit in Physics.RaycastAll(ray, registMaxDistance))
                {
                    if (hit.collider.gameObject.layer == (int)LayerManager.FIELD)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 滑る地面の接触判定
        /// </summary>
        /// <param name="character">キャラクターコントローラー</param>
        /// <param name="transform">オブジェクトの位置・角度・スケール</param>
        /// <param name="registMaxDistance">接触判定の最大距離</param>
        /// <returns>接地状態か否か</returns>
        public static GameObject IsIcePlanedAndObject(CharacterController character, Transform transform, float registMaxDistance)
        {
            var result = new GameObject();

            Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * registMaxDistance, Color.green);
            var ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
            foreach (RaycastHit hit in Physics.RaycastAll(ray, registMaxDistance))
            {
                var g = hit.collider.gameObject;
                if (DeadNullReference.CheckReferencedComponent(g, ComponentManager.ICE_PLANE) == true)
                {
                    if (g.layer == (int)LayerManager.FIELD || g.GetComponent<IcePlane>()._icePlane == true)
                    {
                        result = g;
                    }
                }
            }

            return result.tag.Equals(TagManager.ICE_PLANE) ? result : null;
        }

        /// <summary>
        /// 壁の接触判定
        /// </summary>
        /// <param name="transform">オブジェクトの位置・角度・スケール</param>
        /// <param name="distance">接触判定の最大距離</param>
        /// <returns>接地状態か否か</returns>
        public static int IsWalled(Transform transform, float distance)
        {
            var result = (int)GravityDirection.AIR;

            // 横向きの壁
            Debug.DrawRay(transform.position + Vector3.back * 0.1f, Vector3.forward * distance, Color.green);
            var rayVtcl = new Ray(transform.position + Vector3.back * 0.1f, Vector3.forward);
            foreach (RaycastHit hit in Physics.RaycastAll(rayVtcl, distance))
            {
                var g = hit.collider.gameObject;
                if (g.tag.Equals(TagManager.VERTICAL_WALL))
                {
                    if (g.transform.parent.gameObject.layer == (int)LayerManager.WALL)
                    {
                        result = (int)GravityDirection.VERTICAL;
                    }
                }
            }
            // 縦向きの壁（左）
            Debug.DrawRay(transform.position + Vector3.right * 0.1f, Vector3.left * distance, Color.green);
            var rayHntlLeft = new Ray(transform.position + Vector3.right * 0.1f, Vector3.left);
            foreach (RaycastHit hit in Physics.RaycastAll(rayHntlLeft, distance))
            {
                var g = hit.collider.gameObject;
                if (g.tag.Equals(TagManager.HORIZONTAL_WALL))
                {
                    if (g.transform.parent.gameObject.layer == (int)LayerManager.WALL)
                    {
                        result = (int)GravityDirection.HORIZONTAL_LEFT;
                    }
                }
            }
            // 縦向きの壁（右）
            Debug.DrawRay(transform.position + Vector3.left * 0.1f, Vector3.right * distance, Color.green);
            var rayHntlRight = new Ray(transform.position + Vector3.left * 0.1f, Vector3.right);
            foreach (RaycastHit hit in Physics.RaycastAll(rayHntlRight, distance))
            {
                var g = hit.collider.gameObject;
                if (g.tag.Equals(TagManager.HORIZONTAL_WALL))
                {
                    if (g.transform.parent.gameObject.layer == (int)LayerManager.WALL)
                    {
                        result = (int)GravityDirection.HORIZONTAL_RIGHT;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// コンベア接着判定
        /// </summary>
        /// <param name="transform">位置・角度・スケール</param>
        /// <param name="distance">距離</param>
        /// <returns>接着状態か否か</returns>
        public static bool IsConveyor(Transform transform, float distance)
        {
            var result = false;
            Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.down * distance, Color.green);
            var rayVtcl = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
            foreach (RaycastHit hit in Physics.RaycastAll(rayVtcl, distance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.CONVEYOR)
                {
                    result = true;
                }
            }
            Debug.DrawRay(transform.position + Vector3.right * 0.1f, Vector3.left * distance, Color.green);
            var rayHntlLeft = new Ray(transform.position + Vector3.right * 0.1f, Vector3.left);
            foreach (RaycastHit hit in Physics.RaycastAll(rayHntlLeft, distance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.CONVEYOR)
                {
                    result = true;
                }
            }
            Debug.DrawRay(transform.position + Vector3.left * 0.1f, Vector3.right * distance, Color.green);
            var rayHntlRight = new Ray(transform.position + Vector3.left * 0.1f, Vector3.right);
            foreach (RaycastHit hit in Physics.RaycastAll(rayHntlRight, distance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.CONVEYOR)
                {
                    result = true;
                }
            }
            Debug.DrawRay(transform.position + Vector3.back * 0.1f, Vector3.forward * distance, Color.green);
            var rayFoBa = new Ray(transform.position + Vector3.back * 0.1f, Vector3.forward);
            foreach (RaycastHit hit in Physics.RaycastAll(rayFoBa, distance))
            {
                if (hit.collider.gameObject.layer == (int)LayerManager.CONVEYOR)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
