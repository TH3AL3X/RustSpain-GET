using System;
using System.Text.RegularExpressions;
using System.Threading;
using Discord;
using Discord.Gateway;
using TextCopy;
namespace MessageLogger
{
    internal class Program
    {
        public static string token()
        {
            string token = File.ReadAllText("token.txt");
            if (token.Replace(" ", "") == "")
            {
                using (StreamWriter writer = new StreamWriter("token.txt"))
                {
                    token = File.ReadAllText("token.txt");
                }
            }
            return token;
        }

        private static void Main()
        {
            var client = new DiscordSocketClient();
            client.OnLoggedIn += OnLoggedIn;
            client.OnMessageReceived += OnMessageReceived;

            client.Login(token());

            Thread.Sleep(-1);
        }

        private static void OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            Console.Title = "RustSpain /get";
        }

        private static void OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
        {
            DiscordChannel channel = client.GetChannel(args.Message.Channel.Id);

            if(channel.Id == 599043295989071872 && channel.InGuild)
            {
                string mensaje = args.Message.Content;

                string mensaje_extrapolado = mensaje.Replace(":star: ¡El evento del Servidor x5 ha empezado! Escribe `", "");

                string mensaje_copiado = "";

                for (int i = 0; i < mensaje_extrapolado.Length; i++)
                {
                    if (i == 37) break;

                    mensaje_copiado += mensaje_extrapolado[i];
                }

                if (mensaje_copiado.Contains("/get"))
                {
                    Console.WriteLine("[+] Copiado en el porta-papeles: " + mensaje_copiado);

                    Console.Beep();
                    Clipboard clipboard = new();
                    clipboard.SetText(mensaje_copiado);
                }
            }
        }
    }
}