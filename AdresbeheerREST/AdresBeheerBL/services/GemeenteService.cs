using AdresBeheerBL.Exceptions;
using AdresBeheerBL.Interfaces;
using AdresBeheerBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresBeheerBL.services
{
    public class GemeenteService
    {
        private IGemeenteRepository repo;

        public GemeenteService(IGemeenteRepository repo)
        {
            this.repo = repo;
        }

        public Gemeente GeefGemeente(int gemeenteId)
        {
            try
            {
                return repo.GeefGemeente(gemeenteId);
            }
            catch(Exception ex)
            {
                throw new GemeenteServiceException("GeefGemeente", ex);
            }
        }
    }
}
