using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace BornToMove
{
    public class Program
    {
        private static Db database;
        
        public static void Main(string[] args)
        {
            Move? selectedMove = null;
            
            database = new Db("Server=localhost,1433;Database=BornToMove;User ID=SA;Password=yourStrong(Password");
            
            while (selectedMove == null)
            {
                selectedMove = AskForExercise();
            }

            Console.WriteLine("You selected: " + selectedMove.Name);
            Console.WriteLine(selectedMove.Description);
            Console.WriteLine("Press any key when you are done with the exercise");

            Console.ReadLine();
            
            AskForRating();
        }

        private static void AskForRating()
        {
            int rating = 1;
            int intensity = 1;
            
            Console.WriteLine("How do you rate this exercise from 1 to 5?");
            var ratingAnswer = Console.ReadLine();
            if (ratingAnswer != null && ratingAnswer.All(char.IsDigit))
            {
                rating = Convert.ToInt32(ratingAnswer);
            }

            Console.WriteLine("How intensive was this exercise from 1 to 5?");
            var intensityAnswer = Console.ReadLine();
            if (intensityAnswer != null && intensityAnswer.All(char.IsDigit))
            {
                intensity = Convert.ToInt32(intensityAnswer);
            }
            
            Console.WriteLine("Rating processed! Overall: "+rating+", Intensity: "+intensity);
        }

        private static Move? AskForExercise()
        {
            Console.WriteLine("Enter (0) for a random exercise or (1) to select a entry from the list");
            var selected = Console.ReadLine();
            if (selected == null || !selected.All(char.IsDigit))
            {
                return null;
            }

            var selectedNum = Convert.ToInt32(selected);
            

            var moves = database.GetMoves(null);
            var movesCount = moves.Count;
            
            if (selectedNum == 0)
            {
                var rd = new Random();
                return moves[rd.Next(0, movesCount - 1)];
            }
            if (selectedNum == 1)
            {
                Console.WriteLine("Select a exercise and enter the number to get started! \nEnter (0) for creating a new exercise.");
                for (var i = 0; i < movesCount; i++)
                {
                    Console.WriteLine(moves[i].Id + " | " + moves[i].Name + " | " + moves[i].SweatRate);
                }

                var answer = Convert.ToInt32(Console.ReadLine());
                var move = moves.FirstOrDefault(m => m.Id == answer);
                if (move != null)
                {
                    return move;
                }
                if (answer == 0)
                {
                    CreateNewExercise();
                }
            }

            return null;
        }

        private static void CreateNewExercise()
        {
            string? name = null;
            string? description = null;
            var sweatRate = 0;
            
            var moves = database.GetMoves(null);
            while (true)
            {
                if (name == null)
                {
                    Console.WriteLine("Enter a name for the new exercise:");
                    name = Console.ReadLine();
                    continue;
                }
                
                var existing = moves.FirstOrDefault(m => m.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
                if (existing != null)
                {
                    Console.WriteLine("There is already a exercise named "+ name + "\nPlease select a unique name.");
                    name = null;
                    continue;
                }
                    
                Console.WriteLine("Enter a description: ");
                description = Console.ReadLine();
                    
                if (String.IsNullOrEmpty(description))
                {
                    continue;
                }
                    
                Console.WriteLine("Enter a sweatRate: ");
                sweatRate = Convert.ToInt32(Console.ReadLine());
                database.Create(name, description, sweatRate);
                Console.WriteLine("Record saved!");
                                
                break;
            }
        }
    }
}