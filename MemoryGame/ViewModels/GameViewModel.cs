using MemoryGame.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MemoryGame.ViewModels
{
    //De thema's
    public enum SlideCategories
    {
        Ruimtevaart,
        Planeten,
        Films
    }
    public class GameViewModel : ObservableObject
    {
        //Slides van het gekozen thema
        public Slidesweergave Slides { get; private set; }
        //Game informatie
        public GameInfo GameInfo { get; private set; }
        //Oplopende Timer
        public Timer Timer { get; private set; }
        //Gekozen categorie
        public SlideCategories Category { get; private set; }

        //Start game voor gekozen thema
        public GameViewModel(SlideCategories category)
        {
            Category = category;
            SetupGame(category);
        }

        //Nieuwe game stats
        private void SetupGame(SlideCategories category)
        {

            Slides = new Slidesweergave();
            Timer = new Timer(new TimeSpan(0, 0, 1));
            GameInfo = new GameInfo();

            //Game herstellen na teveel pogingen
            GameInfo.ClearInfo();

            //Haalt en memorised de images
            Slides.CreateSlides("Assets/" + category.ToString());
            Slides.Memorize();

            //Start Timer bij begin game
            Timer.Start();

            //Slides worden ge-update
            OnPropertyChanged("Slides");
            OnPropertyChanged("Timer");
            OnPropertyChanged("GameInfo");
        }

        //Slide wordt geklikt
        public void ClickedSlide(object slide)
        {
            if(Slides.canSelect)
            {
                var selected = slide as ImageWeergave;
                Slides.SelectSlide(selected);
            }

            if(!Slides.AreSlidesActive)
            {
                if (Slides.CheckIfMatched())
                    GameInfo.Award(); //Correcte match
                else
                    GameInfo.Penalize();//Incorrecte match
            }

            GameStatus();
        }

        //Status van de current game
        private void GameStatus()
        {
            if(GameInfo.MatchAttempts < 0)
            {
                GameInfo.GameStatus(false);
                Slides.RevealUnmatched();
                Timer.Stop();
            }

            if(Slides.AllSlidesMatched)
            {
                GameInfo.GameStatus(true);
                Timer.Stop();
            }
        }

        //Restart de game
        public void Restart()
        {
            SoundManager.PlayIncorrect();
            SetupGame(Category);
        }
    }
}
