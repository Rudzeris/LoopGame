using Unity.Mathematics;
using UnityEngine;

namespace TopDown.Movement
{
    public class Rotator : MonoBehaviour
    {
        protected void LookAt(Vector3 target)
        {
            float lookAngle = AngleBetweenTwoPoints(transform.position, target) + 90;

            transform.eulerAngles = new Vector3(0, lookAngle, 0);
        }
        private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.z - b.z, b.x - a.x) * Mathf.Rad2Deg;
        }
    }

}