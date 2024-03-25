using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Presentation;
using ScriptableObjects;
using UnityEngine;

public class CardGame : MonoBehaviour
{
    [SerializeField] private GameObject cardTemplate;
    
    [SerializeField] private Sprite cardFace;
    
    [SerializeField] private List<CardAsset> firstPlayerStartHand;
    
    [SerializeField] private List<CardAsset> secondPlayerStartHand;

    [SerializeField] private int handCapacity;
    
    public static CardGame Instance { get; private set; }

    public Dictionary<CardInstance, CardView> Cards { get; private set; }
    

    public Sprite CardFace => cardFace;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        Cards = new Dictionary<CardInstance, CardView>();
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        foreach (var cardData in firstPlayerStartHand)
        {
            CreateCard(cardData, (int)Layout.FirstPlayer);
        }
        
        foreach (var cardData in secondPlayerStartHand)
        {
            CreateCard(cardData, (int)Layout.SecondPlayer);
        }
    }

    private void CreateCardView(CardInstance card)
    {
        var cardPrefab = Instantiate(cardTemplate);
        var cardView = cardPrefab.AddComponent<CardView>();
        cardView.Init(card);
        Cards[card] = cardView;
    }

    private void CreateCard(CardAsset cardData, int layoutId)
    {
        var card = new CardInstance(cardData);

        CreateCardView(card);
        card.MoveToLayout(layoutId);
    }

    public List<CardInstance> GetCardsInLayout(int layoutId) => 
        (from pair in Cards where pair.Key.LayoutId == layoutId select pair.Key).ToList();

    public void RecalculateLayout(int layoutId)
    {
        var id = 0;
        var layoutCards = (from card in GetCardsInLayout(layoutId)
                orderby card.CardPosition
                select card)
            .ToList();
        foreach (var card in layoutCards)
        {
            card.CardPosition = id++;
        }
    }

    public void ShuffleLayout(int layoutId)
    {
        var id = 0;
        var random = new System.Random();
        var layoutCards = (from card in GetCardsInLayout(layoutId)
                orderby random.Next()
                select card)
            .ToList();
        foreach (var card in layoutCards)
        {
            card.CardPosition = id++;
        }
    }

    public int GetLayoutSIze(int layoutId) => 
        (from pair in Cards where pair.Key.LayoutId == layoutId select pair.Key).Count();

    public void StartTurn()
    {
        var deck = GetCardsInLayout((int)Layout.Deck);
        foreach (var card in deck)
        {
            if (GetLayoutSIze((int)Layout.FirstPlayer) == handCapacity)
            {
                break;
            }
            
            card.MoveToLayout((int)Layout.FirstPlayer);
        }
        
        foreach (var card in deck)
        {
            if (GetLayoutSIze((int)Layout.SecondPlayer) == handCapacity)
            {
                break;
            }
            
            card.MoveToLayout((int)Layout.SecondPlayer);
        }
    }
}
