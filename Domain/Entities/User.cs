using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User: IEntity
    {
        public virtual long Id { get; set; }
        public virtual bool Archived { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string username { get; set; }
        public virtual string correo { get; set; }
        public virtual string password { get; set; }
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
