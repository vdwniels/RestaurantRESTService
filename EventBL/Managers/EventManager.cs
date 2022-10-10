using EventBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBL.Managers
{
    public class EventManager
    {
        private Dictionary<string,Event> events=new Dictionary<string,Event>();
        public void AddEvent(Event ev)
        {
            if (ev == null) throw new EventException("EM - AddEvent");
            if (events.ContainsKey(ev.Name)) throw new EventException("EM - AddEvent");
            events.Add(ev.Name, ev);
        }
        public void RemoveEvent(Event ev)
        {
            if (ev == null) throw new EventException("EM -RemoveEvent");
            if (!events.ContainsKey(ev.Name)) throw new EventException("EM - RemoveEvent");
            events.Remove(ev.Name);
        }
        public void UpdateEvent(Event ev)
        {
            if (ev == null) throw new EventException("EM -RemoveEvent");
            if (!events.ContainsKey(ev.Name)) throw new EventException("EM - RemoveEvent");
            //TODO zijn er wel verschillen ?
            events[ev.Name] = ev;
        }
        public IReadOnlyList<Event> GetEvents()
        {
            return events.Values.ToList().AsReadOnly();
        }
        public IReadOnlyList<Event> GetEventsForLocation(string location)
        {
            return events.Values.Where(e=>e.Location.Equals(location)).ToList();
        }
        public IReadOnlyList<Event> GetEventsForDate(DateTime dateTime)
        {
            return events.Values.Where(e=>e.Date.Date == dateTime.Date).ToList();
        }
        public void SubscribeVisitor(Visitor visitor,Event ev)
        {
            if (visitor==null) throw new EventException("EM - subscribeVisitor");
            if (ev==null) throw new EventException("EM - subscribeVisitor");
            if (!events.ContainsKey(ev.Name)) throw new EventException("EM - subscribeVisitor");
            try
            {
                events[ev.Name].SubscribeVisitor(visitor);
            }
            catch(Exception ex)
            {
                throw new EventException("EM - SubscribeVisitor");
            }
        }
        public void UnsubscribeVisitor(Visitor visitor, Event ev)
        {
            if (visitor == null) throw new EventException("EM - subscribeVisitor");
            if (ev == null) throw new EventException("EM - subscribeVisitor");
            if (!events.ContainsKey(ev.Name)) throw new EventException("EM - subscribeVisitor");
            try
            {
                events[ev.Name].UnsubscribeVisitor(visitor);
            }
            catch (Exception ex)
            {
                throw new EventException("EM - SubscribeVisitor");
            }
        }
    }
}
