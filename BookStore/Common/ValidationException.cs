using System;
using System.Collections.Generic;

namespace BookStore.Common
{
    public class ValidationException : Exception
    {
        public IEnumerable<KeyValuePair<string, string>> State { get; private set; }

        public ValidationException(IEnumerable<KeyValuePair<string, string>> state)
        {
            this.State = state;
        }

        public ValidationException(string propertyName, string message)
        {
            this.State = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>(propertyName, message)
            };
        }

        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
