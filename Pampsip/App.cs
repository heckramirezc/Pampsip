using System;
using Pampsip.Data;
using Pampsip.Helpers;
using Pampsip.Pages.Menu;
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
		public static double NavigationBarHeight = 0f;
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
			MessagingCenter.Subscribe<Pages.Menu.Menu>(this, "logout", (sender) =>
            {
				MainPage = new Login();
            });
			MessagingCenter.Subscribe<Login>(this, "Login", (sender) =>
			{
				MainPage = new RootPagina();
				Settings.session_Session_Token = "1";
				Settings.session_idUsuario = "1";
			});

			MessagingCenter.Subscribe<LoginVerificacion>(this, "Login", (sender) =>
            {
                MainPage = new RootPagina();
				Settings.session_Session_Token = "1";
				Settings.session_idUsuario = "1";
            });


			if (string.IsNullOrEmpty(Settings.session_SubscriptionKey))
				Settings.session_SubscriptionKey = Constantes.SubscriptionKey;

			ManejadorDatos = new ManejadorDatos(new ServicioWeb());

			if(!string.IsNullOrEmpty(Settings.session_Session_Token))
				MainPage = new RootPagina();
			else
				MainPage = new Login();
			
			
        }

		protected override void OnStart()
        {
            System.Diagnostics.Debug.WriteLine("OnStart");
        }

        protected override void OnSleep()
        {
            System.Diagnostics.Debug.WriteLine("OnSleep");
			Constantes.RedSocialPresentada = true;
        }

        protected override void OnResume()
        {
            System.Diagnostics.Debug.WriteLine("OnResume");
			Constantes.RedSocialPresentada = false;
        }
    }
}
