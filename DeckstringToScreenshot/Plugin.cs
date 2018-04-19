using System;
using System.Windows;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.FlyoutControls.DeckScreenshot;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Plugins;
using Hearthstone_Deck_Tracker.Utility.Extensions;
using Hearthstone_Deck_Tracker.Utility.Logging;

namespace DeckstringToScreenshot
{
    public class Plugin : IPlugin
    {
		public Plugin()
		{
			MenuItem.Click += (sender, e) =>
			{
				try
				{
					var hdbDeck = HearthDb.Deckstrings.DeckSerializer.Deserialize(Clipboard.GetText());
					var deck = HearthDbConverter.FromHearthDbDeck(hdbDeck);
					var screenshot = DeckScreenshotHelper.Generate(deck, true);
					DeckScreenshotHelper.CopyToClipboard(screenshot);
				}
				catch(Exception exception)
				{
					Hearthstone_Deck_Tracker.Windows.MessageDialogs.ShowMessage(Core.MainWindow,
						"Could not create screenshot", exception.ToString()).Forget();
					Log.Error(exception);
				}
			};
		}

		public void OnLoad()
		{
		}

		public void OnUnload()
		{
		}

		public void OnButtonPress()
		{
		}

		public void OnUpdate()
		{
		}

		public string Name { get; } = "DeckstringToScreenshot";
		public string Description { get; }
		public string ButtonText { get; }
		public string Author { get; } = "HearthSim";
		public Version Version { get; } = new Version(1, 0);

		public MenuItem MenuItem { get; } = new MenuItem()
		{
			Header = "Deckstring => Screenshot",
		};
	}
}
