/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cartisan.Config;
using Cartisan.Infrastructure;
using Cartisan.Infrastructure.Extensions;

namespace Cartisan.Message.Default {
    public abstract class HandlerProvider<THandler>: IHandlerProvider where THandler: class {
        private string[] Assemblies { get; set; }
        private readonly Dictionary<Type, List<Type>> _handlerTypes;
        private readonly HashSet<Type> _discardKeyTypes;
        private readonly Dictionary<Type, ParameterInfo[]> _handlerConstuctParametersInfo;

        protected HandlerProvider(params string[] assemblies) {
            this.Assemblies = assemblies;
            this._handlerTypes = new Dictionary<Type, List<Type>>();
            _handlerConstuctParametersInfo = new Dictionary<Type, ParameterInfo[]>();
            _discardKeyTypes = new HashSet<Type>();

            RegisterHandlers();
        }

        public void ClearRegistration() {
            this._handlerTypes.Clear();
        }

        void RegisterHandlers() {
            HandlerElementCollection handlerElements =
                ConfigurationReader.Instance.GetConfigurationSection<CartisanConfigurationSection>().Handlers;

            if (handlerElements != null) {
                foreach (HandlerElement handlerElement in handlerElements) {
                    if (Assemblies == null ||
                        Assemblies.Contains(handlerElement.Name)) {
                        try {
                            switch (handlerElement.SourceType) {
                                case HandlerSourceType.Type:
                                    Type type = Type.GetType(handlerElement.Source);
                                    RegisterHandlerFromType(type);
                                    break;
                                case HandlerSourceType.Assembly:
                                    Assembly assembly = Assembly.Load(handlerElement.Source);
                                    RegisterHandlerFromAssembly(assembly);
                                    break;
                            }
                        }
                        catch (Exception) {
                            continue;
                        }
                    }
                }
            }
        }

        private void RegisterHandlerFromAssembly(Assembly assembly) {
            IEnumerable<Type> exportedtypes = assembly.GetExportedTypes().Where(
                x => x.IsInterface == false && x.IsAbstract == false
                    && x.GetInterfaces().Any(y => y.IsGenericType
                        && y.GetGenericTypeDefinition() == typeof(THandler).GetGenericTypeDefinition()));

            foreach(Type type in exportedtypes) {
                this.RegisterHandlerFromType(type);
            }

        }

        private void RegisterHandlerFromType(Type handlerType) {
            IEnumerable<Type> iHandlerTypes = handlerType.GetInterfaces().Where(
                x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(THandler).GetGenericTypeDefinition());

            foreach(Type iHandlerType in iHandlerTypes) {
                Type messageType = iHandlerType.GetGenericArguments().Single();
//                Type messageHandlerWrapperType = typeof(MessageHandler<>).MakeGenericType(messageType);
//                object messageHandler = Activator.CreateInstance(iHandler);
//                IMessageHandler messagehandlerWrapper = Activator.CreateInstance(messageHandlerWrapperType, 
//                    new object[] {messageHandler}) as IMessageHandler;
                this.Register(messageType, iHandlerType);
            }
        }

        public void Register(Type messageType, Type handlerType) {
            if (this._handlerTypes.ContainsKey(messageType)) {
                List<Type> registeredDispatcherHandlerTypes = this._handlerTypes[messageType];
                if (registeredDispatcherHandlerTypes != null) {
                    if (!registeredDispatcherHandlerTypes.Contains(handlerType)) {
                        registeredDispatcherHandlerTypes.Add(handlerType);
                    }
                }
                else {
                    registeredDispatcherHandlerTypes = new List<Type>();
                    this._handlerTypes[messageType] = registeredDispatcherHandlerTypes;
                    registeredDispatcherHandlerTypes.Add(handlerType);
                }
            }
            else {
                List<Type> registeredDispatcherHandlerTypes = new List<Type>();
                this._handlerTypes.Add(messageType, registeredDispatcherHandlerTypes);
                registeredDispatcherHandlerTypes.Add(handlerType);
            }

            ParameterInfo[] parameterInfos =handlerType.GetConstructors().
                OrderByDescending(c => c.GetParameters().Length).FirstOrDefault().GetParameters();

            _handlerConstuctParametersInfo[handlerType] = parameterInfos;
        }

        public Type GetHandlerType(Type messageType) {
            return this.GetHandlerTypes(messageType).FirstOrDefault();
        }

        public IList<Type> GetHandlerTypes(Type messageType) {
            List<Type> avaliableHandlerTypes = new List<Type>();
            if(this._handlerTypes.ContainsKey(messageType)) {
                List<Type> handlerTypes = this._handlerTypes[messageType];
                if(handlerTypes!=null && handlerTypes.Count>0) {
                    avaliableHandlerTypes.AddRange(handlerTypes);
                }
            }
            else if(!_discardKeyTypes.Contains(messageType)) {
                bool isDiscardKeyTypes = true;
                foreach(KeyValuePair<Type, List<Type>> handlerTypes in this._handlerTypes) {
                    if(messageType.IsSubclassOf(handlerTypes.Key)) {
                        List<Type> messageDispatcherHandlerTypess = this._handlerTypes[handlerTypes.Key];
                        if(messageDispatcherHandlerTypess!=null && messageDispatcherHandlerTypess.Count>0) {
                            avaliableHandlerTypes.AddRange(messageDispatcherHandlerTypess);
                            isDiscardKeyTypes = false;
                            this._handlerTypes.Add(messageType, messageDispatcherHandlerTypess);
                            break;
                        }
                    }
                }
                if(isDiscardKeyTypes) {
                    _discardKeyTypes.Add(messageType);
                }
            }

            return avaliableHandlerTypes;
        }

        public object GetHandler(Type messageType) {
            object handler = null;
            var handlerType = GetHandlerType(messageType);
            if (handlerType != null) {
                handler = IoCFactory.Resolve(handlerType);
            }
            return handler;
        }

        public IList<object> GetHandlers(Type messageType) {
            var handlerTypes = GetHandlerTypes(messageType);
            var handlers = new List<object>();
            handlerTypes.ForEach(handlerType => handlers.Add(IoCFactory.Resolve(handlerType)));
            return handlers;
        }
    }
}*/