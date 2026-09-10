using System;
using System.Collections.Generic;
using UnityEngine;
using DeckBuilder.Cards;
using BattleSystem.Instance;

namespace BattleSystem
{
    public class BattleCardModel
    {
        public const int TARGET_HAND_SIZE = 7;

        // 4 Main Piles
        public List<CardInstance> DrawPile { get; private set; } = new();
        public List<CardInstance> Hand { get; private set; } = new();
        public List<CardInstance> DiscardPile { get; private set; } = new();
        public List<CardInstance> BurnPile { get; private set; } = new();

        public event Action<CardInstance> OnCardDrawn;
        public event Action<CardInstance> OnCardPlayed;
        public event Action<CardInstance> OnCardDiscarded;
        public event Action<CardInstance> OnCardBurned;
        public event Action OnDeckReshuffled;
        public event Action OnHandCleared;

        #region Main Card Gameplay Loop

        // Initiation of battle start: copy from MasterDeck to DrawPile, then shuffle
        public void InitializeBattleDeck(List<CardData> masterDeck)
        {
            DrawPile.Clear();
            Hand.Clear();
            DiscardPile.Clear();
            BurnPile.Clear();

            foreach (var cardData in masterDeck)
            {
                // make runtime instance from data template
                DrawPile.Add(new CardInstance(cardData));
            }

            ShuffleDrawPile();
        }

        // Draw cards until the target amount
        public void DrawToHandSize(int targetSize = TARGET_HAND_SIZE)
        {
            while (Hand.Count < targetSize)
            {
                // if draw pile is empty, try to reshuffle from the discard pile
                if (DrawPile.Count == 0)
                {
                    if (DiscardPile.Count == 0)
                    {
                        // no more cards left in draw neither in discard pile
                        break;
                    }
                    ReshuffleDiscardToDraw();
                }

                // take the top card from the draw pile
                CardInstance cardToDraw = DrawPile[0];
                DrawPile.RemoveAt(0);
                Hand.Add(cardToDraw);

                OnCardDrawn?.Invoke(cardToDraw);
            }
        }

        public bool PlayCard(CardInstance card, bool burnAfterPlay = false)
        {
            if (!Hand.Contains(card))
                return false;

            Hand.Remove(card);

            if (burnAfterPlay)
            {
                BurnPile.Add(card);
                OnCardBurned?.Invoke(card);
            }else
            {
                DiscardPile.Add(card);
                OnCardPlayed?.Invoke(card);
            }

            return true;
        }

        // Discard remaining cards in hand to discard pile when End Turn
        public void DiscardRemainingHand()
        {
            for (int i = Hand.Count - 1; i >= 0; i--)
            {
                CardInstance card = Hand[i];
                Hand.RemoveAt(i);
                DiscardPile.Add(card);
                OnCardDiscarded?.Invoke(card);
            }

            OnHandCleared?.Invoke();
        }

        #endregion

        #region Cards Reshuffle

        // Moving all cards on the discard pile to the draw pile and shuffle it
        public void ReshuffleDiscardToDraw()
        {
            if (DiscardPile.Count == 0)
                return;

            DrawPile.AddRange(DiscardPile);
            DiscardPile.Clear();

            ShuffleDrawPile();

            OnDeckReshuffled?.Invoke();
        }

        // Fisher-Yates Shuffle Algorithm to shuffle draw pile
        // Swap value in index n to 1, with value in random index between 1 to n. n move backward
        private void ShuffleDrawPile()
        {
            System.Random random = new System.Random();
            int n = DrawPile.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                CardInstance value = DrawPile[k];
                DrawPile[k] = DrawPile[n];
                DrawPile[n] = value;
            }
        }

        #endregion
    }
}