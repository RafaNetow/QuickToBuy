using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DomainDrivenDatabaseDeployer;
using FizzWare.NBuilder;
using NHibernate;

namespace DatabaseDeployer
{
    class ClasificadosSeeder : IDataSeeder
    {
        readonly ISession _session;

        public ClasificadosSeeder(ISession session)
        {
            _session = session;
        }
        public void Seed()
        {
            var operaciones = Builder<Operaciones>.CreateListOfSize(5).Build();
            foreach (var operacion in operaciones)
            {
                _session.Save(operacion);
            }
        }
    }
}
