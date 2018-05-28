using System;
using Card.IO;
using Pampsip.Interfaces;
using Pampsip.iOS.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(CardService))]

namespace Pampsip.iOS.Services
{
	public class CardService : CardIOPaymentViewControllerDelegate, ICardService
	{
		private UIViewController rootViewController;
		private CreditCardInfo cardInfo;

		public void StartCapture()
		{
			InitCardService();
			var paymentViewController = new CardIOPaymentViewController(this);
			paymentViewController.DisableManualEntryButtons = true;

			rootViewController.PresentViewController(paymentViewController, true, null);
		}

		public string GetCardNumber()
		{
			return (cardInfo != null) ? cardInfo.CardNumber : null;
		}

		public string GetCardholderName()
		{
			return (cardInfo != null) ? cardInfo.CardholderName : null;
		}

		public string GetExpiryMonth()
        {
			return (cardInfo != null) ? cardInfo.ExpiryMonth.ToString(): null;
        }

		public string GetExpiryYear()
        {
			return (cardInfo != null) ? cardInfo.ExpiryYear.ToString() : null;
        }

		public string GetCVV()
        {
			return (cardInfo != null) ? cardInfo.Cvv : null;
        }

        private void InitCardService()
        {
            // Init rootViewController
            var window = UIApplication.SharedApplication.KeyWindow;
            rootViewController = window.RootViewController;
            while (rootViewController.PresentedViewController != null)
            {
                rootViewController = rootViewController.PresentedViewController;
            }
        }

        public override void UserDidCancelPaymentViewController(CardIOPaymentViewController paymentViewController)
        {
			Console.WriteLine("Scanning Canceled!");
			paymentViewController.DismissViewController(true, null);
        }

        public override void UserDidProvideCreditCardInfo(CreditCardInfo cardInfo, CardIOPaymentViewController paymentViewController)
        {
            if (cardInfo == null)
            {
				Console.WriteLine("Scanning Canceled!");
            }
            else
            {
                this.cardInfo = cardInfo;
            }

            paymentViewController.DismissViewController(true, null);
        }
    }
}