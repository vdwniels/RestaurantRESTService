using System.Xml.Linq;

namespace EventBL.Model
{
    public class Event
    {
        private List<Visitor> _visitors= new List<Visitor>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Event(string name, string location, DateTime date, int maxVisitors) {
            SetName(name);
            SetLocation(location);
            Date = date;
            SetMaxVisitors(maxVisitors);
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public string Name { get; set; } // Uniek
        public string Location { get; set; }
        public DateTime Date { get; set; } // ToDo private setters
        public int MaxVisitors { get; set; }

        public IReadOnlyList<Visitor> Visitors => _visitors.AsReadOnly();

        public void SetName(string name) {
            if (string.IsNullOrWhiteSpace(name)) {
                throw new EventException("Event - SetName - Invalid name");
            }
            Name = name;
        }
        public void SetLocation(string location) {
            if (string.IsNullOrWhiteSpace(location)) {
                throw new EventException("Event - SetLocation - Invalid location");
            }
            Location = location;
        }
        public void SetMaxVisitors(int max) {
            if (max <= 0) {
                throw new EventException("Event - SetMaxVisitors");
            }
            MaxVisitors = max;
        }
        public void SubscribeVisitor(Visitor visitor) {
            if (visitor == null) { throw new EventException("Event - SubscribeVisitor"); }
            if (_visitors.Count == MaxVisitors) { throw new EventException("Event - SubscribeVisitor"); }
            if (_visitors.Contains(visitor)) { throw new EventException("Event - SubscribeVisitor"); }
            _visitors.Add(visitor);
        }
        public void UnsubscribeVisitor(Visitor visitor) {
            if (visitor == null) { throw new EventException("Event - UnsubscribeVisitor"); }
            if (!_visitors.Contains(visitor)) { throw new EventException("Event - UnsubscribeVisitor"); }
            _visitors.Remove(visitor);
        }
    }
}