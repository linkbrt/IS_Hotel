using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1 {
    internal class DateRange {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateRange(DateTime start, DateTime end) {
            this.Start = start;
            this.End = end;
        }
/*        public List<DateTime> GetRange() {
            List<DateTime> range = new List<DateTime>();
            for (int i = 0; i < (this.EndDate - this.StartDate).Days; i++) {
                range.Add(this.StartDate + new TimeSpan(i, 0, 0, 0));
            }
            return range;
        }*/
        public bool Intersect(DateRange test) {
            if (this.Start == this.End || test.Start == test.End)
                return false; // No actual date range

            if (this.Start == test.Start || this.End == test.End)
                return true; // If any set is the same time, then by default there must be some overlap. 

            if (this.Start < test.Start) {
                if (this.End > test.Start && this.End < test.End)
                    return true; // Condition 1

                if (this.End > test.End)
                    return true; // Condition 3
            } else {
                if (test.End > this.Start && test.End < this.End)
                    return true; // Condition 2

                if (test.End > this.End)
                    return true; // Condition 4
            }

            return false;
        }
    }
}
