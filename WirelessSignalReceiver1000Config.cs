using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;
using BUILDINGS = TUNING.BUILDINGS;

namespace wuxian
{
  public class WirelessSignalReceiver1000Config : IBuildingConfig
  {
    public static string Id = "WirelessSignalReceiver1000";
    public const string DisplayName = "LED-1000base";

    public override BuildingDef CreateBuildingDef()
    {
      var buildingDef = BuildingTemplates.CreateBuildingDef(
          id: Id,
          width: 1,
          height: 3,
          anim: "logic_counter_kanim",
          hitpoints: BUILDINGS.HITPOINTS.TIER1,
          construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER2,
          construction_mass: BUILDINGS.CONSTRUCTION_MASS_KG.TIER1,
          construction_materials: MATERIALS.REFINED_METALS,
          melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER1,
          build_location_rule: BuildLocationRule.Anywhere,
          decor: DECOR.NONE,
          noise: NOISE_POLLUTION.NONE);

      buildingDef.Overheatable = false;
      buildingDef.Floodable = false;
      buildingDef.Entombable = false;
      buildingDef.ViewMode = OverlayModes.Logic.ID;
      buildingDef.AudioCategory = "Metal";
      buildingDef.SceneLayer = Grid.SceneLayer.Building;

      buildingDef.SelfHeatKilowattsWhenActive = 0f;

      GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, Id);

      return buildingDef;
    }

    public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
    {
      BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
    }

    public override void DoPostConfigureComplete(GameObject go)
    {

      var re=go.AddOrGet<WirelessSignalReceiverINterFace>();
      re.ReceiveChannel = 0;
      re.maxCount = 1000;
    }
  }
}