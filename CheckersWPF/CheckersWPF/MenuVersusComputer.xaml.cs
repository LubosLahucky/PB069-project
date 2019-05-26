using CzechCheckers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Color = CzechCheckers.Color;

namespace CheckersWPF
{
    /// <summary>
    /// Interaction logic for MenuVersusHuman.xaml
    /// </summary>
    public partial class MenuVersusComputer : Window
    {
        public MenuVersusComputer()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validation())
            {
                return;
            }

            Close();

            var humanPlayer = new Player
            {
                Color = WhiteColor.IsChecked == true ? Color.White : Color.Black,
                Name = HumanName.Text
            };

            var computerPlayer = new ComputerPlayer
            {
                Color = WhiteColor.IsChecked == true ? Color.Black : Color.White,
                Name = ComputerName.Text
            };

            Board board = TwoRowVersion.IsChecked == true ? StandardBoards.Get2v2Board() : StandardBoards.Get3v3Board();
            Game game = WhiteColor.IsChecked == true 
                ? new Game(humanPlayer, computerPlayer, board) 
                : new Game(computerPlayer, humanPlayer, board);

            new Chessboard(game).ShowDialog();
        }

        private bool Validation()
        {
            return (TwoRowVersion.IsChecked == true || ThreeRowVersion.IsChecked == true)
                && (WhiteColor.IsChecked == true || BlackColor.IsChecked == true)
                && !string.IsNullOrWhiteSpace(HumanName.Text)
                && !string.IsNullOrWhiteSpace(ComputerName.Text);
        }
    }
}
