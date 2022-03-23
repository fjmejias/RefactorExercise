using HighCard.Contracts.Configuration;
using HighCard.Contracts.Enums;
using HighCard.Contracts.Interfaces;
using HighCard.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HighCard
{
    public class CardSelector : ICardSelector
    {
        public static int SuitsNumber => Enum.GetNames(typeof(Suits)).Length;

        private readonly ICardGameSettings _settings;
        private readonly Random _rnd;
        private readonly List<Card> _cards;

        public CardSelector(ICardGameSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _rnd = new Random(DateTime.Now.Millisecond);
            _cards = new List<Card>();
        }

        public int InitializeCards()
        {
            if (_settings.NumCardsPerSuit == 0)
            {
                throw new Exception($"The Number of Cards should be greater than zero.");
            }

            for (int i = 0; i < _settings.NumDecks; i++)
            {
                foreach (Suits suit in Enum.GetValues(typeof(Suits)))
                {
                    _cards.AddRange(CreateCardsBySuit(_settings.NumCardsPerSuit, suit));
                }

                if (_settings.EnableJoker)
                {
                    _cards.Add(new Card { IsJoker = true });
                }
            }

            return _cards.Count;
        }

        public Card DrawCard()
        {
            if (!_cards.Any())
            {
                throw new Exception($"The cards should be initialized first.");
            }

            var selected = _rnd.Next(_cards.Count);
            var card = _cards.ElementAt(selected);

            _cards.Remove(card);

            return card;
        }

        #region private methods

        private IList<Card> CreateCardsBySuit(int num, Suits suit)
        {
            return Enumerable.Range(1, num).Select(n => GetCard(n, suit)).ToList();
        }

        private Card GetCard(int num, Suits suit)
        {
            return new Card { Number = num, Suit = suit };
        }

        #endregion
    }
}
