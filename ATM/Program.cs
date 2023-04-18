using System.Collections.Immutable;
using System.Xml;

namespace ATM
{

    namespace MyExceptions
    {
        internal class PAN_Exception:ApplicationException
        {
            public PAN_Exception() :base("Invalid Pan") {}
            public PAN_Exception(string message) : base(message) {}
        }
        
        internal class PIN_Exception:ApplicationException
        {
            public PIN_Exception() :base("Invalid Pin") {}
            public PIN_Exception(string message) : base(message) {}
        }
        
        internal class CVV_Exception:ApplicationException
        {
            public CVV_Exception() :base("Invalid Cvv") {}
            public CVV_Exception(string message) : base(message) {}
        }
        
        internal class Balance_Exception:ApplicationException
        {
            public Balance_Exception() :base("Invalid Balance") {}
            public Balance_Exception(string message) : base(message) {}
        }
        internal class Client_Exception:ApplicationException
        {
            public Client_Exception() :base("This client doesn't exist") {}
            public Client_Exception(string message) : base(message) {}
        }
        internal class ExpireDate_Exception:ApplicationException
        {
            public ExpireDate_Exception() :base("This card is outdated") {}
            public ExpireDate_Exception(string message) : base(message) {}
        }

        

    }


    internal class Program
    {

        
        // Menu
        static public int Print(List<string> arr)
        {
            int index = 0;
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < arr.Count; i++)
                {
                    if (i == index) Console.ForegroundColor = ConsoleColor.DarkRed;
                    else Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(50, i + 10);
                    Console.WriteLine(arr[i]);
                }
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (index == 0) index = arr.Count - 1;
                    else index--;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (index == arr.Count - 1) index = 0;
                    else index++;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    return index;
                }
            }
        }
        static void Main(string[] args)
        {


            Client client1;
            Client client2;
            Client client3;
            Client client4;

            // fake datas
            try
            {
                DateTime dt = new DateTime(2028, 5, 22);
                client1 = new Client(18,"Ibrahim","Asadov",new BankCard("1234567890213456","5838","505",2500,dt));
                client2 = new Client(22,"Rustem","Hesenli",new BankCard("5432156789543257","0859","363",9000,dt));
                client3 = new Client(53,"Hesen","Abdullazade",new BankCard("1234567890987654","6502","543",10000,dt));
                client4 = new Client(21,"John","Johnlu",new BankCard("3242356789098767","9021","647",345,dt));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Some of the clients' datas are invalid");
            }




            Bank bank = new Bank();
            try
            {
                bank.AddClient(client1);
                bank.AddClient(client2);
                bank.AddClient(client3);
                bank.AddClient(client4);
            }
            catch(ApplicationException apx)
            {
                Console.WriteLine(apx.Message);
            }

            while(true)
            {
                Console.Write("Enter PIN: ");
                Client? currentClient;
                try
                {
                    currentClient = bank.Enter(Console.ReadLine());
                }
                catch(ApplicationException aex)
                {
                    Console.WriteLine(aex.Message);
                    continue;
                }
                Console.WriteLine($"Welcome , {currentClient.Name} {currentClient.Surname}!");
                Console.ReadKey(true);
                while(true)
                {
                    int choice = Print(new List<string> { "Balance", "Withdraw", "Previous Operations", "Card-to-Card","Exit" });
                    if (choice == 0)
                    {
                        try { bank.ShowBalance(currentClient); }
                        catch (ApplicationException apx) { Console.WriteLine(apx.Message); }
                        Console.ReadKey(true);
                    }
                    else if (choice == 1)
                    {
                        List<string> amounts = new List<string> { "10", "20", "50", "100", "Other" };
                        int wchoice = Print(amounts);
                        try
                        {


                            if (wchoice != 4)
                            {
                                bank.WithdrawCash(currentClient, Convert.ToDecimal(amounts[wchoice]));
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("Enter amount: ");
                                if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                                {
                                    bank.WithdrawCash(currentClient, Convert.ToDecimal(amount));
                                }
                                else throw new ApplicationException("Invalid input format");
                            }
                        }
                        catch (ApplicationException apx)
                        {
                            Console.WriteLine(apx.Message);
                        }
                        Console.ReadKey(true);
                    }
                    else if (choice == 2)
                    {
                        List<string> days = new List<string> { "1", "2", "5", "10" };
                        int wchoice = Print(days);
                        Console.WriteLine("Operations\n");
                        currentClient.ShowOperations(Convert.ToInt32(days[wchoice]));
                        
                        Console.ReadKey(true);
                    }
                    else if (choice == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Enter sending PIN: ");
                        string topin = Console.ReadLine();
                        Console.Write("Enter amount: ");
                        decimal.TryParse(Console.ReadLine(), out decimal amount);
                        try
                        {
                            bank.CardtoCard(topin, currentClient, amount);
                        }
                        catch (ApplicationException apx)
                        {
                            Console.WriteLine(apx.Message);
                        }
                        
                        Console.ReadKey(true);
                    }
                    else break;
                }
                
                
            }
        }
    }
}