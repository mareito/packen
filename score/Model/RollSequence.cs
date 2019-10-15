using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Model
{
    /// <summary>
    /// Class that represents the rolls sequence given by user
    /// </summary>
    public class RollSequence
    {

        /// <summary>
        /// Secuence original
        /// </summary>
        private string originalSequence;

        /// <summary>
        /// Sequence without invalid characters
        /// </summary>
        private string validSequence = "";

        /// <summary>
        /// This list represens the complete game with the ten frames
        /// </summary>
        private List<Frame> Line =  new List<Frame>();

        /// <summary>
        /// Indicates that the line is completed
        /// </summary>
        private bool LineCompleted = false;

        /// <summary>
        /// Constructor for the class. 
        /// </summary>
        /// <param name="rollSequence">String whit the sequence of the rolls</param>
        public RollSequence(string rollSequence)
        {
            

            if (string.IsNullOrWhiteSpace(rollSequence))
            {
                throw new Exception("Invalid Input String");
            }
            originalSequence = rollSequence.ToUpper();

            foreach (var character in originalSequence)
            {
                if (Constants.AppConfiguration.ValidValues.Contains(character.ToString()))
                {
                    validSequence += character.ToString();
                }
            }
            
            if (validSequence.Length > 21)
            {
                validSequence = validSequence.Substring(0, 20);
            }
            
            for (var i = 0; i < 10; i++)
            {
                var frame = new Frame();
                if (i == 9)
                {
                    frame.LastFrame = true;
                }
                Line.Add(frame);
            }

            AssignValues();
            CalculateFrameBonus();
            calculateAcumulatedScore();
        }

        /// <summary>
        /// Method for assign values on first or second try on each frame
        /// </summary>
        private void AssignValues()
        {
            foreach (var character in validSequence)
            {
                var frame = (from fr in Line
                             where fr.Completed == false
                             select fr).FirstOrDefault();
                if (frame is null)
                {
                    break;
                }
                frame.Assign(character);
            }
        }

        /// <summary>
        /// This method calculates the bonus for each frame depending if the player made a strike or a spare
        /// </summary>
        private void CalculateFrameBonus()
        {

            for (var i = 0; i <= 8; i++)
            {
                var totalBonus = 0;
                var frame = Line[i];
                var sumNextTries = 0;
                if (frame.FirstBall == "X")
                {
                    sumNextTries = 2;
                }
                if (frame.SecondBall == "/")
                {
                    sumNextTries = 1;
                }


                var j = i + 1;
                while (sumNextTries > 0)
                {
                    var nextFrame = Line[j];
                    if (!nextFrame.LastFrame)
                    {
                        if (nextFrame.FirstBall == "X")
                        {
                            totalBonus += Constants.AppConfiguration.getCharacterValue(nextFrame.FirstBall);
                            sumNextTries--;
                            j++;
                            continue;
                        }
                        else
                        {
                            if (sumNextTries == 1)
                            {
                                totalBonus += Constants.AppConfiguration.getCharacterValue(nextFrame.FirstBall);
                                sumNextTries = 0;
                            }
                            else
                            {
                                if (nextFrame.SecondBall == "/")
                                {
                                    totalBonus += 10;
                                    sumNextTries = 0;
                                }
                                else
                                {
                                    totalBonus += Constants.AppConfiguration.getCharacterValue(nextFrame.FirstBall);
                                    totalBonus += Constants.AppConfiguration.getCharacterValue(nextFrame.SecondBall);
                                    sumNextTries = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (sumNextTries == 1)
                        {
                            totalBonus += Constants.AppConfiguration.getCharacterValue(nextFrame.FirstBall);
                        }
                        else
                        {
                            if (nextFrame.SecondBall == "/")
                            {
                                totalBonus = 10;
                            }
                            else
                            {
                                totalBonus += Constants.AppConfiguration.getCharacterValue(nextFrame.FirstBall);
                                totalBonus += Constants.AppConfiguration.getCharacterValue(nextFrame.SecondBall);
                            }
                        }
                        sumNextTries = 0;
                    }
                }
                frame.NextBallsBonusScore = totalBonus;             
            }
        }

        /// <summary>
        /// Calculate the acumalted score for each frame
        /// </summary>
        private void calculateAcumulatedScore()
        {
            foreach (var frame in Line)
            {
                var idx = Line.IndexOf(frame);
                frame.AcumulatedScore = frame.FrameScore + frame.NextBallsBonusScore;              
                if (idx > 0)
                {
                    frame.AcumulatedScore += Line[idx - 1].AcumulatedScore;
                }
            }
        }


        /// <summary>
        /// List whith the frames of the game
        /// </summary>
        /// <returns>List whith the frames of the game</returns>
        public List<Frame> getLine()
        {
            return Line;
        }


        /// <summary>
        /// Returns the final score for the game
        /// </summary>
        /// <returns>Integer with the final score for the game</returns>
        public int getFinalScore() {

            var lastFrame = (from frame in Line
                             where frame.LastFrame
                             select frame).SingleOrDefault();

            if (!lastFrame.Completed)
            {
                return 0;
            }
            else
            {
                return lastFrame.AcumulatedScore;
            }
        }
             
    }
}
