using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PrimeTween;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage;
    private Sprite iconSprite;
    private Sprite hiddenSprite;
    private CardController controller;
    private bool isFlipped;
    private bool isInteractive = true;

    public void SetCardController(CardController controller) => this.controller = controller;
    public void SetIcon(Sprite icon, Sprite hidden) 
    {
        iconSprite = icon;
        hiddenSprite = hidden;
        iconImage.sprite = hiddenSprite;
    }
    public Sprite GetIcon() => iconSprite;
    public bool IsFlipped => isFlipped;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isInteractive)
            controller.OnCardSelected(this);
    }
    
    public bool IsMatched { get; set; }

    public void FlipOpen(float duration = 0.3f)
    {
        if (isFlipped) return;
        
        isInteractive = false;
        float halfDuration = duration / 2f;
        Tween.Rotation(transform, new Vector3(0, 90, 0), halfDuration, Ease.InOutSine)
            .OnComplete(() => 
            {
                iconImage.sprite = iconSprite;
                Tween.Rotation(transform, Vector3.zero, halfDuration, Ease.InOutSine)
                    .OnComplete(() =>
                    {
                        isFlipped = true;
                        isInteractive = true;
                    });

            });
    }

    public void FlipClosed(float duration = 0.3f)
    {
        if (!isFlipped) return;

        isInteractive = false;
        float halfDuration = duration / 2f;
        Tween.Rotation(transform, new Vector3(0, 90, 0), halfDuration, Ease.InOutSine)
            .OnComplete(() =>
            {
                iconImage.sprite = hiddenSprite;
                Tween.Rotation(transform, Vector3.zero, halfDuration, Ease.InOutSine)
                    .OnComplete(() => 
                    {
                        isFlipped = false;
                        isInteractive = true;
                    });
            });
    }
}