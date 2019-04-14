using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Common
{
    public class ModelStateException : ApplicationException
    {
        public IEnumerable<KeyValuePair<string, string>> State { get; private set; }

        public ModelStateException(IEnumerable<KeyValuePair<string, string>> state)
        {
            this.State = state;
        }

        public ModelStateException(string propertyName, string message)
        {
            this.State = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(propertyName, message)
            };
        }

        public ModelStateException(string message)
            : base(message)
        {
        }

        public ModelStateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
