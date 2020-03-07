using MSCLoader;
using UnityEngine;
using ModApi.Attachable;
using ModsShop;
using HutongGames.PlayMaker;
using System;

namespace SuperchargerForSatsuma
{

    public class SuperchargerForSatsuma : Mod
    {
        public override string ID => "SuperchargerForSatsuma";
        public override string Name => "Supercharger For Satsuma";
        public override string Author => "Spysi";
        public override string Version => "1.4.2";
        private static GameObject pulley, belt, pipe_rac_carb, pipe, turbine, turbinegauge, satsuma, pullmesh, pipe_2_carb, raccarb, carb2, switch_button, headgasket, filter, engine_head;
        private static Turbine turbinePart;
        private static Belt beltPart;
        private static Switchbutton switch_buttonPart;
        private static Pulley pulleyPart;
        private static Pipe pipePart;
        private static Pipe_rac_carb pipe_rac_carbPart;
        private static Turbinegauge turbinegaugePart;
        private static Pipe_2_carb pipe_2_carbPart;
        private static HeadGasket headgasketPart;
        private static Filter filterPart;
        public float time;
        private int troble = 0;
        public override bool UseAssetsFolder => true;
        private bool racingcarb_inst = false, carb2_inst = false, engine_head_inst = false, turbine_Broken = false, filter_Broken = false;
        private Trigger pipe_2_carbtrigger, pipe_rac_carbtrigger, belttrigger, headgaskettrigger;
        static bool fullreset = false;
        private float rpm, powermp, retard, mixture;
        private ShopItem shop;
        private ProductDetails shopbelt, shop_turbine, shop_filter;
        private Safeinfo buysafeinfo = null;
        private FsmFloat powerMP, n2oPSI, sparkRetard, math1, Mixture, heat;
        private FsmState n2o, noN2O;
        private FsmString inVencle;
        private Drivetrain drivetrain;
        public override void OnNewGame()

        {
            SaveLoad.SerializeSaveFile<PartSaveInfo>(this, null, "pulleySaveInfo");
            SaveLoad.SerializeSaveFile<PartSaveInfo>(this, null, "beltSaveInfo");
            SaveLoad.SerializeSaveFile<PartSaveInfo>(this, null, "turbineSaveInfo");
            SaveLoad.SerializeSaveFile<PartSaveInfo>(this, null, "pipeSaveInfo");
            SaveLoad.SerializeSaveFile<PartSaveInfo>(this, null, "pipe_rac_carbSaveInfo");
            SaveLoad.SerializeSaveFile<PartSaveInfo>(this, null, "turbinegaugeSaveInfo");
            SaveLoad.SerializeSaveFile<PartSaveInfo>(this, null, "pipe_2_carbSaveInfo");
            SaveLoad.SerializeSaveFile<PartSaveInfo>(this, null, "switch_buttonSaveInfo");
            SaveLoad.SerializeSaveFile<PartSaveInfo>(this, null, "headgasketSaveInfo");
            SaveLoad.SerializeSaveFile<PartSaveInfo>(this, null, "filterSaveInfo");
            SaveLoad.SerializeSaveFile<Safeinfo>(this, null, "Safeinfo");
            ModConsole.Print("Supercharger for Satsuma was resetting.");
        }

        private PartSaveInfo LoadSaveData(string name)
        {
            try
            {
                return SaveLoad.DeserializeSaveFile<PartSaveInfo>(this, name);
            }
            catch (System.NullReferenceException)
            {
                return null;
            }
        }

        public override void OnLoad()
        {
            satsuma = GameObject.Find("SATSUMA(557kg, 248)");
            raccarb = GameObject.Find("racing carburators(Clone)");
            carb2 = GameObject.Find("twin carburators(Clone)");
            powerMP = FsmVariables.GlobalVariables.FindFsmFloat("EnginePowerMultiplier");
            pullmesh = GameObject.Find("crankshaft_pulley_mesh");
            inVencle = FsmVariables.GlobalVariables.FindFsmString("PlayerCurrentVehicle");
            engine_head = GameObject.Find("cylinder head(Clone)");
            Mixture = satsuma.transform.GetChild(14).GetChild(1).GetChild(3).gameObject.GetComponents<PlayMakerFSM>()[1].FsmVariables.FloatVariables[16];
            math1 = satsuma.transform.GetChild(14).GetChild(1).GetChild(7).gameObject.GetComponent<PlayMakerFSM>().FsmVariables.FloatVariables[1];
            n2o = satsuma.transform.GetChild(14).GetChild(1).GetChild(7).gameObject.GetComponent<PlayMakerFSM>().FsmStates[4];
            n2oPSI = satsuma.transform.GetChild(14).GetChild(1).GetChild(7).gameObject.GetComponent<PlayMakerFSM>().FsmVariables.FloatVariables[4];
            drivetrain = satsuma.GetComponent<Drivetrain>();
            sparkRetard = satsuma.transform.GetChild(14).GetChild(1).GetChild(7).gameObject.GetComponent<PlayMakerFSM>().FsmVariables.FloatVariables[7];
            noN2O = satsuma.transform.GetChild(14).GetChild(1).GetChild(7).gameObject.GetComponent<PlayMakerFSM>().FsmStates[5];
            heat = FsmVariables.GlobalVariables.FindFsmFloat("EngineTemp");

            AssetBundle ab = LoadAssets.LoadBundle(this, "turbo.unity3d");
            turbine = ab.LoadAsset("Turbine.prefab") as GameObject;
            turbine.name = "Turbine";
            turbine.tag = "PART";
            turbine.layer = LayerMask.NameToLayer("Parts");

            pulley = ab.LoadAsset("Pulley.prefab") as GameObject;
            pulley.name = "Pulley";
            pulley.tag = "PART";
            pulley.layer = LayerMask.NameToLayer("Parts");

            pipe = ab.LoadAsset("Pipe.prefab") as GameObject;
            pipe.name = "Pipe";
            pipe.tag = "PART";
            pipe.layer = LayerMask.NameToLayer("Parts");

            pipe_rac_carb = ab.LoadAsset("Pipe rac_carb.prefab") as GameObject;
            pipe_rac_carb.name = "racing carburators pipe";
            pipe_rac_carb.tag = "PART";
            pipe_rac_carb.layer = LayerMask.NameToLayer("Parts");

            belt = ab.LoadAsset("Belt.prefab") as GameObject;
            belt.name = "Turbine belt";
            belt.tag = "PART";
            belt.layer = LayerMask.NameToLayer("Parts");

            turbinegauge = ab.LoadAsset("Датчик.prefab") as GameObject;
            turbinegauge.name = "Turbine gauge";
            turbinegauge.tag = "PART";
            turbinegauge.layer = LayerMask.NameToLayer("Parts");

            pipe_2_carb = ab.LoadAsset("Pipe 2_carb.prefab") as GameObject;
            pipe_2_carb.name = "Twin carburators pipe";
            pipe_2_carb.tag = "PART";
            pipe_2_carb.layer = LayerMask.NameToLayer("Parts");

            switch_button = ab.LoadAsset("Switch.prefab") as GameObject;
            switch_button.name = "Switch button";
            switch_button.tag = "PART";
            switch_button.layer = LayerMask.NameToLayer("Parts");

            headgasket = ab.LoadAsset("Head_gasket.prefab") as GameObject;
            headgasket.name = "Additional Head Gasket";
            headgasket.tag = "PART";
            headgasket.layer = LayerMask.NameToLayer("Parts");

            filter = ab.LoadAsset("filter.prefab") as GameObject;
            filter.name = "filter";
            filter.tag = "PART";
            filter.layer = LayerMask.NameToLayer("Parts");

            PartSaveInfo pulleySaveInfo = null, turbineSaveInfo = null, pipeSaveInfo = null, pipe_rac_carbSaveInfo = null, beltSaveInfo = null, turbinegaugeSaveInfo = null, pipe_2_carbSaveInfo = null, switch_buttonSaveInfo = null, filterSaveInfo = null, headgasketSaveInfo = null;
            pulleySaveInfo = LoadSaveData("pulleySaveInfo");
            turbineSaveInfo = LoadSaveData("turbineSaveInfo");
            pipeSaveInfo = LoadSaveData("pipeSaveInfo");
            pipe_rac_carbSaveInfo = LoadSaveData("pipe_rac_carbSaveInfo");
            beltSaveInfo = LoadSaveData("beltSaveInfo");
            turbinegaugeSaveInfo = LoadSaveData("turbinegaugeSaveInfo");
            pipe_2_carbSaveInfo = LoadSaveData("pipe_2_carbSaveInfo");
            switch_buttonSaveInfo = LoadSaveData("switch_buttonSaveInfo");
            filterSaveInfo = LoadSaveData("filterSaveInfo");
            headgasketSaveInfo = LoadSaveData("headgasketSaveInfo");
            try
            {
                buysafeinfo = SaveLoad.DeserializeSaveFile<Safeinfo>(this, "Safeinfo");
            }
            catch
            {
            }
            if (buysafeinfo == null)
            {
                buysafeinfo = new Safeinfo()
                {
                    Buybelt = false,
                    Buypipe = false,
                    Buypipe_2_carb = false,
                    Buypipe_rac_carb = false,
                    Buypulley = false,
                    Buyturbine = false,
                    Buyturbinegauge = false,
                    Buyswitch_button = false,
                    Buyfilter = false,
                    Buyheadgasket = false,
                    Beltwear = 100,
                    Filterwear = 100,
                    Turbinewear = 100,
                    BoostOn = true
                };
            }
            if (!buysafeinfo.Buyturbine) turbineSaveInfo = null;
            if (!buysafeinfo.Buybelt) beltSaveInfo = null;
            if (!buysafeinfo.Buypulley) pulleySaveInfo = null;
            if (!buysafeinfo.Buypipe) pipeSaveInfo = null;
            if (!buysafeinfo.Buypipe_rac_carb) pipe_rac_carbSaveInfo = null;
            if (!buysafeinfo.Buypipe_2_carb) pipe_2_carbSaveInfo = null;
            if (!buysafeinfo.Buyturbinegauge) turbinegaugeSaveInfo = null;
            if (!buysafeinfo.Buyswitch_button) switch_buttonSaveInfo = null;
            if (!buysafeinfo.Buyfilter) filterSaveInfo = null;
            if (!buysafeinfo.Buyheadgasket) headgasketSaveInfo = null;

            GameObject pulleyparent = GameObject.Find("crankshaft_pulley_mesh");
            Trigger pulleytrigger = new Trigger("pulleyTrigger", pulleyparent, new Vector3(0.027f, 0.0f, 0.0f), Quaternion.Euler(0, 0, 0), new Vector3(0.1f, 0.1f, 0.1f), false);
            pulleyPart = new Pulley(pulleySaveInfo, pulleytrigger, new Vector3(0.027f, 0.0f, 0.0f), new Quaternion(0, 0, 0, 0), pulley, pulleyparent);

            GameObject turbineparent = GameObject.Find("block(Clone)");
            Trigger turbinetrigger = new Trigger("turbineTrigger", turbineparent, new Vector3(0.234f, 0.14f, 0.0817f), Quaternion.Euler(0, 0, 0), new Vector3(0.1f, 0.1f, 0.1f), false);
            turbinePart = new Turbine(turbineSaveInfo, turbinetrigger, new Vector3(0.234f, 0.14f, 0.0817f), Quaternion.Euler(90, 0, 0), turbine, turbineparent);

            GameObject pipeparent = turbinePart.rigidPart;
            Trigger pipetrigger = new Trigger("pipeTrigger", pipeparent, new Vector3(-0.12f, 0.085f, -0.04f), Quaternion.Euler(0, 0, 0), new Vector3(0.1f, 0.1f, 0.1f), false);
            pipePart = new Pipe(pipeSaveInfo, pipetrigger, new Vector3(-0.12f, 0.085f, -0.04f), new Quaternion(0, 0, 0, 0), pipe, pipeparent);

            GameObject pipe_rac_carbparent = GameObject.Find("cylinder head(Clone)");
            pipe_rac_carbtrigger = new Trigger("pipe_rac_carbTrigger", pipe_rac_carbparent, new Vector3(0.013f, -0.139f, 0.12f), Quaternion.Euler(90, 0, 0), new Vector3(0.3f, 0.3f, 0.3f), false);
            pipe_rac_carbPart = new Pipe_rac_carb(pipe_rac_carbSaveInfo, pipe_rac_carbtrigger, new Vector3(0.013f, -0.139f, 0.12f), Quaternion.Euler(90, 0, 0), pipe_rac_carb, pipe_rac_carbparent);

            GameObject beltparent = turbinePart.rigidPart;
            belttrigger = new Trigger("beltTrigger", beltparent, new Vector3(0.035f, -0.091f, 0.08f), Quaternion.Euler(0, 0, 0), new Vector3(0.1f, 0.1f, 0.1f), false);
            beltPart = new Belt(beltSaveInfo, belttrigger, new Vector3(0.035f, -0.091f, 0.08f), new Quaternion(0, 0, 0, 0), belt, beltparent);

            GameObject turbinegaugeParent = GameObject.Find("dashboard(Clone)");
            Trigger turbinegaugeTrigger = new Trigger("turbinegaugeTriger", turbinegaugeParent, new Vector3(0.48f, -0.04f, 0.135f), Quaternion.Euler(0, 0, 0), new Vector3(0.1f, 0.1f, 0.1f), false);
            turbinegaugePart = new Turbinegauge(turbinegaugeSaveInfo, turbinegaugeTrigger, new Vector3(0.48f, -0.04f, 0.135f), Quaternion.Euler(0, 0, 345), turbinegauge, turbinegaugeParent);

            GameObject pipe_2_carbparent = GameObject.Find("cylinder head(Clone)");
            pipe_2_carbtrigger = new Trigger("pipe_2_carbTrigger", pipe_rac_carbparent, new Vector3(0.06f, -0.147f, 0.04f), Quaternion.Euler(0, 0, 0), new Vector3(0.3f, 0.3f, 0.3f), false);
            pipe_2_carbPart = new Pipe_2_carb(pipe_2_carbSaveInfo, pipe_2_carbtrigger, new Vector3(0.06f, -0.147f, 0.04f), Quaternion.Euler(90, 0, 0), pipe_2_carb, pipe_2_carbparent);

            GameObject switchbuttonparent = GameObject.Find("dashboard(Clone)");
            Trigger switchbuttontrigger = new Trigger("switchbuttonTrigger", switchbuttonparent, new Vector3(0.54f, -0.062f, -0.065f), Quaternion.Euler(0, 0, 0), new Vector3(0.1f, 0.1f, 0.1f), false);
            switch_buttonPart = new Switchbutton(switch_buttonSaveInfo, switchbuttontrigger, new Vector3(0.54f, -0.062f, -0.065f), Quaternion.Euler(12, 0, 0), switch_button, switchbuttonparent);

            GameObject filterparent = pipePart.rigidPart;
            Trigger filtertrigger = new Trigger("filterTrigger", filterparent, new Vector3(-0.14f, 0.075f, -0.035f), Quaternion.Euler(0, 0, 0), new Vector3(0.1f, 0.1f, 0.1f), false);
            filterPart = new Filter(filterSaveInfo, filtertrigger, new Vector3(-0.14f, 0.075f, -0.035f), Quaternion.Euler(0, 0, 0), filter, filterparent);

            GameObject headgasketparent = GameObject.Find("head gasket(Clone)");
            headgaskettrigger = new Trigger("headgasketTrigger", headgasketparent, new Vector3(0f, 0f, 0.003f), Quaternion.Euler(0, 0, 0), new Vector3(0.1f, 0.1f, 0.1f), false);
            headgasketPart = new HeadGasket(headgasketSaveInfo, headgaskettrigger, new Vector3(0f, 0f, 0.003f), Quaternion.Euler(0, 0, 0), headgasket, headgasketparent);

            shop = GameObject.Find("Shop for mods").GetComponent<ShopItem>();
            shop_turbine = new ProductDetails()
            {
                productName = "Centrifugal supercharger",
                multiplePurchases = false,
                productCategory = "Details",
                productIcon = ab.LoadAsset<Sprite>("Turbine_ico.png"),
                productPrice = 3499
            };
            if (!buysafeinfo.Buyturbine)
            {
                shop.Add(this, shop_turbine, ShopType.Fleetari, Buy_turbine, turbinePart.activePart);
                turbinePart.activePart.SetActive(false);
            }


            shopbelt = new ProductDetails()
            {
                productName = "Belt",
                multiplePurchases = false,
                productCategory = "Details",
                productIcon = ab.LoadAsset<Sprite>("Belt.png"),
                productPrice = 299
            };
            if (!buysafeinfo.Buybelt)
            {
                shop.Add(this, shopbelt, ShopType.Fleetari, Buy_belt, beltPart.activePart);
                beltPart.activePart.SetActive(false);
            }


            if (!buysafeinfo.Buypulley)
            {
                ProductDetails shop_pulley = new ProductDetails()
                {
                    productName = "Pulley",
                    multiplePurchases = false,
                    productCategory = "Details",
                    productIcon = ab.LoadAsset<Sprite>("pulley.png"),
                    productPrice = 399
                };
                shop.Add(this, shop_pulley, ShopType.Fleetari, Buy_pulley, pulleyPart.activePart);
                pulleyPart.activePart.SetActive(false);
            }


            if (!buysafeinfo.Buypipe)
            {
                ProductDetails shop_pipe = new ProductDetails()
                {
                    productName = "Pipe",
                    multiplePurchases = false,
                    productCategory = "Details",
                    productIcon = ab.LoadAsset<Sprite>("pipe.png"),
                    productPrice = 749
                };
                shop.Add(this, shop_pipe, ShopType.Fleetari, Buy_pipe, pipePart.activePart);
                pipePart.activePart.SetActive(false);
            }


            if (!buysafeinfo.Buypipe_rac_carb)
            {
                ProductDetails shop_pipe_rac_carb = new ProductDetails()
                {
                    productName = "Racing carburators pipe",
                    multiplePurchases = false,
                    productCategory = "Details",
                    productIcon = ab.LoadAsset<Sprite>("pipe2.png"),
                    productPrice = 749
                };
                shop.Add(this, shop_pipe_rac_carb, ShopType.Fleetari, Buy_pipe_rac_carb, pipe_rac_carbPart.activePart);
                pipe_rac_carbPart.activePart.SetActive(false);
            }


            if (!buysafeinfo.Buypipe_2_carb)
            {
                ProductDetails shop_pipe_2_carb = new ProductDetails()
                {
                    productName = "Twin carburators pipe",
                    multiplePurchases = false,
                    productCategory = "Details",
                    productIcon = ab.LoadAsset<Sprite>("pipe3.png"),
                    productPrice = 749
                };
                shop.Add(this, shop_pipe_2_carb, ShopType.Fleetari, Buy_pipe_2_carb, pipe_2_carbPart.activePart);
                pipe_2_carbPart.activePart.SetActive(false);
            }


            if (!buysafeinfo.Buyturbinegauge)
            {
                ProductDetails shop_turbinegauge = new ProductDetails()
                {
                    productName = "Turbine gauge",
                    multiplePurchases = false,
                    productCategory = "Details",
                    productIcon = ab.LoadAsset<Sprite>("Гаджет.png"),
                    productPrice = 499
                };
                shop.Add(this, shop_turbinegauge, ShopType.Fleetari, Buy_turbinegauge, turbinegaugePart.activePart);
                turbinegaugePart.activePart.SetActive(false);
            }


            if (!buysafeinfo.Buyswitch_button)
            {
                ProductDetails shop_switch_button = new ProductDetails()
                {
                    productName = "Switch button",
                    multiplePurchases = false,
                    productCategory = "Details",
                    productIcon = ab.LoadAsset<Sprite>("switch 1.png"),
                    productPrice = 249
                };
                shop.Add(this, shop_switch_button, ShopType.Fleetari, Buy_switch_button, switch_buttonPart.activePart);
                switch_buttonPart.activePart.SetActive(false);
            }

            shop_filter = new ProductDetails()
            {
                productName = "Filter",
                multiplePurchases = false,
                productCategory = "Details",
                productIcon = ab.LoadAsset<Sprite>("filter.png"),
                productPrice = 99
            };
            if (!buysafeinfo.Buyfilter)
            {
                shop.Add(this, shop_filter, ShopType.Fleetari, Buy_filter, filterPart.activePart);
                filterPart.activePart.SetActive(false);
            }

            if (!buysafeinfo.Buyheadgasket)
            {
                ProductDetails shop_headgasket = new ProductDetails()
                {
                    productName = "Additional Head Gasket",
                    multiplePurchases = false,
                    productCategory = "Details",
                    productIcon = ab.LoadAsset<Sprite>("headgasket.png"),
                    productPrice = 329
                };
                shop.Add(this, shop_headgasket, ShopType.Fleetari, Buy_headgasket, headgasketPart.activePart);
                headgasketPart.activePart.SetActive(false);
            }


            ab.Unload(false);

            UnityEngine.Object.Destroy(turbine);
            UnityEngine.Object.Destroy(pulley);
            UnityEngine.Object.Destroy(pipe);
            UnityEngine.Object.Destroy(pipe_rac_carb);
            UnityEngine.Object.Destroy(belt);
            UnityEngine.Object.Destroy(turbinegauge);
            UnityEngine.Object.Destroy(ab);
            UnityEngine.Object.Destroy(pipe_2_carb);
            UnityEngine.Object.Destroy(switch_button);
            turbinePart.rigidPart.GetComponent<AudioSource>().volume = 0;

            racingcarb_inst = false;
            carb2_inst = false;

            fullreset = false;

            if (buysafeinfo.BoostOn)
            {
                switch_buttonPart.rigidPart.transform.GetChild(0).transform.localRotation = Quaternion.Euler(50, 0, 0);
            }
            else
            {
                switch_buttonPart.rigidPart.transform.GetChild(0).transform.localRotation = Quaternion.Euler(-50, 0, 0);
            }

            ModConsole.Print("Supercharger for Satsuma was loaded.");

        }
        private Settings reset_mod = new Settings("button", "Fully reset mod", Fullreset);
        private Settings resetpos_mod = new Settings("button", "Reset parts positions", Posreset);
        private Keybind keyBoostOn = new Keybind("boostOn", "key for on and off boost", KeyCode.Y);
        public override void ModSettings()
        {
            Settings.AddButton(this, resetpos_mod, "You can find them near the Fleetari repair shop.");
            Settings.AddButton(this, reset_mod, "Changes will occur after restarting the save");
            Keybind.Add(this, keyBoostOn);
        }

        public static void Fullreset()
        {
            fullreset = true;
        }
        public static void Posreset()
        {
            if (!turbinePart.installed) turbinePart.activePart.transform.position = turbinePart.defaultPartSaveInfo.position;
            if (!beltPart.installed) beltPart.activePart.transform.position = beltPart.defaultPartSaveInfo.position;
            if (!pulleyPart.installed) pulleyPart.activePart.transform.position = pulleyPart.defaultPartSaveInfo.position;
            if (!pipePart.installed) pipePart.activePart.transform.position = pipePart.defaultPartSaveInfo.position;
            if (!pipe_rac_carbPart.installed) pipe_rac_carbPart.activePart.transform.position = pipe_rac_carbPart.defaultPartSaveInfo.position;
            if (!pipe_2_carbPart.installed) pipe_2_carbPart.activePart.transform.position = pipe_2_carbPart.defaultPartSaveInfo.position;
            if (!turbinegaugePart.installed) turbinegaugePart.activePart.transform.position = turbinegaugePart.defaultPartSaveInfo.position;
            if (!switch_buttonPart.installed) switch_buttonPart.activePart.transform.position = switch_buttonPart.defaultPartSaveInfo.position;
            if (!headgasketPart.installed) headgasketPart.activePart.transform.position = headgasketPart.defaultPartSaveInfo.position;
            if (!filterPart.installed) filterPart.activePart.transform.position = filterPart.defaultPartSaveInfo.position;
        }

        public override void OnSave()
        {
            SaveLoad.SerializeSaveFile(this, pulleyPart.getPartSaveInfo, "pulleySaveInfo");
            SaveLoad.SerializeSaveFile(this, beltPart.getPartSaveInfo, "beltSaveInfo");
            SaveLoad.SerializeSaveFile(this, turbinePart.getPartSaveInfo, "turbineSaveInfo");
            SaveLoad.SerializeSaveFile(this, pipePart.getPartSaveInfo, "pipeSaveInfo");
            SaveLoad.SerializeSaveFile(this, pipe_rac_carbPart.getPartSaveInfo, "pipe_rac_carbSaveInfo");
            SaveLoad.SerializeSaveFile(this, turbinegaugePart.getPartSaveInfo, "turbinegaugeSaveInfo");
            SaveLoad.SerializeSaveFile(this, pipe_2_carbPart.getPartSaveInfo, "pipe_2_carbSaveInfo");
            SaveLoad.SerializeSaveFile(this, switch_buttonPart.getPartSaveInfo, "switch_buttonSaveInfo");
            SaveLoad.SerializeSaveFile(this, headgasketPart.getPartSaveInfo, "headgasketSaveInfo");
            SaveLoad.SerializeSaveFile(this, filterPart.getPartSaveInfo, "filterSaveInfo");
            if (!fullreset) SaveLoad.SerializeSaveFile(this, buysafeinfo, "Safeinfo");
            else SaveLoad.SerializeSaveFile<Safeinfo>(this, null, "Safeinfo");
        }

        public override void Update()
        {
            if (Camera.main != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit[] hits = Physics.RaycastAll(ray, 1f);
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider == switch_buttonPart.rigidPart.transform.GetChild(0).GetComponent<Collider>())
                    {
                        PlayMakerGlobals.Instance.Variables.FindFsmBool("GUIuse").Value = true;
                        PlayMakerGlobals.Instance.Variables.FindFsmString("GUIinteraction").Value = "Boost On/Off";
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (buysafeinfo.BoostOn)
                            {
                                buysafeinfo.BoostOn = false;
                                switch_buttonPart.rigidPart.transform.GetChild(0).transform.localRotation = Quaternion.Euler(-50, 0, 0);
                                switch_buttonPart.rigidPart.GetComponent<AudioSource>().Play();
                            }
                            else
                            {
                                buysafeinfo.BoostOn = true;
                                switch_buttonPart.rigidPart.transform.GetChild(0).transform.localRotation = Quaternion.Euler(50, 0, 0);
                                switch_buttonPart.rigidPart.GetComponent<AudioSource>().Play();
                            }
                        }
                    }
                }
            }


            if (keyBoostOn.IsDown() && switch_buttonPart.installed && inVencle.Value == "Satsuma")
            {

                if (buysafeinfo.BoostOn)
                {
                    buysafeinfo.BoostOn = false;
                    switch_buttonPart.rigidPart.transform.GetChild(0).transform.localRotation = Quaternion.Euler(-50, 0, 0);
                    switch_buttonPart.rigidPart.GetComponent<AudioSource>().Play();
                }
                else
                {
                    buysafeinfo.BoostOn = true;
                    switch_buttonPart.rigidPart.transform.GetChild(0).transform.localRotation = Quaternion.Euler(50, 0, 0);
                    switch_buttonPart.rigidPart.GetComponent<AudioSource>().Play();
                }
            }
            if (time >= 1)
            {
                try
                {
                    carb2_inst = carb2.transform.parent.transform.parent.name == "cylinder head(Clone)";
                }
                catch
                {
                    try
                    {
                        carb2_inst = GameObject.Find("twin carburators(Clone)").transform.parent.transform.parent.name == "cylinder head(Clone)";
                    }
                    catch { }
                }
                try
                {
                    racingcarb_inst = raccarb.transform.parent.transform.parent.name == "cylinder head(Clone)";
                }
                catch
                {
                    try
                    {
                        racingcarb_inst = GameObject.Find("racing carburators(Clone)").transform.parent.transform.parent.name == "cylinder head(Clone)";
                    }
                    catch { }
                }
                try
                {
                    engine_head_inst = engine_head.transform.parent.name == "pivot_cylinder head";
                }
                catch { }

                if (!racingcarb_inst)
                {
                    pipe_rac_carbtrigger.triggerGameObject.SetActive(false);
                }
                else
                {
                    pipe_rac_carbtrigger.triggerGameObject.SetActive(true);
                }

                if (!carb2_inst)
                {
                    pipe_2_carbtrigger.triggerGameObject.SetActive(false);
                }
                else
                {
                    pipe_2_carbtrigger.triggerGameObject.SetActive(true);
                }

                if (pulleyPart.installed && turbinePart.installed)
                {
                    belttrigger.triggerGameObject.SetActive(true);
                }
                else
                {
                    belttrigger.triggerGameObject.SetActive(false);
                }

                if (engine_head_inst)
                {
                    headgaskettrigger.triggerGameObject.SetActive(false);
                }
                else
                {
                    headgaskettrigger.triggerGameObject.SetActive(true);
                }
                if (filterPart.installed)
                {
                    pipePart.rigidPart.GetComponent<Collider>().enabled = false;
                }
                else
                {
                    pipePart.rigidPart.GetComponent<Collider>().enabled = true;
                }
                if (pipePart.installed || beltPart.installed)
                {
                    turbinePart.rigidPart.GetComponent<MeshCollider>().enabled = false;
                }
                else
                {
                    turbinePart.rigidPart.GetComponent<Collider>().enabled = true;
                }

                if (buysafeinfo.Beltwear <= 0f)
                {
                    shop.Add(this, shopbelt, ShopType.Fleetari, Buy_belt, beltPart.activePart);
                    beltPart.removePart();
                    beltPart.activePart.SetActive(false);
                    buysafeinfo.Buybelt = false;
                    buysafeinfo.Beltwear = 100;
                }
                if (buysafeinfo.Turbinewear < 25f && !turbine_Broken)
                {
                    shop.Add(this, shop_turbine, ShopType.Fleetari, Buy_turbine, turbinePart.activePart);
                    turbine_Broken = true;
                }
                if (buysafeinfo.Turbinewear <= 0)
                {
                    troble = 10000;
                }
                if (drivetrain.engineFrictionFactor < 3)
                {
                    if (beltPart.installed) buysafeinfo.Beltwear -= 0.01f;
                    if (turbinePart.installed && !filterPart.installed && buysafeinfo.Turbinewear > 0) buysafeinfo.Turbinewear -= 0.1f;
                    if (filterPart.installed) buysafeinfo.Filterwear -= 0.02f;
                    if (0 < buysafeinfo.Turbinewear && buysafeinfo.Turbinewear < 25)
                    {
                        System.Random r = new System.Random();
                        troble = r.Next(2000);
                    }
                }
                if (buysafeinfo.Filterwear <= 40 && !filter_Broken)
                {
                    shop.Add(this, shop_filter, ShopType.Fleetari, Buy_filter, filterPart.activePart);
                    filter_Broken = true;
                }
                if(powermp > 1) heat.Value += powermp * rpm / 55000;
                time = 0;
            }
            if (beltPart.installed)
            {
                pulleyPart.rigidPart.GetComponent<MeshCollider>().enabled = false;
                turbinePart.rigidPart.transform.GetChild(0).transform.localRotation = pullmesh.transform.localRotation;
            }
            else
            {
                pulleyPart.rigidPart.GetComponent<MeshCollider>().enabled = true;
            }
            powermp = 1;
            retard = 0;
            mixture = 0;
            if (beltPart.installed && pulleyPart.installed && turbinePart.installed && (pipe_rac_carbPart.installed || pipe_2_carbPart.installed) && drivetrain.engineFrictionFactor < 3 && buysafeinfo.BoostOn)
            {
                
                retard += Mathf.Clamp(rpm / 2187, 0, 3.2f);
                if (racingcarb_inst) {
                    mixture -= Mathf.Clamp(rpm / 14000, 0, 0.5f);
                    powermp += Mathf.Clamp(rpm / 14000, 0, 0.5f);
                }
                if (carb2_inst) {
                    mixture -= Mathf.Clamp(rpm / 4666, 0, 1.5f);
                    powermp += Mathf.Clamp(rpm / 14000, 0, 0.5f) / 2f;
                }
            }
            if (n2o.Active)
            {
                powermp += n2oPSI.Value / 3000;
                retard += n2oPSI.Value / 300;
                mixture += Mathf.Clamp(math1.Value * 15,0,5);
            }
            if (noN2O.Active) mixture += 7;
            if (headgasketPart.installed)
            {
                retard = retard / 2;
            }
            if (drivetrain.rpm > 7250)
            {
                powermp -= Mathf.Clamp((drivetrain.rpm - 7250) / 1500, 0, 0.5f);
            }
            if (drivetrain.engineFrictionFactor < 3)
            {
                n2o.Actions[3].Enabled = false;
                n2o.Actions[18].Enabled = false;
                n2o.Actions[11].Enabled = false;
                n2o.Actions[12].Enabled = false;
                n2o.Actions[13].Enabled = false;
                n2o.Actions[17].Enabled = false;
                satsuma.transform.GetChild(14).GetChild(1).GetChild(7).gameObject.GetComponent<PlayMakerFSM>().FsmStates[1].Actions[0].Enabled = false;
                satsuma.transform.GetChild(14).GetChild(1).GetChild(7).gameObject.GetComponent<PlayMakerFSM>().FsmStates[0].Actions[5].Enabled = false;
                satsuma.transform.GetChild(14).GetChild(1).GetChild(7).gameObject.GetComponent<PlayMakerFSM>().FsmStates[1].Actions[1].Enabled = false;
                satsuma.transform.GetChild(14).GetChild(1).GetChild(7).gameObject.GetComponent<PlayMakerFSM>().FsmStates[5].Actions[1].Enabled = false;
                satsuma.transform.GetChild(14).GetChild(1).GetChild(0).gameObject.SetActive(false);
                Mixture.Value = mixture;
                powerMP.Value = powermp;
                drivetrain.powerMultiplier = powermp;
                sparkRetard.Value = retard;
            }
            if (drivetrain.engineFrictionFactor < 3 && beltPart.installed && turbinePart.installed && pulleyPart.installed)
            {
                if (buysafeinfo.BoostOn)
                {
                    rpm = Mathf.Clamp(drivetrain.rpm - troble, 0, 10000);
                }
                else
                {
                    rpm = Mathf.Clamp(rpm - 400, 0, 10000);
                }
                if (filterPart.installed && filter_Broken)
                {
                    rpm -= Mathf.Clamp((40 - buysafeinfo.Filterwear) * 0.01f * 10000, 0, 10000);
                }

                turbinePart.rigidPart.GetComponent<AudioSource>().enabled = true;
                turbinePart.rigidPart.GetComponent<AudioSource>().pitch = 0.75f + Mathf.Clamp(rpm / 11400 - 0.35f, 0, 0.45f);
                turbinePart.rigidPart.GetComponent<AudioSource>().volume = Mathf.Clamp(rpm / 53333 - 0.075f, 0, 1);
                turbinegaugePart.rigidPart.transform.GetChild(0).localRotation = Quaternion.Euler(90, Mathf.Clamp(136 - Mathf.Clamp(rpm - 500, 0, 7000) / 45, -9, 136), 0);

            }
            else
            {
                turbinePart.rigidPart.GetComponent<AudioSource>().enabled = false;
                turbinegaugePart.rigidPart.transform.GetChild(0).localRotation = Quaternion.Euler(90, 136, 0);
            }
        }
        public override void FixedUpdate()
        {
            time += Time.deltaTime;
        }
        public void Buy_belt(PurchaseInfo item)
        {
            item.gameObject.transform.position = FleetariSpawnLocation.desk;
            item.gameObject.SetActive(true);
            buysafeinfo.Buybelt = true;
        }

        public void Buy_pipe(PurchaseInfo item)
        {
            item.gameObject.transform.position = FleetariSpawnLocation.outside;
            item.gameObject.SetActive(true);
            buysafeinfo.Buypipe = true;
        }

        public void Buy_pipe_2_carb(PurchaseInfo item)
        {
            item.gameObject.transform.position = FleetariSpawnLocation.outside;
            item.gameObject.SetActive(true);
            buysafeinfo.Buypipe_2_carb = true;
        }

        public void Buy_pipe_rac_carb(PurchaseInfo item)
        {
            item.gameObject.transform.position = FleetariSpawnLocation.outside;
            item.gameObject.SetActive(true);
            buysafeinfo.Buypipe_rac_carb = true;
        }

        public void Buy_pulley(PurchaseInfo item)
        {
            item.gameObject.transform.position = FleetariSpawnLocation.outside;
            item.gameObject.SetActive(true);
            buysafeinfo.Buypulley = true;
        }

        public void Buy_turbine(PurchaseInfo item)
        {
            if (filterPart.installed)
            {
                filterPart.removePart();
            }
            if (pipePart.installed)
            {
                pipePart.removePart();
            }
            if (beltPart.installed)
            {
                beltPart.removePart();
            }
            if (turbinePart.installed)
            {
                turbinePart.removePart();
            }
            item.gameObject.transform.position = FleetariSpawnLocation.desk;
            item.gameObject.SetActive(true);
            buysafeinfo.Buyturbine = true;
            buysafeinfo.Turbinewear = 100;
            turbine_Broken = false;
            troble = 0;

        }

        public void Buy_turbinegauge(PurchaseInfo item)
        {
            item.gameObject.transform.position = FleetariSpawnLocation.desk;
            item.gameObject.SetActive(true);
            buysafeinfo.Buyturbinegauge = true;

        }

        public void Buy_switch_button(PurchaseInfo item)
        {
            item.gameObject.transform.position = FleetariSpawnLocation.desk;
            item.gameObject.SetActive(true);
            buysafeinfo.Buyswitch_button = true;
        }

        public void Buy_filter(PurchaseInfo item)
        {
            if (filterPart.installed)
            {
                filterPart.removePart();
            }
            item.gameObject.transform.position = FleetariSpawnLocation.desk;
            item.gameObject.SetActive(true);
            buysafeinfo.Buyfilter = true;
            buysafeinfo.Filterwear = 100;
            filter_Broken = false;
        }

        public void Buy_headgasket(PurchaseInfo item)
        {
            item.gameObject.transform.position = FleetariSpawnLocation.desk;
            item.gameObject.SetActive(true);
            buysafeinfo.Buyheadgasket = true;
        }
    }
}
