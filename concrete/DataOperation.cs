using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWPF.Dto;
using TaskWPF.Interface;

namespace TaskWPF.concrete
{
    /// <summary>
    /// Concrete implementation of IDataOperation interface for data manipulation operations.
    /// </summary>
    public class DataOperation : IDataOperation
    {
        /// <summary>
        /// Filters out noisy areas based on SNR and speed thresholds.
        /// </summary>
        /// <param name="distances">List of distances.</param>
        /// <param name="SNRThreshold">Signal-to-noise ratio threshold.</param>
        /// <param name="SpeedThreshold">Speed threshold.</param>
        /// <returns>List of clean measurement areas represented as tuples of (start distance, end distance, speed).</returns>
        public List<Tuple<double, double, double>> FilterNoisyAreas(IList<Distances> distances, double SNRThreshold, double SpeedThreshold)
        {
            List<Tuple<double, double, double>> cleanAreas = new List<Tuple<double, double, double>>();
            double start = 0;
            bool inCleanArea = false;

            foreach (var distance in distances)
            {
                if (distance.SNR1 < SNRThreshold && distance.Speed > SpeedThreshold)
                {
                    if (!inCleanArea)
                    {
                        start = distance.Distance;
                        inCleanArea = true;
                    }
                }
                else
                {
                    if (inCleanArea)
                    {
                        cleanAreas.Add(Tuple.Create(start, distance.Distance, distance.Speed));
                        inCleanArea = false;
                    }
                }
            }
            return cleanAreas;
        }

        /// <summary>
        /// Loads JSON data from a file and deserializes it into ApplicationData object.
        /// </summary>
        /// <param name="filePath">Path to the JSON file.</param>
        /// <returns>Deserialized ApplicationData object.</returns>
        public ApplicationData LoadJsonData(string filePath)
        {
            // Read JSON file and deserialize into objects
            string jsonText = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<ApplicationData>(jsonText);
        }
    }
}