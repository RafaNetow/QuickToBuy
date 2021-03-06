﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class Follows : IEntity
    {
        public virtual long Id { get; set; }
        public virtual bool Archived { get; protected set; }
        public virtual Users Seguido { get;  set; }
        public virtual Users Seguidor { get;  set; } 
        public virtual void Archive()
        {
            Archived = false;
        }

        public virtual void Activate()
        {
            Archived = true;
        }

        public virtual bool IsArchived()
        {
            return Archived;
        }
    }
}
