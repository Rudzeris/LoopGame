using Assets.Scripts.GameObjects.Entities;
using System;
using UnityEngine;


namespace TopDown.Generator
{
    public class PlatformVisibleObstacle : MonoBehaviour, IActivatable
    {
        private bool active;
        private float raycastDistance = 100f;
        private GameObject objectBelowPlayer;

        public bool Active => active;

        public void Activate()
        {
            RaycastHit hit;
            var raycast = (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance));
            if (raycast)
            {
                objectBelowPlayer = hit.collider.gameObject;

                if (objectBelowPlayer.CompareTag("Platform"))
                {
                    objectBelowPlayer.SetActive(false);
                }
            }
        }

        void OnEnable()
        {
            Messenger<IBasicEntity>.AddListener(GameEvent.ENTITY_IS_DESTROYED, OnEntityDestroyed);
        }

        private void OnEntityDestroyed(IBasicEntity entity)
        {
            if(objectBelowPlayer != null)
            {
                objectBelowPlayer.SetActive(true);
            }
        }
    }
}
