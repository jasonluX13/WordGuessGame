using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordGuess
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to WordGuess");
            while (true)
            {
                PlayGame();
                Console.WriteLine("Would you like to play again?");
                if (Console.ReadLine().ToUpper() != "YES")
                {
                    break;
                }
                Console.Clear();
            }
           
        }

        /// <summary>
        /// Get a collection of phrases
        /// </summary>
        /// <returns>A List that contains the phrases</returns>
        private static List<string> GetPhrases()
        {
            string filename = @"C:\Users\jason\Documents\AppAcademy\Week2\WordGuess\WordGuess\Phrases.txt";
            List<string> phrases = new List<string>();
            if (File.Exists(filename))
            {
                var fileLines = File.ReadAllLines(filename);
                foreach(string line in fileLines)
                {
                    phrases.Add(line);
                }
            }
            else
            {
                Console.WriteLine("File not found. Using default phrases.");
                phrases.Add("Harry Potter and the Prisoner of Azkaban");
                phrases.Add("May the Force be with you");
                phrases.Add("There is no try");
                phrases.Add("A random phrase");
            }
            return phrases;

        }

        /// <summary>
        /// Selects a random phrase from a list
        /// </summary>
        /// <param name="phrases">List of phrases</param>
        /// <returns>The random phrase</returns>
        private static string SelectPhrase(List<string> phrases)
        {
            var random = new Random();
            int randomNum = random.Next(0, phrases.Count);
            return phrases[randomNum];
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        private static void PlayGame()
        {
            List<string> phrases = GetPhrases();
            string phrase = SelectPhrase(phrases);
            char[] phraseCharacters = GetPhraseCharacters(phrase);
            HashSet<char> phraseDistinctCharacters = GetPhraseDistinctCharacters(phraseCharacters);

            List<char> phraseGuessedCharacters = new List<char>();
            List<char> guessedCharacters = new List<char>();
            const int MaxGuesses = 5;
            int incorrectGuesses = 0;
            while(incorrectGuesses < MaxGuesses)
            {
                
                DisplayPhrase(phraseCharacters, phraseGuessedCharacters);
                char guessedcharacter = GetCharacterGuess(guessedCharacters);
                if (phraseDistinctCharacters.Contains(guessedcharacter))
                {
                    Console.WriteLine("Your guess was correct");
                    phraseGuessedCharacters.Add(guessedcharacter);
                    if (phraseGuessedCharacters.Count == phraseDistinctCharacters.Count)
                    {
                        break;
                    }
                }
                else
                {
                    incorrectGuesses++;
                    Console.Beep(350, 500);
                    Console.WriteLine($"Your guess was incorrect. You have {MaxGuesses - incorrectGuesses} guesses left.");

                }
            }
            if (incorrectGuesses == MaxGuesses)
            {
                Console.WriteLine("You Lost");
            } 
            else
            {
                Console.Beep(650, 500);
                Console.Beep(750, 300);
                Console.Beep(850, 500);
                Console.WriteLine("You Won!");
                DisplayPhrase(phraseCharacters, phraseGuessedCharacters);
            }

        }

        /// <summary>
        /// Get the individual characters from a phrase
        /// </summary>
        /// <param name="phrase">The input phrase</param>
        /// <returns>An array of char values</returns>
        private static char[] GetPhraseCharacters(string phrase)
        {
            phrase = phrase.ToUpper();
            return phrase.ToCharArray();
        }

        /// <summary>
        /// Get the distinct characters from a char[]
        /// </summary>
        /// <param name="phraseCharacters">The array of chars of the phrase</param>
        /// <returns></returns>
        private static HashSet<char> GetPhraseDistinctCharacters(char[] phraseCharacters)
        {
            HashSet<char> distinctPhraseCharacters = new HashSet<char>();
            foreach(char c in phraseCharacters)
            {
                if (c != ' ')
                {
                    distinctPhraseCharacters.Add(c);
                }
            }
            return distinctPhraseCharacters;
        }

        /// <summary>
        /// Display the phrase
        /// </summary>
        /// <param name="phraseCharacters">Array of phrase characters</param>
        /// <param name="phraseGuessedCharacters">List of guessed characters</param>

        private static void DisplayPhrase(char[] phraseCharacters, List<char> phraseGuessedCharacters)
        {
            foreach(char c in phraseCharacters)
            {
                if (phraseGuessedCharacters.Contains(c) || c == ' ')
                {
                    Console.Write(c);
                }
                else
                {
                    Console.Write('X');
                }
            }
            Console.Write('\n');
        }

        /// <summary>
        /// Get the guess from the player 
        /// </summary>
        /// <param name="guessedCharacters">A list of already guessed characters</param>
        /// <returns>The character selected</returns>
        private static char GetCharacterGuess(List<char> guessedCharacters)
        {
            Console.Write("Guess a character: ");
            char guess = Console.ReadKey().KeyChar;
            Console.WriteLine("");
            guess = char.ToUpper(guess);
            while(guess < 'A' || guess > 'Z')
            {
                Console.WriteLine("Please enter a letter between A-Z");
                guess = Console.ReadKey().KeyChar;
                guess = char.ToUpper(guess);
                Console.WriteLine("");


            }
            while (guessedCharacters.Contains(guess))
            {
                Console.WriteLine("You have already guessed this letter. Guess another letter");
                guess = Console.ReadKey().KeyChar;
                guess = char.ToUpper(guess);
                Console.WriteLine("");

            }
            guessedCharacters.Add(guess);
            return guess;
        }
    }
}
