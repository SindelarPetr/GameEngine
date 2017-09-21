namespace GameEngine.Audio
{
	public static class AudioOptions
	{
		private static float _musicVolume = 1;
		
		/// <summary>
		/// Adjusts volume of the music in the game. 
		/// </summary>
		public static float MusicVolume
		{
			get => _musicVolume;
			set
			{
				_musicVolume = value;
				//SoundEff.SetMusicVolume(value);
			}
		}

		public static float SoundVolume = 1;
	}
}
