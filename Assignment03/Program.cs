using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;

namespace Assignment03
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
    #region Q3
    //A- Indexer, lets objects be accessed in an array like way
    //B-Invalid, Add validation
    //C-Yes
    #endregion
    #region Q4
    //a) What does the `static` keyword mean on `TotalOrders`? How is it different from the `Item` field?
    //no object from TotalOrders will be created, it is different as all objects can access TotalOrders but not the opposite

    //b) Can a static method inside `Order` access the `Item` field directly? Why or why not?
    //No because it is not static

    #endregion
    #region Part02
    internal enum Type
    {
        Standard,
        VIP,
        IMAX
    }

    internal struct SeatLocation
    {
        public char Row;
        public int Number;
        public SeatLocation(char row, int number)
        {
            Row = row;
            Number = number;
        }
    }


    internal class Ticket
    {
        private string movieName;
        private Type t;
        private SeatLocation seat;
        private double price;
        static int ticketCounter = 0;


        //Properties
        public string MovieName
        {
            get { return movieName; }
            set { if (!string.IsNullOrEmpty(value))
                     movieName=value;
            }
        }

        public Type T { set; get;}

        public SeatLocation Seat { set; get; }

        
        public double Price
        {
            get{ return price; }
            set {if(value > 0) 
                price = value; }
        }

        public double PriceAfterTax => price * 1.14;

        public int TicketId { get; }

        public Ticket(string _MovieName, Type _Type, SeatLocation _Seat, double _Price)
        {
            MovieName = _MovieName;
            T = _Type;
            Seat = _Seat;
            Price = _Price;
            ticketCounter++;
            TicketId = ticketCounter;
        }
       
        public Ticket(string _MovieName) : this(_MovieName, Type.Standard, new SeatLocation('A', 1), 50)
        {

        }


        public double CalcTotal(double taxPercent)
        {
            return Price + (Price * taxPercent / 100);
        }



        public double ApplyDiscount(ref double discountAmount)
        {
            if (discountAmount > 0 && discountAmount < Price)
            {
                Price = Price - discountAmount;
                discountAmount = 0;
                return Price;
            }
            else
            {
                return Price;
            }
        }

        public static int  GetTotalTicketsSold() {  
            return ticketCounter; }


        public void PrintTicket(double taxPercent)
        {
            Console.WriteLine("===== Ticket Info =====");
            Console.WriteLine($"Movie   :  {MovieName}");
            Console.WriteLine($"Type    : {T}");
            Console.WriteLine($"Seat    : {Seat}");
            Console.WriteLine($"Price   : {Price:F2}");
            Console.WriteLine($"Total ({taxPercent}% tax) : {CalcTotal(taxPercent):F2}");

        }
    }

    internal class Cinema
    {
        private Ticket[] _tickets = new Ticket[20];

        public Ticket this[int index]
        {
            get
            {
                if (index >= 0 && index < _tickets.Length)
                    return _tickets[index];
                else
                    return null;
            }
            set
            {
                if (index >= 0 && index < _tickets.Length)
                    _tickets[index] = value;
            }
        }
    }

    static class BookingHelper
    {
        public static double CalcGroupDiscount(int numberOfTickets, double pricePerTicket)
        {
            double total = numberOfTickets * pricePerTicket;
            if (numberOfTickets >= 5) 
                return total *= 0.90;  
            else
                return total;
        }

        private static int counter=0;
        public static string GenerateBookingReference()
        {
            counter++;
            return $"BK-{counter}";
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Movie Name: ");
            string movie = Console.ReadLine();

            Console.Write("Enter Ticket Type (0 = Standard , 1 = VIP , 2 = IMAX ): ");
            int typeInput = int.Parse(Console.ReadLine());
            Type Type = (Type)typeInput;

            Console.Write("Enter Seat Row (A, B, C...): ");
            char row = char.Parse(Console.ReadLine());

            Console.Write("Enter Seat Number: ");
            int number = int.Parse(Console.ReadLine());

            Console.Write("Enter Price: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Enter Discount Amount: ");
            double discount = double.Parse(Console.ReadLine());


            SeatLocation seat = new SeatLocation(row, number);
            Ticket t1 = new Ticket(movie, Type, seat, price);
            double tax = 14;

            t1.PrintTicket(tax);
            Console.WriteLine("===== After Discount =====");

            double discountBefore = discount;
            t1.ApplyDiscount(ref discount);
            Console.WriteLine($"Discount Before : {discountBefore:F2}");
            Console.WriteLine($"Discount After  : {discount:F2}");


            t1.PrintTicket(tax);
            Console.WriteLine("===== Statistics =====");
            Console.WriteLine($"Total Tickets Sold: {Ticket.GetTotalTicketsSold()}\n");
            Console.WriteLine($"Booking Reference 1: {BookingHelper.GenerateBookingReference()}");
            double groupTotal = BookingHelper.CalcGroupDiscount(5, 80);
            Console.WriteLine($"Group total for 5 tickets at 80 EGP each: {groupTotal:F2}");
        }
    }

    #endregion
}
