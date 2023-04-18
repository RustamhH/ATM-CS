using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ATM.MyExceptions;
namespace ATM
{
    internal class BankCard
    {
		string _Pan;
		string _Pin;
		string _Cvv;
		decimal _balance;
		DateTime _expiredate;

		public DateTime ExpireDate {
			get { return _expiredate; } 
			init
			{
				if(value<_expiredate) throw new ExpireDate_Exception();
				_expiredate = value;
			}
		}

		public decimal Balance
		{
			get { return _balance; }
			set {
				if (value < 0) throw new Balance_Exception();
				_balance = value; 
			}
		}
		public string PAN
		{
			get { return _Pan; }
			init 
			{
				if (value == null || value.Length != 16) throw new PAN_Exception();
				_Pan = value;
			}
		}
		public string PIN
		{
			get { return _Pin; }
			set 
			{
				if (value == null || value.Length != 4) throw new PIN_Exception();
				_Pin = value;
			}
		}


		public string CVV
		{
			get { return _Cvv; }
			init {
                if (value == null || value.Length != 3) throw new CVV_Exception();
                _Cvv = value;
            }
		}



		public BankCard() { }

		public BankCard(string Pan,string Pin,string Cvv,decimal Balance,DateTime expireDate)
		{
			PAN = Pan;
			PIN = Pin;
			CVV = Cvv;
			this.Balance = Balance;
			ExpireDate = expireDate;
		}




	}
}
