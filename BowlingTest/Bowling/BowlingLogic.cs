using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BowlingExampleTemplate.Bowling
{
    public static class BowlingLogic
    {
        private static int firstThrow = 0;
        private static int secondThrow = 0;
        private static int sum = 0;
        private static int frames = 10;
        private static Random rng = new Random();

        private static List<int> strikes = new List<int>();
        private static List<int> spares = new List<int>();
        private static List<int> scores = new List<int>();

        public static void bowlingGame()
        {
            for (int i = 0; i < frames; i++)
            {
                try
                {
                    Console.WriteLine("Frame: " + (i + 1) + " ---> First throw: ");
                    firstThrow = int.Parse(Console.ReadLine());
                    if (firstThrow > 10)
                    {
                        Console.WriteLine("You can't hit more than 10 bowles");
                        i--;
                        continue;
                    }
                    else if (firstThrow == 10)
                    {
                        strike(i);
                    }
                    else
                    {
                        Console.WriteLine("Frame: " + (i + 1) + " ---> Second throw: ");
                        secondThrow = int.Parse(Console.ReadLine());
                        if (secondThrow > 10 || firstThrow + secondThrow > 10)
                        {
                            Console.WriteLine("You can't hit more than 10 bowles");
                            i--;
                            continue;
                        }
                        else if (firstThrow == 0 && secondThrow == 10)
                        {
                            strike(i);
                        }
                        else if (firstThrow + secondThrow == 10)
                        {
                            sum += secondThrow;
                            spare(i);
                        }
                        else
                        {
                            sum += secondThrow;
                        }
                        sum += firstThrow;
                    }
                }
                catch
                {
                    Console.WriteLine("You must enter a numbered values");
                }
            }
            Console.WriteLine("Your final score is: " + sum);
        }

        public static void bowlingGameWithRNG()
        {
            for (int i = 0; i < frames; i++)
            {
                try
                {
                    firstThrow = rng.Next(1, 11);
                    Console.WriteLine("Frame: " + (i + 1) + " ---> First throw = " + firstThrow);
                    if (firstThrow == 10)
                    {
                        strike(i);
                    }
                    else
                    {
                        secondThrow = rng.Next(1, 11 - firstThrow);
                        Console.WriteLine("Frame: " + (i + 1) + " ---> Second throw = " + secondThrow);
                        if (firstThrow == 0 && secondThrow == 10)
                        {
                            strike(i);
                        }
                        else if (firstThrow + secondThrow == 10)
                        {
                            sum += secondThrow;
                            spare(i);
                        }
                        else
                        {
                            sum += secondThrow;
                        }
                        sum += firstThrow;
                        scores.Add(firstThrow);
                        scores.Add(secondThrow);
                    }
                }
                catch
                {
                    Console.WriteLine("You must enter a numbered values");
                }
            }

            for (int i = 0; i < strikes.Count; i++)
            {
                int framesFirstThrow = strikes[i] * 2 + 2;
                int framesSecondThrow = strikes[i] * 2 + 3;
                if (strikes[i] == 9)
                {
                    int newThrow = rng.Next(1, 11);
                    sum += newThrow;
                    Console.WriteLine("Bonus throw: " + newThrow);
                    break;
                }
                else if (framesFirstThrow == 10)
                {
                    framesSecondThrow++;
                }
                sum += scores[framesFirstThrow];
                sum += scores[framesSecondThrow];

            }
            for (int i = 0; i < spares.Count; i++)
            {
                int framesFirstThrow = spares[i] * 2 + 2;
                if (spares[i] == 9)
                {
                    int newThrow = rng.Next(1, 11);
                    sum += newThrow;
                    Console.WriteLine("Bonus throw: " + newThrow);
                    break;
                }
                sum += scores[framesFirstThrow];

            }
            Console.WriteLine("Your final score is: " + sum);
        }

        private static void strike(int i)
        {
            scores.Add(10);
            scores.Add(0);
            strikes.Add(i);
            sum += 10;
        }

        private static void spare(int i)
        {
            spares.Add(i);
        }
    }
}
