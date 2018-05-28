using Android.App;
using Android.Content;
using Card.IO;
using Pampsip.Droid.Services;
using Pampsip.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(CardService))]

namespace Pampsip.Droid.Services
{
	public class CardService : ICardService
    {
        private Activity activity;

        public void StartCapture()
        {
            InitCardService();

            var intent = new Intent(activity, typeof(CardIOActivity));
            intent.PutExtra(CardIOActivity.ExtraRequireExpiry, true);
            intent.PutExtra(CardIOActivity.ExtraRequireCvv, true);
            intent.PutExtra(CardIOActivity.ExtraRequirePostalCode, false);
            intent.PutExtra(CardIOActivity.ExtraUseCardioLogo, true);

            activity.StartActivityForResult(intent, 101);
        }

        public string GetCardNumber()
        {
            return (InfoShareHelper.Instance.CardInfo != null) ? InfoShareHelper.Instance.CardInfo.CardNumber : null;
        }

        public string GetCardholderName()
        {
            return (InfoShareHelper.Instance.CardInfo != null) ? InfoShareHelper.Instance.CardInfo.CardholderName : null;
        }

		public string GetExpiryYear()
		{
			return (InfoShareHelper.Instance.CardInfo != null) ? InfoShareHelper.Instance.CardInfo.ExpiryYear.ToString() : null;	
		}

		public string GetExpiryMonth()
		{
			return (InfoShareHelper.Instance.CardInfo != null) ? InfoShareHelper.Instance.CardInfo.ExpiryMonth.ToString() : null;
		}

        public string GetCVV()
		{
			return (InfoShareHelper.Instance.CardInfo != null) ? InfoShareHelper.Instance.CardInfo.Cvv : null;
		}

        private void InitCardService()
        {
            // Init current activity
            var context = Forms.Context;
            activity = context as Activity;
        }
    }

    public class InfoShareHelper
    {
        private static InfoShareHelper instance = null;
        private static readonly object padlock = new object();

        public CreditCard CardInfo { get; set; }

        public static InfoShareHelper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new InfoShareHelper();
                    }
                    return instance;
                }
            }
        }
    }
}