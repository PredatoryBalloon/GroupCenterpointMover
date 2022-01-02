
using System;

using System;
using System.Collections.Generic;
using System.Linq;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using LevelEditorTools;
using UnityEngine;

namespace Mod.GroupCenterpointMover.Harmony
{
    [HarmonyPatch(typeof(Group), "Visit")]
    internal static class Group__Visit
    {
        //__instance is the class you are patching, so you can call functions on it.
        //If patching a function with paramaters, you can just add those paramaters as paramaters inside Postfix.
        //If the function you're patching has a return type, you can modify the result value with the parameter 'ref [type of return value] __result'
        [HarmonyPostfix]
        internal static void Postfix(Group __instance, IVisitor visitor, ISerializable prefabComp)
        {
            if(Mod.GCPMM)
            {
                if ((UnityEngine.Object)prefabComp == (UnityEngine.Object)null)
                {
                    Vector3 transfoval = new Vector3(__instance.transform.localPosition.x, __instance.transform.localPosition.y, __instance.transform.localPosition.z);
                    Quaternion rotoval = new Quaternion(__instance.transform.rotation.x, __instance.transform.rotation.y, __instance.transform.rotation.z, __instance.transform.rotation.w);
                    List<Vector3> chileinittrans = new List<Vector3>();
                    List<Quaternion> chileinitroto = new List<Quaternion>();
                    foreach (GameObject groupchile in __instance.gameObject.GetChildren())
                    {
                        chileinittrans.Add(groupchile.transform.position);
                        chileinitroto.Add(groupchile.transform.rotation);
                    }

                    visitor.Visit("Centerpoint Position", ref transfoval);
                    visitor.Visit("Centerpoint Rotation", ref rotoval);
                    Vector3 transfochange = transfoval - __instance.transform.localPosition;
                    __instance.transform.localPosition = __instance.transform.localPosition + transfochange;
                    __instance.transform.rotation = rotoval;
                    int chileintitransindex = 0;
                    foreach (GameObject groupchile in __instance.gameObject.GetChildren())
                    {
                        groupchile.transform.position = chileinittrans.ToArray()[chileintitransindex];
                        groupchile.transform.rotation = chileinitroto.ToArray()[chileintitransindex];
                        chileintitransindex++;
                    }

                    __instance.localBounds_ = Group.CalculateBoundsFromImmediateChildren(__instance);
                }
            }
        }
    }
}