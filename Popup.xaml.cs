using Playnite.SDK;
using System.Windows;
using System.Windows.Controls;

namespace ClearCookies
{
	public partial class ClearCookiesPopup : UserControl
	{
		public string Domain { get; set; }

		public ClearCookiesPopup()
		{
			InitializeComponent();
		}

		public static string Show(IPlayniteAPI api)
		{
			var window = api.Dialogs.CreateWindow(new WindowCreationOptions
			{
				ShowMinimizeButton = false,
				ShowMaximizeButton = false,
			});
			var popup = new ClearCookiesPopup();

			window.Height = 350;
			window.Width = 400;
			window.Title = ResourceProvider.GetString("LOCClearCookies");
			window.Content = popup;
			window.Owner = api.Dialogs.GetCurrentAppWindow();
			window.WindowStartupLocation = WindowStartupLocation.CenterOwner;

			popup.ConfirmButton.Click += (s, e) =>
			{
				popup.Domain = popup.Input.Text;
				window.DialogResult = true;
				window.Close();
			};

			if (!(window.ShowDialog() ?? false) || string.IsNullOrWhiteSpace(popup.Domain))
			{
				return null;
			}

			return popup.Domain;
		}
	}
}