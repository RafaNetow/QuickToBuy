using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Products : IEntity
    {
        public virtual long Id { get; set; }
        public virtual bool Archived { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Category { get; set; }
        public virtual double Preci { get; set; }
        public virtual string UrlImage { get; set; }
        public virtual string  Coin { get; set; }
        public virtual string username { get; set; }

       
        public virtual void Archive()
        {
            throw new NotImplementedException();
        }

        public virtual void Activate()
        {
            throw new NotImplementedException();
        }
    }
}
