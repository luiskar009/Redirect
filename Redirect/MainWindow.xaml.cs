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
        
        public void newDomain()
        {
            String urlRaiz = urlRaizBox.Text;
            if (!(urlRaiz.Contains("http://")))
            {
                if (!(urlRaiz.Contains("https://")))
                    urlRaiz = "http://" + urlRaiz;
            }
            ResultBox.Text= "RedirectMatch 301 (.*) " + urlRaiz;
        }

        public void ip()
        {
            String urlRaiz = urlRaizBox.Text;
            String ip = txtIp.Text;
            ip = ip.Replace(".", "\\.");

            if (!(urlRaiz.Contains("http://")))
            {
                if (!(urlRaiz.Contains("https://")))
                    urlRaiz = "http://" + urlRaiz;
            }

            ResultBox.Text = "<IfModule mod_rewrite.c>";
            ResultBox.Text = ResultBox.Text + "\n" + "RewriteCond %{HTTP_HOST} ^" + ip;
            ResultBox.Text = ResultBox.Text + "\n" + "RewriteRule (.*) " + urlRaiz + "/$1 [R=301,L,QSA]";
            ResultBox.Text = ResultBox.Text + "\n" + "</IfModule>";

        }

        public void httpsToNo()
        {

            String urlRaiz = urlRaizBox.Text;

            ResultBox.Text = "<IfModule mod_rewrite.c>";
            ResultBox.Text = ResultBox.Text + "\n" + "RewriteCond %{HTTPS} on";
            if (!(urlRaiz.Contains("http://")))
            {
                if (!(urlRaiz.Contains("https://")))
                    urlRaiz = "http://" + urlRaiz;
            }
            ResultBox.Text = ResultBox.Text + "\n" + "RewriteRule (.*) " + urlRaiz + "/$1 [R=301,L,QSA]";
            ResultBox.Text = ResultBox.Text + "\n" + "</IfModule>";

        }

        public void noToHttps()
        {

            String urlRaiz = urlRaizBox.Text;

            ResultBox.Text = "<IfModule mod_rewrite.c>";
            ResultBox.Text = ResultBox.Text + "\n" + "RewriteCond %{HTTPS} off";
            if (!(urlRaiz.Contains("http://")))
            {
                if (!(urlRaiz.Contains("https://")))
                    urlRaiz = "https://" + urlRaiz;
            }
            ResultBox.Text = ResultBox.Text + "\n" + "RewriteRule (.*) " + urlRaiz + "/$1 [R=301,L,QSA]";
            ResultBox.Text = ResultBox.Text + "\n" + "</IfModule>";

        }

        public void cache()
        {
            ResultBox.Text = "<IfModule mod_expires.c>";
            ResultBox.Text = ResultBox.Text + "\n" +"  ExpiresActive on";
            ResultBox.Text = ResultBox.Text + "\n" + "\n" + "  # Por defecto 1 mes de caché";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresDefault                                          \"access plus 1 month\"";
            ResultBox.Text = ResultBox.Text + "\n" + "\n" + "  # los manifiestos appcache necesitan solicitarse cada vez, por firefox 3.6 (probablemente no necesario actualmente?";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType text/cache-manifest         \"access plus 0 seconds\"";
            ResultBox.Text = ResultBox.Text + "\n" + "\n" + "  # El HTML nunca debe de ser cacheado";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType text/html                           \"access plus 0 seconds\"";
            ResultBox.Text = ResultBox.Text + "\n" + "\n" + "  # Los datos dinámicos tampoco (tal vez podría variar dependiendo de tu aplicación)";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType text/xml                            \"access plus 0 seconds\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType application / xml               \"access plus 0 seconds\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType application/json                \"access plus 0 seconds\"";
            ResultBox.Text = ResultBox.Text + "\n" + "\n" + "  # Una hora para los feeds (cambiar dependiendo de la fecha de actualización de tu web)";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType application/rss+xml        \"access plus 1 hour\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType application/atom+xml     \"access plus 1 hour\"";
            ResultBox.Text = ResultBox.Text + "\n" + "\n" + "  # Favicon (Sólo una semana porque el nombre no cambia, luego podría haber cambios y mantenerse el cacheado)";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType image/x-icon                \"access plus 1 week\"";
            ResultBox.Text = ResultBox.Text + "\n" + "\n" + "  # Imágenes, vídeo, audio: 1 mes";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType image / gif                    \"access plus 1 month\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType image/png                    \"access plus 1 month\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType image/jpeg                   \"access plus 1 month\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType video/ogg                    \"access plus 1 month\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType audio/ogg                 \"access plus 1 month\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType video/mp4                 \"access plus 1 month\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType video/webm                \"access plus 1 month\"";
            ResultBox.Text = ResultBox.Text + "\n" + "\n" + "  # Fuentes web: 1 mes";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType application/x-font-ttf    \"access plus 1 month\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType font/opentype             \"access plus 1 month\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType application/x-font-woff   \"access plus 1 month\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType image/svg+xml             \"access plus 1 month\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType application / vnd.ms - fontobject \"access plus 1 month\"";
            ResultBox.Text = ResultBox.Text + "\n" + "\n" + "  # CSS y JavaScript: 1 año. Ten en cuenta que si cambias los ficheros deberías usar una query string o un nombre de archivo diferente para evitar que los visitantes reciban archivos cacheados.";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType text/css                  \"access plus 1 year\"";
            ResultBox.Text = ResultBox.Text + "\n" + "  ExpiresByType application/javascript    \"access plus 1 year\"";
            ResultBox.Text = ResultBox.Text + "\n" + "\n" + "</IfModule>";
            ResultBox.Text = ResultBox.Text + "\n" + "\n" + "# Eliminar E-Tag";
            ResultBox.Text = ResultBox.Text + "\n" + "# Estamos enviando periodos de caché muy amplios, así que no es necesario que el navegador compruebe mediante E-Tag si el fichero cambió";
            ResultBox.Text = ResultBox.Text + "\n" + "<IfModule mod_headers.c>";
            ResultBox.Text = ResultBox.Text + "\n" + "  Header unset ETag";
            ResultBox.Text = ResultBox.Text + "\n" + "</IfModule>";
            ResultBox.Text = ResultBox.Text + "\n" + "FileETag None";
        }

        public void gzip()
        {
            ResultBox.Text = "<IfModule mod_deflate.c>";
            ResultBox.Text = ResultBox.Text + "\n" + "    # Force compression for mangled headers.";
            ResultBox.Text = ResultBox.Text + "\n" + "    # http://developer.yahoo.com/blogs/ydn/posts/2010/12/pushing-beyond-gzipping";
            ResultBox.Text = ResultBox.Text + "\n" + "    <IfModule mod_setenvif.c>";
            ResultBox.Text = ResultBox.Text + "\n" + "        <IfModule mod_headers.c>";
            ResultBox.Text = ResultBox.Text + "\n" + "            SetEnvIfNoCase ^(Accept-EncodXng|X-cept-Encoding|X{15}|~{15}|-{15})$";
            ResultBox.Text = ResultBox.Text + "\n" + "^((gzip|deflate)\\s*,?\\s*)+|[X~-]{4,13}$ HAVE_Accept-Encoding";
            ResultBox.Text = ResultBox.Text + "\n" + "            RequestHeader append Accept-Encoding \"gzip,deflate\" env=HAVE_Accept-Encoding";
            ResultBox.Text = ResultBox.Text + "\n" + "        </IfModule>";
            ResultBox.Text = ResultBox.Text + "\n" + "    </IfModule>";
            ResultBox.Text = ResultBox.Text + "\n" + "\n" + "    <IfModule mod_filter.c>";
            ResultBox.Text = ResultBox.Text + "\n" + "        AddOutputFilterByType DEFLATE application/atom+xml \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      application/javascript \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      application/json \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      application/rss+xml \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      application/vnd.ms-fontobject \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      application/x-font-ttf \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      application/x-web-app-manifest+json \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      application/xhtml+xml \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      application/xml \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      font/opentype \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      image/svg+xml \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      image/x-icon \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      text/css \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      text/html \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      text/plain \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      text/x-component \\";
            ResultBox.Text = ResultBox.Text + "\n" + "                                      text/xml";
            ResultBox.Text = ResultBox.Text + "\n" + "    </IfModule>";
            ResultBox.Text = ResultBox.Text + "\n" + "\n" + "</IfModule>";


        }

        public void uft8()
        {
            ResultBox.Text = "AddDefaultCharset utf-8";
            ResultBox.Text = ResultBox.Text + "\n" + "AddCharset utf-8 .atom .css .js .json .rss .vtt .xml";
        }
        
        
        
        
        
    }
}
