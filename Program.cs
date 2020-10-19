using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab7_PigLatin
{
    class Program
    {
        static void Main(string[] args)
        {
            // declaration and initialization
            string userInput, inputYN, translatedWord, translatedWordCase;
            string[] userInputSplit;
            char firstLetter;
            char[] userInputCharArray;
            bool runAgain = true;
            List<int> uppercaseIndexes = new List<int>();
            List<string> inputTranslated = new List<string>();

            // welcome message
            Console.WriteLine("Welcome to the Pig Latin translator!");
            Console.WriteLine();
            
            // main loop that runs as long as user selects y when prompted
            while (runAgain)
            {
                Console.Write("Enter a line to be translated: ");
                userInput = Console.ReadLine();
                userInputSplit = userInput.Split();

                foreach(string word in userInputSplit)
                {
                    if(word.Length == 1)
                    {
                        inputTranslated.Add(word);
                        continue;
                    }
                    foreach(char c in word)
                    {
                        IsAVowel(c);
                    }
                    //if (!word.Any(Char.IsSymbol))
                    //{
                    //    Console.WriteLine("not symbol");
                    //}

                    // storing the index of uppercase chars (for added challenge provided by Stephen)                    
                    userInputCharArray = word.ToCharArray();
                    uppercaseIndexes = IndexUppercase(userInputCharArray);

                    // set word to be translated to lowercase
                    string lowerWord = word.ToLower();

                    // sets the first character of string to variable
                    firstLetter = lowerWord[0];

                    // checking if first character is a vowel and calling appropriate method
                    if (IsAVowel(firstLetter))
                    {
                        translatedWord = StartsWithVowel(lowerWord);
                        translatedWordCase = ConvertCase(translatedWord, uppercaseIndexes);
                        inputTranslated.Add(translatedWordCase.ToString());
                    }
                    else
                    {
                        translatedWord = StartsWithConsonant(lowerWord);
                        translatedWordCase = ConvertCase(translatedWord, uppercaseIndexes);
                        inputTranslated.Add(translatedWordCase.ToString());
                    }
                }

                foreach(string word in inputTranslated)
                {
                    Console.Write(word + " ");
                }

                Console.WriteLine();
                inputTranslated.Clear();
                
                // prompt to ask user to continue
                Console.Write("Would you like to translate another word? (y/n): ");
                inputYN = Console.ReadLine().ToLower().Trim();
                if(!inputYN.Equals("y"))
                {
                    runAgain = false;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Thanks for using the translator!!");
        }

        public static bool IsAVowel(char c)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            if (vowels.Contains(c))
            {
                return true;
            } 
            else
            {
                return false;
            }
        }
        public static string StartsWithVowel (string word)
        {
            word = word + "way";
            return word;
        }
        public static string StartsWithConsonant(string word)
        {
            // set first letter of given string to variable
            char firstLetter = word.ElementAt(0);
            
            // remove first letter of given string
            word = word.Substring(1);
            
            // concat the first letter to the end of the word
            word += firstLetter.ToString();
            if (IsAVowel(word.ElementAt(0)) == false)
            {
                // as long as the first letter is a consonant this method is recursively called
                return StartsWithConsonant(word);
            } 
            else
            {
                // once the first letter is a vowel you end here and concat "ay" to the end of the word variable
                return word + "ay";
            }
        }
        public static List<int> IndexUppercase(char[] word)
        {
            int cnt = 0;
            List<int> results = new List<int>();
            foreach (char c in word)
            {
                if (Char.IsUpper(c))
                {
                    // adds the index of the uppcase character to list uppcaseIndexes
                    results.Add(cnt);
                }
                cnt++;
            }
            return results;
        }
        public static string ConvertCase(string word, List<int> upperIndex)
        {
            char[] wordChars;
            string outWord = "";
            word = word.ToLower();
            wordChars = word.ToCharArray();

            for(int i = 0; i < word.Length; i++)
            {
                if(upperIndex.Contains(i))
                {
                    outWord += Char.ToUpper(wordChars[i]);
                }
                else
                {
                    outWord += wordChars[i];
                }
            }
            return outWord;
        }
    }
}