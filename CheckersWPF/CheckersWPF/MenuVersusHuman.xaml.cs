using CzechCheckers;
using System.Windows;
using Color = CzechCheckers.Color;

namespace CheckersWPF
{
    /// <summary>
    /// Interaction logic for MenuVersusHuman.xaml
    /// </summary>
    public partial class MenuVersusHuman : Window
    {
        public MenuVersusHuman()
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
                return;

            Close();

            Player whitePlayer = new Player
            {
                Color = Color.White,
                Name = WhiteName.Text
            };
            Player blackPlayer = new Player
            {
                Color = Color.Black,
                Name = BlackName.Text
            };

            Board board = TwoRowVersion.IsChecked == true ? StandardBoards.Get2v2Board() : StandardBoards.Get3v3Board();
            Game game = new Game(whitePlayer, blackPlayer, board);

            new Chessboard(game).ShowDialog();
        }

        private bool Validation()
        {
            return (TwoRowVersion.IsChecked == true || ThreeRowVersion.IsChecked == true)
                && !string.IsNullOrWhiteSpace(WhiteName.Text)
                && !string.IsNullOrWhiteSpace(BlackName.Text);
        }
    }
}
