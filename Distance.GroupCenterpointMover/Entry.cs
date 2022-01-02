using Centrifuge.Distance.Game;
using Centrifuge.Distance.GUI.Controls;
using Centrifuge.Distance.GUI.Data;
using Events.MainMenu;
using Events.QuitLevelEditor;
using Reactor.API.Attributes;
using Reactor.API.Interfaces.Systems;
using Reactor.API.Logging;
using Reactor.API.Runtime.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Mod.GroupCenterpointMover
{
    [ModEntryPoint("com.github.PredatoryBalloon/Distance.GroupCenterpointMover")]
    public class Mod : MonoBehaviour
    {
        public static Mod Instance;

        public IManager Manager { get; set; }

        public static Log Logger { get; private set; }

        public static ConfigurationLogic Config { get; private set; }

        public static bool ModEnabled { get; set; }

        public void Initialize(IManager manager)
        {
            Instance = this;
            Logger = LogManager.GetForCurrentAssembly();
            Manager = manager;
            Config = gameObject.AddComponent<ConfigurationLogic>();
            //Events.MainMenu.Initialized.Subscribe(OnMainMenuInitialized);
            //Events.QuitLevelEditor.Quit.Subscribe(OnMainMenuInitialized2);
            //Events.MainMenu.Initialized.Unsubscribe(OnMainMenuInitialized);
            //Events.MainMenu.Initialized.Broadcast(new Initialized.Data());
            CreateSettingsMenu();
            //GameObject lamp = new GameObject();
            //Type lamptrans = System.Type.GetType("Transform");
            //lamp.AddComponent(lamptrans);
            RuntimePatcher.AutoPatch();
        }

        public void CreateSettingsMenu()
	    {
            MenuTree settingsMenu;
            settingsMenu = new MenuTree("menu.mod.GroupCenterpointMover", "GroupCenterpointMover Settings")
            {
            new CheckBox(MenuDisplayMode.MainMenu, "setting:enable_disable_gcpmmod", "ENABLE GroupCenterpointMover MOD")
            .WithGetter(() => Config.gcpmm)
            .WithSetter((x) => Config.gcpmm = x)
            .WithDescription("Enables the mod.")
            };

            Menus.AddNew(MenuDisplayMode.Both, settingsMenu, "GroupCenterpointMover", "Settings for the GroupCenterpointMover mod.");
        }
        public static bool GCPMM => Config.gcpmm;

        private void OnMainMenuInitialized(Initialized.Data data)
        {
            Resource.CreateLevelEditorPrefabDirInfo();
        }

        private void OnMainMenuInitialized2(Quit.Data data)
        {
            Resource.CreateResourceList();
        }
    }
}