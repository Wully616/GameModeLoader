﻿using System.Collections;
using GameModeLoader.Data;
using GameModeLoader.Utils;
using ThunderRoad;
using Wully.Utils;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameModeLoader.Component
{
    public class OneHPLeft : LevelModuleOptional
    {
		private short healthOri;
		public override IEnumerator OnLoadCoroutine()
		{
			//You must always call the following, so the IDs are setup for this LevelModuleOptional
			SetId();
			EventManager.onPossess += EventManager_onPossess;
            EventManager.onUnpossess += EventManager_onUnpossess;
			yield break;
		}

        private void EventManager_onUnpossess(Creature creature, EventTime eventTime)
        {
			if (eventTime == EventTime.OnStart)
			{
				Debug.Log("UnPossess : ");
				Debug.Log("healthOri : " + healthOri);
				Player.local.creature.data.health = healthOri;
				Debug.Log("dataHealth : " + Player.local.creature.data.health);
			}
		}

        private void EventManager_onPossess(Creature creature, EventTime eventTime)
		{
			if (eventTime == EventTime.OnEnd)
			{
				Debug.Log("Possess : ");
				Debug.Log("dataHealth : " + Player.local.creature.data.health);
				healthOri = Player.local.creature.data.health;
				Debug.Log("healthOri : " + healthOri);
				Player.local.creature.data.health = 1;
				Player.local.creature.maxHealth = 1f;
				Player.local.creature.currentHealth = 1f;
				return;
			}
		}

        public override void OnUnload()
        {
            base.OnUnload();
			EventManager.onPossess -= EventManager_onPossess;
			EventManager.onUnpossess -= EventManager_onUnpossess;
		}
    }
}
