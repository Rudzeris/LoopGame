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
            Debug.Log("Инициализация зоны " + index);

            foreach (Transform child in children)
            {
                if (child.CompareTag("Obstacle"))
                {
                    obstacles.Add(child.gameObject);
                    child.gameObject.SetActive(false);
                }
            }
            Debug.Log("Количество припятсвий зоны " + index + " = " + obstacles.Count);

            var temp = Helper.ShuffleList(obstacles);

            obstacles = temp;   

            ByLevelObstacles = Helper.SplitList(obstacles, Helper.maxLEVEL);
        }

        public void LvlUp()
        {
                foreach (var obs in ByLevelObstacles[zoneManager.LVL - 1])
                {
                    activeObstacles.Add(obs);
                }
        }

        public void SetZoneActive(bool bActive)
        {
            foreach (var child in children)
            {
                child.gameObject.SetActive(bActive);
            }

            foreach (var child in obstacles)
            {
                child.SetActive(false);
            }

            foreach (var child in activeObstacles)
            {
                child.SetActive(bActive);
            }
        }

    }
}
