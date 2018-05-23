using System;
using Pampsip.Data;
using Pampsip.Helpers;
using Pampsip.Views;
using Pampsip.Views.Acceso;
using Xamarin.Forms;

namespace Pampsip
{
    public class App : Application
    {
		DateTime lastUpdateDefault = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local);
        public static PampsipDatabase database;
        public static ManejadorDatos ManejadorDatos { get; set; }


        public static double DisplayScreenWidth = 0f;
        public static double DisplayScreenHeight = 0f;
        public static double DisplayScaleFactor = 0f;

        public static PampsipDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new PampsipDatabase();
                }
                return database;
            }
        }

        public App()
        {
			MessagingCenter.Subscribe<Login>(this, "Login", (sender) =>
			{
				MainPage = new NavigationPage(new Inicio());
			});

			if (string.IsNullOrEmpty(Settings.session_SubscriptionKey))
				Settings.session_SubscriptionKey = Constantes.SubscriptionKey;
			ManejadorDatos = new ManejadorDatos(new ServicioWeb());
			MainPage = new Login();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
