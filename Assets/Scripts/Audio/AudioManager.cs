using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
	public class AudioManager : MonoBehaviour
	{

		public Sound[] sounds;
		private static List<Sound> staticSounds;

		void Awake()
		{
			staticSounds = new List<Sound>();

			foreach (Sound s in sounds)
			{
				s.source = gameObject.AddComponent<AudioSource>();
				s.source.clip = s.clip;

				s.source.loop = s.loop;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;

				staticSounds.Add(s);
			}
		}

		public void MuteMusic()
		{
			foreach (Sound s in staticSounds)
			{
				if (s.name == "Song")
				{
					s.source.volume = 0;
					return;
				}
			}
		}

		public void UnmuteMusic()
		{
			foreach (Sound s in staticSounds)
			{
				if (s.name == "Song")
				{
					s.source.volume = 0.65f;
					return;
				}
			}
		}

		public static void Play(string n)
		{
			foreach (Sound s in staticSounds)
			{
				if (s.name == n)
				{
					s.source.Play();
					return;
				}
			}
		}

		public static void Stop(string n)
		{
			foreach (Sound s in staticSounds)
			{
				if (s.name == n)
				{
					s.source.Stop();
					return;
				}
			}
		}
	}
}