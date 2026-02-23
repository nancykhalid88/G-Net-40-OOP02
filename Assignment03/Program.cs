using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Assignment03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Q1
            //A- 1-attribute should be private    2-No validation in the method
            //B- make the attributes private and add validation to the method
            //C-Anyone can access the data and change it

            #endregion
            #region Q2
            //What is the difference between a field and a property in C#? Can a property contain logic? 
            //Give an example of a read-only property that returns a calculated value.

            //A feild is a variable that can be assigned a value, it doesnt have validation or constrints on it
            //A property is like a method (not a method) where it can set and get and validation can be added 
            //ex:
        //    public class Student
        //{
        //    private string name;
        //    public string Name
        //    {
        //        get
        //        {
        //            return name;
        //        }
        //    }
        //}
        #endregion
    }
    }
}
