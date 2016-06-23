using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GPS_Coordinate_Calculator
{
    public partial class formCalculator : Form
    {
        Calculations calculate = new Calculations();
        FileHandler fileHandler;

        public formCalculator()
        {
            fileHandler = new FileHandler(calculate);
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            double temp = 0;
            double latC1 = 0;
            double lonC1 = 0;
            double latC2 = 0;
            double lonC2 = 0;

            if(double.TryParse(tbLattitudeC1.Text, out latC1) || double.TryParse(tbLongitudeC1.Text, out lonC1) || 
               double.TryParse(tbLattitudeC2.Text, out latC2) || double.TryParse(tbLongitudeC2.Text, out lonC2) )
            {
                temp = calculate.CalculateDistance(latC1,
                                                   lonC1,
                                                   latC2,
                                                   lonC2);
            }

            tbDistance.Text = temp.ToString();

            lblTotalDistance.Text = calculate.getTotalDistance().ToString() + " Meter";
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            fileHandler.browseFile(tbFileName);
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            fileHandler.readFile();
            int minutesTaken;
            if(int.TryParse(tbTimeTaken.Text,out minutesTaken))
            {
                lblAvgSpeedNr.Text = calculate.calculateAvgSpeed(minutesTaken).ToString() + "km/h";
            }
            lblTotalDistance.Text = calculate.getTotalDistance().ToString() + " meter";

            lblNrOfSprints.Text = calculate.calculateAmountOfSprints(fileHandler.getCoordinatesAmount()).ToString() + " sprints";
            lblAvgSprintDistance.Text = Math.Round(calculate.calculateAvgSprintDistance(calculate.calculateAmountOfSprints(fileHandler.getCoordinatesAmount()), calculate.getTotalDistance()),2).ToString() + " meter";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            fileHandler.resetFile();
            calculate.setTotalDistanceToZero();

            tbLattitudeC1.Text = "";
            tbLattitudeC2.Text = "";
            tbLongitudeC1.Text = "";
            tbLongitudeC2.Text = "";
            tbDistance.Text = "";
            tbFileName.Text = "";
            tbTimeTaken.Text = "";

            lblNrOfSprints.Text = "0 sprints";
            lblAvgSprintDistance.Text = "0 Meter";
            lblAvgSpeedNr.Text = "0 km/h";
            lblTotalDistance.Text = calculate.getTotalDistance().ToString() + " Meter";
        }
    }
}
