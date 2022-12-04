using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBL.Model {
    public class Visitor {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Visitor(string name, DateTime birthDay) {
            SetName(name);
            BirthDay = birthDay;
        }
        public Visitor(int id, string name, DateTime birthDay) : this (name, birthDay){
            SetId(id);
        }
        public Visitor() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public int Id { get; set; }
        public string Name { get; set; } // Hij zaagde hier bij private set, hij gebruikt lege
        public DateTime BirthDay { get; set; }

        public bool IsDifferent(Visitor visitor) {
            if (!Name.Equals(visitor.Name)) { return true; }
            if (!BirthDay.Equals(visitor.BirthDay)) { return true; }
            if (!Id.Equals(visitor.Id)) { return true; }
            return false;
        }
        public override bool Equals(object? obj) {
            return obj is Visitor visitor &&
                   Id == visitor.Id;
        }

        public override int GetHashCode() {
            return HashCode.Combine(Id, Name, BirthDay);
        }

        public void SetId(int id) {
            if (id <= 0) {
                throw new EventException("Visitor - SetId - id kleiner dan 0");
            }
            Id = id;
        }
        public void SetName(string name) {
            if (string.IsNullOrWhiteSpace(name)) {
                throw new EventException("Visitor - SetName - Invalid name");
            }
            Name = name;
        }
    }
}
