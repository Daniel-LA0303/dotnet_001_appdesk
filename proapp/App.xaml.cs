using proapp.config;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace proapp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Inicializar el estado con datos 
            ApplicationState.Instance.Alimentos.AddRange(new List<Alimento>
            {
                new Alimento
                {
                    Id = "66b6ca34e7b3d0f651826db0",
                    Nombre = "Pizza",
                    Descripcion = "Pizza con queso, tomate y albahaca.",
                    Precio = 12.99m
                },
                new Alimento
                {
                    Id = "66b6ca34e7b3d0f651826db1",
                    Nombre = "Ensalada",
                    Descripcion = "Ensalada fresca con lechuga, tomate y zanahoria.",
                    Precio = 8.99m
                },
                new Alimento
                {
                    Id = "66b6ca34e7b3d0f651826db2",
                    Nombre = "Hamburguesa",
                    Descripcion = "Hamburguesa con carne, queso, y pan.",
                    Precio = 10.99m
                },
                new Alimento
                {
                    Id = "66b6ca34e7b3d0f651826db3",
                    Nombre = "Sopa",
                    Descripcion = "Sopa casera con verduras y fideos.",
                    Precio = 7.99m
                },
                new Alimento
                {
                    Id = "66b6ca34e7b3d0f651826db4",
                    Nombre = "Tacos",
                    Descripcion = "Tacos de carne con salsa y guacamole.",
                    Precio = 9.99m
                },
                new Alimento
                {
                    Id = "66b6ca34e7b3d0f651826db5",
                    Nombre = "Pasta",
                    Descripcion = "Pasta con salsa de tomate y queso parmesano.",
                    Precio = 11.99m
                },
                new Alimento
                {
                    Id = "66b6ca34e7b3d0f651826db6",
                    Nombre = "Curry",
                    Descripcion = "Curry de pollo con arroz basmati.",
                    Precio = 13.99m
                },
                new Alimento
                {
                    Id = "66b6ca34e7b3d0f651826db7",
                    Nombre = "Sushi",
                    Descripcion = "Sushi variado con pescado fresco.",
                    Precio = 15.99m
                },
                new Alimento
                {
                    Id = "66b6ca34e7b3d0f651826db8",
                    Nombre = "Sandwich",
                    Descripcion = "Sandwich de jamón y queso con pan integral.",
                    Precio = 6.99m
                },
                new Alimento
                {
                    Id = "66b6ca34e7b3d0f651826db9",
                    Nombre = "Brownie",
                    Descripcion = "Brownie de chocolate con nueces.",
                    Precio = 5.99m
                }
            });

            ApplicationState.Instance.Usuarios.AddRange(new List<Usuario>
            {
                new Usuario
                {
                    Id = "66b6cb97e7b3d0f651826dbc",
                    Username = "Pablo",
                    DOB = DateTime.Parse("2000-01-01"),
                    PWD = "1234"
                },
                new Usuario
                {
                    Id = "66b6cb97e7b3d0f651826dbd",
                    Username = "Miguel",
                    DOB = DateTime.Parse("2001-01-01"),
                    PWD = "1234"
                }
            });

            var ordenes = new List<Orden>
            {
                //ordenes para el usuario Pablo
                new Orden
                {
                    _id = "66b6cc97e7b3d0f651826dbf1",
                    UserId = "66b6cb97e7b3d0f651826dbc",
                    Timestamp = DateTime.Now.AddHours(-2),
                    Nota = "Orden para la cena.",
                    Total = 22.98m,
                    Selection = new List<Alimento>
                    {
                        ApplicationState.Instance.Alimentos.First(a => a.Id == "66b6ca34e7b3d0f651826db0"), // Pizza
                        ApplicationState.Instance.Alimentos.First(a => a.Id == "66b6ca34e7b3d0f651826db3")  // Sopa
                    }
                },
                new Orden
                {
                    _id = "66b6cc97e7b3d0f651826dbf2",
                    UserId = "66b6cb97e7b3d0f651826dbc",
                    Timestamp = DateTime.Now.AddHours(-1),
                    Nota = "Orden para el almuerzo.",
                    Total = 19.98m,
                    Selection = new List<Alimento>
                    {
                        ApplicationState.Instance.Alimentos.First(a => a.Id == "66b6ca34e7b3d0f651826db2"), // Hamburguesa
                        ApplicationState.Instance.Alimentos.First(a => a.Id == "66b6ca34e7b3d0f651826db4")  // Tacos
                    }
                },
                //ordenes para el usuario Miguel
                new Orden
                {
                    _id = "66b6cc97e7b3d0f651826dbf3",
                    UserId = "66b6cb97e7b3d0f651826dbd",
                    Timestamp = DateTime.Now.AddHours(-3),
                    Nota = "Orden para la cena.",
                    Total = 20.98m,
                    Selection = new List<Alimento>
                    {
                        ApplicationState.Instance.Alimentos.First(a => a.Id == "66b6ca34e7b3d0f651826db5"), // Pasta
                        ApplicationState.Instance.Alimentos.First(a => a.Id == "66b6ca34e7b3d0f651826db9")  // Brownie
                    }
                },
                new Orden
                {
                    _id = "66b6cc97e7b3d0f651826dbf4",
                    UserId = "66b6cb97e7b3d0f651826dbd",
                    Timestamp = DateTime.Now.AddHours(-4),
                    Nota = "Orden para el almuerzo.",
                    Total = 14.98m,
                    Selection = new List<Alimento>
                    {
                        ApplicationState.Instance.Alimentos.First(a => a.Id == "66b6ca34e7b3d0f651826db6"), // Curry
                        ApplicationState.Instance.Alimentos.First(a => a.Id == "66b6ca34e7b3d0f651826db8")  // Sandwich
                    }
                }
            };

            foreach (var orden in ordenes)
            {
                ApplicationState.Instance.Ordenes.Add(orden);
            }
        }

    }
}


