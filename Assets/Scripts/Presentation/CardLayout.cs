using System;
using UnityEngine;

namespace Presentation
{
    public class CardLayout : MonoBehaviour
    {
        [SerializeField] private Vector2 offset;

        [SerializeField] private int layoutId;

        [SerializeField] private bool faceUp;

        private void Update()
        {
            foreach (var card in CardGame.Instance.GetCardsInLayout(layoutId))
            {
                var cardView = CardGame.Instance.Cards[card];
                cardView.transform.SetParent(transform);
                cardView.transform.position = offset * card.CardPosition;
                cardView.transform.SetSiblingIndex(card.CardPosition);
                cardView.Rotate(faceUp);
            }
        }
    }
}
