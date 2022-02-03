using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Domain
{
    public class EntityBase<T>
    {
        public T Id { get; private set; }
        public DateTime CreationDate { get; private set; }

        public EntityBase()
        {
            CreationDate = DateTime.Now;
        }
    }
}
