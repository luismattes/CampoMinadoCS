using CampoMinado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampoMinado
{
    class Tabuleiro
    {
        private Espaco[,] espaco = new Espaco[Jogo.x, Jogo.y];

        public Tabuleiro()
        {
            for (int i = 0; i < Jogo.x; i++)
            {
                for (int j = 0; j < Jogo.y; j++)
                {
                    espaco[i, j] = new Espaco(false);
                }
            }
            setBombs();
            calculate();
        }

        private void setBombs()
        {
            int count = Jogo.bombCount;
            int x;
            int y;
            while (count > 0)
            {
                Random random = new Random();
                x = random.Next(0 , Jogo.x);
                y = random.Next(0, Jogo.y);
                while (espaco[x, y].getBomba())
                {
                    y++;
                    if (y >= Jogo.y)
                    {
                        x++;
                        y = 0;
                    }
                    if (x >= Jogo.x && y >= Jogo.y)
                    {
                        x = 0;
                        y = 0;
                    }
                }
                espaco[x, y] = new Espaco(true);
                espaco[x, y].setImagem("/Imagens/bomb.png");
                count--;
            }
        }

        private void calculate()
        {
            int cont = 0;
            for (int i = 0; i < Jogo.x; i++)
            {
                for (int j = 0; j < Jogo.y; j++)
                {
                    if (i < Jogo.x - 1 && espaco[i + 1, j].getBomba())
                    {
                        cont++;
                    }
                    if (i > 0 && espaco[i - 1, j].getBomba())
                    {
                        cont++;
                    }
                    if (j > 0 && espaco[i, j - 1].getBomba())
                    {
                        cont++;
                    }
                    if (j < Jogo.y - 1 && espaco[i, j + 1].getBomba())
                    {
                        cont++;
                    }
                    if (i < Jogo.x - 1 && j < Jogo.y - 1 && espaco[i + 1, j + 1].getBomba())
                    {
                        cont++;
                    }
                    if (i > 0 && j > 0 && espaco[i - 1, j - 1].getBomba())
                    {
                        cont++;
                    }
                    if (i > 0 && j < Jogo.y - 1 && espaco[i - 1, j + 1].getBomba())
                    {
                        cont++;
                    }
                    if (i < Jogo.x - 1 && j > 0 && espaco[i + 1, j - 1].getBomba())
                    {
                        cont++;
                    }
                    if (!espaco[i, j].getBomba())
                    {
                        espaco[i, j].setBombaPerto(cont);
                        espaco[i, j].setImagem("/Imagens/" + cont + ".png");
                    }
                    cont = 0;
                }
            }
        }

        public Espaco[,] getSpace()
        {
            return this.espaco;
        }
    }
}
