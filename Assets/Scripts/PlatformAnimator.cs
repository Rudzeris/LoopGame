using PrimeTween;
using UnityEngine;

public class PlatformAnimator : MonoBehaviour
{
    private float posY;
    private float offsetY = 30;
    private bool appear;
    private bool updown;
    void Start()
    {
        posY = transform.position.y;
    }

    public void PlayAnimation(bool bAppear, bool _updown = false)
    {
        appear = bAppear;
        updown = _updown;
        if(appear)
        AppearAnim();
        else
        DisappearAnim();
    }

    private void AppearAnim()
    {
        var heightAnim = Anim(true, 0.2f);
        gameObject.SetActive(true);
    }
    private void DisappearAnim()
    {
        var heightAnim = Anim(false);

            heightAnim.OnComplete(() =>
            {
                if(!appear)
                gameObject.SetActive(false);
            });
        
    }

    private Tween Anim(bool bAppear, float delay = 0)
    {
        var randomDelay = Random.Range(0.2f, 0.6f);
        var offset = (bAppear ? 0 : offsetY * (updown ? 1 : -1));
        return Tween.PositionY(transform, offset + posY, randomDelay, Ease.InSine, startDelay: delay);
    }

}
