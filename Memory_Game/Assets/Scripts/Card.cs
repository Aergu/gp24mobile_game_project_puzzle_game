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
        iconImage.sprite = hidden;
    }
    public Sprite GetIcon() => iconSprite;
    public bool IsFlipped => isFlipped;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isInteractive)
            controller.OnCardSelected(this);
    }
    
    public bool IsMatched { get; set; }

    public void FlipOpen()
    {
        if (isFlipped) return;
        
        isInteractive = false;
        Tween.Rotation(transform, Vector3.zero, 0.3f)
            .OnComplete(() => 
            {
                iconImage.sprite = iconSprite;
                isFlipped = true;
                isInteractive = true;
            });
    }

    public void FlipClosed()
    {
        if (!isFlipped) return;

        isInteractive = false;
        Tween.Rotation(transform, new Vector3(0, 90, 0), 0.3f)
            .OnComplete(() =>
            {
                iconImage.sprite = hiddenSprite;
                Tween.Rotation(transform, Vector3.zero, 0.3f)
                    .OnComplete(() => 
                    {
                        isFlipped = false;
                        isInteractive = true;
                    });
            });
    }
}