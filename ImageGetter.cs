using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Drawing.Imaging;  
using System.Drawing; 
using Microsoft.Office.Interop.PowerPoint;
using Microsoft;

namespace PowerPointCreator { 
      
    class ImageGetter { 
        
        /// <summary>
        /// Sends a GET request for the intended URI
        /// </summary>
        /// <returns>A string containing the contents of the reply - may possibly be a different type but we only send a GET to Google Images</returns>
        private static string HttpGet(string URI){
            WebClient client = new WebClient();
            // Add a user agent header in case the 
            // requested URI contains a query.
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            Stream data = client.OpenRead(URI);
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();
            data.Close();
            reader.Close();

            return s;
        }
 
        /// <summary>
        /// This method sends a request to Google Images for the desired string
        /// </summary>
        /// <param name="search"> Search string to query Google for </param>
        /// <returns> Returns the unformatted HTML of a google images search</returns>
        private static string getSearchPage(string search ){
            string text = "https://www.google.com/search?q="+search+"&tbm=isch"; 
            return HttpGet( text );
        }

        /// <summary>
        /// Download an image from the selected URL
        /// </summary>
        /// <param name="fromUrl"> URL of the image to download</param>
        /// <returns> Image at URL, or null if URL is not an image </returns>
        private static Image DownloadImage(string fromUrl){
            try{
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    using (Stream stream = webClient.OpenRead(fromUrl))
                    {
                        return Image.FromStream(stream);
                    }
                }
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
                return null;
            }
        }


        public static void getRelatedImages(string search, int numImages){
            string text = getSearchPage(search);
            int loc = 0;
            int locEndOfURL=0;
            string link;
            for(int i=0; i<numImages+1; i++){
                loc = text.IndexOf("src=\"",loc)+5; //this index search looks for html elements containing image links - we add 5 here to skip past the src="
                if(i==0){continue;}                 //there is a banner image this search picks up
                locEndOfURL = text.IndexOf("\"",loc);
                link = text.Substring(loc, locEndOfURL - loc );
                Console.WriteLine(link);
                Image pic = DownloadImage(link);
                pic.Save("./pic.jpg",System.Drawing.Imaging.ImageFormat.Jpeg);
            }

        }

      
    } 
} 