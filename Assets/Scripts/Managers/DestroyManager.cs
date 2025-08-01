using Assets.Scripts.GameObjects.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class DestroyManager : MonoBehaviour
    {
        private List<GameObject> destroyQueue = new List<GameObject>();
        [SerializeField] private float destroyDelay = 1f;
        private void Awake()
        {
            Messenger<IBasicEntity>.AddListener(GameEvent.ENTITY_IS_DESTROYED, OnEntityDestroyed);
        }

        private void OnDestroy()
        {
            Messenger<IBasicEntity>.RemoveListener(GameEvent.ENTITY_IS_DESTROYED, OnEntityDestroyed);
        }

        private void OnEntityDestroyed(IBasicEntity entity)
        {
            Debug.Log($"{entity.Fraction} is destroyed");
            if (entity is MonoBehaviour mono)
            {
                GameObject obj = mono.gameObject;
                if (!destroyQueue.Contains(obj))
                {
                    destroyQueue.Add(obj);
                    StartCoroutine(Destroyer(obj));
                }
            }
        }
        private IEnumerator Destroyer(GameObject obj)
        {
            yield return new WaitForSeconds(destroyDelay);
            Destroy(obj);
            yield return null;
            destroyQueue.Remove(obj);
        }
    }
}
