using ATM.MyExceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    internal partial class Bank
    {
        List<Client> Clients { get; set; }

        public Bank() { Clients=new List<Client>(); }
        
        // Function Prototypes

        public partial void AddClient(Client client);
        public partial void ShowBalance(Client client);
        public partial void CardtoCard(string toPin, Client sender, decimal amount);
        public partial void WithdrawCash(Client client, decimal amount);
        public partial Client Enter(string pin);

    }

    internal partial class Bank
    {


        // Function Bodys

        public partial void AddClient(Client client)
        {
            foreach (var item in Clients) { if (item.CreditCard.PAN == client.CreditCard.PAN || item.CreditCard.PIN == client.CreditCard.PIN || item.CreditCard.CVV == client.CreditCard.CVV) throw new Client_Exception("This user already exist"); }
            Clients.Add(client);
        }

        public partial void ShowBalance(Client client)
        {
            if (Clients.Contains(client))
            {
                Console.WriteLine($"Your Balance : {client.CreditCard.Balance}");
                client.PreviousOperations.Add(DateTime.Now, $"{client.Name + " " + client.Surname} checked his balance: {client.CreditCard.Balance}");
            }
            else throw new Client_Exception();

        }

        public partial void CardtoCard(string toPin, Client sender, decimal amount)
        {
            foreach (var item in Clients)
            {
                if (item.CreditCard.PIN == toPin)
                {
                    if (sender.CreditCard.Balance < amount) throw new Balance_Exception("Insufficient amount");
                    sender.CreditCard.Balance -= amount;
                    item.CreditCard.Balance += amount;
                    sender.PreviousOperations.Add(DateTime.Now, $"{sender.Name + " " + sender.Surname} sended {amount} AZN to {item.Name + " " + item.Surname}'s balance");
                    item.PreviousOperations.Add(DateTime.Now, $"{item.Name + " " + item.Surname} accepted {amount} AZN from {sender.Name + " " + sender.Surname}'s balance");
                    Console.WriteLine($"{amount} AZN sended succesfully");
                    return;
                }
            }
            throw new Client_Exception($"Pin with {toPin} doesn't exist");
        }

        public partial void WithdrawCash(Client client, decimal amount)
        {
            if (client.CreditCard.Balance < amount) throw new Balance_Exception("Insufficient amount");
            client.CreditCard.Balance -= amount;
            client.PreviousOperations.Add(DateTime.Now, $"{client.Name + " " + client.Surname} withdrawed {amount} AZN");
            Console.WriteLine($"{amount} AZN withdrawed succesfully");
        }


        public partial Client Enter(string pin)
        {
            foreach (var item in Clients)
            {
                if (item.CreditCard.PIN == pin) return item;
            }
            throw new Client_Exception();
        }
    }
}
