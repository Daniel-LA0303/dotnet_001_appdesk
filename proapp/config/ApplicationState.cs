using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace proapp.config
{
    /**
     * Estado global
     * 
     */
    class ApplicationState
    {
        private static readonly ApplicationState _instance = new ApplicationState();

        public List<Alimento> Alimentos { get; set; }

        public List<Usuario> Usuarios { get; set; }

        public Usuario CurrentUser { get; set; }

        public ObservableCollection<Orden> Ordenes { get; set; }


        public List<Alimento> AlimentoUserSelected { get; set; }


        private ApplicationState()
        {
            Alimentos = new List<Alimento>();
            Usuarios = new List<Usuario>();
            Ordenes = new ObservableCollection<Orden>();
            AlimentoUserSelected = new List<Alimento>();
        }

        public static ApplicationState Instance
        {
            get { return _instance; }
        }
    }

    public class Alimento
    {
        public string Id { get; set; } 
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }

    public class Usuario
    {
        public string Id { get; set; }        
        public string Username { get; set; } 
        public DateTime DOB { get; set; }      
        public string PWD { get; set; }       
    }

    public class Orden
    {
        public string _id { get; set; }
        public string UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Nota { get; set; }
        public decimal Total { get; set; }
        public List<Alimento> Selection { get; set; }

        public Orden()
        {
            Selection = new List<Alimento>();
        }
    }
}
