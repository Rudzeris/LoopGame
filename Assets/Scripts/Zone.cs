using UnityEngine;

namespace TopDown.Generator
{
    public class Zone : MonoBehaviour
    {
        public int index;
        private int lvl;

        public void LvlUp()
        {
            lvl++;
        }

    }
}
