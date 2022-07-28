using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STRINGS;
using TUNING;
namespace wuxian
{
  public static class Utils
  {
	// Token: 0x06000038 RID: 56 RVA: 0x00002E9C File Offset: 0x0000109C
	public static void AddBuildingStrings(string buildingId, string name, string description, string effect)
	{
	  Strings.Add(new string[]
	  {
				"STRINGS.BUILDINGS.PREFABS." + buildingId.ToUpperInvariant() + ".NAME",
				UI.FormatAsLink(name, buildingId)
	  });
	  Strings.Add(new string[]
	  {
				"STRINGS.BUILDINGS.PREFABS." + buildingId.ToUpperInvariant() + ".DESC",
				description
	  });
	  Strings.Add(new string[]
	  {
				"STRINGS.BUILDINGS.PREFABS." + buildingId.ToUpperInvariant() + ".EFFECT",
				effect
	  });
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002F28 File Offset: 0x00001128
	private static PlanScreen.PlanInfo GetMenu(HashedString category)
	{
	  foreach (PlanScreen.PlanInfo planInfo in TUNING.BUILDINGS.PLANORDER)
	  {
		bool flag = planInfo.category == category;
		if (flag)
		{
		  return planInfo;
		}
	  }
	  throw new Exception("The plan menu was not found in TUNING.BUILDINGS.PLANORDER.");
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002F98 File Offset: 0x00001198
	public static void AddBuildingToPlanScreen(string buildingID, HashedString category, string addAferID = null)
	{
	  PlanScreen.PlanInfo menu = Utils.GetMenu(category);
	  List<string> data = menu.data;
	  bool flag = data != null;
	  if (flag)
	  {
		bool flag2 = addAferID != null;
		if (flag2)
		{
		  int num = data.IndexOf(addAferID);
		  bool flag3 = num == -1 || num == data.Count - 1;
		  if (flag3)
		  {
			data.Add(buildingID);
		  }
		  else
		  {
			data.Insert(num + 1, buildingID);
		  }
		}
		else
		{
		  data.Add(buildingID);
		}
	  }
	}
  }
}
