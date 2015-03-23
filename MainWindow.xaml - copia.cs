using System;
using System.Collections.Generic;
using System.Linq;
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

namespace LenguajeProgramacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string formato = @"\b\d{2}:\d{2}-\d{2}:\d{2}\b";
        bool validado;
        List<Persona> personas;
        List<Resultado> resultado;
        public MainWindow()
        {
            InitializeComponent();
            personas = new List<Persona>();

            //resultado = new List<Persona>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string entrada = "mon 08:30-10:30";
            //string formato = @"\b[a-z]{3}\b\s\d{2}:\d{2}-\d{2}:\d{2}";
            //if (Regex.IsMatch(entrada, formato))
            //    MessageBox.Show("Ok");
            //else
            //    MessageBox.Show("Error");
            cboDia.Items.Add("monday");
            cboDia.Items.Add("tuesday");
            cboDia.Items.Add("wednesday");
            cboDia.Items.Add("thrusday");
            cboDia.Items.Add("friday");
            cboDia.Items.Add("saturday");
            cboDia.Items.Add("sunday");
            cboDia.SelectedIndex = 0;
            resultado = new List<Resultado>();
            //input = new Input();
            //input.hora = 0;
            //input.min = 45;
            //input.files = new List<string>() { "a", "b" };
            
        }

        private void tblckValidacion_KeyUp(object sender, KeyEventArgs e)
        {
            if (Regex.IsMatch(txtHoras.Text, formato))
            {
                tblckValidacion.Foreground = new SolidColorBrush(Colors.Green);
                tblckValidacion.Text = "Ok!";
                validado = true;
            }
            else
            {
                tblckValidacion.Foreground = new SolidColorBrush(Colors.Red);
                tblckValidacion.Text = "Incorrecto!";
                validado = false;
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (validado)
            {
                string contenido = "";
                var existe = personas.Where(p => p.nombre == txtNombre.Text).Select(p => p).FirstOrDefault();
                if (existe == null)
                {
                    Persona p = new Persona();
                    p.nombre = txtNombre.Text;
                    p.dias = new List<Dia>();
                    Dia d = new Dia();
                    d.horas = new List<Hora>();
                    d.nombre = cboDia.SelectionBoxItem.ToString().Substring(0, 3);
                    Hora h = new Hora();
                    string horas = txtHoras.Text;
                    h.horainicio = Convert.ToInt32(horas.Substring(0, 2));
                    h.minutoinicio = Convert.ToInt32(horas.Substring(3, 2));
                    h.horafin = Convert.ToInt32(horas.Substring(6, 2));
                    h.minutofin = Convert.ToInt32(horas.Substring(9, 2));
                    h.cadena = horas;
                    h._horainicio = new DateTime(2015, 3, 22, h.horainicio, h.minutoinicio, 0);
                    h._horafin = new DateTime(2015, 3, 22, h.horafin, h.minutofin, 0);
                    d.horas.Add(h);
                    p.dias.Add(d);
                    personas.Add(p);
                    
                }
                else
                {
                    var existedia = existe.dias.Where(p => p.nombre == cboDia.SelectionBoxItem.ToString().Substring(0, 3)).Select(p => p).FirstOrDefault();
                    if (existedia == null)
                    {
                        Dia d = new Dia();
                        d.horas = new List<Hora>();
                        d.nombre = cboDia.SelectionBoxItem.ToString().Substring(0, 3);
                        Hora h = new Hora();
                        string horas = txtHoras.Text;
                        h.horainicio = Convert.ToInt32(horas.Substring(0, 2));
                        h.minutoinicio = Convert.ToInt32(horas.Substring(3, 2));
                        h.horafin = Convert.ToInt32(horas.Substring(6, 2));
                        h.minutofin = Convert.ToInt32(horas.Substring(9, 2));
                        h.cadena = horas;
                        h._horainicio = new DateTime(2015, 3, 22, h.horainicio, h.minutoinicio, 0);
                        h._horafin = new DateTime(2015, 3, 22, h.horafin, h.minutofin, 0);
                        d.horas.Add(h);
                        existe.dias.Add(d);
                    }
                    else
                    {
                        Hora h = new Hora();
                        string horas = txtHoras.Text;
                        h.horainicio = Convert.ToInt32(horas.Substring(0, 2));
                        h.minutoinicio = Convert.ToInt32(horas.Substring(3, 2));
                        h.horafin = Convert.ToInt32(horas.Substring(6, 2));
                        h.minutofin = Convert.ToInt32(horas.Substring(9, 2));
                        h.cadena = horas;
                        h._horainicio = new DateTime(2015, 3, 22, h.horainicio, h.minutoinicio, 0);
                        h._horafin = new DateTime(2015, 3, 22, h.horafin, h.minutofin, 0);
                        existedia.horas.Add(h);
                    }
                }
                foreach (var p in personas)
                {
                    contenido += "Persona : " + p.nombre + "\n";
                    foreach (var d in p.dias)
                    {
                        contenido += d.nombre + " ";
                        foreach(var h in d.horas)
                        {
                            contenido += h.cadena + " ";
                        }
                        contenido += "\n";
                    }
                    contenido += "\n";
                }
                
                txtHorario.Text = contenido;
                txtHoras.Text = "";
                validado = false;
            }
        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            var bloques = txtConsulta.Text.Split(' ');
            int horas = Convert.ToInt32(bloques[0].Substring(0, 2));
            int minutos = Convert.ToInt32(bloques[0].Substring(3, 2));
            List<Persona> personasseleccionadas = new List<Persona>();
            for (int i = 1; i < bloques.Count(); i++)
            {
                var s = personas.Where(p => p.nombre == bloques[i]).Select(p => p).First();
                if (s != null)
                    personasseleccionadas.Add(s);
            }
            
            //List<Dia> dias = new List<Dia>();
            //for (int i = 1; i < bloques.Count(); i++)
            //{
            //    var s = personas.Where(p => p.nombre == bloques[i]).Select(p => p).First();
            //    foreach (var d in s.dias)
            //    {
            //        var exi = dias.Where(p => p.nombre == d.nombre).Select(p => p).FirstOrDefault();
            //        if (exi != null)
            //            exi.horas.AddRange(d.horas);
            //        else
            //        {
            //            Dia _d = new Dia();
            //            _d.nombre = d.nombre;
            //            _d.horas = d.horas;
            //            dias.Add(d);
            //        }
            //    }
            //}
            //List<Dia> diasord = new List<Dia>();
            //foreach (var d in dias)
            //{
            //    List<Hora> _horas = new List<Hora>();
            //    foreach (var h in d.horas)
            //    {
            //        _horas.Add(new Hora() { horainicio = h.horainicio, minutoinicio = h.minutoinicio });
            //        _horas.Add(new Hora() { horainicio = h.horafin, minutoinicio = h.minutofin });
            //    }
            //    _horas = _horas.OrderByDescending(p => p.horainicio).ToList();
            //    var g = _horas.GroupBy(p => p.horainicio).ToList();
            //    List<Hora> horasord = new List<Hora>();
            //    foreach (var _g in g)
            //    {
            //        var ord = _g.OrderByDescending(p => p.minutoinicio).ToList();
            //        horasord.AddRange(ord);
            //    }
            //    Dia _d = new Dia();
            //    _d.horas = horasord;
            //    _d.nombre = d.nombre;
            //    diasord.Add(_d);
            //}
            resultado.Clear();
            //string resultado="";
            foreach (var persona in personasseleccionadas)
            {
                foreach (var dia in persona.dias)
                {
                    foreach (var hora in dia.horas)
                    {
                        DateTime hi = hora._horainicio;
                        DateTime hf = hora._horainicio.AddHours(horas).AddMinutes(minutos);
                        if (hora._horafin.TimeOfDay >= hf.TimeOfDay)
                        {
                            foreach (var _persona in personasseleccionadas)
                            {
                                if (_persona.nombre != persona.nombre)
                                {
                                    foreach (var _dia in _persona.dias)
                                    {
                                        if (_dia.nombre == dia.nombre)
                                        {
                                            foreach (var _hora in _dia.horas)
                                            {
                                                DateTime hi2 = _hora._horainicio;
                                                DateTime hf2 = _hora._horainicio.AddHours(horas).AddMinutes(minutos);
                                                //hf = hora._horafin
                                                //hf2 = _hora._horafin
                                                if (_hora._horafin.TimeOfDay >= hf2.TimeOfDay && hi2.TimeOfDay < hora._horafin.TimeOfDay && _hora._horafin.TimeOfDay > hi.TimeOfDay)
                                                {
                                                    if (hi2 >= hi && hora._horafin <= _hora._horafin)
                                                    {
                                                        var x = resultado.Where(p => p.nombre == dia.nombre).Select(p => p).ToList();                                                        
                                                        if (x.Count() == 0)
                                                        {
                                                            if (hf2 <= hora._horafin)
                                                            {
                                                                Resultado r = new Resultado();
                                                                r.nombre = dia.nombre;
                                                                r.horas = new List<string>();
                                                                r.horas.Add(hi2.ToShortTimeString() + " - " + hora._horafin.ToShortTimeString());
                                                                resultado.Add(r);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            bool existe = false;
                                                            foreach (var xh in x)
                                                            {
                                                                var y = xh.horas.Where(p => p == hi2.ToShortTimeString() + " - " + hora._horafin.ToShortTimeString()).Select(p => p).FirstOrDefault();
                                                                if (y != null)
                                                                    existe = true;
                                                            }
                                                           
                                                            if (!existe)
                                                            {
                                                                if (hf2 <= hora._horafin)
                                                                {
                                                                    Resultado r = new Resultado();
                                                                    r.nombre = dia.nombre;
                                                                    r.horas = new List<string>();
                                                                    r.horas.Add(hi2.ToShortTimeString() + " - " + hora._horafin.ToShortTimeString());
                                                                    resultado.Add(r);
                                                                }
                                                            }
                                                        }
                                                            //resultado.Add(hi2.ToShortTimeString() + " - " + hora._horafin.ToShortTimeString());
                                                    }
                                                    //resultado += hi2.ToShortTimeString() + " - " + hora._horafin.ToShortTimeString() + "\n";
                                                    else
                                                        if (hi2 >= hi && hora._horafin > _hora._horafin)
                                                        {
                                                            var x = resultado.Where(p => p.nombre == dia.nombre).Select(p => p).ToList();
                                                            if (x.Count() == 0)
                                                            {
                                                                if (hf2 <= _hora._horafin)
                                                                {
                                                                    Resultado r = new Resultado();
                                                                    r.nombre = dia.nombre;
                                                                    r.horas = new List<string>();
                                                                    r.horas.Add(hi2.ToShortTimeString() + " - " + _hora._horafin.ToShortTimeString());
                                                                    resultado.Add(r);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                bool existe = false;
                                                                foreach (var xh in x)
                                                                {
                                                                    var y = xh.horas.Where(p => p == hi2.ToShortTimeString() + " - " + _hora._horafin.ToShortTimeString()).Select(p => p).FirstOrDefault();
                                                                    if (y != null)
                                                                        existe = true;
                                                                }

                                                                if (!existe)
                                                                {
                                                                    if (hf2 <= _hora._horafin)
                                                                    {
                                                                        Resultado r = new Resultado();
                                                                        r.nombre = dia.nombre;
                                                                        r.horas = new List<string>();
                                                                        r.horas.Add(hi2.ToShortTimeString() + " - " + _hora._horafin.ToShortTimeString());
                                                                        resultado.Add(r);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        //resultado += hi2.ToShortTimeString() + " - " + _hora._horafin.ToShortTimeString() + "\n";
                                                        else
                                                            if (hi2 <= hi && hora._horafin <= _hora._horafin)
                                                            {
                                                                var x = resultado.Where(p => p.nombre == dia.nombre).Select(p => p).ToList();
                                                                if (x.Count() == 0)
                                                                {
                                                                    if (hf <= hora._horafin)
                                                                    {
                                                                        Resultado r = new Resultado();
                                                                        r.nombre = dia.nombre;
                                                                        r.horas = new List<string>();
                                                                        r.horas.Add(hi.ToShortTimeString() + " - " + hora._horafin.ToShortTimeString());
                                                                        resultado.Add(r);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    bool existe = false;
                                                                    foreach (var xh in x)
                                                                    {
                                                                        var y = xh.horas.Where(p => p == hi.ToShortTimeString() + " - " + hora._horafin.ToShortTimeString()).Select(p => p).FirstOrDefault();
                                                                        if (y != null)
                                                                            existe = true;
                                                                    }
                                                                    
                                                                    if (!existe)
                                                                    {
                                                                        if (hf <= hora._horafin)
                                                                        {
                                                                            Resultado r = new Resultado();
                                                                            r.nombre = dia.nombre;
                                                                            r.horas = new List<string>();
                                                                            r.horas.Add(hi.ToShortTimeString() + " - " + hora._horafin.ToShortTimeString());
                                                                            resultado.Add(r);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            //resultado += hi.ToShortTimeString() + " - " + hora._horafin.ToShortTimeString() + "\n";
                                                            else
                                                                if (hi2 <= hi && hora._horafin > _hora._horafin)
                                                                {
                                                                    var x = resultado.Where(p => p.nombre == dia.nombre).Select(p => p).ToList();
                                                                    if (x.Count() == 0)
                                                                    {
                                                                        if (hf <= _hora._horafin)
                                                                        {
                                                                            Resultado r = new Resultado();
                                                                            r.nombre = dia.nombre;
                                                                            r.horas = new List<string>();
                                                                            r.horas.Add(hi.ToShortTimeString() + " - " + _hora._horafin.ToShortTimeString());
                                                                            resultado.Add(r);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        bool existe = false;
                                                                          foreach (var xh in x)
                                                                          {
                                                                              var y = xh.horas.Where(p => p == hi.ToShortTimeString() + " - " + _hora._horafin.ToShortTimeString()).Select(p => p).FirstOrDefault();
                                                                              if (y != null)
                                                                                  existe = true;
                                                                          }

                                                                        if (!existe)
                                                                        {
                                                                            if (hf <= _hora._horafin)
                                                                            {
                                                                                Resultado r = new Resultado();
                                                                                r.nombre = dia.nombre;
                                                                                r.horas = new List<string>();
                                                                                r.horas.Add(hi.ToShortTimeString() + " - " + _hora._horafin.ToShortTimeString());
                                                                                resultado.Add(r);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            //resultado += hi.ToShortTimeString() + " - " + _hora._horafin.ToShortTimeString() + "\n";
                                                    ////posiaca
                                                    //if (hi2 <= hi && hf <= hf2)
                                                    //    resultado += hi.ToShortTimeString() + " - " + _hora._horafin.ToShortTimeString() + "\n";
                                                    //else
                                                    //if (hi2 <= hi && hf > hf2)
                                                    //    resultado += hi.ToShortTimeString() + " - " + hora._horafin.ToShortTimeString() + "\n";
                                                }                                               
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }                    
                }
            }
            var grupo = resultado.GroupBy(p => p.nombre).ToList();
            txtResultado.Text = "";
            foreach (var g in grupo)
            {
                txtResultado.Text += g.Select(p => p.nombre).First() + " ";                 
                foreach (var r in g)
                {
                    foreach (var h in r.horas)
                        txtResultado.Text += h + "   "; 
                }
                txtResultado.Text += "\n";
            }
            
            //exe();
            //print();
        }
    }
    public class Resultado
    {
        public string nombre { get; set; }
        public List<string> horas { get; set; }
    }
    public class Persona
    {
        public string nombre { get; set; }
        public List<Dia> dias{ get; set; }

    }
    public class Dia
    {
        public string nombre { get; set; }
        public List<Hora> horas { get; set; }
    }
    public class Hora
    {
        public int horainicio { get; set; }
        public int horafin { get; set; }
        public int minutoinicio { get; set; }
        public int minutofin { get; set; }
        public string cadena { get; set; }

        public DateTime _horainicio { get; set; }
        public DateTime _horafin { get; set; }
    }
}
