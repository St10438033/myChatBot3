namespace myChatBot3
{
    public class backendChatbot
    {

        // Dictionary for storing backend responses and not-respond-to questions
        private Dictionary<string, string> response = new Dictionary<string, string>();
        private Dictionary<string, string> not_respond = new Dictionary<string, string>();
        private Dictionary<string, string> sentimentValues = new Dictionary<string, string>();

        public void chatbot_backend()
        {
            // Constructors that hold/store/add responses and not-respond-to questions/topics and answers
            chatbot_Not_Respond();
            chatbot_Response();
            sentiment_values();
        }

        // possible responses
        private void chatbot_Response()
        {
            response.Add("phishing", "Phishing is a type of cyber attack where attackers impersonate legitimate organizations or individuals to deceive victims into providing sensitive information, such as passwords, credit card numbers, or personal details.");
            response.Add("suspect phishing", "If you suspect a phishing attempt, do not click on any links or download attachments. Take a moment to assess the situation.");
            response.Add("browsing", "Safe browsing involves taking precautions while navigating the internet to protect personal information and avoid cyber threats like malware and phishing. To ensure security, use updated browsers, enable security features, avoid suspicious links, and regularly check for website authenticity.");
            response.Add("cyber attack", "A cyber attack is an intentional attempt to breach the security of a computer system or network, often to steal, alter, or destroy data. Five common types of cyber attacks include malware, phishing, denial-of-service (DoS) attacks, man-in-the-middle attacks, and SQL injection.");
            response.Add("password safety", "Avoid reusing passwords across different accounts to minimize the risk of multiple accounts being compromised if one password is leaked.");
            response.Add("password tip", "Change your passwords periodically and immediately update them if you suspect any compromise. This helps to mitigate risks associated with potential breaches.");
            response.Add("tips on passwords", "Use 2FA wherever possible to add an extra layer of security to your accounts, making it harder for unauthorized users to gain access.");
            response.Add("password", "Create complex passwords that include a mix of uppercase letters, lowercase letters, numbers, and special characters. Aim for at least 12 characters.");
            response.Add("safe browsing", "Always look for \"https://\" in the URL and a padlock icon in the address bar before entering personal information. This indicates a secure connection.");
            response.Add("phishing tip", "Avoid clicking on links or downloading attachments from unknown or suspicious emails. Verify the sender's email address before taking any action.");
        }

        // possible not responses
        private void chatbot_Not_Respond()
        {
            not_respond.Add("hello", " ");
            not_respond.Add("help", " ");
            not_respond.Add("thanks", " ");
            not_respond.Add("bye", " ");
            not_respond.Add("exit", " ");
            not_respond.Add("quit", " ");
        }

        // sentiment values/responses
        private void sentiment_values()
        {
            sentimentValues.Add("sad", "I'm sorry to hear that you're feeling sad. Stay calm. ");
            sentimentValues.Add("mad", "I understand that you're feeling mad. Stay calm. ");
            sentimentValues.Add("worried", "It's okay to feel worried sometimes. Stay calm. ");
            sentimentValues.Add("bad", "I'm here to help you feel better. Stay calm. ");
            sentimentValues.Add("frustrated", "I can see that you're frustrated. Stay calm. ");
            sentimentValues.Add("lost", "It's normal to feel lost at times. Stay calm. ");
            sentimentValues.Add("confused", "Let me help clarify things for you. Stay calm. ");
        }

        // interaction method
        public void chatbot_frontend()
        {
            try
            {
                // variables
                string user_name;
                string user_question;

                // This is the chatbot frontend experience
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("|| ================================================================================= ||" + "\n");

                Thread.Sleep(2000); // control for response time
                Console.WriteLine("|| Chatbot : Hello, Welcome to ProCyber Security ChatBot ||" + "\n");

                Thread.Sleep(3000); // control for response time
                Console.WriteLine("|| This Pro Cyber Security Chatbot will assist and advise you on Safe browsing and other Cyber Security related challenges ||");

                Console.WriteLine("|| =================================================================================================================== ||");

                Thread.Sleep(3000); // control for response time
                Console.WriteLine("|| Chatbot : What is your name ? ||" + "\n");

                Console.ForegroundColor = ConsoleColor.Green;
                user_name = Console.ReadLine();

                Console.WriteLine("|| =============================================================================================== ||" + "\n");

                Thread.Sleep(2500); // control for response time
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("|| Chatbot : " + user_name + ", Welcome to PRO CYBER SECURITY CHATBOT !!! ||" + "\n");

                Console.WriteLine("|| =============================================================================================== ||" + "\n");

                bool continueChat = true;

                while (continueChat) // Continuous interaction loop


                {

                    Thread.Sleep(3000); // control for response time
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("|| Chatbot : " + user_name + ", How can I assist you today? (Type 'exit' to quit). ||");

                    Console.WriteLine("|| =============================================================================================== ||" + "\n");


                    Console.ForegroundColor = ConsoleColor.Green;
                    user_question = Console.ReadLine();
                    Console.WriteLine("|| =============================================================================================== ||" + "\n");


                    if (string.IsNullOrEmpty(user_question))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("|| Chatbot : Please enter a valid question or type 'exit' to quit. ||" + "\n");
                        Console.WriteLine("|| ======================================================================================================== ||");

                        continue;
                    }

                    if (user_question == "exit" || user_question == "quit" || user_question == "bye")
                    {
                        continueChat = false;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("|| Chatbot : Goodbye, " + user_name + "! Thank you for using ProCyber Security ChatBot. ||" + "\n");
                        Console.WriteLine("|| ======================================================================================================== ||");
                        break;
                    }

                    if (user_question.Equals("history", StringComparison.OrdinalIgnoreCase))
                    {
                        
                        continue;
                    }

                    // Sentiment detection
                    string sentimentResponse = DetectSentiment(user_question);
                    if (!string.IsNullOrEmpty(sentimentResponse))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("|| Chatbot : " + sentimentResponse + " How can I assist you ? ||" + "\n");
                        Console.WriteLine("|| =============================================================================================== ||" + "\n");

                        Console.ForegroundColor = ConsoleColor.Green;
                        user_question = Console.ReadLine();
                        Console.WriteLine("|| =============================================================================================== ||" + "\n");


                    }

                    // Filter out ignore words
                    string[] words = user_question.Split(' ');
                    List<string> filteredWords = new List<string>();
                    foreach (var word in words)
                    {
                        if (!not_respond.ContainsKey(word))
                        {
                            filteredWords.Add(word);
                        }
                    }

                    bool foundResponse = false;

                    foreach (var word in filteredWords)
                    {
                        foreach (var key in response.Keys)
                        {
                            if (key.Contains(word))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("|| Chatbot : " + response[key] + " Need to ask about Cyber Security, Search below.||" + "\n");

                                Console.ForegroundColor = ConsoleColor.Green;
                                user_question = Console.ReadLine();
                                Console.WriteLine("|| =============================================================================================== ||" + "\n");


                                //creating an instance for the random class
                                Random random = new Random();

                                // Picking a random index from the response keys
                                int randomIndex = random.Next(0, response.Count);

                                string randomKey = response.Keys.ElementAt(randomIndex);

                                string randomResponse = response[randomKey];

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("|| Chatbot : " + randomResponse + " Need to ask about Cyber Security, Search below.||" + "\n");
                                Console.WriteLine("|| =============================================================================================== ||" + "\n");


                                Console.ForegroundColor = ConsoleColor.Green;
                                user_question = Console.ReadLine();
                                Console.WriteLine("|| =============================================================================================== ||" + "\n");

                                if (user_question.Equals("history", StringComparison.OrdinalIgnoreCase))
                                {

                                    Console.WriteLine("|| =============================================================================================== ||" + "\n");

                                    continue;
                                }

                                foundResponse = true;
                                break;
                            }
                        }

                    }

                    if (!foundResponse)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("|| Chatbot : " + user_name + ", Please search something related to Cyber Security e.g. Password Safety, Phishing or Safe Browsing.");
                    }

                    Console.WriteLine("|| =============================================================================================== ||" + "\n");

                    Console.ForegroundColor = ConsoleColor.Green;
                    user_question = Console.ReadLine();

                    Console.WriteLine("|| =============================================================================================== ||" + "\n");


                    if (user_question.Equals("history", StringComparison.OrdinalIgnoreCase))
                    {

                    }
                } //end while


                Console.ResetColor();


            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred: " + ex.Message);
                Console.ResetColor();
            }
        }

        private string DetectSentiment(string userInput)
        {
            // Basic sentiment detection by checking if any sentiment keyword appears in user input
            foreach (var key in sentimentValues.Keys)
            {
                if (userInput.Contains(key))
                {
                    return sentimentValues[key];
                }
            }
            return string.Empty;
        }
    }


}







