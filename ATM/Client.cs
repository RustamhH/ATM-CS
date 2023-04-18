using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    internal class Client
    {
        ushort _age;

        public SortedDictionary<DateTime,string > PreviousOperations { get; set; }

        public readonly Guid Id;
        public ushort Age {
            get { return _age; } 
            set 
            {
                if (value < 18) throw new Exception("You can't buy a card");
                _age= value;
            } 
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public BankCard CreditCard { get; set; }

        


        public Client() {
            Id= Guid.NewGuid();
            PreviousOperations=new SortedDictionary<DateTime,string>();
        }

        public Client(ushort age, string name, string surname, BankCard creditCard):this()
        {
            Age = age;
            Name = name;
            Surname = surname;
            CreditCard = creditCard;
        }


        public void ShowOperations(int day)
        {
            
            foreach (var op in PreviousOperations)
            {
                if (op.Key.Day >= DateTime.Now.Day - day)
                {
                    Console.WriteLine($"{op.Key.ToShortDateString()} {op.Value}");
                };
            }
        }


    }
}
