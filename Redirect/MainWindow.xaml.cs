using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

namespace Redirect
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                                                                                                                    ///
        ///                                               Redirect 301                                                         /// 
        ///                                                                                                                    /// 
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void redirect301()
        {
            //Clear the fields
            urlRaizBox.Clear();
            urlARedirectBox.Clear();
            urlFinalBox.Clear();

            //Urls
            String urlRaiz = urlRaizBox.Text;
            String urlARedirect = urlARedirectBox.Text;
            String urlFinal = urlFinalBox.Text;

            String old = "";

            if (urlARedirect.Contains(urlRaiz))
            {
                old = urlARedirect.Replace(urlRaiz, "");
            }

            //Write result into the text box

            ResultBox.Text = $"Redirect 301 {old} {urlFinal}";

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                                                                                                                    ///
        ///                                               Redirect 302                                                         /// 
        ///                                                                                                                    /// 
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void redirect302()
        {
            //Clear the fields
            urlRaizBox.Clear();
            urlARedirectBox.Clear();
            urlFinalBox.Clear();

            //Urls
            String urlRaiz = urlRaizBox.Text;
            String urlARedirect = urlARedirectBox.Text;
            String urlFinal = urlFinalBox.Text;

            String old = "";

            if (urlARedirect.Contains(urlRaiz))
            {
                old = urlARedirect.Replace(urlRaiz, "");
            }

            //Write result into the text box

            ResultBox.Text = $"Redirect 302 {old} {urlFinal}";

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                                                                                                                    ///
        ///                                               www to no-www                                                        /// 
        ///                                                                                                                    /// 
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void wwwToNo()
        {
            //Url
            String urlRaiz = urlRaizBox.Text;
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

            //Write result into the text box
            foreach (String element in redirect)
            {
                ResultBox.Text = ResultBox.Text + "\n";
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///                                                                                                                    ///
        ///                                               No-www to www                                                        /// 
        ///                                                                                                                    /// 
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void noToWww()
        {
            //Url
            String urlRaiz = urlRaizBox.Text;
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

            //Write result into the text box
            foreach (String element in redirect)
            {
                ResultBox.Text = ResultBox.Text + "\n";
            }
        }
    }
}
