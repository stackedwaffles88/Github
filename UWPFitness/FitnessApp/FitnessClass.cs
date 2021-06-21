using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessApp
{
    public enum Gender
    {
        male,
        female
    }
    public class FitnessClass
    {
        public string Name;
        public string Email;
        public Gender Gender;
        public double Weight;
        public double Height;
        public int age;
        private string fatpercent = null;
        private double bmi;
        private double bmr;

        public FitnessClass(string n, string em, Gender ge, double we, double he, int ag)
        {
            //initialize all values
            Name = n;
            Email = em;
            Gender = ge;
            Weight = we;
            Height = he;
            age = ag;
        }
        public double BMI {
            get { 
            bmi = 703 *(Weight / (Height * Height));
                bmi = Math.Round(bmi);
            return bmi;
            }

}
        public double BMR
        {
            get
            {
                if(Gender == Gender.male)
                {
                    bmr = 66.47 + (6.24 * Weight) + (12.7 * Height) - (6.755 * age);


                }
                bmr = 655.1 + (4.35 * Weight) + (4.7 * Height) - (4.7 * age);
                bmr = Math.Round(bmr);
                return bmr;
            }
        }

        public string FatPercent
        {
            get
            {
                if(fatpercent == null){
                    //calculate fat percentage
                    double fat = 1.2 * BMI + (0.23 * age) - 5.4;
                    fat = Math.Round(fat);
                    //return a string with % symbol
                    fatpercent = fat.ToString() + "%";
                   
                }
                return fatpercent;
            }
        }
    }
}
