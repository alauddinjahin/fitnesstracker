using _216678_FitnessTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _216678_FitnessTracker.Utils
{
    class CalorieBurned
    {

        
        public static float CaloriesBurnedCustomFormulaWithMET(ActivityRecord record, float weightInKg=0)
        {
            float CaloriesBurned = 0;
            //float weightInKg = 70; // record.UserWeight; 
            float durationInHours = default; 

            string pattern1 = @"(minutes|min)";
            string pattern2 = @"(hours|hour)";

            //Console.WriteLine(record.ActivityId);

            FtActivity ftActivity = FtActivity.FindWithMetrics(record.ActivityId);
            //Console.WriteLine(Regex.IsMatch(ftActivity.Metric3, pattern1));


            if (Regex.IsMatch(ftActivity.Metric1, pattern1))
            {
                durationInHours = record.Metric1 / 60f; // Convert minutes to hours
            }
            else if (Regex.IsMatch(ftActivity.Metric2, pattern1))
            {
                durationInHours = record.Metric2 / 60f; 
            }
            else if (Regex.IsMatch(ftActivity.Metric3, pattern1))
            {
                durationInHours = record.Metric3 / 60f; 
            }
            else if (Regex.IsMatch(ftActivity.Metric1, pattern2))
            {
                durationInHours = record.Metric1; // The value is already in hours format
            }
            else if (Regex.IsMatch(ftActivity.Metric2, pattern2))
            {
                durationInHours = record.Metric2; 
            }
            else if (Regex.IsMatch(ftActivity.Metric3, pattern2))
            {
                durationInHours = record.Metric3; 
            }

            //CaloriesBurned *= weightInKg * durationInHours;  // Adjust by weight and duration

            switch (record.ActivityId)
                {
                    case 1: // Walking
                        CaloriesBurned = (record.Metric1 * 0.01f) + (record.Metric2 * 0.5f) + (record.Metric3 * 0.3f);
                        CaloriesBurned *= weightInKg * durationInHours;
                        break;

                    case 2: // Swimming
                        CaloriesBurned = (record.Metric1 * 1.2f) + (record.Metric2 * 0.6f) + (record.Metric3 * 0.05f);
                        CaloriesBurned *= weightInKg * durationInHours;
                        break;

                    case 3: // Running
                        CaloriesBurned = (record.Metric1 * 1.0f) + (record.Metric2 * 0.4f) + (record.Metric3 * 0.05f);
                        CaloriesBurned *= weightInKg * durationInHours;
                        break;

                    case 4: // Cycling
                        CaloriesBurned = (record.Metric1 * 0.9f) + (record.Metric2 * 0.3f) + (record.Metric3 * 0.5f);
                        CaloriesBurned *= weightInKg * durationInHours;
                        break;

                    case 5: // Jump Rope
                        CaloriesBurned = (record.Metric1 * 0.1f) + (record.Metric2 * 0.5f) + (record.Metric3 * 0.05f);
                        CaloriesBurned *= weightInKg * durationInHours;
                        break;

                    case 6: // Weightlifting
                        CaloriesBurned = (record.Metric1 * 0.5f) + (record.Metric2 * 0.8f) + (record.Metric3 * 0.3f);
                        CaloriesBurned *= weightInKg * durationInHours;
                        break;

                    default:
                        CaloriesBurned = (record.Metric1 * 0.4f) + (record.Metric2 * 0.7f) + (record.Metric3 * 0.3f);
                        CaloriesBurned *= weightInKg * durationInHours;
                        break;
                }

            return CaloriesBurned;
        }

        // tomake calculator: Activity, Duration in minutes and User weight

        public float CaloriesBurnedUsingMETFormula(ActivityRecord record)
        {
            //Calories Burned = MET × weight in kg × duration in hours
            // Formula: Calories burned per minute (CPM) = Met x body weight in Kg x 3.5) 
            //if (!met.Equals(default))
            //{
            //    result = Math.Round(((met * weight * 3.5) / 200) * min, 2);

            //}
            float met = GetMET(record);
            float weightInKg = 70; // record.UserWeight; // A person weight in kg
            float durationInHours = record.Metric3 / 60f;

            return met * weightInKg * durationInHours;
        }

        private float GetMET(ActivityRecord record)
        {
            switch (record.ActivityId)
            {
                case 1: // Walking
                    return 3.5f;

                case 2: // Swimming
                    return record.Metric3 > 150 ? 9.8f : 6.0f; // Use HR to gauge intensity

                case 3: // Running
                    return record.Metric1 >= 10 ? 11.0f : 7.0f; // High MET for >10km

                case 4: // Cycling
                    return record.Metric3 >= 20 ? 8.5f : 4.5f; // Based on average speed

                case 5: // Jump Rope
                    return record.Metric3 > 130 ? 12.0f : 10.0f; // HR-based

                case 6: // Weightlifting
                    float intensity = record.Metric1 * record.Metric2;
                    return intensity > 1000 ? 6.0f : 3.5f;

                default:
                    return 4.0f;
            }
        }




        //Adjusted MET = BaseMET + (M1 * M1Factor) + (M2 * M2Factor) + (M3 * M3Factor)
        //CaloriesBurned = AdjustedMET × Weight(kg) × Duration(hours)
        private float CaloriesBurnedUsingDynamicMET(ActivityRecord record, ActivityMETConfig metConfig)
        {
            float weightInKg = 70; //record.UserWeight;
            float durationInHours = record.Metric3 / 60f;

            float dynamicMET = metConfig.BaseMET +
                               (record.Metric1 * metConfig.Metric1Factor) +
                               (record.Metric2 * metConfig.Metric2Factor) +
                               (record.Metric3 * metConfig.Metric3Factor);

            return dynamicMET * weightInKg * durationInHours;
        }



    }
}
