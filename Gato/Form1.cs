using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gato
{
    public partial class Form1 : Form
    {
        /*Lo primero sera cr4ear una lista de botones que seran utilizadas mas alrato*/
        List<Button> listaBotones = new List<Button>();
        //Creacion de variable turno
        int Turno = 0;
        bool Ganador = false;
        int Movimiento = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreaBotones();
            ActualizarTurno();
        }

        /*Crearemos un arreglo para poder posicionar los botones atravez de un bucle For
    * lo que ara es que se creara un boton y despues de eso se creara otro hasta el tercer boto,
    * pero despues bajara y continuara de la misma forma hasta acompletar los 9 botones.*/
        void CreaBotones()
        {
            /*Se le colocaran la direccion en la cual apareceran los botones en este caso left equivale a X
             * y top equivale a Y.*/
            int left = 50;
            int top = 50;

            for (int index = 0; index < 9; index++)
            {
                var boton = new Button();
                boton.Width = 80;
                boton.Height = 80;
                boton.Font = new Font(new FontFamily("Verdana"), 10);
                boton.Visible = true;
                boton.Left = left;
                boton.Top = top;
                boton.Click += OnClickBoton;
                left += 150;

                /*Esta parte es para que cuando llega a tres el bucle tenga que bajar.*/
                if (index == 2 || index == 5)
                {
                    top += 150;
                    left = 50;
                }
                listaBotones.Add(boton);
                this.Controls.Add(boton);
            }
        }

        /*Se iniciara el evento cada que se le dicli a un boton 
         El sender se refiere a que cuando le damos click al boton el sender
         por asi decirlo obtiene el dato de que boton se selecciono.*/
        void OnClickBoton(object sender, EventArgs eventArgs)
        {
            /*De esta manera cada que demos click mandara que boton se selecciono y
             * a ese boton se le accignara una accion.*/
            Button boton = (Button)sender;

            //La estructura funcionara si el cuadro esta vacio
            if (string.IsNullOrEmpty(boton.Text) && !Ganador)
            {
                /*Este comado es para que cuando el valor sea 0 se le coloque una X y cuando sea
                 * 1 uno se le coloque un O y que cuando se le de click este cambie de valor
                 * dependiendo de que valor tiene es decir si es 0 cambia a 1 y si es 1 cambia a 0*/
                boton.Text = Turno == 0 ? "X" : "O";
                ChequeaGanador();
                if (Movimiento < 8)
                {
                    Turno = Turno == 0 ? 1 : 0;
                    ActualizarTurno();
                    Movimiento++;
                    lblMovimientos.Text = "Movimientos " + Movimiento;
                }
                /*Esto es para cuando hay un empate se hace casi lo mismo que cuando uno gana
                 * solo que aca tuve que hacer unos cambio a diferencia del curso para que funcionarea correctamente.*/
                else
                {
                    if (MessageBox.Show($"Hubo un empate. Desea jugar otra vez?",
                    "Empate", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Ganador = false;
                        Turno = 0;
                        Movimiento = 0;
                        lblMovimientos.Text = "Movimientos " + Movimiento;

                        /*Esto es para Limpiar el cuadro cuando se vuelve a jugar.*/
                        for (int index = 0; index < 9; index++)
                        {
                            listaBotones[index].Text = "";
                        }
                        ActualizarTurno();
                    }
                }
            }
        }

        /*Este comando funciona usando la regla del comando anterior inicia el turno en 0 y se le suma 1
         * haciendo referencia a jugador 1 al darle click se le suma 1 dando paso al jugador 2 pero
         * al darle nuevamente click el valor del turno pasa a ser 0 por la regla anterior y asi sucesivamente.*/
        void ActualizarTurno()
        {
            lblTurno.Text = "Turno: Jugador " + (Turno + 1);
        }

        /*Para verificar quien gano es de una forma sencilla y es que si hay 3 simbolos iguales ya sea 
         * en la columna X o en la fila Y se dara un ganador, tambien si es de forma diagonal.*/
            /*Para eso nos basaremos en el es decir por ejemplo si 0, 1 y 2 son iguales entonces el jugador de esos
             * simbolos gana y asi para las distintas secciones del arreglo.*/

        //Se aran 8 validaciones en total
        void ChequeaGanador()
        {
            if ((!string.IsNullOrEmpty(listaBotones[0].Text) && listaBotones[0].Text == listaBotones[1].Text 
                    && listaBotones[1].Text == listaBotones[2].Text) ||
                (!string.IsNullOrEmpty(listaBotones[3].Text) && listaBotones[3].Text == listaBotones[4].Text 
                    && listaBotones[4].Text == listaBotones[5].Text) ||
                (!string.IsNullOrEmpty(listaBotones[6].Text) && listaBotones[6].Text == listaBotones[7].Text 
                    && listaBotones[7].Text == listaBotones[8].Text) ||
                (!string.IsNullOrEmpty(listaBotones[0].Text) && listaBotones[0].Text == listaBotones[3].Text 
                    && listaBotones[3].Text == listaBotones[6].Text) ||
                (!string.IsNullOrEmpty(listaBotones[1].Text) && listaBotones[1].Text == listaBotones[4].Text 
                    && listaBotones[4].Text == listaBotones[7].Text) ||
                (!string.IsNullOrEmpty(listaBotones[2].Text) && listaBotones[2].Text == listaBotones[5].Text 
                    && listaBotones[5].Text == listaBotones[8].Text) ||
                (!string.IsNullOrEmpty(listaBotones[0].Text) && listaBotones[0].Text == listaBotones[4].Text 
                    && listaBotones[4].Text == listaBotones[8].Text) ||
                (!string.IsNullOrEmpty(listaBotones[2].Text) && listaBotones[2].Text == listaBotones[4].Text 
                    && listaBotones[4].Text == listaBotones[6].Text))
            {
                Ganador = true;
                if (MessageBox.Show($"Gano  el Jugador {Turno + 1}. Desea jugar otra vez?",
                    "Ganador", MessageBoxButtons.YesNo) == DialogResult.Yes) 
                {
                    ReiniciarJuego();
                }
            }
        }

        private void ReiniciarJuego()
        {
            Ganador = false;
            Turno = 1;
            Movimiento = -1;
            lblMovimientos.Text = "Movimientos " + Movimiento;

            /*Esto es para Limpiar el cuadro cuando se vuelve a jugar.*/
            for (int index = 0; index < 9; index++)
            {
                listaBotones[index].Text = "";
            }
            ActualizarTurno();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ganador = false;
            Turno = 0;
            Movimiento = 0;
            lblMovimientos.Text = "Movimientos " + Movimiento;

            /*Esto es para Limpiar el cuadro cuando se vuelve a jugar.*/
            for (int index = 0; index < 9; index++)
            {
                listaBotones[index].Text = "";
            }
            ActualizarTurno();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }   
}
