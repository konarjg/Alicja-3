using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Alicja
{
    public partial class MainPage : ContentPage
    {
        private static Random Random = new Random();
        private static List<string> Messages = new List<string>();
        private static Queue<string> UsedMessages = new Queue<string>();
        private static int MessagesDisplayed = 0;

        private static MainPage Instance;

        public MainPage()
        {
            InitializeComponent();
            Instance = this;
        }

        public static void Init(List<string> messages)
        {
            Messages = messages;
            DisplayMessage();
        }

        private static void DisplayMessage()
        {
            if (Messages.Count == 0)
                return;

            var message = Messages[Random.Next(0, Messages.Count)];

            Instance.MessageUI.Text = message;
            Messages.Remove(message);
            UsedMessages.Enqueue(message);
            ++MessagesDisplayed;
        }

        public static Queue<string> GetUsedMessages()
        {
            return UsedMessages;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (MessagesDisplayed == 3)
            {
                for (int i = 0; i < 3; ++i)
                {
                    var message = UsedMessages.Dequeue();
                    Messages.Add(message);
                }

                MessagesDisplayed = 0;
            }

            DisplayMessage();
        }
    }
}
