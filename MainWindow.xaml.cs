using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_ListBox_1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        public MainWindow()
        {
            InitializeComponent();

            List<Poblacion> listPoblaciones = new List<Poblacion>();

            listPoblaciones.Add(new Poblacion { Poblacion1 = "Santo Domingo", Poblacion2 = "Santiago", Temperatura1 = 30, Temperatura2 = 33 });
            listPoblaciones.Add(new Poblacion { Poblacion1 = "San Pedro", Poblacion2 = "San Cristobal", Temperatura1 = 32, Temperatura2 = 31 });
            listPoblaciones.Add(new Poblacion { Poblacion1 = "San Juan", Poblacion2 = "Punta Cana", Temperatura1 = 31, Temperatura2 = 35 });
            listPoblaciones.Add(new Poblacion { Poblacion1 = "Samana", Poblacion2 = "La Vega", Temperatura1 = 10, Temperatura2 = 40 });

            miListBox.ItemsSource = listPoblaciones;

            foreach (var p in listPoblaciones)
            {

                p.Porcentaje = Math.Abs(p.Temperatura1 - p.Temperatura2);
                string porcentaje1 = p.Porcentaje.ToString();
                p.Valor = porcentaje1 + "%";
            }


        }

        

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (miListBox.SelectedItem as Poblacion != null)
            {
                MessageBox.Show
                    (
                    (miListBox.SelectedItem as Poblacion).Poblacion1 + " " +
                    (miListBox.SelectedItem as Poblacion).Temperatura1 + " °C | " +
                    (miListBox.SelectedItem as Poblacion).Poblacion2 + " " +
                    (miListBox.SelectedItem as Poblacion).Temperatura2 + " °C"
                    );
            }
            else
                MessageBox.Show("¡Seleccione una poblacion!");
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Obtener el DataContext del TextBlock (que es el objeto Poblacion)
            var textBlock = sender as TextBlock;
            var dataContext = textBlock?.DataContext as Poblacion;

            if (dataContext != null)
            {
                MessageBox.Show
                    (
                    $"{dataContext.Poblacion1} {dataContext.Temperatura1} °C | " +
                    $"{dataContext.Poblacion2} {dataContext.Temperatura2} °C"
                    );
            }
        }
    }

    
    class Poblacion: INotifyPropertyChanged
    {
        private double porcentaje;
        private string valor;
        public string Poblacion1 {  get; set; }
        
        public int Temperatura1 { set; get; }

        public string Poblacion2 {  set; get; }

        public int Temperatura2 {  set; get; }

        public double Porcentaje
        {
            get { return porcentaje; }
            set 
            {
                porcentaje = value;
                cambio("Porcentaje");
            }
        }

        public string Valor
        {
            get { return valor; }
            set 
            {
                valor = value;
                cambio("Valor");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void cambio(string Property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Property));
            }
        }
    }
}
