using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class TextUsernameInput : TextBaseInput
    {

        public TextUsernameInput(Vector3 position) : base(position)
        {
            GetComponent<TextInput>().Placeholder = "Username";
            GetComponent<TextInput>().PopupText = "Enter Username";
        }

    }
}