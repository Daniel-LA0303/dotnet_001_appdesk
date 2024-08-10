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

    public partial class MainWindow : Window
    {

        private const string ExpectedDateFormat = @"\d{4}(-\d{2}){2}"; 
        //private const string ExpectedPassword = "1234"; 

        private DispatcherTimer _timer;
        private int CountdownTime = 1; 


        public MainWindow()
        {
            InitializeComponent();
            LoadAlimentos();
            StartCountdown();

        }

        private void LoadAlimentos()
        {
  
            var alimentos = ApplicationState.Instance.Alimentos;

            FoodList.ItemsSource = alimentos;
        }

        private void StartCountdown()
        {

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5) 
            };
            _timer.Tick += Timer_Tick; 
            _timer.Start(); 
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (CountdownTime <= 0)
            {
                _timer.Stop();
                ShowLoginReminder(); 
            }
            else
            {
                CountdownTime--; 
            }
        }

        private void ShowLoginReminder()
        {
 
            LoginReminderWindow reminderWindow = new LoginReminderWindow();
            reminderWindow.Owner = this; 
            reminderWindow.ShowDialog(); 
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
            this.Close();
        }

        private void OnLoginSubmitClick(object sender, RoutedEventArgs e)
        {
            string dobText = DOBTextBox.Text;
            string password = PasswordBox.Password;

            //validando con regex
            bool isDateValid = Regex.IsMatch(dobText, ExpectedDateFormat);


            if (!isDateValid)
            {
                MessageBox.Show("Formato de fecha inválido. Use el formato YYYY-MM-DD.");
                return;
            }


            DateTime dob;
            bool isDateParsed = DateTime.TryParseExact(dobText, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out dob);


            if (!isDateParsed)
            {
                MessageBox.Show("Fecha inválida. Verifique el formato y la fecha ingresada.");
                return;
            }


            var user = ApplicationState.Instance.Usuarios.FirstOrDefault(u => u.DOB.Date == dob.Date);

            if (user != null)
            {
                
                bool isPasswordValid = password == user.PWD;

                if (isPasswordValid)
                {
                    ApplicationState.Instance.CurrentUser = user;
                    AuthUser authUserWindow = new AuthUser();
                    authUserWindow.Show();

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