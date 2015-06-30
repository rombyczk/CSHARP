using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Csharp_app
{
    public partial class Communicator : Form
    {
       

        public Communicator()
        {
            InitializeComponent();
            MyInits();
        }

        void MyInits()
        {
             port = 20000;
             servListener = new ServListener();

             serverThread = new Thread(servListener.serverChecker);
             serverThread.Start();

         }

        private void button1_Click(object sender, EventArgs e)
        {
            string host = textBox1.Text;

            try
            {
                UdpClient client = new UdpClient(host, port);
                Byte[] dane = Encoding.ASCII.GetBytes(textBox2.Text);
                client.Send(dane, dane.Length);
                listBox1.Items.Add(textBox2.Text);
                client.Close();
            }
            catch (Exception ex)
            {
                listBox1.Items.Add("SEND ERROR CLIENT");
                MessageBox.Show(ex.ToString(), "CLIENT ERROR");
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      


    };

    class ServListener
    {
        int port;
        UdpClient server;
        IPEndPoint remoteID;
        
        public ServListener()
        {
            remoteID = new IPEndPoint(IPAddress.Any, 0);
            port = 20000;
        }   

        public void serverChecker()
        {
            while (true)
            {
                try
                {
                    server = new UdpClient(port);
                    Byte[] received = server.Receive(ref remoteID);
                    MessageBox.Show(Encoding.ASCII.GetString(received), "NEW MESSAGE");
                    server.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "SERVER ERROR");
                }
            }
        }
    };
}




/**/