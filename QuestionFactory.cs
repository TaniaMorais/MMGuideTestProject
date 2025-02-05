using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMGuideTestProject.Questions;

namespace MMGuideTestProject {
    public static class QuestionFactory {
        #region Properties
        private static readonly Dictionary<int, IQuestion> questions = new Dictionary<int, IQuestion> {
            { 1, new Question1() },
            { 2, new Question2() },
            { 3, new Question3() },
            { 4, new Question4() },
            { 5, new Question5() },
        };
        #endregion

        #region Methods
        public static IQuestion? GetQuestion(int questionNumber) {
            return questions.ContainsKey(questionNumber) ? questions[questionNumber] : null;
        }
        #endregion

    }
}
