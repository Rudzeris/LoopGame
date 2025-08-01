using UnityEngine;

public class DieEventPublisher : MonoBehaviour
{
    public float criticalHeight = -5;
    private void Update()
    {
        CheckPlayerHeight();
    }
    private void CheckPlayerHeight()
    {
        if (transform.position.y <= criticalHeight)
        {
            try
            {
                Messenger.Broadcast(GameEvent.PLAYER_STAY_CRITICAL_ZONE);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
