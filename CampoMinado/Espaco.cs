using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CampoMinado
{
    class Espaco(bool bomba)
    {
        private int bombaPerto;
        private bool bomba = bomba;
        private Image imagem;
        private bool flagged = false;
        private bool flipped = false;
        private bool marked = false;
        public static int tamanhoQuadrado = 30;

        public void setBombaPerto(int b)
        {
            bombaPerto = b;
        }

        public int getBombaPerto()
        {
            return bombaPerto;
        }

        public void setImagem(string filename)
        {
            this.imagem = Jogo.setImagem(filename);
        }

        public Image getImagem()
        {
            return imagem;
        }

        public bool getBomba()
        {
            return this.bomba;
        }

        public void setFlagged()
        {
            this.flagged = !flagged;
        }

        public bool getFlagged()
        {
            return this.flagged;
        }

        public void flip()
        {
            this.flipped = true;
        }

        public bool getFlipped()
        {
            return this.flipped;
        }

        public void setMarked()
        {
            this.marked = true;
        }

        public bool getMarked()
        {
            return this.marked;
        }
    }
}
