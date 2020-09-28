using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiHatWPF.Enums;
using Newtonsoft.Json;

namespace PiHatWPF.Model
{
    class JoystickModel
    {
        public SenseTickActions Action { get; set; }
        public SenseTickDirections Direction { get; set; }

        public JoystickModel()
        {

        }

        [JsonConstructor]
        public JoystickModel(string action, string direction)
        {
            Action = EnumParser.ParseEnum<SenseTickActions>(action);
            Direction = EnumParser.ParseEnum<SenseTickDirections>(direction);
        }
    }
}
