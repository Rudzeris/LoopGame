using System.Collections;
using UnityEngine;

namespace TopDown.Generator
{
    public class OverlapChecker : MonoBehaviour
    {

        public LayerMask targetLayer;

        private Collider objectCollider;
        public void Start()
        {
            objectCollider = GetComponent<Collider>();
            CheckBoxOverlap();
        }

        public void CheckBoxOverlap()
        {
            Vector3 objectSize = objectCollider.bounds.size;
            Vector3 objectCenter = objectCollider.bounds.center;

            Collider[] hitColliders = Physics.OverlapBox(objectCenter, objectSize / 2, Quaternion.identity, targetLayer);

            foreach (var hitCollider in hitColliders)
            {

                if (hitCollider != objectCollider)
                {
                    Debug.Log("Detected Overlap");
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        public void Update()
        {
            
        }

        private void SoftOverlapCheck()
        {
            gameObject.SetActive(false);
        }
    }
}
