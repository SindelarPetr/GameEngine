using Xamarin.Forms;

namespace GameEngine.Advertisement
{
	public static class AdServiceProvider
	{
		public static void ShowInterstitial(string interstitialId)
		{
			//global::Xamarin.Forms.Forms.Init();
			//"ca-app-pub-6639044173799596/7932523865"

			DependencyService.Get<IAdService>().ShowInterstitial(interstitialId);
		}
	}
}
