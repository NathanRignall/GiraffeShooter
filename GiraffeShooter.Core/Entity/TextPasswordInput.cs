using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class TextPasswordInput : TextBaseInput
    {

        public TextPasswordInput(Vector3 position) : base(position)
        {
            GetComponent<TextInput>().Placeholder = "Password";
            GetComponent<TextInput>().PopupText = "Enter Password";
            GetComponent<TextInput>().IsPassword = true;
        }

    }
}
