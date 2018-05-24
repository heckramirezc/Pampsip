using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageCircle.Forms.Plugin.Abstractions;
using Lottie.Forms;
using Pampsip.Controls;
using Pampsip.Data;
using Pampsip.Interfaces;
using Pampsip.Models.FaceRecognition;
using Pampsip.Views.Notificaciones;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Toasts;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Pampsip.Views.Acceso
{
    public class Login : ContentPage
    {
        private string personGroupId= "pampsip-ciudadanos";
		ExtendedEntry Usuario, Contrasenia;
        public Login()
        {
            //BindingContext = new LoginViewModel();
			try
            {
				DependencyService.Get<INavigationService>().HideStatusBar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }            
			Label Bienvenida = new Label
            {
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,               
                Text = "BIENVENIDO",
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				TextColor = Color.White,
				FontSize = (App.DisplayScreenWidth / 15.666666666666667)
            };

			Label Mensaje = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.Center,
				Text = "Toca sobre el avatar\r\npara iniciar el reconocimiento facial\r\no\r\ningresa con tus datos de acceso manual",
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("4D4D4D"),
				FontSize = (App.DisplayScreenWidth / 26.857142857142857)
            };

			CircleImage avatar = new CircleImage
            {
				BorderColor = Color.FromHex("f6f6f6"),
				BorderThickness = Convert.ToInt32(App.DisplayScreenWidth / 53.714285714285714),                
                Aspect = Aspect.AspectFill,
                Source = "avatar"
            };

			AnimationView avatarDefault = new AnimationView
			{
				AutoPlay = true,
				Animation = "face_id.json",
				Loop = true,
				WidthRequest = App.DisplayScreenHeight / 5.8,
				HeightRequest = App.DisplayScreenHeight / 5.8,
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
			};

			ICommand avatarDefaultCommand = new Command(IdentifyAsync);
			avatarDefault.ClickedCommand = avatarDefaultCommand;
			Grid Avatar = new Grid
			{
				Children=
				{
					avatar,
                    avatarDefault
				}
			};

			TapGestureRecognizer ReconocimientoFacial = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
			ReconocimientoFacial.Tapped += ReconocimientoFacial_Tapped;
			Avatar.GestureRecognizers.Add(ReconocimientoFacial);
			avatar.GestureRecognizers.Add(ReconocimientoFacial);
			//avatarDefault.GestureRecognizers.Add(ReconocimientoFacial);

			Usuario = new ExtendedEntry()
            {
                Margin = 0,                
				Keyboard = Keyboard.Numeric,
                Placeholder = "CUI",
				PlaceholderColor = Color.FromHex("4D4D4D"),
				FontFamily = Device.OnPlatform("Montserrat-Light", "Montserrat-Light", null),
				TextColor = Color.FromHex("4D4D4D"),
				BackgroundColor = Color.Transparent,
                Text = string.Empty,
                HasBorder = false,
				FontSize = (App.DisplayScreenWidth / 25.066666666666667)
            };
			Usuario.TextChanged+= Usuario_TextChanged;


            Contrasenia = new ExtendedEntry()
            {
                Margin =0,
                Placeholder = "Contraseña",
				PlaceholderColor = Color.FromHex("4D4D4D"),
				FontFamily = Device.OnPlatform("Montserrat-Light", "Montserrat-Light", null),
				TextColor = Color.FromHex("4D4D4D"),
				BackgroundColor = Color.Transparent,
                Text = string.Empty,
                HasBorder = false,
                IsPassword = true,
				FontSize = (App.DisplayScreenWidth / 25.066666666666667)
            };


			Label Forget = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = "He olvidado mi contraseña",
				FontFamily = Device.OnPlatform("Montserrat-Light", "Montserrat-Light", null),
				TextColor = Color.FromHex("2E3192"),
				FontSize = (App.DisplayScreenWidth / 26.857142857142857)
            };

			TapGestureRecognizer ForgetTAP = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
			ForgetTAP.Tapped+= ForgetTAP_Tapped;
			Forget.GestureRecognizers.Add(ForgetTAP);

			Button login = new Button
			{
				Margin = 0,
				Text = "INGRESAR",
				TextColor = Color.White,
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				FontSize = (App.DisplayScreenWidth / 25.066666666666667),
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				BackgroundColor = Color.Transparent,
				WidthRequest = (App.DisplayScreenHeight / 3.608888888888889),
				HeightRequest = (App.DisplayScreenHeight / 20.3),
			};
			login.Clicked+= Login_Clicked;

			Grid Login = new Grid
			{
				Padding = 0,
				Children = 
				{
					new Image
                    {
                        Source = "loginButton",
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions= LayoutOptions.Center,
						WidthRequest = (App.DisplayScreenHeight / 3.608888888888889),
                        HeightRequest = (App.DisplayScreenHeight / 20.3),
                    },
                    login
				}
			};
                        

			RelativeLayout CC = new RelativeLayout()
            {
				BackgroundColor = Color.White,
                WidthRequest = App.DisplayScreenWidth,
                MinimumWidthRequest = App.DisplayScreenHeight,
                MinimumHeightRequest = App.DisplayScreenHeight,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

			CC.Children.Add(new Image
    			{
    				Aspect = Aspect.Fill, 
    				Source = "header",
				    HeightRequest = App.DisplayScreenHeight / 2.889679715302491
    			},
                            Constraint.Constant(0),
                            Constraint.Constant(0),
			                Constraint.Constant(App.DisplayScreenWidth),
			                Constraint.Constant(App.DisplayScreenHeight / 2.889679715302491)
            );
            
			CC.Children.Add(Bienvenida,
                            Constraint.Constant(0),
			                Constraint.Constant(App.DisplayScreenHeight / 9.333333333333333),
                            Constraint.Constant(App.DisplayScreenWidth)
                           );
			
			CC.Children.Add(Avatar,
			                Constraint.Constant((App.DisplayScreenWidth / 2)-(App.DisplayScreenHeight/9.022222222222222)),
			                Constraint.Constant(App.DisplayScreenHeight/5.75886524822695),
			                Constraint.Constant(App.DisplayScreenHeight/4.511111111111111),
			                Constraint.Constant(App.DisplayScreenHeight/4.511111111111111)
			               );


			StackLayout Contenido = new StackLayout
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Spacing = App.DisplayScreenHeight/11.6,
				Children =
				{
					Mensaje,
					new StackLayout
                    {						
                        HorizontalOptions = LayoutOptions.Center,
						WidthRequest = (App.DisplayScreenHeight /2.706666666666667),
                        Spacing = 0,
                        Children =
                        {
                            Usuario,
							new BoxView{BackgroundColor = Color.FromHex("BFBFBF"), HeightRequest =(App.DisplayScreenWidth /341.818181818181818), HorizontalOptions = LayoutOptions.FillAndExpand},
							new BoxView{BackgroundColor = Color.Transparent, HeightRequest =(App.DisplayScreenHeight /58.133333333333333), HorizontalOptions = LayoutOptions.FillAndExpand},
                            Contrasenia,
							new BoxView{BackgroundColor = Color.FromHex("BFBFBF"), HeightRequest =(App.DisplayScreenWidth /341.818181818181818), HorizontalOptions = LayoutOptions.FillAndExpand},
							new BoxView{BackgroundColor = Color.Transparent, HeightRequest =(App.DisplayScreenHeight /27.066666666666667), HorizontalOptions = LayoutOptions.FillAndExpand},
                            Forget,                            
							new BoxView{BackgroundColor = Color.Transparent, HeightRequest =(App.DisplayScreenHeight /14.763636363636364), HorizontalOptions = LayoutOptions.FillAndExpand},
                            Login
                        },
                    }
				}
			};

			CC.Children.Add(Contenido,
                            Constraint.Constant(0),
			                Constraint.Constant(App.DisplayScreenHeight / 2.36734693877551),
                            Constraint.Constant(App.DisplayScreenWidth)
                           );

			Padding = 0;
			BackgroundImage = "background";
			Content = new ScrollView
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Content = CC
			};
        }

		void AvatarDefault_Click()
		{
			IdentifyAsync();
		}
		async void IdentifyAsync()
		{
			if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                MediaFile photo;

                await CrossMedia.Current.Initialize();

                // Take photo
                if (CrossMedia.Current.IsCameraAvailable)
                {
                    photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        DefaultCamera = CameraDevice.Front,
						Directory = Constantes.LargePersonGroupId,
                        Name = "person.jpg",
						PhotoSize = PhotoSize.Medium,
						//CustomPhotoSize = 40,
						CompressionQuality = 65
                    });
                }
                else
                {
                    photo = await CrossMedia.Current.PickPhotoAsync();
                }

                await Navigation.PushPopupAsync(new NotificacionCargando());
                using (var stream = photo.GetStream())
                {
                    var faces = await App.ManejadorDatos.DetectAsync(new Detect { Stream = stream, returnFaceId = true, returnFaceLandmarks = false });
					Guid[] faceIds = faces.Select(face => face.FaceId).ToArray();

					if(faceIds.Count()>0)
					{
						var results = await App.ManejadorDatos.IdentifyAsync(
                        new Identify
                        {
                            peticion = new PeticionIdentify
                            {
                                largePersonGroupId = Constantes.LargePersonGroupId,
                                faceIds = faceIds,
                                maxNumOfCandidatesReturned = 1,
                                confidenceThreshold = 0.5
                            }
                        });
						Candidate[] candidates= results[0].Candidates;
						if (candidates.Count() > 0)
						{
							Guid result = results[0].Candidates[0].PersonId;                            
                            var person = await App.ManejadorDatos.GetPersonAsync(
                            new Person
                            {
                                personGroupId = Constantes.LargePersonGroupId,
                                personId = result
                            });
                            MessagingCenter.Send<Login>(this, "Login");
                            ShowToast(ToastNotificationType.Success, "Bienvenido", "Inicio de sesión exitoso", 4);                            
						}
                        else
                        {
                            ShowToast(ToastNotificationType.Error, "Inicio de sesión", "Usuario no registrado", 4);                                           
                            await Navigation.PopAllPopupAsync();
                        }   
					}
					else
					{
						ShowToast(ToastNotificationType.Warning, "Inicio de sesión", "No se ha detectado ningún rostro.", 4);
                        await Navigation.PopAllPopupAsync();
					}                                                                                							
                }
            }
            catch (Exception ex)
            {
                await Navigation.PopAllPopupAsync();
				ShowToast(ToastNotificationType.Error, "Inicio de sesión", "Servicio no disponible", 4);
            }
            finally
            {
                IsBusy = false;
            }
		}

		void ReconocimientoFacial_Tapped(object sender, EventArgs e)
		{
			IdentifyAsync();
		}
               
		async void ForgetTAP_Tapped(object sender, EventArgs e)
		{
			await Navigation.PushPopupAsync(new NotificacionCargando());
            await Task.Delay(3000);
			await Navigation.PopAllPopupAsync();
		}

        async void Login_Clicked(object sender, EventArgs e)
		{			
			if (String.IsNullOrEmpty(Usuario.Text))
            {
                await DisplayAlert("", "Por favor, indique el número de CUI de su DPI", "Aceptar");
                Usuario.Focus();
                return;
            }
			else if(Usuario.Text.Length<15)
			{
				await DisplayAlert("", "Por favor, indique un número de CUI válido", "Aceptar");
                Usuario.Focus();
                return;
			}
            if (String.IsNullOrEmpty(Contrasenia.Text))
            {
                await DisplayAlert("", "Por favor, indique su contraseña", "Aceptar");
                Contrasenia.Focus();
                return;
            }
			string CUI = Regex.Replace(Usuario.Text, @"\s+", "");
			System.Diagnostics.Debug.WriteLine(CUI);
            await Navigation.PushPopupAsync(new NotificacionCargando());            
			await Navigation.PushPopupAsync(new LoginVerificacion("1850"));
		}       

		string getContrasenia()
        {
            //System.Diagnostics.Debug.WriteLine("HASH(MD5) " + MD5.GetHashString(Contrasenia.Text.Trim() + Constantes.Salt_Text.Trim()));
            //return MD5.GetHashString(Contrasenia.Text.Trim() + Constantes.Salt_Text.Trim());
            return Contrasenia.Text.Trim();
        }
        
		private async void ShowToast(ToastNotificationType type, string titulo, string descripcion, int tiempo)
        {
            var notificator = DependencyService.Get<IToastNotificator>();
            bool tapped = await notificator.Notify(type, titulo, descripcion, TimeSpan.FromSeconds(tiempo));
        }
        
		void Usuario_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (((ExtendedEntry)sender).Text.Length == 4 && (e.NewTextValue.Length>=e.OldTextValue.Length))
				((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text + " ";
			if (((ExtendedEntry)sender).Text.Length == 10 && (e.NewTextValue.Length >= e.OldTextValue.Length))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text + " ";			
			if (((ExtendedEntry)sender).Text.Length == 16 && (e.NewTextValue.Length >= e.OldTextValue.Length))
				((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.Substring(0,((ExtendedEntry)sender).Text.Length-1);
		}

	}
}