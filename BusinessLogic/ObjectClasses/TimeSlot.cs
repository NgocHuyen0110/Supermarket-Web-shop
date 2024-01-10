using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ObjectClasses
{
    public class TimeSlot
    {
        [Required, DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        public TimeSlot(TimeSpan time)
        {
            Time = time;
        }

        public TimeSlot()
        {

        }
        public override string ToString()
        {
            return Time.ToString();
        }
    }
}
