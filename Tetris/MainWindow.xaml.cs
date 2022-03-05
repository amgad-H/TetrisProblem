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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Tile-Cyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Tile-Yellow.jpg", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Tile-Orange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Tile-Blue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Tile-Green.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Tile-Purple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Tile-Red.png", UriKind.Relative))
        };

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative))
        };

        private readonly Image[,] imageControls;
        private readonly int maxDelay = 1000;
        private readonly int minDelay = 75;
        private readonly int delayDecrease = 25;

        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;

            for(int r = 0; r < grid.Rows; r++)
            {
                for(int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };

                    Canvas.SetTop(imageControl, (r - 2) * cellSize + 10);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r,c] = imageControl;
                }
            }

            return imageControls;
        }

        private GameState gameState = new GameState();
        private ScoreManager scoreManager;
        private string playerName = "";
        private bool canStartGame = false;
        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);
        }

        private void DrawGrid(GameGrid grid)
        {
            for(int r = 0; r < grid.Rows; r++)
            {
                for(int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Opacity = 1;
                    imageControls[r,c].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach(Position p in block.TilesPosition())
            {
                imageControls[p.Row, p.Column].Opacity = 1;
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        private void DrawNextBlock(BlockQueue blockQueue)
        {
            Block next = blockQueue.NextBlock;
            NextImage.Source = blockImages[next.Id];
        }

        private void DrawGhostBlock(Block block)
        {
            int dropDistance = gameState.BlockDropDistance();
            foreach(Position p in block.TilesPosition())
            {
                imageControls[p.Row + dropDistance, p.Column].Opacity = 0.25;
                imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id];
            }
        }

        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawGhostBlock(gameState.CurrentBlock);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
            ScoreText.Text = $"Score: {gameState.Score}";
        }

        private async Task GameLoop()
        {
            ShowHighScores();
            Draw(gameState);
            while (!gameState.GameOver)
            {
                if (canStartGame)
                {
                    int delay = Math.Max(minDelay, maxDelay - (gameState.Score * delayDecrease));
                    await Task.Delay(delay);
                    await Task.Delay(500);
                    gameState.MoveBlockDown();
                    Draw(gameState);
                }
                else
                {
                    await Task.Delay(500);
                }
            }
            canStartGame = false;
            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {gameState.Score}";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!gameState.GameOver)
            {
                switch (e.Key)
                {
                    case Key.A:
                        gameState.MoveBlockLeft();
                        break;
                    case Key.D:
                        gameState.MoveBlockRight();
                        break;
                    case Key.S:
                        gameState.MoveBlockDown();
                        break;
                    case Key.E:
                        gameState.RotateBlockCW();
                        break;
                    case Key.Q:
                        gameState.RotateBlockCCW();
                        break;
                    case Key.Space:
                        gameState.DropBlock();
                        break;
                    default:
                        return;
                }

                Draw(gameState);
            }
        }

        private void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            GameStartMenu.Visibility = Visibility.Visible;
            scoreManager = new ScoreManager();
        }

        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            scoreManager.AddScore(playerName, gameState.Score);
            scoreManager.SaveScores();
            gameState = new GameState();
            canStartGame = true;
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }

        private async void Play_Click(object sender, RoutedEventArgs e)
        {
            playerName = PlayerName.Text;
            gameState = new GameState();
            canStartGame = true;
            GameStartMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }

        private async void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            scoreManager.AddScore(playerName, gameState.Score);
            scoreManager.SaveScores();
            gameState = new GameState();
            canStartGame = false;
            GameOverMenu.Visibility = Visibility.Hidden;
            GameStartMenu.Visibility = Visibility.Visible;
        }


        //Method will show Highscores once any are available
        private void ShowHighScores()
        {
            Resources["Players"] = scoreManager.GetScoreDictionary();
        }
    }
}
