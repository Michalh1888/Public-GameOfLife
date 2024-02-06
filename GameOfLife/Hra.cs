using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Hra
    {
        private const int maxX = 118;//400
        private const int maxY = 38;//400
        private const int startPosX = maxX/2 - 1;//200-1
        private const int startPosY = maxY/2 - 1;//200-1
        private HashSet<Bod> aktZiveBody;
        HashSet<Bod> mrtveBody = new HashSet<Bod>();
        private char[,] plochaTemp = new char[1,1];
        private List<Bod> kontrPozice = new List<Bod>
        {
            new Bod(1,0),
            new Bod(1,-1),
            new Bod(0,-1),
            new Bod(-1,-1),
            new Bod(-1,0),
            new Bod(-1,1),
            new Bod(0,1),
            new Bod(1,1),
        };
        private bool zivot = true;
        private char[,] vykrPlocha;
        private bool souvisle;

        public Hra(bool souv)
        {
            Console.CursorVisible = false;
            souvisle = souv;
            aktZiveBody = new HashSet<Bod>
            {   //vzor "rabbit"
                new Bod(startPosX-1,startPosY),
                new Bod(startPosX-2,startPosY),
                new Bod(startPosX-3,startPosY),
                new Bod(startPosX-3,startPosY-1),
                new Bod(startPosX-2,startPosY+1),
                new Bod(startPosX+1,startPosY-1),
                new Bod(startPosX+2,startPosY-1),
                new Bod(startPosX+2,startPosY),
                new Bod(startPosX+3,startPosY-1),
            };
            vykrPlocha = NaplnPole(aktZiveBody);
        }

        public char[,] NaplnPole(HashSet<Bod> body2)
        {
            //x-řádky, y-sloupce
            //prázdná(mrtvá)plocha
            char[,] plocha = new char[maxX, maxY];
            for (int i = 0; i < maxX; i++)
                for (int j = 0; j < maxY; j++)
                    plocha[i, j] = '.';
            //zakreslení živých bodů
            if (body2.Count() != 0)
            {
                foreach (Bod bod in body2)
                    plocha[bod.X, bod.Y] = '#';
            }
            return plocha;
        }


        public void Smycka()
        {
            while (zivot)
            {
                Task uloha = Task.Run(() =>
                {
                    aktZiveBody = VratBody();
                    plochaTemp = NaplnPole(aktZiveBody);
                });
                HraciPlocha.VykresliPlochu(vykrPlocha);
                uloha.Wait();
                if (vykrPlocha == plochaTemp)
                {
                    zivot = false;
                    break;
                }
                vykrPlocha = plochaTemp;
                mrtveBody = new HashSet<Bod>();
                Console.SetCursorPosition((maxX - 1) / 2, (maxY - 1) / 2);
                if (souvisle)
                    Thread.Sleep(100);
                else
                    Console.ReadKey();
                Console.Clear();
            }
        }

        public Bod? ZkontrHranice(int bodX, int bodY)
        {
            if (bodX < 0) return null;
            if (bodX > maxX-1) return null;
            if (bodY < 0) return null;
            if (bodY > maxY-1) return null;
            return new Bod(bodX, bodY);
        }

        public bool ZjistiZivot(Bod bod, bool ziva)
        {   
            int pocZiv = 0;
            foreach (Bod bodPoz in kontrPozice)
            {
                var kontrBod = ZkontrHranice(bod.X + bodPoz.X, bod.Y + bodPoz.Y);
                if (kontrBod != null)
                {
                    if (vykrPlocha[kontrBod.Value.X, kontrBod.Value.Y] == '#') pocZiv++;
                    else if(ziva) mrtveBody.Add((Bod)kontrBod);
                }
            }
            bool zivot = ziva ? VratZivot(true, pocZiv) : VratZivot(false, pocZiv);
            return zivot;
        }

        public bool VratZivot(bool ziva, int pocetZivych)
        {
            if (ziva)
            {
                if ((pocetZivych == 2) || (pocetZivych == 3)) return true;
            }
            else
            {
                if (pocetZivych == 3) return true;
            }
            return false;
        }


        public HashSet<Bod> VratBody()
        {
            HashSet<Bod> ziveBody = new HashSet<Bod>();
            if (aktZiveBody.Count() != 0)
            {
                foreach (Bod bod in aktZiveBody)
                {
                    if (ZjistiZivot(bod, true))
                        ziveBody.Add(bod);
                } 
            }
            if (mrtveBody.Count() != 0)
            {
                foreach (Bod bod in mrtveBody)
                {
                    if (ZjistiZivot(bod, false))
                        ziveBody.Add(bod);
                }
            }
            return ziveBody;
        }
    }
}


