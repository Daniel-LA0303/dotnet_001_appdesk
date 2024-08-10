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

namespace proapp
{
    /// <summary>
    /// Lógica de interacción para LoginReminderWindow.xaml
    /// </summary>

    public partial class LoginReminderWindow : Window
    {
        public LoginReminderWindow()
        {
            InitializeComponent();
        }

        private void OnLoginButtonClick(object sender, RoutedEventArgs e)
        {
            // Abre la ventana de inicio de sesión en la aplicación principal
            ((MainWindow)Application.Current.MainWindow).ShowLoginView();
            this.Close(); // Cierra la ventana emergente
        }
    }
}
