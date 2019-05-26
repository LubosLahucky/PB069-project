using CzechCheckers;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CheckersWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Chessboard : Window
    {
        private readonly Game game;

        private Field lastField = Field.Invalid;

        public Chessboard(Game game)
        {
            this.game = game;
            game.GameEnded += Game_GameEnded;

            InitializeComponent();

            for (uint row = 0; row <= Board.MaxRow; ++row)
            { 
                for (uint col = 0; col <= Board.MaxCol; ++col)
                {
                    var field = new Image();
                    field.MouseDown += Field_Click;
                    field.VerticalAlignment = VerticalAlignment.Stretch;
                    field.HorizontalAlignment = HorizontalAlignment.Stretch;
                    chessboardGrid.Children.Add(field);
                }
            }
            game.Start();

            UpdateUI();
        }

        private void Game_GameEnded(object sender, GameEndedEventArgs e)
        {
            UpdateUI();
            MessageBox.Show(
                e.Winner == null ? "Draw!" : $"Player { e.Winner.Name } wins!", 
                "GAME ENDED", 
                MessageBoxButton.OK, 
                MessageBoxImage.Information
                );

            Close();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Serialized JSON object (*.json)|*.json|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                Close();
                Game game = JsonConvert.DeserializeObject<Game>(
                    File.ReadAllText(dialog.FileName),
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Arrays
                    }
                    );
             
                new Chessboard(game).ShowDialog();
            }
        }

        private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "Serialized JSON object (*.json)|*.json|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == true)
            {
                JsonSerializer serializer = new JsonSerializer
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    NullValueHandling = NullValueHandling.Include,
                    Formatting = Formatting.Indented
                };
                using (StreamWriter sw = new StreamWriter(dialog.FileName))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, game);
                }
            }
        }

        private void OfferDrawMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (game.PlayerNotOnMove is ComputerPlayer computer)
            {
                MessageBox.Show(
                $"The computer does not like draws.",
                "BAD COMPUTER",
                MessageBoxButton.OK,
                MessageBoxImage.Information
                );
                return;
            }

            var result = MessageBox.Show(
                $"Player { game.PlayerOnMove.Name } offers you a draw.\nDo you accept?",
                "DRAW OFFER",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
                );

            if (result == MessageBoxResult.Yes)
            {
                game.EndGame(null);
                return;
            }

            MessageBox.Show(
                $"Player { game.PlayerNotOnMove.Name } refused the draw offer.",
                "DRAW OFFER REFUSED",
                MessageBoxButton.OK,
                MessageBoxImage.Information
                );
          
        }

        private void SurrenderMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to surrender?",
                "SURRENDER",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
                );

            if (result == MessageBoxResult.Yes)
            {
                game.EndGame(game.PlayerNotOnMove);
            }
        }

        private void MainMenuMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Field_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Image image))
                return;

            var index = chessboardGrid.Children.IndexOf(image);
            var imageField = GetFieldByIndex(index);
            var chessboardField = GetBoardFieldFromImageField(imageField);

            if (lastField.IsValid() && game.TryMove(lastField, chessboardField))
            {
                UpdateUI();
                if (!game.Board.IsTurnOver())
                {
                    lastField = chessboardField;
                    HighlightFields(game.Board.PieceMoves(chessboardField, game.PlayerOnMove.Color));
                }
                return;
            }
            var pieceMoves = game.Board.PieceMoves(chessboardField, game.PlayerOnMove.Color);
            if (!pieceMoves.Any())
            {
                lastField = Field.Invalid;
                UpdateUI();
                return;
            }

            UpdateUI();
            HighlightFields(pieceMoves);

            lastField = chessboardField;
        }

        private static Field GetBoardFieldFromImageField(Field imageField)
        {
            return new Field
            {
                Row = Board.MaxRow - imageField.Row,
                Column = imageField.Column
            };
        }

        private void HighlightFields(IEnumerable<Field> fields)
        {
            foreach (Field field in fields)
            {
                HighlightField(field);
            }
        }

        private int GetIndexByField(Field field)
        {
            return field.Row * (Board.MaxRow + 1) + field.Column; 
        }

        private Field GetFieldByIndex(int index)
        {
            return new Field
            {
                Row = index / (Board.MaxRow + 1),
                Column = index % (Board.MaxRow + 1)
            };
        }
        
        private void HighlightField(Field boardField)
        {
            var image = chessboardGrid
                .Children
                .OfType<Image>()
                .First(x => GetBoardFieldFromImageField(GetFieldByIndex(chessboardGrid.Children.IndexOf(x))).Equals(boardField));

            image.Source = new BitmapImage(new Uri(@"./res/highlight.png", UriKind.Relative));
        }

        private void UpdateUI()
        {
            foreach (var element in chessboardGrid.Children)
            {
                if (!(element is Image image))
                    continue;

                ImageSource source;
                
                var index = chessboardGrid.Children.IndexOf(image);

                Field imageField = GetFieldByIndex(index);
                Field boardField = GetBoardFieldFromImageField(imageField);
                IPiece piece = game.Board[boardField];
                if (piece == null)
                {
                    source = new BitmapImage(new Uri(boardField.IsBlack() ? @"./res/black-empty.png" : @"./res/white-empty.png", UriKind.Relative));
                }
                else
                {
                    string path = @"./res/" + piece.ToString() + ".png";
                    source = new BitmapImage(new Uri(path, UriKind.Relative));
                }
                image.Source = source;
            }

            statusTextBlock.Text = $"Player { game.PlayerOnMove.Name } ({ game.PlayerOnMove.Color }) is on move!";
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            chessboardGrid.Width = chessboardGrid.ActualHeight;
            e.Handled = true;
        }
    }
}
