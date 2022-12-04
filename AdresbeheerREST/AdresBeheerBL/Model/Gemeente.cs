using AdresBeheerBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresBeheerBL.Model
{
    public class Gemeente
    {
        public Gemeente(int nIScode, string gemeentenaam)
        {
            ZetNIScode(nIScode);
            ZetGemeentenaam(gemeentenaam);
        }

        public int NIScode { get; private set; }
        public string Gemeentenaam { get; private set; }
        public void ZetGemeentenaam(string naam)
        {
            if((string.IsNullOrWhiteSpace(naam)) || (!char.IsUpper(naam[0])))
            {
                GemeenteException ex =  new GemeenteException("naam niet correct");
                ex.Data.Add("Gemeentenaam", naam);
                throw ex;
            }
            Gemeentenaam = naam;
        }
        public void ZetNIScode(int code)
        {
            if ((code < 10000) || (code > 99999))
            {
                GemeenteException ex = new GemeenteException("NIScode niet correct");
                ex.Data.Add("ZetNIScode", code);
                throw ex;

            }
            NIScode = code;
        }
        public override string ToString()
        {
            return $"{NIScode} | {Gemeentenaam}";
        }
    }
}
