﻿using System.Collections;
using System.Linq;
using GameModeLoader.Data;
using ThunderRoad;
using Wully.Utils;

namespace GameModeLoader.Component {
	public class NoSpells : LevelModuleOptional {
		private bool showHighlighterTK;
		public override IEnumerator OnLoadCoroutine() {
			SetId();
			if ( IsEnabled() ) {
				EventManager.onPossess += EventManager_onPossess;
				showHighlighterTK = SpellTelekinesis.showHighlighter;
			}

			yield break;
		}

		private void EventManager_onPossess(Creature creature, EventTime eventTime) {
			if (eventTime == EventTime.OnStart) {
				return;
			}

			if ( IsEnabled() ) {
				creature.handLeft.caster.allowCasting = false;
				creature.handLeft.caster.allowSpellWheel = false;
				SpellTelekinesis.showHighlighter = false;
				creature.handLeft.caster.telekinesis.maxCatchDistance = 0.0f;
				creature.handLeft.caster.telekinesis.radius = 0.0f;
				creature.handLeft.caster.telekinesis.maxAngle = 0.0f;

				creature.handRight.caster.allowCasting = false;
				creature.handRight.caster.allowSpellWheel = false;
				creature.handRight.caster.telekinesis.maxCatchDistance = 0.0f;
				creature.handRight.caster.telekinesis.radius = 0.0f;
				creature.handRight.caster.telekinesis.maxAngle = 0.0f;
			}
		}

		public override void OnUnload() {
			if ( IsEnabled() ) {
				EventManager.onPossess += EventManager_onPossess;
				// Revert back to the original showHighlighter
				SpellTelekinesis.showHighlighter = showHighlighterTK;
			}
		}
	}
}