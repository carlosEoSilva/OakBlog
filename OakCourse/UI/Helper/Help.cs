using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace UI.Helper
{
    public class Help
    {
        public string ByteParaString(byte[] byteArr)
        {
            var stringSaida = Encoding.UTF7.GetString(byteArr);
            return stringSaida;
        }

        public byte[] StringParaByte(string stringEntrada)
        {
            var byteSaida= Encoding.UTF7.GetBytes(stringEntrada);
            return byteSaida;
        }
    }
}