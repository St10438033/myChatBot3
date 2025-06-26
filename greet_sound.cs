using System.Windows.Media;

namespace myChatBot3
{
    public class greet_sound
    {
        private void greeting_sound()
        {

            //creating an instance for the media class
            MediaPlayer player = new MediaPlayer();

            //linking the path with the sound player
            //uri finds path and allows code to play/open different files
            player.Open(new Uri("C:\\Users\\RC_Student_lab\\source\\repos\\myChatBot3\\sound.wav", UriKind.Relative));

            //playing the sound
            player.Play();
        }
    }
}