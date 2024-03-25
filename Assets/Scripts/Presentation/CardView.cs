using System;
using System.Net.Mime;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation
{
    public class CardView : MonoBehaviour
    {
        private CardInstance _card;
        
        [SerializeField] private SpriteRenderer _image;

        private Text _name;

        private Color _color;

        public void Awake()
        {
            _image = GameObject.Find("CardImage").GetComponent<SpriteRenderer>();
            _name = GetComponentInChildren<Text>();
        }

        public void Init(CardInstance card)
        {
            _card = card;
            _image.sprite = card.CardData.picture;
            //_name.text = card.CardData.cardName;
            Debug.Log(card.CardData.cardName);
            _color = card.CardData.color;
        }

        public void Rotate(bool up)
        {
            _image.sprite = up ? _card.CardData.picture : CardGame.Instance.CardFace;
        }

        public void PlayCard()
        {
            _card.MoveToLayout((int)Layout.Field);
        }
    }
}
