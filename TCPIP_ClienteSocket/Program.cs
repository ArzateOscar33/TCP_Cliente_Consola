using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TCPIP_ClienteSocket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //localhost=>127.0.0.1
            //Configuracion para conectarse con el servidor 
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11200);
            try
            {
                //Crear el socket para enviar datos
                Socket sender = new Socket(ipAddress.AddressFamily,SocketType.Stream,ProtocolType.Tcp   );
                //Socket le indicamos concectarse con els ervidor
                sender.Connect(remoteEP);
                //Mensaje de Confirmacion de conexion 
                Console.WriteLine("Conectado con el servidor ");
                //Pedimos al usuario que ingrese un texto para enviar al servidor
                Console.WriteLine("Ingrese un texto para enviar ");
                string texto = Console.ReadLine();
                //convierte el texto en un arreglo de bytes
                byte[] msg = Encoding.ASCII.GetBytes(texto+"<EOF>");
                //Enviar al servidor el mensaje
                int byteSent=sender.Send(msg);
                Console.ReadKey();

                //cerrar la conexion con el servidor 
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();



            }//try
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
        }//main


    }
}
