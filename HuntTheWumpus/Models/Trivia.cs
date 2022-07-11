namespace HuntTheWumpus.Models
{
    public class Trivia
    {
        public string Question { get; set; } = string.Empty;
        public string[] AnswerKey { get; set; } = Array.Empty<string>();
        public int Answer { get; set; }
        string Location { get; set; } = @"DataFiles/TriviaQuestions.txt";
        public List<int> UsedTriviaQuestions { get; set; } = new List<int>();

        private void Save()
        {
            using var writer = new StreamWriter(Location);
            writer.WriteLine(Question);
            foreach (var answer in AnswerKey)
                writer.WriteLine(answer);

            writer.WriteLine(Answer);
            writer.WriteLine();
        }
        public List<Trivia> GetTrivias()
        {
            var trivias = new List<Trivia>();

            using (var reader = new StreamReader(Location))
            {
                while (!reader.EndOfStream)
                {
                    var question = reader.ReadLine();
                    if (string.IsNullOrEmpty(question))
                        continue;

                    var answerKey = new string[4];
                    for (var i = 0; i < 4; i++)
                    {
                        answerKey[i] = i+1 + ") " + reader.ReadLine();
                    }
                    var answer = Convert.ToInt16(reader.ReadLine());

                    var trivia = new Trivia()
                    {
                        Question = question,
                        AnswerKey = answerKey,
                        Answer = answer
                    };
                    trivias.Add(trivia);
                    reader.ReadLine();
                }
            }
            return trivias;
        }
        public Dictionary<int, Trivia> SetupTriviaBattle(int max)
        {
            var trivias = new List<Trivia>();
            trivias.AddRange(new Trivia().GetTrivias());

            var triviaBattle = new Dictionary<int, Trivia>();
            var random = new Random();

            while (triviaBattle.Count != max)
            {
                var questionNum = random.Next(trivias.Count);

                if (UsedTriviaQuestions.Contains(questionNum))
                    continue;

                triviaBattle.Add(questionNum, trivias[questionNum]);
            }

            return triviaBattle;
        }
    }
}
