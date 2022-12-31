using System.Collections.Generic;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class TextInput : Entity
    {
        string text = "";

        public TextInput(Vector2 position)
        {
            id = System.Guid.NewGuid();
            name = "TextInput";
            
        }

        public override void HandleEvents(List<Event> events)
        {
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.KeyPress:
                        text += e.Key.ToString();
                        break;
                }
            }
        }

        public string GetText()
        {
            var temp = text;
            text = "";
            return temp;
        }
    }
}