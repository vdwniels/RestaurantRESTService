using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBL.Model
{
    public class Event
    {
        private List<Visitor> visitors=new List<Visitor>();

        public Event(string name, string location, DateTime date, int maxVisitors)
        {
            SetName(name);
            SetLocation(location);
            Date = date;
            SetMaxVisitors(maxVisitors);
        }

        public string Name { get; private set; }
        public string Location { get; private set; }
        public DateTime Date { get; set; }
        public int MaxVisitors { get; private set; }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new EventException("Event - SetName");
            Name = name;
        }
        public void SetLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location)) throw new EventException("Event - SetLocation");
           Location=location;
        }
        public void SetMaxVisitors(int max)
        {
            if (max <= 0) throw new EventException("Event - SetMaxVisitors");
            MaxVisitors=max;
        }
        public void SubscribeVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventException("SubscribeVisitor");
            if (visitors.Count == MaxVisitors) throw new EventException("SubscribeVisitor");
            if (visitors.Contains(visitor)) throw new EventException("SubscribeVisitor");
            visitors.Add(visitor);
        }
        public void UnsubscribeVisitor(Visitor visitor)
        {
            if (visitor == null) throw new EventException("UnSubscribeVisitor");           
            if (!visitors.Contains(visitor)) throw new EventException("UnSubscribeVisitor");
            visitors.Remove(visitor);
        }
    }
}
