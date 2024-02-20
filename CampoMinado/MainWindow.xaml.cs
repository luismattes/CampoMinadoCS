using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CampoMinado
{
    public partial class MainWindow : Window
    {
        private int tempo;
        private Jogo jogo;
        private Button[,] espacos;
        DispatcherTimer dt = new DispatcherTimer();
        char dificuldade;

        public MainWindow(char dificuldade)
        {
            this.dificuldade = dificuldade;
            this.jogo = new Jogo(dificuldade);
            espacos = new Button[Jogo.x, Jogo.y];

            InitializeComponent();

            flags_textB.Text = jogo.getBandeiras().ToString();

            sun_button.Width = 40;
            sun_button.Content = setImagem2("C:\\Users\\louis\\source\\repos\\CampoMinado\\CampoMinado\\Imagens\\sun.png");
            sun_button.Height = 36;
            campo_grid.Width = (Jogo.x) * (Espaco.tamanhoQuadrado + 1.7);
            campo_grid.Height = (Jogo.y) * Espaco.tamanhoQuadrado;

            topo.Width = campo_grid.Width;
            topo.Height = 80;

            mainWindow.MaxHeight = mainWindow.MinHeight = topo.Height + campo_grid.Height;
            mainWindow.MaxWidth = mainWindow.MinWidth = campo_grid.Width;

            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < Jogo.x; i++)
            {
                campo_grid.RowDefinitions.Add(new RowDefinition());

                for (int j = 0; j < Jogo.y; j++)
                {
                    campo_grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Espaco.tamanhoQuadrado)});
                    Button espaco = new Button();
                    espaco.Width = espaco.Height = Espaco.tamanhoQuadrado;
                    espaco.PreviewMouseLeftButtonDown += Espaco_Click_Left;
                    espaco.PreviewMouseRightButtonDown += Espaco_Click_Right;

                    espaco.Content = Jogo.setImagem("/Imagens/facingDown.png");

                    espacos[i, j] = espaco;

                    Grid.SetRow(espaco, i);
                    Grid.SetColumn(espaco, j);

                    campo_grid.Children.Add(espaco);
                }
            }
        }

        private void Espaco_Click_Left(object sender, RoutedEventArgs e)
        {
            jogo.began = true;

            if (jogo.getDerota() || jogo.getVitoria())
            {
                return;
            }

            for (int i = 0; i < Jogo.x; i++)
            {
                for (int j = 0; j < Jogo.y; j++)
                {
                    if (sender == espacos[i, j] && !jogo.getTab().getSpace()[i, j].getFlagged())
                    {
                        espacos[i, j].Content = jogo.getTab().getSpace()[i, j].getImagem();
                        jogo.getTab().getSpace()[i, j].flip();

                        if (jogo.getTab().getSpace()[i, j].getBomba())
                        {
                            perda();
                        } 
                        else
                        {
                            if (jogo.getTab().getSpace()[i, j].getBombaPerto() == 0)
                            {
                                clear(i, j);
                            }
                            vitoria();
                        }
                        break;
                    }
                }
            }
        }

        private void Espaco_Click_Right(object sender, RoutedEventArgs e)
        {
            jogo.began = true;

            if (jogo.getDerota() || jogo.getVitoria())
            {
                return;
            }

            for (int i = 0; i < Jogo.x; i++)
            {
                for (int j = 0; j < Jogo.y; j++)
                {
                    if (sender == espacos[i, j] && !jogo.getTab().getSpace()[i, j].getFlipped())
                    {
                        if (jogo.getTab().getSpace()[i, j].getFlagged())
                        {
                            espacos[i, j].Content = Jogo.setImagem("/Imagens/facingDown.png");
                            jogo.tiraBandeira();
                        } 
                        else
                        {
                            espacos[i, j].Content = Jogo.setImagem("/Imagens/flagged.png");
                            jogo.colocaBandeiras();
                        }

                        jogo.getTab().getSpace()[i, j].setFlagged();
                        flags_textB.Text = jogo.getBandeiras().ToString();

                        break;
                    }
                }
            }
        }

        private void sun_Click(object sender, RoutedEventArgs e)
        {
            this.jogo = new Jogo(dificuldade);

            for (int i = 0; i < Jogo.x; i++)
            {
                for (int j = 0; j < Jogo.y; j++)
                {
                    espacos[i, j].Content = Jogo.setImagem("/Imagens/facingDown.png");
                }
            }
            sun_button.Content = setImagem2("C:\\Users\\louis\\source\\repos\\CampoMinado\\CampoMinado\\Imagens\\sun.png");
            flags_textB.Text = jogo.getBandeiras().ToString();
            tempo = 0;
            time_textB.Text = tempo.ToString().PadLeft(3, '0');
        }

        public void clear(int x, int y)
        {
            List<int[]> lista = jogo.clear(x, y);
            foreach (int[] a in lista)
            {
                espacos[a[0], a[1]].Content = jogo.getTab().getSpace()[a[0], a[1]].getImagem();
                jogo.getTab().getSpace()[a[0], a[1]].flip();
            }
        }

        private void timer(object sender, RoutedEventArgs e)
        {
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += atualizaTempo;
            dt.Start();
        }

        private void atualizaTempo(object sender, EventArgs e)
        {
            if (!jogo.getDerota() && !jogo.getVitoria() && jogo.began)
            {
                this.tempo++;
                time_textB.Text = tempo.ToString().PadLeft(3, '0');
            }
        }

        private Image setImagem2(string filename, int x = 0, int y = 0)
        {
            Image image = new Image();

            BitmapImage originalImage = new BitmapImage(new Uri(filename, UriKind.Relative));
            Int32Rect croppingRect = new Int32Rect(x, y, 90, 90);
            CroppedBitmap croppedImage = new CroppedBitmap(originalImage, croppingRect);

            image.Source = croppedImage;

            return image;
        }

        public void perda() 
        {
            List<int[]> lista = jogo.perda();
            foreach (int[] a in lista)
            {
                espacos[a[0], a[1]].Content = jogo.getTab().getSpace()[a[0], a[1]].getImagem();
            }
            sun_button.Content = setImagem2("C:\\Users\\louis\\source\\repos\\CampoMinado\\CampoMinado\\Imagens\\sun.png", 290 ,0);
        }

        public void vitoria()
        {
            jogo.vitoriaF();
            if (jogo.getVitoria())
            {
                for (int i = 0; i < Jogo.x; i++)
                {
                    for (int j = 0; j < Jogo.y; j++)
                    {
                        if (!jogo.getTab().getSpace()[i, j].getFlipped())
                        {
                            espacos[i, j].Content = Jogo.setImagem("/Imagens/flagged.png");
                        }
                    }
                }

                flags_textB.Text = "00";
                sun_button.Content = setImagem2("C:\\Users\\louis\\source\\repos\\CampoMinado\\CampoMinado\\Imagens\\sun.png", 190, 0);
            }
        }
    }
}