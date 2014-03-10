using System;
using System.Collections.Generic;
using System.Data.Entity;
using Cartisan.Message;

namespace Cartisan.EntityFramework.MessageStoring {
    public class MessageStore: DbContext, IMessageStore {
        public DbSet<Command> Commands { get; set; }
        public DbSet<DomainEvent> DomainEvents { get; set; }

        public MessageStore(): base("MessageStore") {}

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>()
                .Map<Command>(map => {
                    map.ToTable("Commands");
                    map.MapInheritedProperties();
                })
                .Map<DomainEvent>(map => {
                    map.ToTable("DomainEvents");
                    map.MapInheritedProperties();
                });
        }

        public virtual Command BuildCommand(IMessageContext commandContext, string domainEventId = null) {
            return new Command(commandContext, domainEventId);
        }

        public virtual DomainEvent BuildDomainEvent(IMessageContext domainEventContext, string commandId) {
            return new DomainEvent(domainEventContext, commandId);
        }

        public void Save(IMessageContext commandContext, string domainEventId) {
            try {
                Command command = Commands.Find(commandContext.MessageId);
                if(command==null) {
                    command = this.BuildCommand(commandContext, domainEventId);
                    Commands.Add(command);
                    this.SaveChanges();
                }
            }
            catch(Exception) {
                
            }
        }

        public void Save(IMessageContext commandContext, IEnumerable<IMessageContext> domainEventContexts) {
            try {
                Command command = Commands.Find(commandContext.MessageId);
                if (command == null) {
                    command = this.BuildCommand(commandContext);
                    Commands.Add(command);
                    this.SaveChanges();
                }
            }
            catch(Exception) {
                
            }
        }
    }
}