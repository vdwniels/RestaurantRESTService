using EventBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBL.Managers {
    public class EventManager {
        private Dictionary<string, Event> _events = new Dictionary<string, Event>();

        public EventManager()
        {
            _events.Add("ASP.NET Boot", new Event("ASP.NET Boot", "Schoonmeersen Lokaal 1.0012", DateTime.Parse("24/10/2022"), 20));
            _events.Add("Bijscholing async", new Event("Bijscholing async", "Mercator - Gebouw D", DateTime.Parse("14/11/2022"), 10));
            _events.Add("MongoDB 2022", new Event("MongoDB 2022", "Mercator - Gebouw C", DateTime.Parse("1/12/2022"), 4));
        }

        public void AddEvent(Event e) {
            if (e == null) { throw new EventException("EM - AddEvent"); }
            if (_events.ContainsKey(e.Name)) { throw new EventException("EM - AddEvent"); }
            _events.Add(e.Name, e);
        }
        public void RemoveEvent(Event e) {
            if (e == null) { throw new EventException("EM - RemoveEvent"); }
            if (!_events.ContainsKey(e.Name)) { throw new EventException("EM - RemoveEvent"); }
            _events.Remove(e.Name);
        }
        public void UpdateEvent(Event e) {
            if (e == null) { throw new EventException("EM - UpdateEvent"); }
            if (!_events.ContainsKey(e.Name)) { throw new EventException("EM - UpdateEvent"); }
            // TODO zijn er wel verschillen?
            _events[e.Name] = e;
        }
        public bool ExistsEvent(string name)
        {
            return _events.ContainsKey(name);
        }
        public Event GetEventByName(string name) 
        {
            if (string.IsNullOrWhiteSpace(name)) throw new EventException("EM - GetEventByName");
            if (!_events.ContainsKey(name)) { throw new EventException("EM - GetEventByName"); }
            return _events[name];
        }
        public IReadOnlyList<Event> GetEvents() {
            return _events.Values.ToList<Event>().AsReadOnly();
        }
        public IReadOnlyList<Event> GetEventsForLocation(string location) {
            return _events.Values.Where(e => e.Location == location).ToList();
        }
        public IReadOnlyList<Event> GetEventsForDate(DateTime datetime) {
            return _events.Values.Where(e => e.Date.Date == datetime.Date).ToList<Event>();
        }
        public void SubscribeVisitor(Visitor visitor, Event e) {
            if (e == null) { throw new EventException("EM - subscribeVisitor - event is null"); }
            if (visitor == null) { throw new EventException("EM - subscribeVisitor - visitor is null"); }
            if (!_events.ContainsKey(e.Name)) { throw new EventException("EM - SubscribeVisitor - event bestaat niet"); }
            try {
                _events[e.Name].SubscribeVisitor(visitor); // Kan exceptions gooien. vb maxAantal

            } catch (Exception ex) {
                throw new EventException("EM - SubscribeVisitor", ex);
            }
        }
        public void UnSubscribeVisitor(Visitor visitor, Event e) {
            if (e == null) { throw new EventException("EM - subscribeVisitor - event is null"); }
            if (visitor == null) { throw new EventException("EM - subscribeVisitor - visitor is null"); }
            if (!_events.ContainsKey(e.Name)) { throw new EventException("EM - SubscribeVisitor - event bestaat niet"); }
            try {
                _events[e.Name].UnsubscribeVisitor(visitor); // Kan exceptions gooien. vb maxAantal

            } catch (Exception ex) {
                throw new EventException("EM - SubscribeVisitor", ex);
            }
        }
    }
}
