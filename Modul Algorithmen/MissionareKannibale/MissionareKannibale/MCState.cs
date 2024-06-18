
namespace MissionareKannibale
{
    public class MCState
    {
        public static int MaxNum { get; set; } = 3;
        public int WestM { get; set; }
        public int WestC { get; set; }
        public int EastM { get; set; }
        public int EastC { get; set; }
        public bool Boat { get; set; }

        public MCState(int missionaries, int cannibals, bool boat)
        {
            WestM = missionaries;
            WestC = cannibals;
            EastM = MaxNum - missionaries;
            EastC = MaxNum - cannibals;
            Boat = boat;
        }

        // Zustandsanzeige/Snapshot über M. und K. an beiden Ufern und Lokation des Boots
        public string DisplaySolution()
        {
            string Ufer;
            if (Boat)
            {
                Ufer = "West";
            }
            else
            {
                Ufer = "Ost";
            }

            return $"Am Westufer sind {WestM} Missionare und {WestC} Kannibale"
                + Environment.NewLine + $"Am Ostufer sind {EastM} Missionare und {EastC} Kannibale"
                + Environment.NewLine + $"Das Boot ist am {Ufer}ufer";
        }

        // Prüfen auf Endbedingung (alle am Ostufer)
        public bool GoalTest()
        {
            if (EastC == MaxNum && EastM == MaxNum && IsLegal())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Prüfung auf Zulässigkeit des aktuellen Zustands
        public bool IsLegal()
        {
            if ((WestM < WestC && WestM > 0) || (EastM < EastC && EastM > 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Auffangen aller Folgezustände, die in einem zulässigen Zustand resultieren
        public List<MCState> Successors()
        {
            List<MCState> successors = [];
            if (Boat)
            {
                if (WestM > 1)
                {
                    successors.Add(new MCState(WestM - 2, WestC, !Boat));
                }
                if (WestM > 0)
                {
                    successors.Add(new MCState(WestM - 1, WestC, !Boat));
                }
                if (WestC > 1)
                {
                    successors.Add(new MCState(WestM, WestC - 2, !Boat));
                }
                if (WestC > 0)
                {
                    successors.Add(new MCState(WestM, WestC - 1, !Boat));
                }
                if (WestC > 0 && WestM > 0)
                {
                    successors.Add(new MCState(WestM - 1, WestC - 1, !Boat));
                }
            }
            else
            {
                if (EastM > 1)
                {
                    successors.Add(new MCState(WestM + 2, WestC, !Boat));
                }
                if (EastM > 0)
                {
                    successors.Add(new MCState(WestM + 1, WestC, !Boat));
                }
                if (EastC > 1)
                {
                    successors.Add(new MCState(WestM, WestC + 2, !Boat));
                }
                if (EastC > 0)
                {
                    successors.Add(new MCState(WestM, WestC + 1, !Boat));
                }
                if (EastC > 0 && EastM > 0)
                {
                    successors.Add(new MCState(WestM + 1, WestC + 1, !Boat));
                }
            }
            return successors.Where(x => x.IsLegal()).ToList();
        }
    }
}

