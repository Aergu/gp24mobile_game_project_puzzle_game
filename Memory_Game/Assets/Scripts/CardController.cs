using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform gridTransform;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Sprite cardBackSprite;

    private List<Card> allCards = new List<Card>();
    private List<Sprite> spritePairs;
    private Card firstSelected;
    private Card secondSelected;
    private int matchCount;
    private bool inputEnabled = true;

    private void Start()
    {
        PrepareSprites();
        CreateCards();
        PositionCards();
    }

    private void PrepareSprites()
    {
        spritePairs = new List<Sprite>();
        foreach (var sprite in sprites)
        {
            spritePairs.Add(sprite);
            spritePairs.Add(sprite); // Adds pairs
        }
        ShuffleSprites(spritePairs);
    }

    private void CreateCards()
    {
        for (int i = 0; i < spritePairs.Count; i++)
        {
            Card card = Instantiate(cardPrefab, gridTransform);
            card.SetCardController(this);
            card.SetIcon(spritePairs[i], cardBackSprite);
            allCards.Add(card);
        }
    }

    private void PositionCards()
    {
        // Only shuffle unmatched cards
        List<Card> unmatchedCards = new List<Card>();
        List<Vector3> positions = new List<Vector3>();

        // Collect unmatched cards and their current positions
        foreach (Card card in allCards)
        {
            if (!card.IsMatched)
            {
                unmatchedCards.Add(card);
                positions.Add(card.transform.position);
            }
        }

       

        // Assign new positions to unmatched cards
        for (int i = 0; i < unmatchedCards.Count; i++)
        {
            unmatchedCards[i].transform.position = positions[i];
        }
    }

    public void OnCardSelected(Card card)
    {
        if (!inputEnabled || card.IsFlipped || card.IsMatched || card == firstSelected) 
            return;

        card.FlipOpen();

        if (firstSelected == null)
        {
            firstSelected = card;
        }
        else
        {
            secondSelected = card;
            inputEnabled = false;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);

        if (firstSelected.GetIcon() == secondSelected.GetIcon())
        {
            // Cards are marked as matched, they'll stay in place.
            firstSelected.IsMatched = true;
            secondSelected.IsMatched = true;
            matchCount++;
            
            firstSelected.FlipOpen(0.2f);
            secondSelected.FlipOpen(0.2f);

            if (matchCount >= spritePairs.Count / 2)
            {
                Debug.Log("Game Over - All Matched!");
            }
        }
        else
        {
            // Flip back unmatched cards
            firstSelected.FlipClosed();
            secondSelected.FlipClosed();
            yield return new WaitForSeconds(0.5f); // Wait for flip animation
            PositionCards(); // Only reshuffle unmatched cards
        }

        firstSelected = null;
        secondSelected = null;
        inputEnabled = true;
    }

    private void ShuffleSprites(List<Sprite> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Sprite temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
    
}