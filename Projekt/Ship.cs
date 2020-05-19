using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    public class Ship
    {
		private int publicid;

		public int PublicId
		{
			get { return publicid; }
			set { publicid = value; }
		}
		private double angle;

		public double Angle
		{
			get { return angle; }
			set { angle = value; }
		}

		private int distance;

		public int Distance
		{
			get { return distance; }
			set { distance = value; }
		}
		private	double velocity;

		public double Velocity
		{
			get { return velocity; }
			set { velocity = value; }
		}

		private int points;

		public int Points
		{
			get { return points; }
			set { points = value; }
		}
		private int energy;

		public int Energy
		{
			get { return energy; }
			set { energy = value; }
		}





	}
}
