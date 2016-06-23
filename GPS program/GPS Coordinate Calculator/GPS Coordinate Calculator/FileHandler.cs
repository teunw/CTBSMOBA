using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GPS_Coordinate_Calculator
{
    class FileHandler
    {
        Calculations calculate;
        OpenFileDialog openFileDialog1 = new OpenFileDialog();
        string file;

        public bool coordinateFormat = false; //True means Longitude,Lattitude - False means Lattitude,Longitude
        int size;
        int coordinatesAmount;

        string[,] gpsCoordinates;
        string[] coordinates = new string[0];
        

        public FileHandler(Calculations calculate)
        {
            this.calculate = calculate;
        }

        public void browseFile(TextBox tbFileName)
        {
            size = -1;
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                file = openFileDialog1.FileName;
                try
                {
                    tbFileName.Text = file;
                }
                catch (IOException)
                {

                }
            }
        }

        public void readFile()
        {
            string fileText;

            if (file == null)
            {
                MessageBox.Show("No file selected.");
                return;
            }
            fileText = File.ReadAllText(file);
            size = fileText.Length;

            splitDataIntoCoordinates(fileText);

            fileText = null;
        }

        public void splitDataIntoCoordinates(string fileText)
        {
            char delimiter1 = ' ';
            string[] splitData = fileText.Split(delimiter1);
            char delimiter2 = ',';

            gpsCoordinates = new string[splitData.Length, 2];

            coordinatesAmount = splitData.Length;

            for (int i = 0; i < splitData.Length - 1; i++)
            {
                coordinates = splitData[i].Split(delimiter2);

                for (int t = 0; t < 2; t++)
                {
                    string temp = coordinates[t];
                    coordinates[t] = temp.Replace('.', ',');
                }
                if(coordinateFormat)
                {
                    gpsCoordinates[i, 0] = coordinates[0];
                    gpsCoordinates[i, 1] = coordinates[1];
                }
                else
                {
                    gpsCoordinates[i, 0] = coordinates[1];
                    gpsCoordinates[i, 1] = coordinates[0];
                }
            }

            calculateStats();
        }

        public void calculateStats()
        {
            calculate.setTotalDistanceToZero();
            for (int i = 1; i <= coordinatesAmount - 2; i++)
            {
                double lat1 = Convert.ToDouble(gpsCoordinates[i - 1, 0]);  
                double lon1 = Convert.ToDouble(gpsCoordinates[i - 1, 1]);
                double lat2 = Convert.ToDouble(gpsCoordinates[i, 0]);
                double lon2 = Convert.ToDouble(gpsCoordinates[i, 1]);

                calculate.CalculateDistance(lat1, lon1, lat2, lon2);
            }
        }

        public void resetFile()
        {
            file = null;
            coordinatesAmount = 0;
            size = 0;
        }

        public int getCoordinatesAmount()
        {
            return coordinatesAmount;
        }
    }
}
