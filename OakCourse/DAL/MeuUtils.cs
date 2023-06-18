using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class MeuUtils
    {
            public static string ByteParaString(byte[] byteArr)
            {
                var stringSaida = Encoding.UTF7.GetString(byteArr);
                return stringSaida;
            }

            public static byte[] StringParaByte(string stringEntrada)
            {
                var byteSaida = Encoding.UTF7.GetBytes(stringEntrada);
                return byteSaida;
            }
    }
}
