using proapp.config;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace proapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        private const string ExpectedDateFormat = @"\d{4}(-\d{2}){2}"; // Expresión regular para la fecha en formato YYYY-MM-DD
        private const string ExpectedPassword = "1234"; // Contraseña esperada

        private DispatcherTimer _timer;
        private int CountdownTime = 1; // Tiempo en segundos


        public MainWindow()
        {
            InitializeComponent();
            LoadAlimentos();
            StartCountdown();

        }

        private void LoadAlimentos()
        {
            // Cargar alimentos desde el estado de la aplicación
            var alimentos = ApplicationState.Instance.Alimentos;

            // Establecer el ItemSource del ListBox
            FoodList.ItemsSource = alimentos;
        }

        private void StartCountdown()
        {
            // Inicializa el temporizador
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // Intervalo de 1 segundo
            };
            _timer.Tick += Timer_Tick; // Evento del temporizador
            _timer.Start(); // Inicia el temporizador
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Verifica si han pasado 30 segundos
            if (CountdownTime <= 0)
            {
                _timer.Stop();
                ShowLoginReminder(); // Muestra el mensaje emergente
            }
            else
            {
                CountdownTime--; // Disminuye el contador
            }
        }

        private void ShowLoginReminder()
        {
            // Crea y muestra la ventana emergente
            LoginReminderWindow reminderWindow = new LoginReminderWindow();
            reminderWindow.Owner = this; // Establece la ventana principal como la ventana principal
            reminderWindow.ShowDialog(); // Muestra la ventana emergente
        }

        public void ShowLoginView()
        {
            LoginView.Visibility = Visibility.Visible;
            AlimentosView.Visibility = Visibility.Collapsed;
        }
        private void OnAlimentosClick(object sender, RoutedEventArgs e)
        {
            AlimentosView.Visibility = Visibility.Visible;
            LoginView.Visibility = Visibility.Collapsed;
        }

        private void OnLoginClick(object sender, RoutedEventArgs e)
        {
            LoginView.Visibility = Visibility.Visible;
            AlimentosView.Visibility = Visibility.Collapsed;
        }

        private void OnCerrarClick(object sender, RoutedEventArgs e)
        {
            this.Close(); // Cierra la aplicación
        }

        private void OnLoginSubmitClick(object sender, RoutedEventArgs e)
        {
            string dobText = DOBTextBox.Text;
            string password = PasswordBox.Password;

            // Verificar formato de la fecha
            bool isDateValid = Regex.IsMatch(dobText, ExpectedDateFormat);

            // Si la fecha no es válida, mostrar mensaje de error
            if (!isDateValid)
            {
                MessageBox.Show("Formato de fecha inválido. Use el formato YYYY-MM-DD.");
                return;
            }

            // Convertir la fecha ingresada a DateTime
            DateTime dob;
            bool isDateParsed = DateTime.TryParseExact(dobText, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dob);

            // Si no se puede parsear la fecha, mostrar mensaje de error
            if (!isDateParsed)
            {
                MessageBox.Show("Fecha inválida. Verifique el formato y la fecha ingresada.");
                return;
            }

            // Buscar el usuario con la fecha de nacimiento ingresada
            var user = ApplicationState.Instance.Usuarios.FirstOrDefault(u => u.DOB.Date == dob.Date);

            if (user != null)
            {
                // Verificar si la contraseña ingresada coincide con la contraseña del usuario encontrado
                bool isPasswordValid = password == user.PWD;

                if (isPasswordValid)
                {
                    // Si la contraseña es correcta, mostrar la ventana AuthUser
                    ApplicationState.Instance.CurrentUser = user;
                    AuthUser authUserWindow = new AuthUser();
                    authUserWindow.Show();

                    // Cerrar la ventana de inicio de sesión
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta.");
                }
            }
            else
            {
                MessageBox.Show("No se encontró ningún usuario con la fecha de nacimiento ingresada.");
            }
        }
    }
}