using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace GPS_Coordinate_Calculator
{
    class Calculations
    {
        double totalDistance = 0;

        public double CalculateDistance(double latDegree1, double lonDegree1,
                                        double latDegree2, double lonDegree2)
        {
            double radius = 6371000; //Earth Radius in Metres
            double dLat = deg2rad(latDegree2 - latDegree1);
            double dLon = deg2rad(lonDegree2 - lonDegree1);
            double lat1 = deg2rad(latDegree1);
            double lat2 = deg2rad(latDegree2);

            double a = (Math.Sin(dLat / 2) * Math.Sin(dLat / 2)) +
                        Math.Cos(lat1) * Math.Cos(lat2) *
                        Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double d = radius * c;

            totalDistance += d;

            return d;
        }

        double deg2rad(double degree)
        {
            return degree * (Math.PI / 180);
        }

        public double getTotalDistance()
        {
            return Math.Round(totalDistance, 2);
        }

        public void setTotalDistanceToZero()
        {
            totalDistance = 0;
        }

        public double calculateAvgSpeed(int timeTaken)
        {
            double speedPerSec = totalDistance / timeTaken;
            return Math.Round(speedPerSec * 3.6,2); //KM/hour 
        }

        public int calculateAmountOfSprints(int amountOfCoordinates)
        {
            int amountOfSprints;

            amountOfSprints = (int)(amountOfCoordinates * 0.10);

            return amountOfSprints;
        }

        public double calculateAvgSprintDistance(int amountOfSprints, double coveredDistance)
        {
            double avgSprintDistance;

            avgSprintDistance = (coveredDistance * 0.30) / amountOfSprints;

            return avgSprintDistance;
        }
    }
}