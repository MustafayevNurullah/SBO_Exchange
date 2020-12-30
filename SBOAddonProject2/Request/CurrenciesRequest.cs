using SBOAddonProject2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace SBOAddonProject2.Request
{
    public static class CurrenciesRequest
    {

        public static  List<ValCurs> GetValue(string Month,string Year)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<ValCurs> List = new List<ValCurs>();
            if (Month.Count() == 1)
                Month = "0" + Month;
            string Path = "01";
            int counter = 1;
            do
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.cbar.az/currencies/{Path}.{Month}.{Year}.xml");
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    try
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {

                                XmlSerializer serializer = new XmlSerializer(typeof(ValCurs));
                                using (TextReader reader1 = new StringReader(reader.ReadToEnd()))
                                {
                                    List.Add((ValCurs)serializer.Deserialize(reader1));
                                }
                            }
                        }
                        counter++;
                        if (counter <= 9)
                        {
                            Path = "0" + Convert.ToString(counter);
                        }
                        else
                        {
                            Path = Convert.ToString(counter);

                        }
                    }catch
                    {
                        break;
                    }
            } while (true);
            return List;
        }
    }
}
