using proapp.config;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

    public partial class AuthUser : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Alimento> AlimentoUserSelected { get; private set; }

        public AuthUser()
        {
            InitializeComponent();
            LoadUserInfo();
            LoadAlimentos();
            LoadOrders();

            AlimentoUserSelected = new ObservableCollection<Alimento>();

            // Vincular la colección observable a la lista en la UI
            SelectedFoodList.ItemsSource = AlimentoUserSelected;

            // Suscribirse al evento CollectionChanged para actualizar el total cuando cambie la lista
            AlimentoUserSelected.CollectionChanged += (s, e) => UpdateTotal();
        }


        private void LoadOrders()
        {
            var currentUser = ApplicationState.Instance.CurrentUser;

            if (currentUser != null)
            {
                var userOrders = ApplicationState.Instance.Ordenes
                    .Where(order => order.UserId == currentUser.Id)
                    .Select(order => new
                    {
                        OrderNumber = order._id,
                        FoodCount = order.Selection.Count,
                        Total = order.Total.ToString("C"),
                        Note = order.Nota
                    }).ToList();

                OrdersList.ItemsSource = userOrders;
            }
        }

        private void LoadUserInfo()
        {
            var currentUser = ApplicationState.Instance.CurrentUser;

            if (currentUser != null)
            {
                UserNameTextBlock.Text = $"Bienvenido, {currentUser.Username}";
            }
            else
            {
                UserNameTextBlock.Text = "Usuario no disponible";
            }
        }

        private void LoadAlimentos()
        {
            var alimentos = ApplicationState.Instance.Alimentos;
            FoodList.ItemsSource = alimentos;
        }

        private void OnAgregarClick(object sender, RoutedEventArgs e)
        {
            var currentUser = ApplicationState.Instance.CurrentUser;

            var button = sender as Button;
            var selectedFood = button.DataContext as Alimento;

            if (selectedFood != null && currentUser != null)
            {
                AlimentoUserSelected.Add(selectedFood);

                string message = $"Has agregado: {selectedFood.Nombre}\n" +
                                 $"Descripción: {selectedFood.Descripcion}\n" +
                                 $"Precio: {selectedFood.Precio:C}";

                MessageBox.Show(message, "Detalles del alimento");
            }
        }

        private void UpdateTotal()
        {
            decimal total = AlimentoUserSelected.Sum(alimento => alimento.Precio);

            TotalTextBlock.Text = $"Total: {total:C}";
        }

        private static string GenerateRandomId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 24); 
        }


        private void OnAgregarOrdenClick(object sender, RoutedEventArgs e)
        {
            var currentUser = ApplicationState.Instance.CurrentUser;

            if (AlimentoUserSelected.Any() && currentUser != null)
            {
                var nuevaOrden = new Orden
                {
                    _id = GenerateRandomId(),
                    Selection = new List<Alimento>(AlimentoUserSelected),
                    Total = AlimentoUserSelected.Sum(a => a.Precio),
                    Nota = NoteTextBox.Text,
                    UserId = currentUser.Id
                };

                ApplicationState.Instance.Ordenes.Add(nuevaOrden);

                AlimentoUserSelected.Clear();
                NoteTextBox.Clear();

                //Crea y muestra el mensaje de confirmación
                var mensaje = new StringBuilder();
                mensaje.AppendLine("Orden agregada exitosamente.");
                mensaje.AppendLine("Detalles de la orden:");
                mensaje.AppendLine("Usuario: " + currentUser.Id);
                mensaje.AppendLine("Nota: " + nuevaOrden.Nota);
                mensaje.AppendLine("Total: " + nuevaOrden.Total.ToString("C"));
                mensaje.AppendLine("Alimentos seleccionados:");

                foreach (var alimento in nuevaOrden.Selection)
                {
                    mensaje.AppendLine($"- {alimento.Nombre}: {alimento.Precio.ToString("C")}");
                }

                MessageBox.Show(mensaje.ToString(), "Confirmación");

                // Actualizar la vista de órdenes
                LoadOrders();
            }
            else
            {
                MessageBox.Show("No se pueden agregar órdenes sin seleccionar alimentos.", "Error");
            }
        }



        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private decimal CalcularTotal(List<Alimento> alimentos)
        {
            return alimentos.Sum(alimento => alimento.Precio);
        }


        private void OnOrdenesClick(object sender, RoutedEventArgs e)
        {
            AlimentosView.Visibility = Visibility.Collapsed;
            OrdenesView.Visibility = Visibility.Visible;
        }

        private void OnAlimentosClick(object sender, RoutedEventArgs e)
        {
            AlimentosView.Visibility = Visibility.Visible;
            OrdenesView.Visibility = Visibility.Collapsed;
        }

        private void OnCerrarClick(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        private void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();

            this.Close();
        }
    }
}
