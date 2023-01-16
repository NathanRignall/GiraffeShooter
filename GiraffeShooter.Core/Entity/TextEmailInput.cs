using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class TextEmailInput : TextBaseInput
    {

        public TextEmailInput(Vector3 position) : base(position)
        {
            GetComponent<TextInput>().Placeholder = "Email";
            GetComponent<TextInput>().PopupText = "Enter Email";
        }

    }
}