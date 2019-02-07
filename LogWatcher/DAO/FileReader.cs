using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LogWatcher.DAO
{
    public class FileReader
    {
        private string _fileLocation = "";

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="FileLocation">Fájl elérési útvonala</param>
        public FileReader(string FileLocation)
        {
            this._fileLocation = FileLocation;
        }

        public List<string> getLogFileData()
        {
            List<string> ret = null;
            try
            {
                List<string> data = File.ReadAllLines(_fileLocation, Encoding.Default).ToList();
                ret = data;

            }
            catch (Exception e )
            {
            }
            
            return ret;
        }
    }
}
