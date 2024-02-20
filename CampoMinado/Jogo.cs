using System.Collections;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CampoMinado
{
    class Jogo
    {
        private Tabuleiro layout;
        public static int x;
        public static int y;
        public static int bombCount;
        public bool began = false;
        private bool vitoria = false;
        private bool derrota = false;
        private int bandeiras;

        public Jogo(char dificuldade)
        {
            switch (dificuldade)
            {
                case 'P':
                    x = 9; y = 9; bombCount = 10;

                    break;

                case 'M':
                    x = 16; y = 16; bombCount = 40;

                    break;

                case 'G':
                    x = 30; y = 16; bombCount = 99;

                    break;
            }

            layout = new Tabuleiro();
            bandeiras = bombCount;
        }

        public List<int[]> perda()
        {
            List<int[]> lista = new();
            for (int i = 0; i < Jogo.x; i++)
            {
                for (int j = 0; j < Jogo.y; j++)
                {
                    if (getTab().getSpace()[i, j].getBomba())
                    {
                        int[] pac = [i, j];
                        lista.Add(pac);
                    }
                }
            }
            this.derrota = true;
            return lista;
        }

        public void vitoriaF()
        {
            int count = 0;
            for (int i = 0; i < Jogo.x; i++)
            {
                for (int j = 0; j < Jogo.y; j++)
                {
                    if (getTab().getSpace()[i, j].getFlipped())
                    {
                        count++;
                    }
                }
            }
            if (count >= (x * y) - bombCount)
            {
                this.vitoria = true;
            }
        }

        public Tabuleiro getTab()
        {
            return this.layout;
        }

        public bool getDerota()
        {
            return this.derrota;
        }

        public bool getVitoria()
        {
            return this.vitoria;
        }

        public void tiraBandeira()
        {
            bandeiras++;
        }

        public void colocaBandeiras()
        {
            bandeiras--;
        }

        public int getBandeiras()
        {
            return this.bandeiras;
        }

        public List<int[]> clear(int x, int y)
        {
            List<int[]> lista = new List<int[]>();
            if (x < Jogo.x - 1)
            {
                lista.Add(new int[] { x + 1, y });
                if (getTab().getSpace()[x + 1, y].getBombaPerto() == 0 &&
                        !getTab().getSpace()[x + 1, y].getMarked())
                {
                    getTab().getSpace()[x + 1, y].setMarked();
                    lista.AddRange(clear(x + 1, y));
                }
            }
            if (x > 0)
            {
                lista.Add(new int[] { x - 1, y });
                if (getTab().getSpace()[x - 1, y].getBombaPerto() == 0 &&
                        !getTab().getSpace()[x - 1, y].getMarked())
                {
                    getTab().getSpace()[x - 1, y].setMarked();
                    lista.AddRange(clear(x - 1, y));
                }
            }
            if (y > 0)
            {
                lista.Add(new int[] { x, y - 1 });
                if (getTab().getSpace()[x, y - 1].getBombaPerto() == 0 &&
                        !getTab().getSpace()[x, y - 1].getMarked())
                {
                    getTab().getSpace()[x, y - 1].setMarked();
                    lista.AddRange(clear(x, y - 1));
                }
            }
            if (y < Jogo.y - 1)
            {
                lista.Add(new int[] { x, y + 1 });
                if (getTab().getSpace()[x, y + 1].getBombaPerto() == 0 &&
                        !getTab().getSpace()[x, y + 1].getMarked())
                {
                    getTab().getSpace()[x, y + 1].setMarked();
                    lista.AddRange(clear(x, y + 1));
                }
            }
            if (x < Jogo.x - 1 && y < Jogo.y - 1)
            {
                lista.Add(new int[] { x + 1, y + 1 });
                if (getTab().getSpace()[x + 1, y + 1].getBombaPerto() == 0 &&
                        !getTab().getSpace()[x + 1, y + 1].getMarked())
                {
                    getTab().getSpace()[x + 1, y + 1].setMarked();
                    lista.AddRange(clear(x + 1, y + 1));
                }
            }
            if (x > 0 && y > 0)
            {
                lista.Add(new int[] { x - 1, y - 1 });
                if (getTab().getSpace()[x - 1, y - 1].getBombaPerto() == 0 &&
                        !getTab().getSpace()[x - 1, y - 1].getMarked())
                {
                    getTab().getSpace()[x - 1, y - 1].setMarked();
                    lista.AddRange(clear(x - 1, y - 1));
                }
            }
            if (x > 0 && y < Jogo.y - 1)
            {
                lista.Add(new int[] { x - 1, y + 1 });
                if (getTab().getSpace()[x - 1, y + 1].getBombaPerto() == 0 &&
                        !getTab().getSpace()[x - 1, y + 1].getMarked())
                {
                    getTab().getSpace()[x - 1, y + 1].setMarked();
                    lista.AddRange(clear(x - 1, y + 1));
                }
            }
            if (x < Jogo.x - 1 && y > 0)
            {
                lista.Add(new int[] { x + 1, y - 1 });
                if (getTab().getSpace()[x + 1, y - 1].getBombaPerto() == 0 &&
                        !getTab().getSpace()[x + 1, y - 1].getMarked())
                {
                    getTab().getSpace()[x + 1, y - 1].setMarked();
                    lista.AddRange(clear(x + 1, y - 1));
                }
            }
            return lista;
        }

        public static Image setImagem(string filename)
        {
            Image imagem = new Image();
            imagem.Source = new BitmapImage(new Uri(filename, UriKind.Relative));
            return imagem;
        }
    }
}