using System;
using System.Collections;
using System.Net;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Command.Default {
    public class LinearCommandManager: ILinearCommandManager {
        private readonly Hashtable _linearFuncs = new Hashtable();

        public object GetLinearKey(ILinearCommand command) {
            return this.InvokeGenericMethod(command.GetType(), "GetLinearKeyImpl", new object[] {command});
        }

        public object GetLinearKeyImpl<TLinearCommand>(TLinearCommand command) where TLinearCommand: ILinearCommand {
            object linearKey = null;
            Func<TLinearCommand, object> func = _linearFuncs[typeof(TLinearCommand)] as Func<TLinearCommand, object>;
            if(func!=null) {
                linearKey = func(command);
            }
            else {
                linearKey = typeof(TLinearCommand);                
            }
            return linearKey;
        }

        public void RegisterLinearCommand<TLinearCommand>(Func<TLinearCommand, object> func) where TLinearCommand: ILinearCommand {
            _linearFuncs.Add(typeof(TLinearCommand), func);
        }
    }
}