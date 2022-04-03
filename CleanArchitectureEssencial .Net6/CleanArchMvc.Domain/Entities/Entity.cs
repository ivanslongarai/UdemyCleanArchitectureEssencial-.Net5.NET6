using System;
using Flunt.Notifications;

namespace CleanArchMvc.Domain.Entities
{
    public abstract class Entity : Notifiable
    {
        public abstract void Validate();
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime ModifiedAt { get; protected set; }
    }
}