
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MemoryGame.ViewModels
{
    //Puntentelling
    public class GameInfo : ObservableObject
    {
        private const int _maxAttempts = 10;
        private const int _pointAward = 75;
        private const int _pointDeduction = 15;
        //Pogingen en score
        private int _matchAttempts;
        private int _score;
        //Gewonnen of verloren
        private bool _gameLost;
        private bool _gameWon;

        //Aantal pogingen
        public int MatchAttempts
        {
            get
            {
                return _matchAttempts;
            }
            private set
            {
                _matchAttempts = value;
                OnPropertyChanged("MatchAttempts");
            }
        }
        //Score
        public int Score
        {
            get
            {
                return _score;
            }
            private set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        //LostMessage
        public Visibility LostMessage
        {
            get
            {
                if (_gameLost)
                    return Visibility.Visible;

                return Visibility.Hidden;
            }
        }
        //Win Message
        public Visibility WinMessage
        {
            get
            {
                if (_gameWon)
                    return Visibility.Visible;

                return Visibility.Hidden;
            }
        }
        //Showt Win of lost message
        public void GameStatus(bool win)
        {
            if (!win)
            {
                _gameLost = true;
                OnPropertyChanged("LostMessage");
            }

            if (win)
            {
                _gameWon = true;
                OnPropertyChanged("WinMessage");
            }
        }

        //Cleart de resultaten
        public void ClearInfo()
        {
            Score = 0;
            MatchAttempts = _maxAttempts;
            _gameLost = false;
            _gameWon = false;
            OnPropertyChanged("LostMessage");
            OnPropertyChanged("WinMessage");
        }
        //Speelt geluid af bij correcte match
        public void Award()
        {
            Score += _pointAward;
            SoundManager.PlayCorrect();
        }
        //Speelt geluid af bij incorrecte match
        public void Penalize()
        {
            Score -= _pointDeduction;
            MatchAttempts--;
            SoundManager.PlayIncorrect();
        }
    }
}
