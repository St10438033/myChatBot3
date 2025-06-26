using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.ML;
using Microsoft.ML.Data;


namespace myChatBot3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //quiz game

        //List [generics]
        private List<QuizQuestion> quizData;

        //variables
        private int questionIndex = 0;
        private int currentScore = 0;

        //buttons
        private Button selectedChoice = null;
        private Button correctChoiceButton = null;


        //npl training data
        // ML.NET context
        private readonly MLContext mlContext;

        // List to store training data dynamically
        private List<SentimentData> trainingData;

        // Prediction engine
        private PredictionEngine<SentimentData, SentimentPrediction> predEngine;

        // Sentiment input data class
        private class SentimentData
        {
            public string Text { get; set; }
            public bool Label { get; set; }
        }

        // Sentiment prediction result class
        private class SentimentPrediction
        {
            [ColumnName("PredictedLabel")]
            public bool Prediction { get; set; }
            public float Probability { get; set; }
            public float Score { get; set; }
        }


        //program/ Chatbot main class and code exe start
        public MainWindow()
        {
            InitializeComponent();


            //chatbot interactions in listview
            new backendChatbot();

            //play sound 
            new greet_sound();

            
            //mini game
            LoadQuizData();

            showQuiz();

            //npl data and initialize context
            mlContext = new MLContext();

            // Initialize with base training data
            trainingData = new List<SentimentData>
            {
                new SentimentData { Text = "I am happy", Label = true },
                new SentimentData { Text = "I hate this", Label = false },
                new SentimentData { Text = "I am sad", Label = false },
                new SentimentData { Text = "I am good", Label = true }
            };

            TrainModel();


        }

        //open chat method
        private void open_chat(object sender, RoutedEventArgs e)
        {
            //hidding the page minigame grid
            mini_game_page.Visibility = Visibility.Hidden;
            chat_page.Visibility = Visibility.Visible;

            new backendChatbot();
        }

        //open mini game method
        private void open_mini_game(object sender, RoutedEventArgs e)
        {

            //hidding the page open chat
            mini_game_page.Visibility = Visibility.Visible;
            chat_page.Visibility = Visibility.Hidden;

            LoadQuizData();
            showQuiz();

        }
            private void showQuiz() { 

            //check if the user is not done playing
            if (questionIndex >= quizData.Count)
            {
                //show complete message
                MessageBox.Show("You already completed the game with " + currentScore + " score");
                //then reset the game
                currentScore = 0;
                currentScore = 0;
                questionIndex = 0;

                displayScore.Text = "";

                
                //stop the execute
                return;
            }

            //get the current index quiz
            correctChoiceButton = null;
            selectedChoice = null;

            //then get all the questions values
            var currentQuiz = quizData[questionIndex];

            //displays the question to the user
            displayQuestion.Text = currentQuiz.Question;

            //add the choices to the buttons
            var shuffled = currentQuiz.Choices.OrderBy(_ => Guid.NewGuid()).ToList();

            //then add by index
            firstChoice.Content = shuffled[0];
            secondChoice.Content = shuffled[1];
            thirdChoice.Content = shuffled[2];

            //correct one
            forthChoice.Content = currentQuiz.CorrectChoice;

            clearStyle();

            }

            private void clearStyle()
            {
                //use for each to reset
                foreach (Button choice in new[] { firstChoice, secondChoice,thirdChoice, forthChoice })
                {

                    choice.Background = Brushes.LightGray;


                }

            }//end of the clear style method

            //method to load the quiz data
            private void LoadQuizData()
            {
            //store info
            // Store info
            quizData = new List<QuizQuestion> {
                new QuizQuestion
                {
                    Question = "What is cyber security?",
                    CorrectChoice = "being safe online",
                    Choices = new List<string>
                    {
                        "being safe online", "maintaining short password", "using internet platforms securely"
                }
                },
                new QuizQuestion
                {
                    Question = "What is ph?",
                    CorrectChoice = "27th June",
                    Choices = new List<string>
                    {
                         "27th May", "27th Dec", "27th Jan"
                }
                },
                new QuizQuestion
                {
                    Question = "What does VPN stand for?",
                    CorrectChoice = "Virtual Private Network",
                    Choices = new List<string>
                    {
                        "Virtual Private Network", "Very Private Network", "Variable Private Network"
                }
                },
                new QuizQuestion
                {
                    Question = "What is phishing?",
                    CorrectChoice = "Fraudulent attempts to obtain sensitive information",
                    Choices = new List<string>
                    {
                        "A type of malware", "Fraudulent attempts to obtain sensitive information", "A secure connection"
                }
                },
                new QuizQuestion
                {
                    Question = "What is a firewall?",
                    CorrectChoice = "A security system that monitors and controls incoming and outgoing network traffic",
                    Choices = new List<string>
                    {
                        "A type of virus", "A security system that monitors and controls incoming and outgoing network traffic", "A software for browsing"
                }
                },
                new QuizQuestion
                {
                    Question = "What is two-factor authentication?",
                    CorrectChoice = "An extra layer of security",
                    Choices = new List<string>
                    {
                        "An extra layer of security", "A type of password", "A software update"
                }
                },
                new QuizQuestion
                {
                    Question = "What is malware?",
                    CorrectChoice = "Malicious software designed to harm or exploit any programmable device",
                    Choices = new List<string>
                    {
                        "Malicious software designed to harm or exploit any programmable device", "A type of antivirus", "A secure software"
                }
                },
                new QuizQuestion
                {
                    Question = "What is a strong password?",
                    CorrectChoice = "A password that is hard to guess",
                    Choices = new List<string>
                    {
                        "A password that is hard to guess", "A password with only numbers", "A password with your name"
                }
                },
                new QuizQuestion
                {
                    Question = "What is social engineering?",
                    CorrectChoice = "Manipulating people to gain confidential information",
                    Choices = new List<string>
                    {
                        "Manipulating people to gain confidential information", "A type of software", "A network protocol"
                }
                },
                new QuizQuestion
                {
                    Question = "What is encryption?",
                    CorrectChoice = "Converting information into a code to prevent unauthorized access",
                    Choices = new List<string>
                    {
                        "Converting information into a code to prevent unauthorized access", "A type of firewall", "A method of data storage"
                }
                },
                new QuizQuestion
                {
                    Question = "What is a data breach?",
                    CorrectChoice = "Unauthorized access to confidential data",
                    Choices = new List<string>
                    {
                        "Unauthorized access to confidential data", "A type of software update", "A network connection"
                }
                }//end of second question, put , to add another one
                };


            }//end of the method load quiz data or info




            //answer selected for quiz handler
            private void answer_selected(object sender, RoutedEventArgs e)
            {

            //use sender object name to get the selected button
            selectedChoice = sender as Button;

            string chosen = selectedChoice.Content.ToString();

            //then check with correct on the current quiz
            string correct = quizData[questionIndex].CorrectChoice;

            //then check if correct or not by if statement
            if (chosen == correct)
            {
                //then set the button background color
                selectedChoice.Background = Brushes.Green;
                //assing to hold
                correctChoiceButton = selectedChoice;
            }
            else
            {
                //if incorrect
                selectedChoice.Background = Brushes.DarkRed;
                correctChoiceButton = selectedChoice;
            }

            }

            //next question in quiz button
            private void next_question(object sender, RoutedEventArgs e)
            {

            //check if the user selected one of the choices
            if (selectedChoice == null)
            {
                //then show error message
                MessageBox.Show("Choose one of the 4 choices");
            }
            else
            {
                //then add points , and only if correct
                string chosen = selectedChoice.Content.ToString();
                string correct = quizData[questionIndex].CorrectChoice;

                //check if correct 
                if (chosen == correct)
                {
                    //then add point
                    currentScore++;
                    //then show the score
                    displayScore.Text = "Score : " + currentScore;

                    //then move to the next index question
                    questionIndex++;
                    //show the question again for the next one
                    showQuiz();
                }
                else
                {
                    //move to the next question 
                    questionIndex++;
                    showQuiz();
                }

            }
            }


        //open add task method
        private void add_task(object sender, RoutedEventArgs e)
        {

            //logic for ading task

            //collect what the user enter
            string collect_question = questions.Text.ToString();

            //validate or check if the user have entered something
            if (!collect_question.Equals(""))
            {

                //check if the user want to add the task
                if (collect_question.ToLower().Contains("add task"))
                {

                    //add the task to the list view but get date and time
                    DateTime date = DateTime.Now.Date;
                    DateTime time = DateTime.Now.ToLocalTime();


                    //set the format for the date
                    string format_date = date.ToString("yyyy-MM-dd");

                    //then add to the list
                    show_chats.Items.Add("User : " + collect_question + "\n" + format_date + " Time " + time);

                    //set list view to auto scroll
                    show_chats.ScrollIntoView(show_chats.Items[show_chats.Items.Count - 1]);

                }


            }
            else
            {
                //error message
                MessageBox.Show("Question field is required!!!");
            }

        }

        private void show_chats_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            //get the selected item from the list view
            string selected_task = show_chats.SelectedItem.ToString();


            //check if the task is not marked done
            if (!selected_task.Contains("status done"))
            {

                //get the index of the selected item
                int getIndex = show_chats.Items.IndexOf(selected_task);

                //edit the selected item to be marked done
                show_chats.Items[getIndex] = selected_task + " status done";

            }
            else
            {
                //then remove if marked done

                //get the index

                show_chats.Items.Remove(selected_task);

            }

        }

        //open activity log method
        private void activty_log(object sender, RoutedEventArgs e)
        {

        }

        //send questions method
        private void send(object sender, RoutedEventArgs e)
        {

            


        }

        //exit chatbot method
        private void open_exit(object sender, RoutedEventArgs e)
        {
            //exit the program when button is clicked
            System.Environment.Exit(0);

        }




        //npl training for chatbot

        // Method to train/retrain the model
        private void TrainModel()
        {
            var trainDataView = mlContext.Data.LoadFromEnumerable(trainingData);

            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentData.Text))
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));

            var model = pipeline.Fit(trainDataView);

            predEngine = mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(model);
        }


        private void emotions(object sender, RoutedEventArgs e)
        {

            //npl training
            
            string input = questions.Text;

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Please enter how you are feeling");
                return;
            }

            var prediction = predEngine.Predict(new SentimentData { Text = input });

            // Confidence percentages
            float positiveScore = prediction.Probability * 100;
            float negativeScore = 100 - positiveScore;

            string emotionType = prediction.Prediction ? "Positive" : "Negative";

            // Construct feedback message
            string feedback = $"{emotionType} Emotion\n" +
                              $"Positive: {positiveScore:F1}%\n" +
                              $"Negative: {negativeScore:F1}%\n";

            string reply;
            if (positiveScore > 75)
            {
                reply = "You seem really upbeat! Keep shining ";
            }
            else if (positiveScore > 50)
            {
                reply = "You’re doing alright — keep your chin up!";
            }
            else if (positiveScore > 30)
            {
                reply = "I sense some heaviness — it’s okay to feel down sometimes.";
            }
            else
            {
                reply = "You seem quite low. Be kind to yourself — brighter days will come.";
            }
            show_emotion_detected.Text = feedback + reply;

            // Ask user to confirm if prediction was correct
            var result = MessageBox.Show("Was this prediction correct?", "Feedback", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                // Ask what the correct label was
                var correct = MessageBox.Show("Was it actually Positive?", "Correct Label", MessageBoxButton.YesNo);
                bool correctLabel = (correct == MessageBoxResult.Yes);

                // Add to training data and retrain model
                trainingData.Add(new SentimentData { Text = input, Label = correctLabel });
                TrainModel();

                MessageBox.Show("Thanks! I've learned from that.");
            }
        }

    }
    
}
