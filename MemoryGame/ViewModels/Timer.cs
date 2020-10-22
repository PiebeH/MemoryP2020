using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MemoryGame.ViewModels
{
    public class Timer : ObservableObject
    {//Beheert de timer
        private DispatcherTimer _playedTimer;
        private TimeSpan _timePlayed;

        private const int _playSeconds = 1;

        public TimeSpan Time
        {
            get
            {
                return _timePlayed;
            }
            set
            {
                _timePlayed = value;
                OnPropertyChanged("Time");
            }
        }

        public Timer(TimeSpan time)
        {
            _playedTimer = new DispatcherTimer();
            _playedTimer.Interval = time;
            _playedTimer.Tick += PlayedTimer_Tick;
            _timePlayed = new TimeSpan();
        }
        //Start de timer
        public void Start()
        {
            _playedTimer.Start();
        }
        //Stopt de timer
        public void Stop()
        {
            _playedTimer.Stop();
        }
        //Verhoogt de timer
        private void PlayedTimer_Tick(object sender, EventArgs e)
        {
            Time = _timePlayed.Add(new TimeSpan(0, 0, 1));
        }
    }
}
