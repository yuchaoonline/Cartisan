using System.Collections.Generic;
using System.Text;

namespace Cartisan.Web {
    public class QueryParameter {
        private readonly string _name;
        private string _value;

        public QueryParameter(string name, string value) {
            this._name = name;
            this._value = value;
        }

        public string Name {
            get { return this._name; }
        }

        public string Value {
            get { return this._value; }
            set { this._value = value; }
        }

        public static List<QueryParameter> GetQueryParameters(string parameters) {
            if (parameters.StartsWith("?")) {
                parameters = parameters.Remove(0, 1);
            }

            List<QueryParameter> result = new List<QueryParameter>();

            if (!string.IsNullOrEmpty(parameters)) {
                string[] p = parameters.Split('&');
                foreach (string s in p) {
                    if (!string.IsNullOrEmpty(s)) {
                        if (s.IndexOf('=') > -1) {
                            string[] temp = s.Split('=');
                            result.Add(new QueryParameter(temp[0], temp[1]));
                        }
                        else {
                            result.Add(new QueryParameter(s, string.Empty));
                        }
                    }
                }
            }

            return result;
        }

        public static string NormalizeRequestParameters(IList<QueryParameter> parameters) {
            StringBuilder sb = new StringBuilder();
            QueryParameter p = null;
            for (int i = 0; i < parameters.Count; i++) {
                p = parameters[i];
                sb.AppendFormat("{0}={1}", p.Name, p.Value);

                if (i < parameters.Count - 1) {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        } 
    }
}