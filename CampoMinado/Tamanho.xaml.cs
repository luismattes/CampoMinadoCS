using System.Windows;

namespace CampoMinado
{
    public partial class Tamanho : Window
    {
        public Tamanho() 
        {
            this.InitializeComponent();
        }

        private void tamanhoP_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow('P').Show();
            this.Close();
        }

        private void tamanhoM_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow('M').Show();
            this.Close();
        }

        private void tamanhoG_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow('G').Show();
            this.Close();
        }

        private void custom_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow('P').Show();
            this.Close();
        }
    }
}
