namespace CyberKnight
{
    using System;
    using System.Media;
    using System.Threading;

    class CyberKnight
    {
        static void Main(string[] args)
        {
            // Attempt to play a welcome sound
            try
            {
                SoundPlayer player = new SoundPlayer("media\\welcome.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to play welcome sound: " + ex.Message);
            }

            // Display a stylized ASCII art banner
            DisplayAsciiArt();

            // Start user interaction by greeting and requesting their name
            GreetUser();
        }

        // Displays ASCII logo/banner in blue color
        static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(@"

   ______      __              __ __       _       __    __ 
  / ____/_  __/ /_  ___  _____/ //_/____  (_)___ _/ /_  / /_
 / /   / / / / __ \/ _ \/ ___/ ,<  / __ \/ / __ `/ __ \/ __/
/ /___/ /_/ / /_/ /  __/ /  / /| |/ / / / / /_/ / / / / /_  
\____/\__, /_.___/\___/_/  /_/ |_/_/ /_/_/\__, /_/ /_/\__/  
     /____/                              /____/             

        Cybersecurity Awareness Assistant
");
            Console.ResetColor();
            Console.WriteLine();
        }

        // Greets the user, prompts for their name, and starts conversation
        static void GreetUser()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("__________________________________________");
            Console.WriteLine("|  Welcome to CyberKnight - Your trusted |");
            Console.WriteLine("|  cybersecurity awareness assistant!    |");
            Console.WriteLine("|________________________________________|");
            Console.ResetColor();
            Console.WriteLine();

            // Ask for user name
            Console.Write("Before we begin, what should I call you? ");
            Console.ForegroundColor = ConsoleColor.Green;
            string userName = Console.ReadLine();
            Console.ResetColor();

            // Prompt again if name is empty or whitespace
            while (string.IsNullOrWhiteSpace(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("I didn't catch your name. Could you please tell me again?");
                Console.ResetColor();
                Console.Write("What should I call you? ");
                Console.ForegroundColor = ConsoleColor.Green;
                userName = Console.ReadLine();
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Hello, {userName}! I'm CyberKnight, here to help you stay safe online.");
            Console.ResetColor();
            Console.WriteLine();

            // Begin interactive Q&A session
            StartConversation(userName);
        }

        // Starts an interactive session with the user
        static void StartConversation(string userName)
        {
            bool continueChat = true;

            // Display available options
            DisplayHelp();

            while (continueChat)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{userName}, what would you like to know about? (type 'help' for the menu or 'exit' to quit): ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                string userInput = Console.ReadLine()?.ToLower() ?? string.Empty;
                Console.ResetColor();
                Console.WriteLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("I didn't quite catch that. Could you please ask again?");
                    Console.ResetColor();
                    continue;
                }

                if (userInput == "exit")
                {
                    // End session gracefully
                    continueChat = false;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Goodbye, {userName}! Stay safe online!");
                    Console.ResetColor();
                    continue;
                }

                if (userInput.Contains("help") || userInput.Contains("options"))
                {
                    // Redisplay help menu
                    DisplayHelp();
                    continue;
                }

                // Process user query
                ProcessUserInput(userInput, userName);
            }
        }

        // Displays list of topics user can ask about
        static void DisplayHelp()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("__________________________________________");
            Console.WriteLine("|          Available Topics              |");
            Console.WriteLine("|________________________________________|");
            Console.WriteLine("| • Passwords      • Phishing              |");
            Console.WriteLine("| • Browsing       • Social Engineering    |");
            Console.WriteLine("| • Malware        • Privacy               |");
            Console.WriteLine("| • How are you?   • What's your purpose?  |");
            Console.WriteLine("| • What can I ask you about?             |");
            Console.WriteLine("|________________________________________|");
            Console.ResetColor();
            Console.WriteLine();
        }

        // Responds based on user input topic
        static void ProcessUserInput(string input, string userName)
        {
            bool responseFound = true;

            Console.ForegroundColor = ConsoleColor.White;

            // Respond with preset cybersecurity info based on keywords
            if (input.Contains("how are you"))
            {
                TypeWriterEffect("I'm doing GREAT! I'm just a program after all, and I'm here to help you with cybersecurity!");
            }
            else if (input.Contains("purpose") || input.Contains("why do you exist"))
            {
                TypeWriterEffect("My purpose is to educate people like you about cybersecurity threats and best practices to stay safe online.");
            }
            else if (input.Contains("what can i ask") || input.Contains("topics"))
            {
                TypeWriterEffect("You can ask me about various cybersecurity topics including:");
                Console.WriteLine("- Creating strong passwords");
                Console.WriteLine("- Identifying phishing attempts");
                Console.WriteLine("- Safe browsing practices");
                Console.WriteLine("- Recognizing social engineering");
                Console.WriteLine("- Protecting against malware");
                Console.WriteLine("- Maintaining online privacy");
            }
            else if (input.Contains("password"))
            {
                TypeWriterEffect("Strong passwords are your first line of defense:");
                Console.WriteLine("- Use at least 12 characters");
                Console.WriteLine("- Combine letters, numbers, and symbols");
                Console.WriteLine("- Avoid common words or personal information");
                Console.WriteLine("- Use a unique password for each account");
                Console.WriteLine("- Consider using a password manager");
            }
            else if (input.Contains("phish"))
            {
                TypeWriterEffect("Phishing is when attackers try to trick you into revealing sensitive information:");
                Console.WriteLine("- Be wary of unsolicited emails/messages");
                Console.WriteLine("- Check sender addresses carefully");
                Console.WriteLine("- Don't click on suspicious links");
                Console.WriteLine("- Look for poor grammar and/or spelling");
                Console.WriteLine("- When in doubt, contact the organization directly");
            }
            else if (input.Contains("brows") || input.Contains("internet"))
            {
                TypeWriterEffect("Safe browsing practices:");
                Console.WriteLine("- Look for 'https://' and a padlock icon");
                Console.WriteLine("- Keep your browser updated");
                Console.WriteLine("- Use reputable browser extensions");
                Console.WriteLine("- Clear cookies/cache regularly");
                Console.WriteLine("- Be cautious with public Wi-Fi (use VPN if possible)");
            }
            else if (input.Contains("malware"))
            {
                TypeWriterEffect("Protecting against malware:");
                Console.WriteLine("- Install reputable antivirus software");
                Console.WriteLine("- Keep your operating system updated");
                Console.WriteLine("- Don't download files from untrusted sources");
                Console.WriteLine("- Be cautious with email attachments");
                Console.WriteLine("- Regularly back up your important files");
            }
            else if (input.Contains("privacy"))
            {
                TypeWriterEffect("Maintaining online privacy:");
                Console.WriteLine("- Review privacy settings on social media");
                Console.WriteLine("- Be careful what personal info you share online");
                Console.WriteLine("- Use private browsing when appropriate");
                Console.WriteLine("- Consider using privacy-focused search engines");
                Console.WriteLine("- Regularly check app permissions on your devices");
            }
            else if (input.Contains("social") || input.Contains("engineer"))
            {
                TypeWriterEffect("Social engineering tricks people into breaking security procedures:");
                Console.WriteLine("- Be skeptical of urgent requests for information");
                Console.WriteLine("- Verify identities before sharing sensitive info");
                Console.WriteLine("- Don't trust caller ID because it can be spoofed");
                Console.WriteLine("- Be cautious of 'too good to be true' offers");
                Console.WriteLine("- Educate yourself about common tactics");
            }
            else
            {
                // Input didn't match any known keywords
                responseFound = false;
            }

            if (!responseFound)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypeWriterEffect("I'm not sure I understand. Could you try asking about:");
                Console.WriteLine("- Passwords");
                Console.WriteLine("- Phishing");
                Console.WriteLine("- Safe browsing");
                Console.WriteLine("- Or ensure your spelling is correct");
                Console.WriteLine("- Type 'help' for more options");
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        // Simulates typing effect for text display
        static void TypeWriterEffect(string text, int delay = 30)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay); // Delay between characters
            }
            Console.WriteLine();
        }
    }
}
