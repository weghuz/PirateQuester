﻿using DFK;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PirateQuester.Utils;
using Microsoft.JSInterop;

namespace PirateQuester.Bot
{
    public class DFKBotSettings
	{
		public DFKBotSettings() 
		{
			QuestEnabled = Enumerable.Range(0, 25).Select(i => true).ToList();
		}
		public int CancelTxnDelay { get; set; } = 60000;

		public int UpdateInterval { get; set; } = 180;
        public int MinStamina { get; set; } = 20;
        public int MaxGasFeeGwei { get; set; } = 200;
        public bool LevelUp { get; set; } = true;
        public List<bool> QuestEnabled { get; set; } = new();
		public List<DFKStatAmount> MinTrainingStats { get; set; } = new();
		public List<LevelUpSetting> LevelUpSettings { get; set; } = new()
		{
			new()
			{
				HeroClass = "warrior",
				MainAttribute = Constants.DFKStats[0],
				SecondaryAttribute = Constants.DFKStats[1],
				TertiaryAttribute = Constants.DFKStats[3],
			},
			new()
			{
				HeroClass = "knight",
				MainAttribute = Constants.DFKStats[3],
				SecondaryAttribute = Constants.DFKStats[4],
				TertiaryAttribute = Constants.DFKStats[0],
			},
			new()
			{
				HeroClass = "thief",
				MainAttribute = Constants.DFKStats[2],
				SecondaryAttribute = Constants.DFKStats[7],
				TertiaryAttribute = Constants.DFKStats[1],
			},
			new()
			{
				HeroClass = "archer",
				MainAttribute = Constants.DFKStats[1],
				SecondaryAttribute = Constants.DFKStats[4],
				TertiaryAttribute = Constants.DFKStats[0],
			},
			new()
			{
				HeroClass = "pirate",
				MainAttribute = Constants.DFKStats[0],
				SecondaryAttribute = Constants.DFKStats[1],
				TertiaryAttribute = Constants.DFKStats[3],
			},
			new()
			{
				HeroClass = "monk",
				MainAttribute = Constants.DFKStats[0],
				SecondaryAttribute = Constants.DFKStats[2],
				TertiaryAttribute = Constants.DFKStats[3],
			},
			new()
			{
				HeroClass = "priest",
				MainAttribute = Constants.DFKStats[6],
				SecondaryAttribute = Constants.DFKStats[5],
				TertiaryAttribute = Constants.DFKStats[4],
			},
			new()
			{
				HeroClass = "wizard",
				MainAttribute = Constants.DFKStats[5],
				SecondaryAttribute = Constants.DFKStats[6],
				TertiaryAttribute = Constants.DFKStats[3],
			},
            new()
            {
                HeroClass = "seer",
                MainAttribute = Constants.DFKStats[6],
                SecondaryAttribute = Constants.DFKStats[5],
                TertiaryAttribute = Constants.DFKStats[2],
            },
            new()
            {
                HeroClass = "berserker",
                MainAttribute = Constants.DFKStats[0],
                SecondaryAttribute = Constants.DFKStats[3],
                TertiaryAttribute = Constants.DFKStats[1],
            },
            new()
            {
                HeroClass = "legionnaire",
                MainAttribute = Constants.DFKStats[4],
                SecondaryAttribute = Constants.DFKStats[0],
                TertiaryAttribute = Constants.DFKStats[3],
            },
            new()
            {
                HeroClass = "scholar",
                MainAttribute = Constants.DFKStats[5],
                SecondaryAttribute = Constants.DFKStats[6],
                TertiaryAttribute = Constants.DFKStats[3],
            },
            new()
			{
				HeroClass = "paladin",
				MainAttribute = Constants.DFKStats[0],
				SecondaryAttribute = Constants.DFKStats[3],
				TertiaryAttribute = Constants.DFKStats[4],
			},
			new()
			{
				HeroClass = "darkknight",
				MainAttribute = Constants.DFKStats[0],
				SecondaryAttribute = Constants.DFKStats[3],
				TertiaryAttribute = Constants.DFKStats[5],
			},
			new()
			{
				HeroClass = "ninja",
				MainAttribute = Constants.DFKStats[2],
				SecondaryAttribute = Constants.DFKStats[1],
				TertiaryAttribute = Constants.DFKStats[7],
			},
			new()
			{
				HeroClass = "summoner",
				MainAttribute = Constants.DFKStats[5],
				SecondaryAttribute = Constants.DFKStats[6],
				TertiaryAttribute = Constants.DFKStats[3],
			},
            new()
            {
                HeroClass = "shapeshifter",
                MainAttribute = Constants.DFKStats[2],
                SecondaryAttribute = Constants.DFKStats[1],
                TertiaryAttribute = Constants.DFKStats[3],
            },
            new()
            {
                HeroClass = "bard",
                MainAttribute = Constants.DFKStats[1],
                SecondaryAttribute = Constants.DFKStats[2],
                TertiaryAttribute = Constants.DFKStats[7],
            },
            new()
			{
				HeroClass = "dragoon",
				MainAttribute = Constants.DFKStats[0],
				SecondaryAttribute = Constants.DFKStats[1],
				TertiaryAttribute = Constants.DFKStats[2],
			},
            new()
            {
                HeroClass = "sage",
                MainAttribute = Constants.DFKStats[0],
                SecondaryAttribute = Constants.DFKStats[4],
                TertiaryAttribute = Constants.DFKStats[1],
            },
            new()
            {
                HeroClass = "spellbow",
                MainAttribute = Constants.DFKStats[1],
                SecondaryAttribute = Constants.DFKStats[5],
                TertiaryAttribute = Constants.DFKStats[7],
            },
            new()
			{
				HeroClass = "dreadknight",
				MainAttribute = Constants.DFKStats[0],
				SecondaryAttribute = Constants.DFKStats[1],
				TertiaryAttribute = Constants.DFKStats[4],
			},
		};
        public List<LevelUpSetting> SuggestedLevelUpSettings { get; set; } = new()
        {
            new()
            {
                HeroClass = "warrior",
                MainAttribute = Constants.DFKStats[0],
                SecondaryAttribute = Constants.DFKStats[1],
                TertiaryAttribute = Constants.DFKStats[3],
            },
            new()
            {
                HeroClass = "knight",
                MainAttribute = Constants.DFKStats[3],
                SecondaryAttribute = Constants.DFKStats[4],
                TertiaryAttribute = Constants.DFKStats[0],
            },
            new()
            {
                HeroClass = "thief",
                MainAttribute = Constants.DFKStats[2],
                SecondaryAttribute = Constants.DFKStats[7],
                TertiaryAttribute = Constants.DFKStats[1],
            },
            new()
            {
                HeroClass = "archer",
                MainAttribute = Constants.DFKStats[1],
                SecondaryAttribute = Constants.DFKStats[4],
                TertiaryAttribute = Constants.DFKStats[0],
            },
            new()
            {
                HeroClass = "pirate",
                MainAttribute = Constants.DFKStats[0],
                SecondaryAttribute = Constants.DFKStats[1],
                TertiaryAttribute = Constants.DFKStats[3],
            },
            new()
            {
                HeroClass = "monk",
                MainAttribute = Constants.DFKStats[0],
                SecondaryAttribute = Constants.DFKStats[2],
                TertiaryAttribute = Constants.DFKStats[3],
            },
            new()
            {
                HeroClass = "priest",
                MainAttribute = Constants.DFKStats[6],
                SecondaryAttribute = Constants.DFKStats[5],
                TertiaryAttribute = Constants.DFKStats[4],
            },
            new()
            {
                HeroClass = "wizard",
                MainAttribute = Constants.DFKStats[5],
                SecondaryAttribute = Constants.DFKStats[6],
                TertiaryAttribute = Constants.DFKStats[3],
            },
            new()
            {
                HeroClass = "seer",
                MainAttribute = Constants.DFKStats[6],
                SecondaryAttribute = Constants.DFKStats[5],
                TertiaryAttribute = Constants.DFKStats[2],
            },
            new()
            {
                HeroClass = "berserker",
                MainAttribute = Constants.DFKStats[0],
                SecondaryAttribute = Constants.DFKStats[3],
                TertiaryAttribute = Constants.DFKStats[1],
            },
            new()
            {
                HeroClass = "legionnaire",
                MainAttribute = Constants.DFKStats[4],
                SecondaryAttribute = Constants.DFKStats[0],
                TertiaryAttribute = Constants.DFKStats[3],
            },
            new()
            {
                HeroClass = "scholar",
                MainAttribute = Constants.DFKStats[5],
                SecondaryAttribute = Constants.DFKStats[6],
                TertiaryAttribute = Constants.DFKStats[3],
            },
            new()
            {
                HeroClass = "paladin",
                MainAttribute = Constants.DFKStats[0],
                SecondaryAttribute = Constants.DFKStats[3],
                TertiaryAttribute = Constants.DFKStats[4],
            },
            new()
            {
                HeroClass = "darkknight",
                MainAttribute = Constants.DFKStats[0],
                SecondaryAttribute = Constants.DFKStats[3],
                TertiaryAttribute = Constants.DFKStats[5],
            },
            new()
            {
                HeroClass = "ninja",
                MainAttribute = Constants.DFKStats[2],
                SecondaryAttribute = Constants.DFKStats[1],
                TertiaryAttribute = Constants.DFKStats[7],
            },
            new()
            {
                HeroClass = "summoner",
                MainAttribute = Constants.DFKStats[5],
                SecondaryAttribute = Constants.DFKStats[6],
                TertiaryAttribute = Constants.DFKStats[3],
            },
            new()
            {
                HeroClass = "shapeshifter",
                MainAttribute = Constants.DFKStats[2],
                SecondaryAttribute = Constants.DFKStats[1],
                TertiaryAttribute = Constants.DFKStats[3],
            },
            new()
            {
                HeroClass = "bard",
                MainAttribute = Constants.DFKStats[1],
                SecondaryAttribute = Constants.DFKStats[2],
                TertiaryAttribute = Constants.DFKStats[7],
            },
            new()
            {
                HeroClass = "dragoon",
                MainAttribute = Constants.DFKStats[0],
                SecondaryAttribute = Constants.DFKStats[1],
                TertiaryAttribute = Constants.DFKStats[2],
            },
            new()
            {
                HeroClass = "sage",
                MainAttribute = Constants.DFKStats[0],
                SecondaryAttribute = Constants.DFKStats[4],
                TertiaryAttribute = Constants.DFKStats[1],
            },
            new()
            {
                HeroClass = "spellbow",
                MainAttribute = Constants.DFKStats[1],
                SecondaryAttribute = Constants.DFKStats[5],
                TertiaryAttribute = Constants.DFKStats[7],
            },
            new()
            {
                HeroClass = "dreadknight",
                MainAttribute = Constants.DFKStats[0],
                SecondaryAttribute = Constants.DFKStats[1],
                TertiaryAttribute = Constants.DFKStats[4],
            },
        };
	}
}