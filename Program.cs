using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace Proyecto_Final_POO
{
    class Comentario
    {
        public string id { get; set; }
        public string persona { get; set; }
        public DateTime fecha { get; set; }
        public string comentario { get; set; }
        public string direccionIP { get; set; }
        public bool inapropiado { get; set; }
        public int likes { get; set; }
        public override string ToString()

        {
            return string.Format($"{persona} - Comentario = {comentario} - Inapropiado = {inapropiado} - IP = {direccionIP} - Fecha = {fecha} - Likes = {likes}");
        }


    }


    class ComentarioDB
    {
        public static void SaveToFile(List<Comentario> comentarios, string path)
        {

            StreamWriter Text = null;

            try
            {
                Text = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write));
                foreach (var comentario in comentarios)
                {
                    Text.Write(comentario.persona);
                    Text.Write(comentario.comentario);
                    Text.Write(comentario.direccionIP);
                    Text.Write(comentario.inapropiado);
                    Text.Write(comentario.fecha);
                    Text.Write(comentario.likes);
                    Text.WriteLine(comentario.id);
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally

            
            {
                if (Text != null)
                    Text.Close();
            }
        }
        public static void ObtenLikes(string path)
        {
            List<Comentario> comentarios;
            comentarios = ReadFromFile(path);
            var Filtrar = from c in comentarios
                          orderby c.likes descending
                          select c;
            foreach (var c in Filtrar)
                if (c.inapropiado == false)
                    Console.WriteLine(c);
        }
        public static void ObtenFecha(string path)
        {
            List<Comentario> comentarios;
            comentarios = ReadFromFile(path);
            var Filtrar = from c in comentarios
                          orderby c.fecha descending
                          select c;
            foreach (var c in Filtrar)
                if (c.inapropiado == false)
                    Console.WriteLine(c);
        }

        public static List<Comentario> ReadFromFile(string path)
        {

            List<Comentario> comentarios = new List<Comentario>();
            StreamReader textIn = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read));
            try
            {
                while (textIn.Peek() != -1)
                
                
                {
                    string line = textIn.ReadLine();
                    string[] strArray = new string[7];
                    Comentario c = new Comentario();
                    c.persona = strArray[0];
                    c.comentario = strArray[1];
                    c.direccionIP = strArray[2];
                    c.inapropiado = bool.Parse(strArray[3]);
                    c.fecha = DateTime.Parse(strArray[4]);
                    c.likes = int.Parse(strArray[5]);
                    c.id = strArray[6];
                    comentarios.Add(c);
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                textIn.Close();
            }
            return comentarios;
        }
       

    }
    
    class Program
    {
        public static void DelCom(List<Comentario> comentarios)
        {
            Console.Clear();
            Console.WriteLine("Escriba el Id de la persona del comentario que desea eliminar");
            string Remove = Console.ReadLine();

            comentarios.RemoveAll(a => a.id.Contains(Remove));
            foreach(var c in comentarios.Where(c => c.inapropiado == false))
            {
                Console.WriteLine(c);
            }
            Console.ReadKey();
        }
        static void Main(string[] args)
        {

            
          //  List<Comentario> comentarios = new List<Comentario>();

           // comentarios.Add(new Comentario())
          //  {
          //      persona = "Cristiano Ronaldo",
            //    comentario = "SIIIIIIIIIIUUUU",
           //     direccionIP = "205.22.44.1",
            //    inapropiado = false,
           //     fecha = new DateTime(2021, 18, 2),
             //   likes = 30,
             //   id = "CR7"

          //  };

            //Comentarios.SaveToFile(comentarios, (@"C:\Users\Roberto\Desktop\Comentario\Comentarios.txt");
            
            
            List<Comentario> comentarios = ComentarioDB.ReadFromFile(@"C:\Users\Roberto\Desktop\Comentario\Comentarios.txt");
            foreach(var c in from c in comentarios
                             where c.inapropiado == false
                             select c)
            {
                Console.WriteLine(c);
            }
            Console.WriteLine();
            Console.WriteLine(" Comentarios Ordenados por Likes ");
            ComentarioDB.ObtenLikes(@"C:\Users\Roberto\Desktop\Comentario\Comentarios.txt");

            Console.WriteLine();
            Console.WriteLine(" Comentarios Ordenados por Fecha ");
            ComentarioDB.ObtenFecha(@"C:\Users\Roberto\Desktop\Comentario\Comentarios.txt");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine(" ¿Quiere quitar un comentario? ( Y / N ) ");
            string respuesta = Console.ReadLine();
            if (respuesta == "Y" | respuesta == "y")
            {
                DelCom(comentarios);
            }
            else if (respuesta == "N" | respuesta == "n")
            {
                Console.Clear();
                Console.WriteLine(" Gracias por intentarlo, buen dia :) ");
            }
            else if (respuesta == "" | respuesta != "")
            {
                Console.Clear();
                Console.WriteLine("Ingrese un digito valido mostrado en la pantalla");
            }
            Console.ReadKey();
        }
    }
}
