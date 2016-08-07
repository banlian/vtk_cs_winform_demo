using System;
using System.IO;

namespace Vtk
{
    public class SimplePointFile
    {
        private static Stream fs;
        private static StreamReader sr;
        private static StreamWriter sw;


        public static int ImageWidth;
        public static int ImageHeight;

        #region File Methods

        public static void OpenReadFile(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException();
            }

            fs = File.Open(filename, FileMode.Open, FileAccess.ReadWrite);
            sr = new StreamReader(fs);
        }

        public static void OpenWriteFile(string filename)
        {
            if (!File.Exists(filename))
            {
                File.Create(filename);
            }

            fs = File.Open(filename, FileMode.Create, FileAccess.ReadWrite);
            sw = new StreamWriter(fs);
        }

        public static double[] ReadLine()
        {
            var readLine = sr.ReadLine();
            if (readLine != null && readLine != "\r\n")
            {
                string[] dataStrings = readLine.Split(' ');
                if (dataStrings.Length < 6)
                {
                    throw new InvalidDataException();
                }

                double[] data = new double[dataStrings.Length];
                data[0] = double.Parse(dataStrings[0]);
                data[1] = double.Parse(dataStrings[1]);
                data[2] = double.Parse(dataStrings[2]);
                data[3] = double.Parse(dataStrings[3]);
                data[4] = double.Parse(dataStrings[4]);
                data[5] = double.Parse(dataStrings[5]);
                return data;
            }
            return null;
        }

        public static double[] ReadShortLine()
        {
            var readLine = sr.ReadLine();
            if (readLine != null && readLine != "\r\n")
            {
                string[] dataStrings = readLine.Split(' ');
                if (dataStrings.Length < 3)
                {
                    throw new InvalidDataException();
                }

                double[] data = new double[dataStrings.Length];
                data[0] = double.Parse(dataStrings[0]);
                data[1] = double.Parse(dataStrings[1]);
                data[2] = double.Parse(dataStrings[2]);
                return data;
            }
            return null;
        }


        public static void Writeline(double[] data)
        {
            if (data.Length < 6)
            {
                throw new InvalidDataException("writeline data error");
            }

            sw.WriteLine(string.Format("{0} {1} {2} {3} {4} {5}", data[0], data[1], data[2], data[3], data[4], data[5]));
        }


        public static void CloseReadFile()
        {
            if (sr != null)
            {
                sr.Close();
            }
            if (fs != null)
            {
                fs.Close();
            }
        }

        public static void CloseWriteFile()
        {
            if (sw != null)
            {
                sw.Close();
            }
            if (fs != null)
            {
                fs.Close();
            }
        }

        #endregion

        #region Image Methods

        public static void GetImageInfo(string filename)
        {
            ImageWidth = 0;
            ImageHeight = 0;

            SimplePointFile.OpenReadFile(filename);

            int lines = 0;
            double[] pixels = {0, 0};
            double[] temp;
            while ((temp = SimplePointFile.ReadLine()) != null)
            {
                pixels = temp;
                lines++;
            }

            //Console.WriteLine("File Lines:" + lines);
            if (lines == ((int) pixels[0] + 1)*((int) pixels[1] + 1))
            {
                ImageWidth = (int) pixels[0] + 1;
                ImageHeight = (int) pixels[1] + 1;
            }
            else
            {
                throw new NotSupportedException("File Type Error!");
            }

            SimplePointFile.CloseReadFile();
        }

        #endregion
    }
}