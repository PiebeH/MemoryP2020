using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.ViewModels
{
    public class StartMenuView
    {//Start de muziek
        private MainWindow _mainWindow;
        public StartMenuView(MainWindow main)
        {
            _mainWindow = main;
            SoundManager.PlayBackgroundMusic();
        }
        //Start de game
        public void StartNewGame(int categoryIndex)
        {
            var category = (SlideCategories)categoryIndex;
            GameViewModel newGame = new GameViewModel(category);
            _mainWindow.DataContext = newGame;
        }

        internal void StartNewGame(object selectedIndex)
        {
            throw new NotImplementedException();
        }
    }
}
