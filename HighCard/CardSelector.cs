using HighCard.Configuration;
using HighCard.Enums;
using HighCard.Interfaces;
using HighCard.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HighCard
{
    public class CardSelector : ICardSelector
    {
        private readonly HighCardSettings _settings;
        private readonly Random _rnd;
        private readonly List<Card> _cards;

        public static int SuitsNumber => Enum.GetNames(typeof(Suits)).Length;

        public CardSelector(HighCardSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _rnd = new Random(DateTime.Now.Millisecond);
            _cards = InitializeCards();
        }

        public Card DrawCard()
        {
            var maxValue = _cards.Count;
            var card = _cards.ElementAt(_rnd.Next(maxValue));

            _cards.Remove(card);

            return card;
        }

        #region private methods

        private List<Card> InitializeCards()
        {
            var cards = new List<Card>();
            
            for (int i = 0; i < _settings.NumDecks; i++)
            {
                cards.AddRange(CreateCardsBySuit(_settings.NumCardsPerSuit, Suits.Clubs));
                cards.AddRange(CreateCardsBySuit(_settings.NumCardsPerSuit, Suits.Diamonds));
                cards.AddRange(CreateCardsBySuit(_settings.NumCardsPerSuit, Suits.Hearts));
                cards.AddRange(CreateCardsBySuit(_settings.NumCardsPerSuit, Suits.Spades));
            }

            return cards;
        }

        private IList<Card> CreateCardsBySuit(int num, Suits suit)
        {
            return Enumerable.Range(1, num).Select(n => new Card
            {
                Number = n,
                Suit = suit
            }).ToList();
        }

        #endregion
    }
}
