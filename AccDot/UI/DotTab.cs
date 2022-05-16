using System;
using System.ComponentModel;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.GameplaySetup;
using UnityEngine;
using Zenject;
using Colour = UnityEngine.Color;

namespace AccDot.UI
{
	public class DotTab : MonoBehaviour, IInitializable, IDisposable, INotifyPropertyChanged
	{
		private Config config;
		private GameplaySetupViewController gameplaySetupViewController;
		public event PropertyChangedEventHandler PropertyChanged = null!;

		[UIComponent("root")]
		private readonly RectTransform rootTransform = null!;

		[UIComponent("modal")]
		private readonly RectTransform modalTransform = null!;

		[UIValue("dot-color")]
		public Colour DotColorValue
		{
			get => config.Color;
			set => config.Color = value;
		}

		[UIValue("dot-scale")]
		public float DotScaleValue
		{
			get => config.Scale;
			set
			{
				config.Scale = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DotScaleValue)));
			}
		}

		[UIValue("dot-distance")]
		public float DotDistanceValue
		{
			get => config.Distance;
			set
			{
				config.Distance = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DotDistanceValue)));
			}
		}

		[Inject]
		internal void Inject(Config _config, GameplaySetupViewController _gameplaySetupViewController)
		{
			config = _config;
			gameplaySetupViewController = _gameplaySetupViewController;
		}

		public void Initialize()
		{
			gameplaySetupViewController.didDeactivateEvent += YeetModalEvent;

			GameplaySetup.instance.AddTab("AccDot", "AccDot.UI.DotTab.bsml", this, MenuType.All);
		}

		public void Dispose()
		{
			GameplaySetup.instance?.RemoveTab("AccDot");
			gameplaySetupViewController.didDeactivateEvent -= YeetModalEvent;
		}

		private void YeetModalEvent(bool removedFromHierarchy, bool screenSystemDisabling)
		{
			if (rootTransform != null && modalTransform != null)
			{
				modalTransform.SetParent(rootTransform);
				modalTransform.gameObject.SetActive(false);
			}
		}
	}
}
