using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class HraciPlocha
    {
        
        public static void VykresliPlochu(char[,] plocha)
        {
            StringBuilder sb = new StringBuilder();
            //vykreslení plochy
            for (int j = 0; j < plocha.GetLength(1); j++)//řádky-Y
            {
                for (int i = 0; i < plocha.GetLength(0); i++)//sloupce-X
                    sb.Append(plocha[i, j]);
                sb.Append('\n');
            }
            Console.WriteLine(sb.ToString());

        }

    }
}
