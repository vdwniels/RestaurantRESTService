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

        }
        public void RemoveEvent(Event ev)
        {

        }
        public void UpdateEvent(Event ev)
        {

        }
        public IReadOnlyList<Event> GetEvents()
        {

        }
        public IReadOnlyList<Event> GetEventsForLocation(string location)
        {

        }
        public IReadOnlyList<Event> GetEventsForDate(DateTime dateTime)
        {

        }
        public void SubscribeVisitor(Visitor visitor,Event ev)
        {

        }
        public void UnsubscribeVisitor(Visitor visitor, Event ev)
        {

        }
    }
}
