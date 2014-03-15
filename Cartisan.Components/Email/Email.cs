using System.Collections.Generic;

namespace Cartisan.Components.Email {
    public class Email {
         private readonly string _template;
        private string _subject;

        private IList<string> _to = new List<string>();
        private IList<string> _cc = new List<string>();

        private IDictionary<string, string> _params = new Dictionary<string, string>();

        public Email(string template, string subject) {
            this._template = template;
            this._subject = subject;
        }

        public Email(string template, string subject, string to) {
            this._template = template;
            this._subject = subject;
            this.To.Add(to);
        }

        public string Subject {
            get { return this._subject; }
        }

        public string Template {
            get { return this._template; }
        }

        public IList<string> To {
            get { return this._to; }
            set { this._to = value; }
        }

        public IList<string> CC {
            get { return this._cc; }
            set { this._cc = value; }
        }

        public IDictionary<string, string> Params {
            get { return this._params; }
            set { this._params = value; }
        }
    }
}