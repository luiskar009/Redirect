using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redirect
{
    class Program
    {
        public static void redirect301()
        {
            //Lee los datos de pantalla, hay que cambiarlo por formulario
            Console.WriteLine("Escriba la Url Raiz:");
            String urlRaiz = Console.ReadLine();
            Console.WriteLine("Escriba la Url a Redireccionar");
            String urlARedirect = Console.ReadLine();
            Console.WriteLine("Escriba la Url Final");
            String urlFinal = Console.ReadLine();

            String old = "";

            if (urlARedirect.Contains(urlRaiz))
            {
                old = urlARedirect.Replace(urlRaiz, "");
            }

            //Imprime el resultado por pantalla, hay que cambiarlo a la web

            String redirect = $"Redirect 301 {old} {urlFinal}";

            Console.WriteLine(redirect);
        }

        public static void redirect302()
        {
            //Lee los datos de pantalla, hay que cambiarlo por formulario
            Console.WriteLine("Escriba la Url Raiz:");
            String urlRaiz = Console.ReadLine();
            Console.WriteLine("Escriba la Url a Redireccionar");
            String urlARedirect = Console.ReadLine();
            Console.WriteLine("Escriba la Url Final");
            String urlFinal = Console.ReadLine();

            String old = "";

            if (urlARedirect.Contains(urlRaiz))
            {
                old = urlARedirect.Replace(urlRaiz, "");
            }

            //Imprime el resultado por pantalla, hay que cambiarlo a la web

            String redirect = $"Redirect 302 {old} {urlFinal}";

            Console.WriteLine(redirect);
        }

        public static void wwwToNo()
        {
            //Lee los datos de pantalla, hay que cambiarlo por formulario
            Console.WriteLine("Escriba la Url Raiz:");
            String urlRaiz = Console.ReadLine();
            String[] redirect = new String[4];

            redirect[0] = "<IfModule mod_rewrite.c>";

            String aux = urlRaiz;
            if (aux.Contains("http://"))
            {
                aux = urlRaiz.Replace("http://", "");
                if (aux.Contains("www."))
                    aux = aux.Replace("www.", "");
            } 
            else if (aux.Contains("https://"))
            {
                aux = urlRaiz.Replace("https://", "");
                if (aux.Contains("www."))
                    aux = aux.Replace("www.", "");
            }
            redirect[1] = "RewriteCond %{HTTP_HOST} ^www." + aux + " [NC]";

            redirect[2] = "RewriteRule (.*) http://" + aux + "/$1 [R=301,L,QSA]";

            redirect[3] = "</IfModule>";

            //Imprime el resultado por pantalla, hay que cambiarlo a la web
            foreach (String element in redirect)
            {
                Console.WriteLine(element);
            }
        }

        public static void noToWww()
        {
            //Lee los datos de pantalla, hay que cambiarlo por formulario
            Console.WriteLine("Escriba la Url Raiz:");
            String urlRaiz = Console.ReadLine();
            String[] redirect = new String[4];

            redirect[0] = "<IfModule mod_rewrite.c>";

            String aux = urlRaiz;
            if (aux.Contains("http://"))
            {
                aux = urlRaiz.Replace("http://", "");
                if (aux.Contains("www."))
                    aux = aux.Replace("www.", "");
            }
            else if (aux.Contains("https://"))
            {
                aux = urlRaiz.Replace("https://", "");
                if (aux.Contains("www."))
                    aux = aux.Replace("www.", "");
            }
            redirect[1] = "RewriteCond %{HTTP_HOST} ^" + aux + " [NC]";

            redirect[2] = "RewriteRule (.*) http://www." + aux + "/$1 [R=301,L,QSA]";

            redirect[3] = "</IfModule>";

            //Imprime el resultado por pantalla, hay que cambiarlo a la web
            foreach (String element in redirect)
            {
                Console.WriteLine(element);
            }
        }

        static void Main(string[] args)
        {
            //Esta eleccion se leera de un desplegable con todas las opciones
            Console.WriteLine("Escriba el tipo de Redireccionamiento");
            String tipoRedirect = Console.ReadLine();

            if (tipoRedirect == "301")
                redirect301();

            else if (tipoRedirect == "302")
                redirect302();


            else if (tipoRedirect == "wwwtono")
                wwwToNo();

            else if (tipoRedirect == "notowww")
                noToWww();

        }
    }
}
