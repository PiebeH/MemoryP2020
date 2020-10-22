using MemoryGame.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MemoryGame.ViewModels
{
    public class Slidesweergave : ObservableObject
    {
        //Slide images
        public ObservableCollection<ImageWeergave> MemorySlides { get; private set; }

        //Select slides voor matching
        private ImageWeergave SelectedSlide1;
        private ImageWeergave SelectedSlide2;

        //Timers 
        private DispatcherTimer _peekTimer;
        private DispatcherTimer _openingTimer;

        //Tijd dat de speler de geselecteerde slides kan bekijken
        private const int _peekSeconds = 1;
        //Hoeveelheid tijd dat de speler heeft om de slides te onthouden voor het begin van de game
        private const int _openSeconds = 0;

        //Geselecteerde slides worden nog weergeven
        public bool AreSlidesActive
        {
            get
            {
                if (SelectedSlide1 == null || SelectedSlide2 == null)
                    return true;

                return false;
            }
        }

        //Alle slides zijn gematched
        public bool AllSlidesMatched
        {
            get
            {
                foreach(var slide in MemorySlides)
                {
                    if (!slide.isMatched)
                        return false;
                }

                return true;
            }
        }

        //Speler kan slides selecteren
        public bool canSelect { get; private set; }

        //Peek timers
        public Slidesweergave()
        {
            _peekTimer = new DispatcherTimer();
            _peekTimer.Interval = new TimeSpan(0, 0, _peekSeconds);
            _peekTimer.Tick += PeekTimer_Tick;

            _openingTimer = new DispatcherTimer();
            _openingTimer.Interval = new TimeSpan(0, 0, _openSeconds);
            _openingTimer.Tick += OpeningTimer_Tick;
        }

        //Slides creeëren van de images
        public void CreateSlides(string imagesPath)
        {
            //Nieuwe lijst van slides
            MemorySlides = new ObservableCollection<ImageWeergave>();
            var models = GetModelsFrom(@imagesPath);

            // Slides crëeeren met matchende paren
            for (int i = 0; i < 8; i++)
            {
                // 2 matching slides
                ImageWeergave newSlide = new ImageWeergave(models[i]);
                var newSlideMatch = new ImageWeergave(models[i]);
                //Add nieuwe slides aan de collectie
                MemorySlides.Add(newSlide);
                MemorySlides.Add(newSlideMatch);
            }
            //Shuffled de slides
            ShuffleSlides();
            OnPropertyChanged("MemorySlides");
        }   

        //Slide selecteren om te matchen
        public void SelectSlide(ImageWeergave slide)
        {
            slide.PeekAtImage();

            if (SelectedSlide1 == null)
            {
                SelectedSlide1 = slide;
            }
            else if (SelectedSlide2 == null)
            {
                SelectedSlide2 = slide;
                HideUnmatched();
            }

            SoundManager.PlayCardFlip();
            OnPropertyChanged("areSlidesActive");
        }

        //Wanneer de slides matchen
        public bool CheckIfMatched()
        {
            if (SelectedSlide1.Id == SelectedSlide2.Id)
            {
                MatchCorrect();
                return true;
            }
            else
            {
                MatchFailed();
                return false;
            }
        }

        //De slides matchen niet
        private void MatchFailed()
        {
            SelectedSlide1.MarkFailed();
            SelectedSlide2.MarkFailed();
            ClearSelected();
        }

        //Geselecteerde slides matchen
        private void MatchCorrect()
        {
            SelectedSlide1.MarkMatched();
            SelectedSlide2.MarkMatched();
            ClearSelected();
        }

        //Geselecteerde slides herstellen
        private void ClearSelected()
        {
            SelectedSlide1 = null;
            SelectedSlide2 = null;
            canSelect = false;
        }

        //Alle unmatche slides laten zien aan het einde van de game
        public void RevealUnmatched()
        {
            foreach(var slide in MemorySlides)
            {
                if(!slide.isMatched)
                {
                    _peekTimer.Stop();
                    slide.MarkFailed();
                    slide.PeekAtImage();
                }
            }
        }

        //Alle unmatchte slides verbergen
        public void HideUnmatched()
        {
            _peekTimer.Start();
        }

        //Slides laten zien om te memorizen
        public void Memorize()
        {
            _openingTimer.Start();
        }

        //Haal slide picture models om 
        private List<PictureModel> GetModelsFrom(string relativePath)
        {
            //Lijst van models voor slides
            var models = new List<PictureModel>();
            //Haal alle images
            var images = Directory.GetFiles(@relativePath, "*.jpg", SearchOption.AllDirectories);
            //Slide id begint op 0
            var id = 0;

            foreach (string i in images)
            {
                models.Add(new PictureModel() { Id = id, ImageSource = "/MemoryGame;component/" + i });
                id++;
            }

            return models;
        }

        //Randomize de plaatsen van de slides
        private void ShuffleSlides()
        {
            //Randomize slide indexes
            var rnd = new Random();
            //Shuffled memory slides
            for (int i = 0; i < 64; i++)
            {
                MemorySlides.Reverse();
                MemorySlides.Move(rnd.Next(0, MemorySlides.Count), rnd.Next(0, MemorySlides.Count));
            }
        }

        //Sluit slides die worden gememorised
        private void OpeningTimer_Tick(object sender, EventArgs e)
        {
            foreach (var slide in MemorySlides)
            {
                slide.ClosePeek();
                canSelect = true;
            }
            OnPropertyChanged("areSlidesActive");
            _openingTimer.Stop();
        }

        //Toont geselecteerde kaart
        private void PeekTimer_Tick(object sender, EventArgs e)
        {
            foreach(var slide in MemorySlides)
            {
                if(!slide.isMatched)
                {
                    slide.ClosePeek();
                    canSelect = true;
                }
            }
            OnPropertyChanged("areSlidesActive");
            _peekTimer.Stop();
        }
    }
}
