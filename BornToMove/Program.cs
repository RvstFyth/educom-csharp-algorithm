using BornToMove.Business;
using System.Linq;
using BornToMove.DAL;

namespace BornToMove
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Move? selectedMove = null;

            while (selectedMove == null)
            {
                selectedMove = AskForExercise();
            }

            Console.WriteLine("You selected: " + selectedMove.Name);
            Console.WriteLine(selectedMove.Description);
            Console.WriteLine("Press any key when you are done with the exercise");

            Console.ReadLine();
            
            AskForRating(selectedMove);
        }

        private static void AskForRating(Move move)
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

            var moveRating = new MoveRating
            {
                Rating = rating,
                Intensity = intensity
            };

            BuMove.AddRating(move, moveRating);
            
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

            if (selectedNum == 0)
            {
                return BuMove.GetRandomMove();
            }
            if (selectedNum == 1)
            {
                var moves = BuMove.GetAllMoves();
                var movesCount = moves.Count;
                
                Console.WriteLine("Select a exercise and enter the number to get started! \nEnter (0) for creating a new exercise.");
                for (var i = 0; i < movesCount; i++)
                {
                    var averageRating = 0;
                    if (moves[i].Ratings.Count > 0)
                    {
                        averageRating = Convert.ToInt32(moves[i].Ratings.Average(rating => rating.Rating));    
                    }
                    
                    Console.WriteLine(moves[i].Id + " | " + moves[i].Name + " | " + moves[i].SweatRate);
                    Console.WriteLine("Average rating: " + averageRating + " of " + moves[i].Ratings.Count + " submissions");
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

            var moves = BuMove.GetAllMoves();
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
                BuMove.SaveMove(name, description, sweatRate);
                Console.WriteLine("Record saved!");
                                
                break;
            }
        }
    }
}