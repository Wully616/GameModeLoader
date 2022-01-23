﻿using System.Collections;
using System.Collections.Generic;
using ThunderRoad;
using UnityEngine;

namespace GameModeLoader.Component {
	/// <summary>
	///     This survival mode inherits from the base games survival game mode
	/// </summary>
	public class BladeGame : LevelModule {
		protected WaveSpawner waveSpawner;
		protected Coroutine waveEndedCoroutine;

		public override void Update() {

		}

		public override IEnumerator OnLoadCoroutine() {

			if (WaveSpawner.instances.Count > 0) {
				waveSpawner = WaveSpawner.instances[0];
				waveSpawner.OnWaveEndEvent.AddListener(OnWaveEnded);
				level.StartCoroutine(LevelLoadedCoroutine());
				yield break;
			}

			Debug.LogError("No wave spawner available for Blade Game module!");
		}

		protected new void OnWaveEnded() {
			waveEndedCoroutine = level.StartCoroutine(WaveEndedCoroutine());
		}

		private IEnumerator LevelLoadedCoroutine() {
			while (!Player.local || !Player.local.creature)
				yield return new WaitForSeconds(2f);
		}


		private IEnumerator WaveEndedCoroutine() {
			yield return new WaitForSeconds(2f);
		}
	}
}