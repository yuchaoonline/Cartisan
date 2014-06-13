//
//using IFramework.Command;
//using IFramework.Config;
//using IFramework.Event;
//using IFramework.Infrastructure;
//using IFramework.Infrastructure.Logging;
//using IFramework.Message;
//using Microsoft.Practices.Unity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Http;
//using System.Web.Mvc;
//using System.Web.Optimization;
//using System.Web.Routing;
//using IFramework.MessageQueue.ZeroMQ;
//
//namespace Sample.CommandService {
//    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
//    // visit http://go.microsoft.com/?LinkId=9394801
//
//    public class WebApiApplication: System.Web.HttpApplication {
//        static ILogger _Logger;
//
//        static WebApiApplication() {
//            try {
//                Configuration.Instance.UseLog4Net();
//                _Logger = IoCFactory.Resolve<ILoggerFactoryAdapter>().Create(typeof(WebApiApplication));
//
//                var commandDistributor = new CommandDistributor("tcp://127.0.0.1:5000",
//                                                                new string[] { 
//                                                                    "tcp://127.0.0.1:5001"
//                                                                    , "tcp://127.0.0.1:5002"
//                                                                    , "tcp://127.0.0.1:5003"
//                                                                }
//                                                               );
//
//                Configuration.Instance.RegisterCommandConsumer(commandDistributor, "CommandDistributor")
//                             .CommandHandlerProviderBuild(null, "CommandHandlers")
//                             .RegisterDisposeModule()
//                             .RegisterMvc();
//
//                IoCFactory.Resolve<IEventPublisher>();
//                IoCFactory.Resolve<IMessageConsumer>("DomainEventConsumer").Start();
//                IoCFactory.Resolve<IMessageConsumer>("ApplicationEventConsumer").Start();
//
//                var commandHandlerProvider = IoCFactory.Resolve<ICommandHandlerProvider>();
//                var commandConsumer1 = new CommandConsumer(commandHandlerProvider,
//                                                           "tcp://127.0.0.1:5001");
//                var commandConsumer2 = new CommandConsumer(commandHandlerProvider,
//                                                           "tcp://127.0.0.1:5002");
//                var commandConsumer3 = new CommandConsumer(commandHandlerProvider,
//                                                           "tcp://127.0.0.1:5003");
//
//
//                commandConsumer1.Start();
//                commandConsumer2.Start();
//                commandConsumer3.Start();
//                commandDistributor.Start();
//
//                ICommandBus commandBus = IoCFactory.Resolve<ICommandBus>();
//                commandBus.Start();
//            }
//            catch (Exception ex) {
//                _Logger.Error(ex.GetBaseException().Message, ex);
//            }
//
//        }
//
//        // ZeroMQ Application_Start
//        protected void Application_Start() {
//            AreaRegistration.RegisterAllAreas();
//            WebApiConfig.Register(GlobalConfiguration.Configuration);
//            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
//            RouteConfig.RegisterRoutes(RouteTable.Routes);
//            BundleConfig.RegisterBundles(BundleTable.Bundles);
//        }
//
//
//        // EQueue Application_Start
//        /*
//        public static List<CommandConsumer> CommandConsumers = new List<CommandConsumer>();
//         
//        protected void Application_Start()
//        {
//            try
//            {
//                Configuration.Instance.UseLog4Net();
//
//                Configuration.Instance
//                             .CommandHandlerProviderBuild(null, "CommandHandlers")
//                             .RegisterMvc();
//
//                global::EQueue.Configuration
//                .Create()
//                .UseAutofac()
//                .UseLog4Net()
//                .UseJsonNet()
//                .RegisterFrameworkComponents();
//
//                new BrokerController().Initialize().Start();
//                var consumerSettings = ConsumerSettings.Default;
//                consumerSettings.MessageHandleMode = MessageHandleMode.Sequential;
//                var producerPort = 5000;
//                IEventPublisher eventPublisher = new EventPublisher("domainevent", 
//                                                                    consumerSettings.BrokerAddress,
//                                                                    producerPort);
//                IoCFactory.Instance.CurrentContainer.RegisterInstance(typeof(IEventPublisher), eventPublisher);
//
//                var eventHandlerProvider = IoCFactory.Resolve<IHandlerProvider>("AsyncDomainEventSubscriber");
//                IMessageConsumer domainEventSubscriber = new DomainEventSubscriber("domainEventSubscriber1",
//                                                                                   consumerSettings,
//                                                                                   "DomainEventSubscriber",
//                                                                                   "domainevent",
//                                                                                   eventHandlerProvider);
//                domainEventSubscriber.Start();
//                IoCFactory.Instance.CurrentContainer.RegisterInstance("DomainEventConsumer", domainEventSubscriber);
//
//
//
//                var commandHandlerProvider = IoCFactory.Resolve<ICommandHandlerProvider>();
//                var commandConsumer1 = new CommandConsumer("consumer1", consumerSettings, 
//                                                           "CommandConsumerGroup",
//                                                           "Command",
//                                                           consumerSettings.BrokerAddress,
//                                                           producerPort,
//                                                           commandHandlerProvider);
//
//                var commandConsumer2 = new CommandConsumer("consumer2", consumerSettings,
//                                                           "CommandConsumerGroup",
//                                                           "Command",
//                                                           consumerSettings.BrokerAddress,
//                                                           producerPort,
//                                                           commandHandlerProvider);
//
//                var commandConsumer3 = new CommandConsumer("consumer3", consumerSettings,
//                                                           "CommandConsumerGroup",
//                                                           "Command",
//                                                           consumerSettings.BrokerAddress,
//                                                           producerPort,
//                                                           commandHandlerProvider);
//
//                commandConsumer1.Start();
//                commandConsumer2.Start();
//                commandConsumer3.Start();
//
//                CommandConsumers.Add(commandConsumer1);
//                CommandConsumers.Add(commandConsumer2);
//                CommandConsumers.Add(commandConsumer3);
//
//                ICommandBus commandBus = new CommandBus("CommandBus",
//                                                        commandHandlerProvider,
//                                                        IoCFactory.Resolve<ILinearCommandManager>(),
//                                                        consumerSettings.BrokerAddress,
//                                                        producerPort,
//                                                        consumerSettings,
//                                                        "CommandBus",
//                                                        "Reply", 
//                                                        "Command",
//                                                        true);
//                IoCFactory.Instance.CurrentContainer.RegisterInstance(typeof(ICommandBus),
//                                                                      commandBus,
//                                                                      new ContainerControlledLifetimeManager());
//                commandBus.Start();
//
//                AreaRegistration.RegisterAllAreas();
//                WebApiConfig.Register(GlobalConfiguration.Configuration);
//                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
//                RouteConfig.RegisterRoutes(RouteTable.Routes);
//                BundleConfig.RegisterBundles(BundleTable.Bundles);
//            }
//            catch (Exception ex)
//            {
//                Logger.Error(ex.GetBaseException().Message, ex);
//            }
//        }
//        */
//        protected void Application_Error(object sender, EventArgs e) {
//
//            Exception ex = Server.GetLastError().GetBaseException(); //获取错误
//            _Logger.Debug(ex.Message, ex);
//        }
//    }
//}