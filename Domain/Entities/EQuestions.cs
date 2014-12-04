using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EQuestions: IEntity
    {
        public virtual long Id { get; set; }
        public virtual bool Archived { get; protected set; }
        public virtual string usuario{ get; set; }
        public virtual string pregunta { get; set; }
        public virtual string respuesta { get; set; }
        public virtual void Archive()
        {
            throw new NotImplementedException();
        }

        public virtual  void Activate()
        {
            throw new NotImplementedException();
        }
    }
}
