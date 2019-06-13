using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Xam.LaGalerna
{
    public static class Utils
    {

        public static T LoadDataFromApp<T>(String filename = "Xam.LaGalerna.Assets.data.Sample.json")
        {
            String data = LoadFileDataString(filename);
            try
            {
                var rootobject = JsonConvert.DeserializeObject<T>(data);
                return rootobject;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error DeserializeObject in LoadDataForApp {0}", ex.Message);
                return default(T);
            }
        }


        private static String LoadFileDataString(String filename)
        {
            var assembly = typeof(Xam.LaGalerna.App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(filename);
            String jsonstr = String.Empty;
            using (var reader = new System.IO.StreamReader(stream))
            {
                jsonstr = reader.ReadToEnd();
            }
            return jsonstr;
        }
    }
}
