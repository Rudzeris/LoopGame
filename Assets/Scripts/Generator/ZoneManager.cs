using UnityEngine;

namespace TopDown.Generator
{
    public class ZoneManager : MonoBehaviour
    {
        public int LVL = 1;


        public void LVLup()
        {
            if (LVL < Helper.maxLEVEL)
            LVL++;
            else
            ReachedMaxLevel();
        }

        private void ReachedMaxLevel()
        {

            try
            {
                Messenger.Broadcast(GameEvent.MAX_LEVEL_REACHED);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}


