﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reactor.API.Configuration;
using System;
using UnityEngine;

namespace Mod.GroupCenterpointMover
{
	public class ConfigurationLogic : MonoBehaviour
	{
		#region Properties
		public bool gcpmm
		{
			get => Get<bool>("gcpmm");
			set => Set("gcpmm", value);
		}
		#endregion

		internal Settings Config;

		public event Action<ConfigurationLogic> OnChanged;

		private void Load()
		{
			Config = new Settings("Config");
		}

		public void Awake()
		{
			Load();
			Get("gcpmm", true);
			Save();
		}

		public T Get<T>(string key, T @default = default)
		{
			return Config.GetOrCreate(key, @default);
		}

		public void Set<T>(string key, T value)
		{
			Config[key] = value;
			Save();
		}

		public void Save()
		{
			Config?.Save();
			OnChanged?.Invoke(this);
		}
	}
}