using System;
using System.IO;
using System.Net;
using System.Timers;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;
using Timer = System.Timers.Timer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TelNetApp
{
    public partial class Form1 : Form
    {
        #region VARIAVEIS
        private IPAddress ip;
        private List<string> ArquivoTXT;

        static string linha1 = "";
        static string linha2 = "";
        static string linha3 = "";
        static string linha4 = "";
        static string linha5 = "";

        static string conteudo1 = "";
        static string conteudo2 = "";
        static string conteudo3 = "";
        static string conteudo4 = "";
        static string conteudo5 = "";

        static string hora1 = "";
        static string hora2 = "";
        static string hora3 = "";
        static string hora4 = "";
        static string hora5 = "";

        static int H1;
        static int H2;
        static int H3;
        static int H4;
        static int H5;

        static int M1;
        static int M2;
        static int M3;
        static int M4;
        static int M5;
        #endregion

        public Form1()
        {
            #region FORM PRINCIPAL
            InitializeComponent();

            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            ArquivoTXT = new List<string>();
            LerTextosDoArquivo();
            ConfigurarTextosDasLinhas();
            editarTexto();
            igualarTextoBoxs();
            horario();
            horario1();
            horario2();
            horario3();
            horario4();
            horario5();

            ip = IPAddress.Parse("192.168.5.70"); // Endereço IP estático definido
            #endregion
        }

        private void LerTextosDoArquivo()
        {
            string nomeArquivo = "horarios.txt"; // Coloque o caminho do arquivo desejado aqui

            if (File.Exists(nomeArquivo))
            {
                ArquivoTXT = new List<string>(File.ReadAllLines(nomeArquivo));
            }
            else
            {
                MessageBox.Show("O arquivo não existe.");
            }
        }

        private void ConfigurarTextosDasLinhas()
        {
            try
            {
                if (ArquivoTXT.Count >= 5)
                {
                    linha1 = ArquivoTXT[0];
                    linha2 = ArquivoTXT[1];
                    linha3 = ArquivoTXT[2];
                    linha4 = ArquivoTXT[3];
                    linha5 = ArquivoTXT[4];
                }
            }
            catch
            {
                MessageBox.Show("O arquivo .TXT não possui 5 linhas");
            }

        }

        private void editarTexto()
        {
            try
            {
                string[] line1 = linha1.Split(';');
                string[] line2 = linha2.Split(';');
                string[] line3 = linha3.Split(';');
                string[] line4 = linha4.Split(';');
                string[] line5 = linha5.Split(';');

                conteudo1 = line1[0];
                hora1 = line1[1];

                conteudo2 = line2[0];
                hora2 = line2[1];

                conteudo3 = line3[0];
                hora3 = line3[1];

                conteudo4 = line4[0];
                hora4 = line4[1];

                conteudo5 = line5[0];
                hora5 = line5[1];



            }
            catch
            {
                MessageBox.Show($"Ocorreu um erro ao dividir o conteudo do horario", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void igualarTextoBoxs()
        {
            txtBx1.Text = conteudo1;
            txtBx2.Text = conteudo2;
            txtBx3.Text = conteudo3;
            txtBx4.Text = conteudo4;
            txtBx5.Text = conteudo5;

            TxtBxHR1.Text = hora1;
            TxtBxHR2.Text = hora2;
            TxtBxHR3.Text = hora3;
            TxtBxHR4.Text = hora4;
            TxtBxHR5.Text = hora5;
        }

        private void horario()
        {
            try
            {
                string[] hr1 = hora1.Split(':');
                string[] hr2 = hora2.Split(':');
                string[] hr3 = hora3.Split(':');
                string[] hr4 = hora4.Split(':');
                string[] hr5 = hora5.Split(':');

                H1 = int.Parse(hr1[0]);
                M1 = int.Parse(hr1[1]);

                H2 = int.Parse(hr2[0]);
                M2 = int.Parse(hr2[1]);

                H3 = int.Parse(hr3[0]);
                M3 = int.Parse(hr3[1]);

                H4 = int.Parse(hr4[0]);
                M4 = int.Parse(hr4[1]);

                H5 = int.Parse(hr5[0]);
                M5 = int.Parse(hr5[1]);

            }
            catch
            {
                MessageBox.Show($"Ocorreu um erro ao tratar a hora e os minutos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Atualizar(object sender, EventArgs e)
        {
            string LinhasTXT1 = txtBx1.Text + ";" + TxtBxHR1.Text;
            string LinhasTXT2 = txtBx2.Text + ";" + TxtBxHR2.Text;
            string LinhasTXT3 = txtBx3.Text + ";" + TxtBxHR3.Text;
            string LinhasTXT4 = txtBx4.Text + ";" + TxtBxHR4.Text;
            string LinhasTXT5 = txtBx5.Text + ";" + TxtBxHR5.Text;

            string[] linhasTXT = new string[5];
            linhasTXT[0] = LinhasTXT1;
            linhasTXT[1] = LinhasTXT2;
            linhasTXT[2] = LinhasTXT3;
            linhasTXT[3] = LinhasTXT4;
            linhasTXT[4] = LinhasTXT5;

            string caminhoArquivo = "horarios.txt";

            File.WriteAllLines(caminhoArquivo, linhasTXT);

            LerTextosDoArquivo();
            ConfigurarTextosDasLinhas();
            editarTexto();
            igualarTextoBoxs();
            horario();
            horario1();
            horario2();
            horario3();
            horario4();
            horario5();

        }

        #region LINHA1
        private void horario1()
        {
            try
            {
                // Defina o horário específico em que deseja executar a função
                DateTime horarioEspecifico1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, H1, M1, 0); // Exemplo: 10:00:00

                // Verifica se o horário específico já passou
                if (horarioEspecifico1 < DateTime.Now)
                {
                    // Adiciona um dia completo ao horário específico
                    horarioEspecifico1 = horarioEspecifico1.AddDays(1);
                }

                // Calcule o intervalo entre o horário atual e o horário específico
                TimeSpan intervalo1 = horarioEspecifico1 - DateTime.Now;

                // Crie um temporizador com o intervalo calculado
                Timer temporizador1 = new Timer(intervalo1.TotalMilliseconds);
                temporizador1.Elapsed += (sender, e) =>
                {
                    // Função que será executada no horário específico
                    Enviar1();

                    // Após a execução da função, você pode parar o temporizador se desejar
                    temporizador1.Stop();
                };
                temporizador1.Start();

            }
            catch
            {
                MessageBox.Show($"Ocorreu um erro no 1°Horario", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Enviar1()
        {
            // Código da função que será executada no horário específico
            EnviarTextoTelnet(conteudo1);
        }
        #endregion

        #region LINHA2
        private void horario2()
        {
            try
            {
                // Defina o horário específico em que deseja executar a função
                DateTime horarioEspecifico2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, H2, M2, 0); // Exemplo: 20:00:00

                // Verifica se o horário específico já passou
                if (horarioEspecifico2 < DateTime.Now)
                {
                    // Adiciona um dia completo ao horário específico
                    horarioEspecifico2 = horarioEspecifico2.AddDays(1);
                }

                // Calcule o intervalo entre o horário atual e o horário específico
                TimeSpan intervalo2 = horarioEspecifico2 - DateTime.Now;

                // Crie um temporizador com o intervalo calculado
                Timer temporizador2 = new Timer(intervalo2.TotalMilliseconds);
                temporizador2.Elapsed += (sender, e) =>
                {
                    // Função que será executada no horário específico
                    Enviar2();

                    // Após a execução da função, você pode parar o temporizador se desejar
                    temporizador2.Stop();
                };
                temporizador2.Start();

            }
            catch
            {
                MessageBox.Show($"Ocorreu um erro no 2°Horario", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Enviar2()
        {
            // Código da função que será executada no horário específico
            EnviarTextoTelnet(conteudo2);
        }
        #endregion

        #region LINHA3
        private void horario3()
        {
            try
            {
                // Defina o horário específico em que deseja executar a função
                DateTime horarioEspecifico3 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, H3, M3, 0); // Exemplo: 30:00:00

                // Verifica se o horário específico já passou
                if (horarioEspecifico3 < DateTime.Now)
                {
                    // Adiciona um dia completo ao horário específico
                    horarioEspecifico3 = horarioEspecifico3.AddDays(1);
                }

                // Calcule o intervalo entre o horário atual e o horário específico
                TimeSpan intervalo3 = horarioEspecifico3 - DateTime.Now;

                // Crie um temporizador com o intervalo calculado
                Timer temporizador3 = new Timer(intervalo3.TotalMilliseconds);
                temporizador3.Elapsed += (sender, e) =>
                {
                    // Função que será executada no horário específico
                    Enviar3();

                    // Após a execução da função, você pode parar o temporizador se desejar
                    temporizador3.Stop();
                };
                temporizador3.Start();
            }
            catch
            {
                MessageBox.Show($"Ocorreu um erro no 3°Horario", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Enviar3()
        {
            // Código da função que será executada no horário específico
            EnviarTextoTelnet(conteudo3);
        }
        #endregion

        #region LINHA4
        private void horario4()
        {
            try
            {
                // Defina o horário específico em que deseja executar a função
                DateTime horarioEspecifico4 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, H4, M4, 0); // Exemplo: 40:00:00

                // Verifica se o horário específico já passou
                if (horarioEspecifico4 < DateTime.Now)
                {
                    // Adiciona um dia completo ao horário específico
                    horarioEspecifico4 = horarioEspecifico4.AddDays(1);
                }

                // Calcule o intervalo entre o horário atual e o horário específico
                TimeSpan intervalo4 = horarioEspecifico4 - DateTime.Now;

                // Crie um temporizador com o intervalo calculado
                Timer temporizador4 = new Timer(intervalo4.TotalMilliseconds);
                temporizador4.Elapsed += (sender, e) =>
                {
                    // Função que será executada no horário específico
                    Enviar4();

                    // Após a execução da função, você pode parar o temporizador se desejar
                    temporizador4.Stop();
                };
                temporizador4.Start();
            }
            catch
            {
                MessageBox.Show($"Ocorreu um erro no 4°Horario", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Enviar4()
        {
            // Código da função que será executada no horário específico
            EnviarTextoTelnet(conteudo4);
        }
        #endregion

        #region LINHA5
        private void horario5()
        {
            try
            {
                // Defina o horário específico em que deseja executar a função
                DateTime horarioEspecifico5 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, H5, M5, 0); // Exemplo: 50:00:00

                // Verifica se o horário específico já passou
                if (horarioEspecifico5 < DateTime.Now)
                {
                    // Adiciona um dia completo ao horário específico
                    horarioEspecifico5 = horarioEspecifico5.AddDays(1);
                }

                // Calcule o intervalo entre o horário atual e o horário específico
                TimeSpan intervalo5 = horarioEspecifico5 - DateTime.Now;

                // Crie um temporizador com o intervalo calculado
                Timer temporizador5 = new Timer(intervalo5.TotalMilliseconds);
                temporizador5.Elapsed += (sender, e) =>
                {
                    // Função que será executada no horário específico
                    Enviar5();

                    // Após a execução da função, você pode parar o temporizador se desejar
                    temporizador5.Stop();
                };
                temporizador5.Start();
            }
            catch
            {
                MessageBox.Show($"Ocorreu um erro no 5°Horario", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Enviar5()
        {
            // Código da função que será executada no horário específico
            EnviarTextoTelnet(conteudo5);
        }
        #endregion

        #region BOTÃO1
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                string texto = txtBx1.Text;


                EnviarTextoTelnet(texto);
            }
            catch
            {
                MessageBox.Show($"Ocorreu um erro no 1°Botao", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region BOTÃO2
        private void buttonEnviar2_Click(object sender, EventArgs e)
        {
            try
            {
                string texto = txtBx2.Text;

                EnviarTextoTelnet(texto);
            }
            catch
            {
                MessageBox.Show($"Ocorreu um erro no 2°Botao", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region BOTÃO3
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string texto = txtBx3.Text;

                EnviarTextoTelnet(texto);
            }
            catch
            {
                MessageBox.Show($"Ocorreu um erro no 3°Botao", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region BOTÃO4
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string texto = txtBx4.Text;

                EnviarTextoTelnet(texto);
            }
            catch
            {
                MessageBox.Show($"Ocorreu um erro no 4°Botao", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region BOTÃO5
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string texto = txtBx5.Text;

                EnviarTextoTelnet(texto);
            }
            catch
            {
                MessageBox.Show($"Ocorreu um erro no 5°Botao", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region ENVIAR TelNet
        private void EnviarTextoTelnet(string texto)
        {
            int porta = 22201; // Endereço da porta;
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(ip, porta);
                    using (StreamWriter writer = new StreamWriter(client.GetStream()))
                    using (StreamReader reader = new StreamReader(client.GetStream()))
                    {
                        this.BackColor = Color.Green;
                        timer1.Enabled = true;
                        // Enviar o texto para o servidor
                        writer.WriteLine("RT=" + texto);
                        //MessageBox.Show("Texto Enviado!! \n conteudo do texto: RT=" + texto);
                        writer.Flush();

                        // Ler a resposta do servidor (opcional)
                        //string resposta = reader.ReadToEnd();
                        //MessageBox.Show(resposta);
                    }
                }
            }
            catch (SocketException)
            {
                this.BackColor = Color.Red;
                timer1.Enabled = true;
                MessageBox.Show("Não foi possível conectar ao servidor Telnet. \n RT=" + texto);
            }
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.BackColor = Color.Black;
            timer1.Enabled = false;

        }
    }
}