using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace score.Model
{
    /// <summary>
    /// This class represents each frame of the bowlig game
    /// </summary>
    public class Frame
    {
        /// <summary>
        /// First try for the player 
        /// </summary>
        public string FirstBall { set; get; } = string.Empty;

        /// <summary>
        /// Second try for the player
        /// </summary>
        public string SecondBall { set; get; } = string.Empty;

        /// <summary>
        /// Boolean indicating that is the las frame of the player
        /// </summary>
        public bool LastFrame { get; set; } = false;

        /// <summary>
        /// Third try for the gamer. Only for the last frame
        /// </summary>
        public string LastFrameBonusBall { get; set; } = "-";

        /// <summary>
        /// Sum of first, second and third ball
        /// </summary>
        public int FrameScore { set; get; } = 0;

        /// <summary>
        /// Total bonus if the player made a spare or a strike
        /// </summary>
        public int NextBallsBonusScore { get; set; } = 0;

        /// <summary>
        /// Acumulated Score
        /// </summary>
        public int AcumulatedScore { set; get; } = 0;

        /// <summary>
        /// Inidicates if the frame is completed 
        /// </summary>
        public bool Completed { set; get; } = false;

        /// <summary>
        /// Inidicates if the frame is completed 
        /// </summary>
        /// <returns> True or false depending if the frame is completed or not</returns>
        public bool IsCompleted()
        {
            return Completed;
        }

        /// <summary>
        /// This method assigns the value for the first or the second try on each frame.
        /// </summary>
        /// <param name="value">Charatecter thar represents the score of the try</param>
        public void Assign(char value)
        {
            if (!Constants.AppConfiguration.ValidValues.Contains(value.ToString()))
            {
                return;
            }
            if (string.IsNullOrEmpty(FirstBall))
            {
                AssignFirstTry(value);
            }
            else
            {
                if (LastFrame)
                {
                    if (string.IsNullOrEmpty(SecondBall))
                    {
                        AssignSecondTry(value);
                    }
                    else
                    {
                        if (FirstBall == "X" || SecondBall == "/")
                        {
                            AssignLastTryBonus(value);
                        }
                    }                    
                }
                else
                {
                    AssignSecondTry(value);
                }
            }
                
        }

        /// <summary>
        /// This method saves the first try of the player
        /// </summary>
        /// <param name="value">Charatecter thar represents the score of the first try</param>
        private void AssignFirstTry(char value)
        {            
            if (Completed || !(string.IsNullOrWhiteSpace(FirstBall)) || !(string.IsNullOrWhiteSpace(SecondBall)))
            {
                throw new Exception("Invalid Character");
            }

            if (value == '/')
            {
                throw new Exception("Invalid Character");
            }

            FirstBall = value.ToString();
            
            if(FirstBall == "X" && !LastFrame)
            {
                Completed = true;
                CalculateFrameValue();
            }
        }

        /// <summary>
        /// This method saves the second try of the player
        /// </summary>
        /// <param name="value">Charatecter thar represents the score of the second try</param>
        private void AssignSecondTry(char value)
        {

            if (!LastFrame)
            {
                if (value == '/' && FirstBall == "X")
                {
                    throw new Exception("Invalid Character");
                }
            }

            SecondBall = value.ToString();
            if (!LastFrame)
            {
                Completed = true;
                CalculateFrameValue();
            }
            else
            {
                if (FirstBall != "X" && SecondBall != "/")
                {
                    Completed = true;
                    CalculateFrameValue();
                }
            }
        }

        /// <summary>
        /// Assings the third try in the last frame (when it applies)
        /// </summary>
        /// <param name="value">Charatecter thar represents the score of the third try on last frame</param>
        private void AssignLastTryBonus(char value)
        {
            LastFrameBonusBall = value.ToString();
            Completed = true;
            CalculateFrameValue();    
        }


        /// <summary>
        /// Calculate score on each frame
        /// </summary>
        private void CalculateFrameValue()
        {
            var total = 0;
            total += Constants.AppConfiguration.getCharacterValue(FirstBall);
            if (SecondBall == "/")
            {
                total = 10;
            }
            else
            {
                total += Constants.AppConfiguration.getCharacterValue(SecondBall);
            }

            if (LastFrame)
            {
                total += Constants.AppConfiguration.getCharacterValue(LastFrameBonusBall);
            }

            FrameScore = total;
        }

    }
}
