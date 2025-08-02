using PrimeTween;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

namespace TopDown.Generator
{
    public class Zone : MonoBehaviour
    {
        public int index;
        private List<GameObject> obstacles = new List<GameObject>();
        private List<Transform> children = new List<Transform>();
        private List<GameObject> activeObstacles = new List<GameObject>();
        private List<List<GameObject>> ByLevelObstacles = new List<List<GameObject>>();
        private ZoneManager zoneManager;

        private void Start()
        {
            zoneManager = GameObject.FindGameObjectWithTag("Generator").GetComponent<ZoneManager>();
        }

        public void InitializeObstacle()
        {
            children = GetComponentsInChildren<Transform>().ToList<Transform>();


            var allObj = children.ToList<Transform>();

            foreach (Transform child in allObj)
            {
                if (child.CompareTag("Obstacle"))
                {
                    obstacles.Add(child.gameObject);
                    child.gameObject.SetActive(false);
                    children.Remove(child);
                }
            }

            var temp = Helper.ShuffleList(obstacles);

            obstacles = temp;   

            ByLevelObstacles = Helper.SplitList(obstacles, Helper.maxLEVEL);
        }

        public void LvlUp()
        {
            if(zoneManager.LVL <= Helper.maxLEVEL)
            {
                foreach (var obs in ByLevelObstacles[zoneManager.LVL - 1])
                {
                    activeObstacles.Add(obs);
                }
            }
        }

        public void SetZoneActive(bool bActive)
        {
            foreach (var child in children)
            {
                var animator = child.gameObject.GetComponent<PlatformAnimator>();
                if (animator != null)
                animator.PlayAnimation(bActive);
            }

            foreach (var child in activeObstacles)
            {
                var animator = child.gameObject.GetComponent<PlatformAnimator>();
                if (animator != null)
                animator.PlayAnimation(bActive, true);
            }
        }

        public void ActivateActiveObstacles()
        {
            foreach (var child in activeObstacles)
            {
                child.SetActive(true);
            }
        }

    }
}
