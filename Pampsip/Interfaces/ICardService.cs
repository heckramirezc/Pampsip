using System;
namespace Pampsip.Interfaces
{
	public interface ICardService
    {
        void StartCapture();

        string GetCardNumber();

        string GetCardholderName();

		string GetExpiryYear();        

		string GetExpiryMonth();

		string GetCVV();
    }
}