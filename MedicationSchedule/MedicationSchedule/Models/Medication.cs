using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MedicationSchedule.Models
{
    public class Medication
    {
        private DateTime _when;
        private string _whenStr;
        private Side _whichSide;
        private bool _isDone = false;

        public DateTime When { 
            get => _when;
            set
            {
                _when = value;
                _whenStr = _when.ToString();
            }
        }
        public Side WhichSide { get => _whichSide; set => _whichSide = value; }
        public bool IsDone { get => _isDone; 
            set {
                _isDone = value;
                //Debug.Print("Setter IsDone called");
            } 
        }
        public string WhenStr { get => _whenStr; private set => _whenStr = value; }

        public static Side Flip(Side side)
        {
            return (side == Side.Left) ? Side.Right : Side.Left;
        }
    }

    public enum Side
    {
        Left,
        Right
    }
}
