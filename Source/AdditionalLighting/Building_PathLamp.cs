using System;
using System.Text;
using UnityEngine;
using Verse;
using RimWorld;
namespace AdditionalLighting
{
    public class Building_PathLamp : Building
    {
        public decimal energy = 0m;

        public override void SpawnSetup()
        {
            base.SpawnSetup();
        }

        public override void TickRare()
        {
            base.TickRare();
            if(SkyManager.CurSkyGlow >= 0.6f)
            {
                energy += 250m;
                if(energy >= 14000m)
                {
                    energy = 14000m;
                }
                this.GetComp<CompGlower>().ReceiveCompSignal("PowerTurnedOff");
            }
            else
            {
                energy -= 250m;
                if(energy <= 0m)
                {
                    energy = 0m;
                    this.GetComp<CompGlower>().ReceiveCompSignal("PowerTurnedOff");
                    return;
                }
                this.GetComp<CompGlower>().ReceiveCompSignal("PowerTurnedOn");
            }
        }

        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string inspectString = base.GetInspectString();
            if (!inspectString.NullOrEmpty())
            {
                stringBuilder.AppendLine(inspectString);
            }
            decimal energyPercent = (energy / 14000m) * 100m;            
            stringBuilder.AppendLine("Power = " + energyPercent.ToString("F") + "%");
            return stringBuilder.ToString();
        }
    }
}

