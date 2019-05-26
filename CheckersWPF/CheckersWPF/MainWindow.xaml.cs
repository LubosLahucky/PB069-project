using CzechCheckers;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CheckersWPF
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HumanButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MenuVersusHuman().ShowDialog();
            Show();
        }

        private void ComputerButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new MenuVersusComputer().ShowDialog();
            Show();
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Serialized JSON object (*.json)|*.json|All files (*.*)|*.*"
            }; 
            if (dialog.ShowDialog() == true)
            {
                Hide();
                Game game = JsonConvert.DeserializeObject<Game>(
                    File.ReadAllText(dialog.FileName), 
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    }
                    );

                new Chessboard(game).ShowDialog();
                Show();
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
